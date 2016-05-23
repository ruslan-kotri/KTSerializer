using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KT.Common.Classes
{
	/// <summary>
	/// Contains functions to facilitate operations with <see cref="decimal"/> values.
	/// </summary>
	public static class DecimalHelper
	{
		#region GetBytes().

		/// <summary>
		/// Gets bytes representation of the <see cref="decimal"/> value.
		/// </summary>
		/// <param name="value">Value to process.</param>
		/// <returns>Bytes representation of the <see cref="decimal"/> value.</returns>
		public static byte[] GetBytes(decimal value)
		{
			int[] bits = Decimal.GetBits(value);
			byte[] bytes = new byte[16];

			for (int i = 0; i < 4; i++)
			{
				byte[] tempBytes = BitConverter.GetBytes(bits[i]);
				int offset = i * 4;

				for (int j = 0; j < 4; j++)
				{
					bytes[offset + j] = tempBytes[j];
				}
			}

			return bytes;
		}
		
		#endregion


		#region ToDecimal().

		/// <summary>
		/// Gets <see cref="decimal"/> value from its bytes representation.
		/// </summary>
		/// <param name="bytes">Bytes to convert.</param>
		/// <returns>Converted <see cref="decimal"/> value.</returns>
		public static decimal ToDecimal(byte[] bytes)
		{
			int[] bits = new int[4];
			for (int i = 0; i <= 15; i += 4)
			{
				bits[i / 4] = BitConverter.ToInt32(bytes, i);
			}

			return new decimal(bits);
		}
		
		#endregion
	}
}
