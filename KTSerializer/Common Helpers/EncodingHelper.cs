using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace KT.Common.Classes.Application
{
	/// <summary>
	/// Provides additional methods for operation with encodings.
	/// </summary>
	public static class EncodingHelper
	{
		#region Public functions.

		#region UTF8 functions.

		// NB.! These functions were written, because work of standart UTF8 encoding with higher (>55295) chars seems to be buggy.

		#region GetUTF8Bytes().

		#region Get processed array.

		/// <summary>
		/// Gets UTF8 bytes encoding of the given text.
		/// </summary>
		/// <param name="text">Text to parse.</param>
		/// <returns>UTF8 bytes encoding.</returns>
		public static byte[] GetUTF8Bytes(string text)
		{
			// Initial check.
			if (text == null) return new byte[0];

			int length = text.Length;
			if (length == 0) return new byte[0];


			#region For short texts.

			if (length < 500)
			{
				unsafe
				{
					// Total count of bytes.
					int totalCount = 0;

					fixed (char* textPointerInitial = text)
					{
						char* textPointer = textPointerInitial;
						char* textEndPointer = textPointerInitial + length;

						// Get total count of bytes.
						while (textPointer != textEndPointer)
						{
							int value = (int)*textPointer;

							if (value < 128) totalCount += 1;
							else if (value < 2048) totalCount += 2;
							else totalCount += 3; // because "Char.MaxValue = 0xFFFF" in .NET, thus 4 bytes are impossible

							textPointer++;
						}


						// Fill array.
						byte[] result = new byte[totalCount];

						fixed (byte* resultInitialPointer = result)
						{
							// Restore pointer state.
							textPointer = textPointerInitial;

							// Process text.
							byte* resultPointer = resultInitialPointer;
							processUTF8Text(ref textPointer, ref textEndPointer, ref resultPointer);
						}

						return result;
					}
				}
			}

			#endregion


			#region For long texts.

			else
			{
				unsafe
				{
					fixed (char* textPointerInitial = text)
					{
						// Set text pointers.
						char* textPointer = textPointerInitial;
						char* textEndPointer = textPointerInitial + length;


						// Fill temporary array.
						byte[] tempResult = new byte[length * 3];

						fixed (byte* tempResultInitialPointer = tempResult)
						{
							// Process text.
							byte* tempResultPointer = tempResultInitialPointer;
							processUTF8Text(ref textPointer, ref textEndPointer, ref tempResultPointer);


							// Create copy of array with necessary length.
							int resultLength = (int)(tempResultPointer - tempResultInitialPointer);
							byte[] result = new byte[resultLength];

							fixed (byte* resultInitialPointer = result)
							{
								tempResultPointer = tempResultInitialPointer; // reset pointer to start position

								byte* resultPointer = resultInitialPointer;
								byte* resultEndPointer = resultInitialPointer + resultLength;

								while (resultPointer != resultEndPointer)
								{
									*resultPointer = *tempResultPointer;

									tempResultPointer++;
									resultPointer++;
								}
							}

							return result;
						}
					}
				}
			}

			#endregion
		}
		
		#endregion



		#region Get raw array with length, string.

		/// <summary>
		/// Gets UTF8 bytes encoding of the given text.
		/// </summary>
		/// <param name="text">Text to parse.</param>
		/// <param name="resultLength">Length of the real array in the result.</param>
		/// <returns>UTF8 bytes encoding. It's length is bigger than necessary, use <see cref="resultLength"/> to get necessary subarray.</returns>
		public static byte[] GetUTF8Bytes(string text, out int resultLength)
		{
			// Initial check.
			if (text == null)
			{
				resultLength = 0;
				return new byte[0];
			}

			int length = text.Length;
			if (length == 0)
			{
				resultLength = 0;
				return new byte[0];
			}


			unsafe
			{
				fixed (char* textPointerInitial = text)
				{
					// Set text pointers.
					char* textPointer = textPointerInitial;
					char* textEndPointer = textPointerInitial + length;


					// Fill temporary array.
					byte[] tempResult = new byte[length * 3]; // UTF8 in .NET is no more than 3 bytes long

					fixed (byte* tempResultInitialPointer = tempResult)
					{
						// Process text.
						byte* tempResultPointer = tempResultInitialPointer;

						// NB.! Direct include of the following code works quicker than calling processUTF8Text().
						while (textPointer != textEndPointer)
						{
							ushort value = (ushort)*textPointer;

							// One byte.
							if (value < 128)
							{
								*tempResultPointer = (byte)value;
								tempResultPointer++;
							}

							// Two bytes.
							else if (value < 2048)
							{
								*tempResultPointer = (byte)((value >> 6) | 192);
								tempResultPointer++;

								*tempResultPointer = (byte)((value & 63) | 128);
								tempResultPointer++;
							}

							// Three bytes.
							else
							{
								*tempResultPointer = (byte)((value >> 12) | 224);
								tempResultPointer++;

								*tempResultPointer = (byte)(((value & 4032) >> 6) | 128);
								tempResultPointer++;

								*tempResultPointer = (byte)((value & 63) | 128);
								tempResultPointer++;
							}


							textPointer++;
						}

						// Get length of array.
						resultLength = (int)(tempResultPointer - tempResultInitialPointer);
					}


					// Return big array.
					return tempResult;
				}
			}
		}

		#endregion


		#region Get raw array with length, char[].

		/// <summary>
		/// Gets UTF8 bytes encoding of the given text.
		/// </summary>
		/// <param name="text">Text to parse.</param>
		/// <param name="resultLength">Length of the real array in the result.</param>
		/// <param name="length">Possible length of the array to take.</param>
		/// <returns>UTF8 bytes encoding. It's length is bigger than necessary, use <see cref="resultLength"/> to get necessary subarray.</returns>
		public static byte[] GetUTF8Bytes(char[] text, out int resultLength, int length = -1)
		{
			// Initial check.
			if (text == null)
			{
				resultLength = 0;
				return new byte[0];
			}

			if (length < 0) length = text.Length;
			if (length == 0)
			{
				resultLength = 0;
				return new byte[0];
			}


			unsafe
			{
				fixed (char* textPointerInitial = text)
				{
					// Set text pointers.
					char* textPointer = textPointerInitial;
					char* textEndPointer = textPointerInitial + length;


					// Fill temporary array.
					byte[] tempResult = new byte[length * 3]; // UTF8 in .NET is no more than 3 bytes long

					fixed (byte* tempResultInitialPointer = tempResult)
					{
						// Process text.
						byte* tempResultPointer = tempResultInitialPointer;
						processUTF8Text(ref textPointer, ref textEndPointer, ref tempResultPointer);

						// Get length of array.
						resultLength = (int)(tempResultPointer - tempResultInitialPointer);
					}


					// Return big array.
					return tempResult;
				}
			}
		}

		#endregion



		#region processUTF8CharPointer().

		/// <summary>
		/// Processes UTF8 encoded text to the temporary byte array.
		/// </summary>
		/// <param name="textPointer">Pointer to the text start.</param>
		/// <param name="textEndPointer">Pointer immediately behind the text end.</param>
		/// <param name="tempResultPointer">Pointer to the byte array start.</param>
		private unsafe static void processUTF8Text(
			ref char* textPointer,
			ref char* textEndPointer,
			ref byte* tempResultPointer
			)
		{
			while (textPointer != textEndPointer)
			{
				ushort value = (ushort)*textPointer;

				// One byte.
				if (value < 128)
				{
					*tempResultPointer = (byte)value;
					tempResultPointer++;
				}

				// Two bytes.
				else if (value < 2048)
				{
					*tempResultPointer = (byte)((value >> 6) | 192);
					tempResultPointer++;

					*tempResultPointer = (byte)((value & 63) | 128);
					tempResultPointer++;
				}

				// Three bytes.
				else
				{
					*tempResultPointer = (byte)((value >> 12) | 224);
					tempResultPointer++;

					*tempResultPointer = (byte)(((value & 4032) >> 6) | 128);
					tempResultPointer++;

					*tempResultPointer = (byte)((value & 63) | 128);
					tempResultPointer++;
				}


				textPointer++;
			}
		}
		
		#endregion

		#endregion


		#region GetUTF8String().

		#region Get restored string.

		/// <summary>
		/// Gets UTF8 encoded text from the given bytes array.
		/// </summary>
		/// <param name="stream">Stream to parse.</param>
		/// <returns>UTF8 encoded text.</returns>
		public static string GetUTF8String(Stream stream)
		{
			// Kep current position.
			long currentPosition = stream.Position;

			// Prepare collection.
			int size = (int)stream.Length;
			byte[] data = new byte[size];

			// Read.
			stream.Position = 0;
			stream.Read(data, 0, size);

			// Restore position.
			stream.Position = currentPosition;

			return EncodingHelper.GetUTF8String(data);
		}

		/// <summary>
		/// Gets UTF8 encoded text from the given bytes array.
		/// </summary>
		/// <param name="bytes">Bytes array to parse.</param>
		/// <returns>UTF8 encoded text.</returns>
		public static string GetUTF8String(byte[] bytes)
		{
			// Initial check.
			int length = bytes.Length;
			if (length == 0) return "";

			char[] chars = new char[length];

			unsafe
			{
				fixed (byte* bytePointerInitial = bytes)
				fixed (char* charPointerInitial = chars)
				{
					char* charPointer = charPointerInitial;

					byte* bytePointer = bytePointerInitial;
					byte* bytePointerEnd = bytePointerInitial + length;

					while (bytePointer < bytePointerEnd)
					{
						byte b1 = *bytePointer;
						bytePointer++;

						// One byte.
						if ((b1 >> 7) == 0)
						{
							*charPointer = (char)b1;
						}

						// Two bytes.
						else if ((b1 >> 5) == 6)
						{
							*charPointer =
								(char)(
									(int)((b1 & 31) << 6) |
									(int)(*bytePointer & 63)
									);

							bytePointer++;
						}

						// Three bytes.
						else
						{
							int b2 = (*bytePointer & 63);
							bytePointer++;

							*charPointer =
								(char)(
									(int)((b1 & 15) << 12) |
									(int)(b2 << 6) |
									(int)(*bytePointer & 63)
									);

							bytePointer++;
						}

						charPointer++;
					}

					// Create string from chars array.
					return new string(charPointerInitial, 0, (int)(charPointer - charPointerInitial));
				}
			}
		}
		
		#endregion


		#region Get raw array with length.

		/// <summary>
		/// Gets UTF8 encoded text from the given bytes array.
		/// </summary>
		/// <param name="bytes">Bytes array to process.</param>
		/// <param name="resultLength">Length of the real array in the result.</param>
		/// <returns>UTF8 encoded chars. It's length is bigger than necessary, use <see cref="resultLength"/> to get necessary subarray.</returns>
		public static char[] GetUTF8String(byte[] bytes, out int resultLength)
		{
			// Initial check.
			int length = bytes.Length;
			if (length == 0)
			{
				resultLength = 0;
				return new char[0];
			}


			char[] chars = new char[length];

			unsafe
			{
				fixed (byte* bytePointerInitial = bytes)
				fixed (char* charPointerInitial = chars)
				{
					char* charPointer = charPointerInitial;

					byte* bytePointer = bytePointerInitial;
					byte* bytePointerEnd = bytePointerInitial + length;

					while (bytePointer != bytePointerEnd)
					{
						byte b1 = *bytePointer;
						bytePointer++;

						// One byte.
						if ((b1 >> 7) == 0)
						{
							*charPointer = (char)b1;
						}

						// Two bytes.
						else if ((b1 >> 5) == 6)
						{
							*charPointer =
								(char)(
									(int)((b1 & 31) << 6) |
									(int)(*bytePointer & 63)
									);

							bytePointer++;
						}

						// Three bytes.
						else
						{
							int b2 = (*bytePointer & 63);
							bytePointer++;

							*charPointer =
								(char)(
									(int)((b1 & 15) << 12) |
									(int)(b2 << 6) |
									(int)(*bytePointer & 63)
									);

							bytePointer++;
						}

						charPointer++;
					}


					// Set length of necessary array.
					resultLength = (int)(charPointer - charPointerInitial);
					return chars;
				}
			}
		}

		#endregion

		#endregion

		#endregion


		#region RTF.

		#region GetRtfUnicodeEscapedString().

		/// <summary>
		/// Converts non-ASCII text to RTF applicable.
		/// </summary>
		/// <param name="text">Text to convert.</param>
		/// <returns>Converted text to use in RTF.</returns>
		public static string GetRtfUnicodeEscapedString(string text)
		{
			StringBuilder sb = new StringBuilder();
			foreach (char c in text)
			{
				if (c == '\\' || c == '{' || c == '}')
					sb.Append(@"\" + c);
				else if (c <= 0x7f)
					sb.Append(c);
				else
					sb.Append("\\u" + Convert.ToUInt32(c) + "?");
			}
			return sb.ToString();
		}
		
		#endregion
		
		#endregion

		#endregion
	}
}
