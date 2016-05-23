using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace KT.Common.Classes.Application
{
	/// <summary>
	/// Provides functions to facilitate work with serialization.
	/// </summary>
	internal static class SerializeHelper
	{
		#region Auxiliary properties.

		#region Hexadecimal chars.

		/// <summary>
		/// Auxiliary. Mapping of hexadecimal char - its byte.
		/// </summary>
		private static byte[] hexCharMapping = new byte[103];

		/// <summary>
		/// Auxiliary. Mapping of hexadecimal char of first grade - its byte.
		/// </summary>
		private static byte[] hexFirstCharMapping = new byte[103];


		/// <summary>
		/// Auxiliary. Mapping of two hexadecimal chars - their byte.
		/// </summary>
		private static byte[][] hexCharMappingAll = new byte[103][];
		
		#endregion


		#region Preset bytes arrays.

		/// <summary>
		/// Array of length 16.
		/// </summary>
		private static byte[] staticBytes16 = new byte[16];

		/// <summary>
		/// Array of length 8.
		/// </summary>
		private static byte[] staticBytes8 = new byte[8];

		/// <summary>
		/// Array of length 4.
		/// </summary>
		private static byte[] staticBytes4 = new byte[4];

		#endregion


		/// <summary>
		/// Total length of a known type header.
		/// </summary>
		public const int TypeHeaderLength = 2; // 2 chars for object type

		#endregion


		#region Static constructor.

		/// <summary>
		/// Performs actions at properties initialization.
		/// </summary>
		static SerializeHelper()
		{
			#region Fill chars mapping for hex parsing.

			hexCharMapping[(byte)'0'] = 0;
			hexCharMapping[(byte)'1'] = 1;
			hexCharMapping[(byte)'2'] = 2;
			hexCharMapping[(byte)'3'] = 3;
			hexCharMapping[(byte)'4'] = 4;
			hexCharMapping[(byte)'5'] = 5;
			hexCharMapping[(byte)'6'] = 6;
			hexCharMapping[(byte)'7'] = 7;
			hexCharMapping[(byte)'8'] = 8;
			hexCharMapping[(byte)'9'] = 9;
			hexCharMapping[(byte)'a'] = 10;
			hexCharMapping[(byte)'b'] = 11;
			hexCharMapping[(byte)'c'] = 12;
			hexCharMapping[(byte)'d'] = 13;
			hexCharMapping[(byte)'e'] = 14;
			hexCharMapping[(byte)'f'] = 15;

			hexFirstCharMapping[(byte)'1'] = 1 * 16;
			hexFirstCharMapping[(byte)'2'] = 2 * 16;
			hexFirstCharMapping[(byte)'3'] = 3 * 16;
			hexFirstCharMapping[(byte)'4'] = 4 * 16;
			hexFirstCharMapping[(byte)'5'] = 5 * 16;
			hexFirstCharMapping[(byte)'6'] = 6 * 16;
			hexFirstCharMapping[(byte)'7'] = 7 * 16;
			hexFirstCharMapping[(byte)'8'] = 8 * 16;
			hexFirstCharMapping[(byte)'9'] = 9 * 16;
			hexFirstCharMapping[(byte)'a'] = 10 * 16;
			hexFirstCharMapping[(byte)'b'] = 11 * 16;
			hexFirstCharMapping[(byte)'c'] = 12 * 16;
			hexFirstCharMapping[(byte)'d'] = 13 * 16;
			hexFirstCharMapping[(byte)'e'] = 14 * 16;
			hexFirstCharMapping[(byte)'f'] = 15 * 16;
			

			// NB.! We do not save to uppercase and do not accept such data now.
			
			#endregion


			#region Fill all chars mapping.

			byte[] usedBytes = new byte[16] { (byte)'0', (byte)'1', (byte)'2', (byte)'3', (byte)'4', (byte)'5', (byte)'6', (byte)'7', (byte)'8', (byte)'9', (byte)'a', (byte)'b', (byte)'c', (byte)'d', (byte)'e', (byte)'f' };

			for (int i = 0; i < 103; i++)
			{
				hexCharMappingAll[i] = new byte[103];
			}


			for (int i = 0; i < 16; i++)
			{
				byte byteFirst = usedBytes[i];

				for (int j = 0; j < 16; j++)
				{
					byte byteSecond = usedBytes[j];
					hexCharMappingAll[byteFirst][byteSecond] = (byte)(j + i * 16);
				}
			}


			#endregion
		}
		
		#endregion


		#region Write values.

		#region WriteLength().

		/// <summary>
		/// Writes one length value to the given <see cref="StringBuilder"/>.
		/// </summary>
		/// <param name="sb"><see cref="StringBuilder"/> to write to.</param>
		/// <param name="value">Value to write.</param>
		public static void WriteLength(StringBuilder sb, int value)
		{
			WriteInt32(sb, value);
		}


		/// <summary>
		/// Writes one length value to the given <see cref="StringBuilder"/>.
		/// </summary>
		/// <param name="sb"><see cref="StringBuilder"/> to write to.</param>
		/// <param name="value">Value to write.</param>
		/// <remarks>Is used now in writing collection items length only.</remarks>
		public static void WriteLength(StringBuilder sb, uint value)
		{
			WriteUInt32(sb, value);
		}

		#endregion


		#region WriteTypeNumber().

		/// <summary>
		/// Writes one type number to the given <see cref="StringBuilder"/>.
		/// </summary>
		/// <param name="sb"><see cref="StringBuilder"/> to write to.</param>
		/// <param name="value">Value to write.</param>
		public static void WriteTypeNumber(StringBuilder sb, ref int value)
		{
			WriteInt32(sb, value);
		}


		/// <summary>
		/// Writes one type number to the given <see cref="StringBuilder"/>.
		/// </summary>
		/// <param name="sb"><see cref="StringBuilder"/> to write to.</param>
		/// <param name="value">Value to write.</param>
		public static void WriteTypeNumber(StringBuilder sb, ref uint value)
		{
			WriteUInt32(sb, value);
		}

		#endregion



		#region WriteInt32().

		/// <summary>
		/// Writes <see cref="Int32"/> value to the given <see cref="StringBuilder"/>.
		/// </summary>
		/// <param name="sb"><see cref="StringBuilder"/> to write to.</param>
		/// <param name="value">Value to write.</param>
		public static void WriteInt32(StringBuilder sb, int value)
		{
			sb.Append(
				(char)(value >> 16)
				);
			sb.Append(
				(char)(value & 65535)
				);
		}

		#endregion


		#region WriteUInt32().

		/// <summary>
		/// Writes <see cref="UInt32"/> value to the given <see cref="StringBuilder"/>.
		/// </summary>
		/// <param name="sb"><see cref="StringBuilder"/> to write to.</param>
		/// <param name="value">Value to write.</param>
		public static void WriteUInt32(StringBuilder sb, uint value)
		{
			sb.Append(
				(char)(value >> 16)
				);
			sb.Append(
				(char)(value & 65535)
				);
		}
		
		#endregion



		#region WriteInt64().

		/// <summary>
		/// Writes <see cref="Int64"/> value to the given <see cref="StringBuilder"/>.
		/// </summary>
		/// <param name="sb"><see cref="StringBuilder"/> to write to.</param>
		/// <param name="value">Value to write.</param>
		public static void WriteInt64(StringBuilder sb, long value)
		{
			sb.Append(
				(char)(value >> 48)
				);
			sb.Append(
				(char)((value & 281470681743360) >> 32)
				);
			sb.Append(
				(char)((value & 4294901760) >> 16)
				);
			sb.Append(
				(char)(value & 65535)
				);
		}

		#endregion


		#region WriteUInt64().

		/// <summary>
		/// Writes <see cref="UInt64"/> value to the given <see cref="StringBuilder"/>.
		/// </summary>
		/// <param name="sb"><see cref="StringBuilder"/> to write to.</param>
		/// <param name="value">Value to write.</param>
		public static void WriteUInt64(StringBuilder sb, ulong value)
		{
			sb.Append(
				(char)(value >> 48)
				);
			sb.Append(
				(char)((value & 281470681743360) >> 32)
				);
			sb.Append(
				(char)((value & 4294901760) >> 16)
				);
			sb.Append(
				(char)(value & 65535)
				);
		}

		#endregion



		#region WriteUInt16().

		/// <summary>
		/// Writes <see cref="UInt16"/> value to the given <see cref="StringBuilder"/>.
		/// </summary>
		/// <param name="sb"><see cref="StringBuilder"/> to write to.</param>
		/// <param name="value">Value to write.</param>
		public static void WriteUInt16(StringBuilder sb, ushort value)
		{
			sb.Append((char)value);
		}

		#endregion


		#region WriteInt16().

		/// <summary>
		/// Writes <see cref="Int16"/> value to the given <see cref="StringBuilder"/>.
		/// </summary>
		/// <param name="sb"><see cref="StringBuilder"/> to write to.</param>
		/// <param name="value">Value to write.</param>
		public static void WriteInt16(StringBuilder sb, short value)
		{
			sb.Append((char)value);
		}

		#endregion



		#region WriteByte().

		/// <summary>
		/// Writes <see cref="Byte"/> value to the given <see cref="StringBuilder"/>.
		/// </summary>
		/// <param name="sb"><see cref="StringBuilder"/> to write to.</param>
		/// <param name="value">Value to write.</param>
		public static void WriteByte(StringBuilder sb, byte value)
		{
			sb.Append((char)value);
		}

		#endregion


		#region WriteFloat().

		/// <summary>
		/// Writes <see cref="float"/> value to the given <see cref="StringBuilder"/>.
		/// </summary>
		/// <param name="sb"><see cref="StringBuilder"/> to write to.</param>
		/// <param name="value">Value to write.</param>
		public static void WriteFloat(StringBuilder sb, float value)
		{
			writeBytesAsChars(sb, BitConverter.GetBytes(value), 4);
		}

		#endregion


		#region WriteDouble().

		/// <summary>
		/// Writes <see cref="double"/> value to the given <see cref="StringBuilder"/>.
		/// </summary>
		/// <param name="sb"><see cref="StringBuilder"/> to write to.</param>
		/// <param name="value">Value to write.</param>
		public static void WriteDouble(StringBuilder sb, double value)
		{
			writeBytesAsChars(sb, BitConverter.GetBytes(value), 8);
		}

		#endregion


		#region WriteDecimal().

		/// <summary>
		/// Writes <see cref="decimal"/> value to the given <see cref="StringBuilder"/>.
		/// </summary>
		/// <param name="sb"><see cref="StringBuilder"/> to write to.</param>
		/// <param name="value">Value to write.</param>
		public static void WriteDecimal(StringBuilder sb, decimal value)
		{
			writeBytesAsChars(sb, DecimalHelper.GetBytes(value), 16);
		}

		#endregion



		#region WriteGuid().

		/// <summary>
		/// Writes <see cref="Guid"/> value in byte form to the given <see cref="StringBuilder"/>.
		/// </summary>
		/// <param name="sb"><see cref="StringBuilder"/> to write to.</param>
		/// <param name="guid"><see cref="Guid"/> to write.</param>
		public static void WriteGuid(StringBuilder sb, Guid guid)
		{
			writeBytesAsChars(sb, guid.ToByteArray(), 16);
		}
		
		#endregion


		#region WriteBool().

		/// <summary>
		/// Writes <see cref="Boolean"/> value in serialization form to the given <see cref="StringBuilder"/>.
		/// </summary>
		/// <param name="sb"><see cref="StringBuilder"/> to write to.</param>
		/// <param name="value">Value to write.</param>
		public static void WriteBool(StringBuilder sb, bool value)
		{
			sb.Append(
				value ? '1' : '0'
			);
		}

		#endregion


		#region WriteChar().

		/// <summary>
		/// Writes <see cref="char"/> value in serialization form to the given <see cref="StringBuilder"/>.
		/// </summary>
		/// <param name="sb"><see cref="StringBuilder"/> to write to.</param>
		/// <param name="value">Value to write.</param>
		public static void WriteChar(StringBuilder sb, char value)
		{
			sb.Append(value);
		}

		#endregion



		#region writeBytes().
		/*
		/// <summary>
		/// Writes bytes array to the given <see cref="StringBuilder"/>.
		/// </summary>
		/// <param name="sb"><see cref="StringBuilder"/> to write bytes to.</param>
		/// <param name="bytes">Bytes array to write.</param>
		/// <param name="bytesLength">Length of the bytes array.</param>
		/// <remarks>NOT IN USE NOW.</remarks>
		private static void writeBytes(StringBuilder sb, byte[] bytes, int bytesLength)
		{
			for (int i = 0; i < bytesLength; i++)
			{
				sb.Append((char)bytes[i]);
			}
		}
		*/
		// is not in use
		#endregion


		#region writeBytesAsChars().

		/// <summary>
		/// Writes bytes array as characters to the given <see cref="StringBuilder"/>.
		/// </summary>
		/// <param name="sb"><see cref="StringBuilder"/> to write bytes to.</param>
		/// <param name="bytes">Bytes array to write.</param>
		/// <param name="bytesLength">Length of the bytes array.</param>
		private static void writeBytesAsChars(StringBuilder sb, byte[] bytes, int bytesLength)
		{
			for (int i = 0; i != bytesLength; i += 2)
			{
				sb.Append(
					(char)((bytes[i] << 8) | bytes[i + 1])
					);
			}
		}

		#endregion

		#endregion


		#region Parse values.

		#region SplitUnsafe().

		/// <summary>
		/// Splits (part of the) text by a character.
		/// </summary>
		/// <param name="textPointer">Pointer to start position in the text to split.</param>
		/// <param name="textLength">Length of the text to use.</param>
		/// <param name="resultArray">Array to fill.</param>
		/// <param name="resultLength">Length of the result array to set.</param>
		public unsafe static void SplitUnsafe(
			char* textPointer,
			int textLength,
			ref int[] resultArray,
			ref int resultLength
			)
		{
			resultLength = textLength / 2;
			resultArray = new int[resultLength];

			fixed (int* tempArray = resultArray)
			{
				char* lastPointer = textPointer + textLength;
				int* resultArrayPosition = tempArray;

				while (textPointer != lastPointer)
				{
					*resultArrayPosition =
						(((int)*textPointer) << 16) | // take first character as upper part; we can be sure that right part is now empty
						(int)*(textPointer + 1) // take second character as upper part
						;

					resultArrayPosition++;
					textPointer += 2;
				}
			}
		}

		/**/
		#endregion


		#region ParseIntLength().

		/// <summary>
		/// Parses text to integer length value in safe way.
		/// </summary>
		/// <param name="textPointer">Pointer to text start position.</param>
		/// <returns>Parsed integer value.</returns>
		public static unsafe int ParseIntLength(ref char* textPointer)
		{
			return
				(((int)(*textPointer)) << 16) | // take first character as upper part; we can be sure that right part is now empty
				(int)(*(textPointer + 1)) // take second character as upper part
				;
		}

		#endregion



		#region ParseInt().

		/// <summary>
		/// Parses text to integer value.
		/// </summary>
		/// <param name="textPointer">Pointer to text start position.</param>
		/// <returns>Parsed value.</returns>
		public static unsafe int ParseInt(ref char* textPointer)
		{
			return
				(((int)(*textPointer)) << 16) |
				(int)(*(textPointer + 1))
				;
		}

		#endregion


		#region ParseUInt().

		/// <summary>
		/// Parses text to <see cref="UInt32"/> value in safe way.
		/// </summary>
		/// <param name="textPointer">Pointer to text start position.</param>
		/// <returns>Parsed value.</returns>
		public static unsafe uint ParseUInt(ref char* textPointer)
		{
			return
				(((uint)(*textPointer)) << 16) |
				(uint)(*(textPointer + 1))
				;
		}

		#endregion



		#region ParseInt64().

		/// <summary>
		/// Parses text to <see cref="Int64"/> value in safe way.
		/// </summary>
		/// <param name="textPointer">Pointer to text start position.</param>
		/// <returns>Parsed value.</returns>
		public static unsafe long ParseInt64(ref char* textPointer)
		{
			return
				(((long)*textPointer) << 48) |
				(((long)*(textPointer + 1)) << 32) |
				(((long)*(textPointer + 2)) << 16) |
				(long)*(textPointer + 3)
				;
		}

		#endregion


		#region ParseUInt64().

		/// <summary>
		/// Parses text to <see cref="UInt64"/> value in safe way.
		/// </summary>
		/// <param name="textPointer">Pointer to text start position.</param>
		/// <returns>Parsed value.</returns>
		public static unsafe ulong ParseUInt64(ref char* textPointer)
		{
			return
				(((ulong)*textPointer) << 48) |
				(((ulong)*(textPointer + 1)) << 32) |
				(((ulong)*(textPointer + 2)) << 16) |
				(ulong)*(textPointer + 3)
				;
		}

		#endregion



		#region ParseInt16().

		/// <summary>
		/// Parses text to <see cref="Int16"/> value in safe way.
		/// </summary>
		/// <param name="textPointer">Pointer to text start position.</param>
		/// <returns>Parsed value.</returns>
		public static unsafe short ParseInt16(ref char* textPointer)
		{
			return (short)*textPointer;
		}

		#endregion


		#region ParseUInt16().

		/// <summary>
		/// Parses text to <see cref="UInt16"/> value in safe way.
		/// </summary>
		/// <param name="textPointer">Pointer to text start position.</param>
		/// <returns>Parsed value.</returns>
		public static unsafe ushort ParseUInt16(ref char* textPointer)
		{
			return (ushort)*textPointer;
		}

		#endregion



		#region ParseByte().

		/// <summary>
		/// Parses text to <see cref="Byte"/> value in safe way.
		/// </summary>
		/// <param name="textPointer">Pointer to text start position.</param>
		/// <returns>Parsed value.</returns>
		public static unsafe byte ParseByte(ref char* textPointer)
		{
			return (byte)*textPointer;
		}

		#endregion


		#region ParseFloat().

		/// <summary>
		/// Parses text to <see cref="float"/> value in safe way.
		/// </summary>
		/// <param name="textPointer">Pointer to text start position.</param>
		/// <returns>Parsed value.</returns>
		public static unsafe float ParseFloat(ref char* textPointer)
		{
			getBytesFromCharsUnsafe(ref textPointer, 2, ref staticBytes4);
			return BitConverter.ToSingle(staticBytes4, 0);
		}

		#endregion


		#region ParseDouble().

		/// <summary>
		/// Parses text to <see cref="double"/> value in safe way.
		/// </summary>
		/// <param name="textPointer">Pointer to text start position.</param>
		/// <returns>Parsed value.</returns>
		public static unsafe double ParseDouble(ref char* textPointer)
		{
			getBytesFromCharsUnsafe(ref textPointer, 4, ref staticBytes8);
			return BitConverter.ToDouble(staticBytes8, 0);
		}

		#endregion


		#region ParseDecimal().

		/// <summary>
		/// Parses text to <see cref="decimal"/> value in safe way.
		/// </summary>
		/// <param name="textPointer">Pointer to text start position.</param>
		/// <returns>Parsed value.</returns>
		public static unsafe decimal ParseDecimal(ref char* textPointer)
		{
			getBytesFromCharsUnsafe(ref textPointer, 8, ref staticBytes16);
			return DecimalHelper.ToDecimal(staticBytes16);
		}

		#endregion



		#region ParseGuid().

		/// <summary>
		/// Parses <see cref="Guid"/>, saved in byte form.
		/// </summary>
		/// <param name="textPointer">Pointer to text start position.</param>
		/// <returns>Parsed <see cref="Guid"/> object.</returns>
		public static unsafe Guid ParseGuid(ref char* textPointer)
		{
			getBytesFromCharsUnsafe(ref textPointer, 8, ref staticBytes16);
			return new Guid(staticBytes16);
		}

		#endregion


		#region Parse header GUID.

		// NB.! We cannot move this function to common work with GUIDs because we need a pecial header separator which can be confused with GUIDs bytes.

		#region Version 1. Requires appr. 10 KB more memory.

		#region Main function.

		/// <summary>
		/// Parses <see cref="Guid"/>, saved in text form.
		/// </summary>
		/// <param name="text">Text to parse.</param>
		/// <param name="start">Text start position.</param>
		/// <returns>Parsed <see cref="Guid"/> object.</returns>
		public unsafe static Guid ParseHeaderGuid(ref string text)
		{
			int start = 0;
			return ParseHeaderGuid(ref text, ref start);
		}


		/// <summary>
		/// Parses <see cref="Guid"/>, saved in text form.
		/// </summary>
		/// <param name="text">Text to parse.</param>
		/// <param name="start">Text start position.</param>
		/// <returns>Parsed <see cref="Guid"/> object.</returns>
		public unsafe static Guid ParseHeaderGuid(ref string text, ref int start)
		{
			byte[] guidBytes = new byte[16];

			fixed (char* value = text)
			{
				char* pointer = value + start;

				guidBytes[3] = ParseHexByte(ref pointer);
				pointer += 2;

				guidBytes[2] = ParseHexByte(ref pointer);
				pointer += 2;

				guidBytes[1] = ParseHexByte(ref pointer);
				pointer += 2;

				guidBytes[0] = ParseHexByte(ref pointer);
				pointer += 2;


				guidBytes[5] = ParseHexByte(ref pointer);
				pointer += 2;

				guidBytes[4] = ParseHexByte(ref pointer);
				pointer += 2;


				guidBytes[7] = ParseHexByte(ref pointer);
				pointer += 2;

				guidBytes[6] = ParseHexByte(ref pointer);
				pointer += 2;


				guidBytes[8] = ParseHexByte(ref pointer);
				pointer += 2;

				guidBytes[9] = ParseHexByte(ref pointer);
				pointer += 2;

				guidBytes[10] = ParseHexByte(ref pointer);
				pointer += 2;

				guidBytes[11] = ParseHexByte(ref pointer);
				pointer += 2;

				guidBytes[12] = ParseHexByte(ref pointer);
				pointer += 2;

				guidBytes[13] = ParseHexByte(ref pointer);
				pointer += 2;

				guidBytes[14] = ParseHexByte(ref pointer);
				pointer += 2;

				guidBytes[15] = ParseHexByte(ref pointer);
			}

			return new Guid(guidBytes);
		}

		#endregion


		#region Auxiliary function.

		/// <summary>
		/// Parses text to integer value in safe way.
		/// </summary>
		/// <param name="pointer">Pointer to the text start.</param>
		/// <returns>Parsed integer value.</returns>
		private unsafe static byte ParseHexByte(ref char* pointer)
		{
			return hexCharMappingAll[*pointer][*(pointer + 1)];
		}

		#endregion
		
		#endregion


		#region Version 2. Works a little slower, requires less memory.

		#region Main function.

		/// <summary>
		/// Parses <see cref="Guid"/> from text.
		/// </summary>
		/// <param name="text">Text to parse.</param>
		/// <param name="start">Text start position.</param>
		/// <returns>Parsed <see cref="Guid"/> object.</returns>
		public unsafe static Guid ParseHeaderGuid2(ref string text)
		{
			int start = 0;
			return ParseHeaderGuid2(ref text, ref start);
		}

		/// <summary>
		/// Parses <see cref="Guid"/> from text.
		/// </summary>
		/// <param name="text">Text to parse.</param>
		/// <param name="start">Text start position.</param>
		/// <returns>Parsed <see cref="Guid"/> object.</returns>
		public unsafe static Guid ParseHeaderGuid2(ref string text, ref int start)
		{
			byte[] guidBytes = new byte[16];

			fixed (char* value = text)
			{
				char* pointer = value + start;

				guidBytes[3] = ParseHexByte2(ref pointer);
				pointer += 2;

				guidBytes[2] = ParseHexByte2(ref pointer);
				pointer += 2;

				guidBytes[1] = ParseHexByte2(ref pointer);
				pointer += 2;

				guidBytes[0] = ParseHexByte2(ref pointer);
				pointer += 2;


				guidBytes[5] = ParseHexByte2(ref pointer);
				pointer += 2;

				guidBytes[4] = ParseHexByte2(ref pointer);
				pointer += 2;


				guidBytes[7] = ParseHexByte2(ref pointer);
				pointer += 2;

				guidBytes[6] = ParseHexByte2(ref pointer);
				pointer += 2;


				guidBytes[8] = ParseHexByte2(ref pointer);
				pointer += 2;

				guidBytes[9] = ParseHexByte2(ref pointer);
				pointer += 2;

				guidBytes[10] = ParseHexByte2(ref pointer);
				pointer += 2;

				guidBytes[11] = ParseHexByte2(ref pointer);
				pointer += 2;

				guidBytes[12] = ParseHexByte2(ref pointer);
				pointer += 2;

				guidBytes[13] = ParseHexByte2(ref pointer);
				pointer += 2;

				guidBytes[14] = ParseHexByte2(ref pointer);
				pointer += 2;

				guidBytes[15] = ParseHexByte2(ref pointer);
			}

			return new Guid(guidBytes);
		}

		#endregion


		#region Auxiliary functions.

		/// <summary>
		/// Parses text to integer value in safe way.
		/// </summary>
		/// <param name="pointer">Pointer to the text start.</param>
		/// <returns>Parsed integer value.</returns>
		private unsafe static byte ParseHexByte2(ref char* pointer)
		{
			return (byte)(hexFirstCharMapping[*pointer] + hexCharMapping[*(pointer + 1)]);
		}

		#endregion
		
		#endregion

		#endregion



		#region getBytes().
		/*
		/// <summary>
		/// Gets bytes array from given text in safe manner.
		/// </summary>
		/// <param name="text">Text to parse.</param>
		/// <param name="start">Text start position.</param>
		/// <param name="count">Array count.</param>
		/// <returns>Parsed bytes array.</returns>
		/// <remarks>Was intended to be used with <see cref="Int16"/> and <see cref="UInt16"/>, but works longer than parsing one char. Is not in use now.</remarks>
		private unsafe static byte[] getBytes(
			ref string text,
			ref int start,
			int count
			)
		{
			byte[] bytes = new byte[count];
			for (int i = 0; i < count; i++)
			{
				bytes[i] = (byte)text[start + i];
			}

			return bytes;
		}
		*/
		// is not in use
		#endregion


		#region getBytesFromCharsUnsafe().

		/// <summary>
		/// Fills bytes array, stored as characters, from given text in unsafe manner.
		/// </summary>
		/// <param name="pointer">Pointer to text start position.</param>
		/// <param name="count">Array count.</param>
		/// <param name="bytes">Bytes array of necessary length.</param>
		private unsafe static void getBytesFromCharsUnsafe(
			ref char* pointer,
			int count,
			ref byte[] bytes
			)
		{
			fixed (byte* byteInitialPointer = bytes)
			{
				char* lastPointer = pointer + count;

				byte* bytePointer = byteInitialPointer;

				while (pointer != lastPointer)
				{
					char c = *pointer;

					*bytePointer = (byte)(c >> 8);
					bytePointer++;

					*bytePointer = (byte)(c & 255);
					bytePointer++;

					pointer++;
				}
			}
		}

		#endregion

		#endregion
	}
}
