using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace KT.Common.Classes.Application
{
	// Exceptions to use in KT serialization.

	#region KTSerializeException.

	/// <summary>
	/// Is raised when a common error happens during KT serialization.
	/// </summary>
	[Serializable]
	public class KTSerializeException : Exception
	{
		public KTSerializeException()
		{ }

		public KTSerializeException(string message)
			: base(message)
		{ }

		public KTSerializeException(string message, Exception innerException)
			: base(message, innerException)
		{ }

		protected KTSerializeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{ }
	}

	#endregion


	#region KTDeserializeException.

	/// <summary>
	/// Is raised when a common error happens during KT deserialization.
	/// </summary>
	[Serializable]
	public class KTDeserializeException : Exception
	{
		public KTDeserializeException()
		{ }

		public KTDeserializeException(string message)
			: base(message)
		{ }

		public KTDeserializeException(string message, Exception innerException)
			: base(message, innerException)
		{ }

		protected KTDeserializeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{ }
	}

	#endregion


	#region KTSerializeAttributeException.

	/// <summary>
	/// Is raised when there is an error in an attribute in KT serialization.
	/// </summary>
	[Serializable]
	public class KTSerializeAttributeException : Exception
	{
		public KTSerializeAttributeException()
		{ }

		public KTSerializeAttributeException(string message)
			: base(message)
		{ }

		public KTSerializeAttributeException(string message, Exception innerException)
			: base(message, innerException)
		{ }

		protected KTSerializeAttributeException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{ }
	}

	#endregion

}
