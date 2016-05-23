#define LOCK_TRACING

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace KT.Common.Classes
{
	/// <summary>
	/// Leveled lock intance.
	/// </summary>
	public sealed class LeveledLock
	{
		#region Properties.

		#region Level.

		/// <summary>
		/// Lock level.
		/// </summary>
		private int _level;
		/// <summary>
		/// Lock level.
		/// </summary>
		public int Level
		{
			get { return _level; }
		}

		#endregion


		#region Reentrant.

		/// <summary>
		/// Is lock reentrant.
		/// </summary>
		private bool _reentrant;
		/// <summary>
		/// Is lock reentrant.
		/// </summary>
		public bool Reentrant
		{
			get { return _reentrant; }
		}

		#endregion


		#region Name.

		/// <summary>
		/// Lock name.
		/// </summary>
		private string _name;
		/// <summary>
		/// Lock name.
		/// </summary>
		public string Name
		{
			get { return _name; }
		}

		#endregion


		private static LocalDataStoreSlot lockLevelTlsSlot;

		private object _lock = new object();
		private bool _permitIntraLevel;

		/// <summary>
		/// Default timeout for Monitor.TryEnter.
		/// </summary>
		private int _timeout;

		#endregion


		#region Constructors.

		/// <summary>
		/// Static constructor.
		/// </summary>
		static LeveledLock()
		{
#if LOCK_TRACING
			lockLevelTlsSlot = Thread.AllocateNamedDataSlot("__current#lockLevel__");
#endif
		}


		/// <summary>
		/// Creates new instance of <see cref="LeveledLock"/>.
		/// </summary>
		/// <param name="level">Lock level.</param>
		public LeveledLock(int level)
			: this(level, true, true)
		{ }

		/// <summary>
		/// Creates new instance of <see cref="LeveledLock"/>.
		/// </summary>
		/// <param name="level">Lock level.</param>
		/// <param name="name">Name of the current lock. Informative only.</param>
		public LeveledLock(int level, string name)
			: this(level, true, true, 1000, name)
		{ }

		/// <summary>
		/// Creates new instance of <see cref="LeveledLock"/>.
		/// </summary>
		/// <param name="level">Lock level.</param>
		/// <param name="reentrant">Can lock be reentrant.</param>
		/// <param name="permitIntraLevel">Can same level be used.</param>
		public LeveledLock(int level, bool reentrant, bool permitIntraLevel)
			: this(level, reentrant, permitIntraLevel, 1000)
		{ }

		/// <summary>
		/// Creates new instance of <see cref="LeveledLock"/>.
		/// </summary>
		/// <param name="level">Lock level.</param>
		/// <param name="timeout">Default timeout for locking operations.</param>
		public LeveledLock(int level, int timeout)
			: this(level, true, true, timeout, null)
		{ }

		/// <summary>
		/// Creates new instance of <see cref="LeveledLock"/>.
		/// </summary>
		/// <param name="level">Lock level.</param>
		/// <param name="timeout">Default timeout for locking operations.</param>
		/// <param name="name">Name of the current lock. Informative only.</param>
		public LeveledLock(int level, int timeout, string name)
			: this(level, true, true, timeout, name)
		{ }

		/// <summary>
		/// Creates new instance of <see cref="LeveledLock"/>.
		/// </summary>
		/// <param name="level">Lock level.</param>
		/// <param name="reentrant">Can lock be reentrant.</param>
		/// <param name="permitIntraLevel">Can same level be used.</param>
		/// <param name="timeout">Default timeout for locking operations.</param>
		public LeveledLock(int level, bool reentrant, bool permitIntraLevel, int timeout)
			: this(level, reentrant, permitIntraLevel, timeout, null)
		{ }


		/// <summary>
		/// Creates new instance of <see cref="LeveledLock"/>.
		/// </summary>
		/// <param name="level">Lock level.</param>
		/// <param name="reentrant">Can lock be reentrant.</param>
		/// <param name="permitIntraLevel">Can same level be used.</param>
		/// <param name="timeout">Default timeout for locking operations.</param>
		/// <param name="name">Name of the current lock. Informative only, can be omitted.</param>
		public LeveledLock(int level, bool reentrant, bool permitIntraLevel, int timeout, string name)
		{
			this._level = level;
			this._reentrant = reentrant;
			this._name = name;
			this._timeout = timeout;
			this._permitIntraLevel = permitIntraLevel;
		}
		
		#endregion


		#region Public functions.

		#region Enter().

		/// <summary>
		/// Enters lock.
		/// </summary>
		public IDisposable Enter()
		{
			return Enter(this._permitIntraLevel, this._timeout);
		}

		/// <summary>
		/// Enters lock.
		/// </summary>
		/// <param name="permitIntraLevel">Can same level be used.</param>
		public IDisposable Enter(bool permitIntraLevel)
		{
			return Enter(permitIntraLevel, this._timeout);
		}

		/// <summary>
		/// Enters lock.
		/// </summary>
		/// <param name="millisecondsTimeout">Timeout to enter lock.</param>
		public IDisposable Enter(int millisecondsTimeout)
		{
			return Enter(this._permitIntraLevel, millisecondsTimeout);
		}


		/// <summary>
		/// Enters lock.
		/// </summary>
		/// <param name="permitIntraLevel">Can same level be used.</param>
		/// <param name="millisecondsTimeout">Timeout to enter lock.</param>
		/// <returns><see cref="IDisposable"/> to use in using statement.</returns>
		public IDisposable Enter(bool permitIntraLevel, int millisecondsTimeout)
		{
			Thread.BeginThreadAffinity();
			Thread.BeginCriticalRegion();
			bool taken = false;
			try
			{
				PushLevel(permitIntraLevel);
				taken = Monitor.TryEnter(_lock, millisecondsTimeout);
				if (!taken)
					throw new TimeoutException("Timeout occurred while attempting to acquire monitor");
			}
			finally
			{
				if (!taken)
				{
					Thread.EndCriticalRegion();
					Thread.EndThreadAffinity();
				}
			}

			return new LeveledLockCookie(this);
		}
		
		#endregion


		#region Exit().

		/// <summary>
		/// Exits lock.
		/// </summary>
		public void Exit()
		{
			Monitor.Exit(_lock);
			try
			{
				PopLevel();
			}
			finally
			{
				Thread.EndCriticalRegion();
				Thread.EndThreadAffinity();
			}
		}
		
		#endregion
		
		#endregion


		#region Auxiliary functions.

		#region PushLevel().

		/// <summary>
		/// Adds current lock to the stack.
		/// </summary>
		/// <param name="permitIntraLevel">Can same level be used.</param>
		[Conditional("LOCK_TRACING")]
		private void PushLevel(bool permitIntraLevel)
		{
			Stack<LeveledLock> currentLevelStack = Thread.GetData(lockLevelTlsSlot) as Stack<LeveledLock>;

			if (currentLevelStack == null)
			{
				// We've never accessed the TLS data yet; construct a new Stack for our levels
				// and stash it away in TLS.
				currentLevelStack = new Stack<LeveledLock>();
				Thread.SetData(lockLevelTlsSlot, currentLevelStack);
			}
			else if (currentLevelStack.Count > 0)
			{
				// If the stack in TLS already recorded a lock, validate that we are not violating
				// the locking protocol. A violation occurs when our lock is higher level than the
				// current lock, or equal to the level (when the reentrant bit has not been set on
				// at least one of the locks involved).
				LeveledLock currentLock = currentLevelStack.Peek();
				int currentLevel = currentLock._level;

				if (_level > currentLevel ||
					(currentLock == this && !_reentrant) ||
					(_level == currentLevel && !permitIntraLevel))
				{
					throw new LockLevelException(currentLock, this);
				}
			}

			// If we reached here, we are OK to proceed with locking. Stash the current level in TLS.
			currentLevelStack.Push(this);
		}
		
		#endregion


		#region PopLevel().

		/// <summary>
		/// Pops current lock from the stack.
		/// </summary>
		[Conditional("LOCK_TRACING")]
		private void PopLevel()
		{
			Stack<LeveledLock> currentLevelStack = Thread.GetData(lockLevelTlsSlot) as Stack<LeveledLock>;

			// Just pop the latest level placed into TLS.
			if (currentLevelStack != null)
			{
				if (currentLevelStack.Peek() != this)
					throw new InvalidOperationException("You released a lock out of order. This is illegal with leveled locks.");
				currentLevelStack.Pop();
			}
		}
		
		#endregion
		
		#endregion


		#region ToString().

		/// <summary>
		/// Gets string representation of the current object.
		/// </summary>
		/// <returns>String representation of the current object.</returns>
		public override string ToString()
		{
			return String.Format("<level={0}, reentrant={1}, name={2}>", _level, _reentrant, _name);
		}
		
		#endregion


		#region LeveledLockCookie class.

		class LeveledLockCookie : IDisposable
		{
			// Fields
			private LeveledLock _lck;

			// Constructor
			internal LeveledLockCookie(LeveledLock lck)
			{
				this._lck = lck;
			}

			// Methods
			void IDisposable.Dispose()
			{
				_lck.Exit();
			}
		}
		
		#endregion
	}


	#region LockLevelException.

	/// <summary>
	/// Exception for leveled lock.
	/// </summary>
	public class LockLevelException : Exception
	{
		public LockLevelException() : base() { }
		public LockLevelException(string m) : base(m) { }
		public LockLevelException(string m, Exception innerException) : base(m, innerException) { }
		public LockLevelException(LeveledLock currentLock, LeveledLock newLock) :
			base(string.Format("You attempted to violate the locking protocol by acquiring lock {0} " +
			"while the thread already owns lock {1}.", currentLock, newLock)) { }
	}
	
	#endregion
}