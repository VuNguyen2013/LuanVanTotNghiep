using System;
using System.Collections.Generic;
using System.Text;

namespace OTS.WebLib.utilities
{
	/// <summary> 
	/// Class for converting data that comes from a any obscure (not strongly typed) source, 
	/// such as a database, or an Xml document. 
	/// </summary> 
	public class SafeTypecast
	{

		/// <summary> 
		/// Converts to byte datatype from an arbitrary object. 
		/// </summary> 
		/// <remarks>Returns 0 if conversion fails.</remarks>
		/// <param name="Value">An arbitrary object</param> 
		/// <returns>byte value</returns> 
		public static byte ToByte(object Value)
		{
			return ToByte(Value, 0);
		}

		/// <summary> 
		/// Converts to byte datatype from an arbitrary object. 
		/// </summary>
		/// <param name="Value">An arbitrary object</param> 
		/// <param name="DefaultValue">Value to be returned if conversion fails or is null.</param>
		/// <returns>byte value</returns> 
		public static byte ToByte(object Value, byte DefaultValue)
		{
			try
			{
				return (Value == DBNull.Value || Value == null ? DefaultValue : Convert.ToByte(Value));
			}
			catch
			{
				return DefaultValue;
			}
		}

		/// <summary> 
		/// Converts to short (Int16) datatype from an arbitrary object. 
		/// </summary> 
		/// <remarks>Returns -1 if conversion fails.</remarks>
		/// <param name="Value">An arbitrary object</param> 
		/// <returns>short value</returns> 
		public static short ToShort(object Value)
		{
			return ToShort(Value, -1);
		}

		/// <summary> 
		/// Converts to short (Int16) datatype from an arbitrary object. 
		/// </summary> 
		/// <param name="Value">An arbitrary object</param>
		/// <param name="DefaultValue">Value to be returned if conversion fails or is null.</param>
		/// <returns>short value</returns> 
		public static short ToShort(object Value, short DefaultValue)
		{
			try
			{
				return (Value == DBNull.Value || Value == null ? DefaultValue : Convert.ToInt16(Value));
			}
			catch
			{
				return DefaultValue;
			}
		}

		/// <summary> 
		/// Converts to int (Int32) datatype from an arbitrary object. 
		/// </summary> 
		/// <remarks>Returns -1 if conversion fails.</remarks>
		/// <param name="Value">An arbitrary object</param> 
		/// <returns>Int value</returns> 
		public static int ToInt(object Value)
		{
			return ToInt(Value, -1);
		}

		/// <summary> 
		/// Converts to int (Int32) datatype from an arbitrary object. 
		/// </summary> 
		/// <param name="Value">An arbitrary object</param>
		/// <param name="DefaultValue">Value to be returned if conversion fails or is null.</param>
		/// <returns>Int value</returns> 
		public static int ToInt(object Value, int DefaultValue)
		{
			try
			{
				return (Value == DBNull.Value || Value == null ? DefaultValue : Convert.ToInt32(Value));
			}
			catch
			{
				return DefaultValue;
			}
		}

		/// <summary> 
		/// Converts to long (Int64) datatype from an arbitrary object. 
		/// </summary> 
		/// <remarks>Returns -1 if conversion fails.</remarks>
		/// <param name="Value">An arbitrary object</param> 
		/// <returns>Long value</returns> 
		public static long ToLong(object Value)
		{
			return ToLong(Value, -1);
		}


		/// <summary> 
		/// Converts to long (Int64) datatype from an arbitrary object. 
		/// </summary> 
		/// <param name="Value">An arbitrary object</param> 
		/// <param name="DefaultValue">Value to be returned if conversion fails or is null.</param>
		/// <returns>Long value</returns> 
		public static long ToLong(object Value, long DefaultValue)
		{
			try
			{
				return (Value == DBNull.Value || Value == null ? DefaultValue : Convert.ToInt64(Value));
			}
			catch
			{
				return DefaultValue;
			}
		}

		/// <summary> 
		/// Converts to unsigned short (UInt16) datatype from an arbitrary object.
		/// </summary> 
		/// <remarks>Returns 0 if conversion fails.</remarks>
		/// <param name="Value">An arbitrary object</param> 
		/// <returns>ushort value</returns> 
		public static ushort ToUnsignedShort(object Value)
		{
			return ToUnsignedShort(Value, 0);
		}

		/// <summary> 
		/// Converts to unsigned short (UInt16) datatype from an arbitrary object. 
		/// </summary> 
		/// <param name="Value">An arbitrary object</param>
		/// <param name="DefaultValue">Value to be returned if conversion fails or is null.</param>
		/// <returns>ushort value</returns> 
		public static ushort ToUnsignedShort(object Value, ushort DefaultValue)
		{
			try
			{
				return (Value == DBNull.Value || Value == null ? DefaultValue : Convert.ToUInt16(Value));
			}
			catch
			{
				return DefaultValue;
			}
		}

		/// <summary> 
		/// Converts to unsigned int (UInt32) datatype from an arbitrary object. 
		/// </summary> 
		/// <remarks>Returns 0 if conversion fails.</remarks>
		/// <param name="Value">An arbitrary object</param> 
		/// <returns>UInt value</returns> 
		public static uint ToUnsignedInt(object Value)
		{
			return ToUnsignedInt(Value, 0);
		}

		/// <summary> 
		/// Converts to unsigned int (UInt32) datatype from an arbitrary object. 
		/// </summary> 
		/// <param name="Value">An arbitrary object</param>
		/// <param name="DefaultValue">Value to be returned if conversion fails or is null.</param>
		/// <returns>UInt value</returns> 
		public static uint ToUnsignedInt(object Value, uint DefaultValue)
		{
			try
			{
				return (Value == DBNull.Value || Value == null ? DefaultValue : Convert.ToUInt32(Value));
			}
			catch
			{
				return DefaultValue;
			}
		}

		/// <summary> 
		/// Converts to unsigned long (UInt64) datatype from an arbitrary object. 
		/// </summary> 
		/// <remarks>Returns 0 if conversion fails.</remarks>
		/// <param name="Value">An arbitrary object</param> 
		/// <returns>ULong value</returns> 
		public static ulong ToUnsignedLong(object Value)
		{
			return ToUnsignedLong(Value, 0);
		}


		/// <summary> 
		/// Converts to long (Int64) datatype from an arbitrary object. 
		/// </summary> 
		/// <param name="Value">An arbitrary object</param> 
		/// <param name="DefaultValue">Value to be returned if conversion fails or is null.</param>
		/// <returns>Long value</returns> 
		public static ulong ToUnsignedLong(object Value, ulong DefaultValue)
		{
			try
			{
				return (Value == DBNull.Value || Value == null ? DefaultValue : Convert.ToUInt64(Value));
			}
			catch
			{
				return DefaultValue;
			}
		}

		/// <summary>
		/// Converts to decimal datatype from an arbitrary object. 
		/// </summary> 
		/// <remarks>Returns 0 if conversion fails.</remarks>
		/// <param name="Value">An arbitrary object</param> 
		/// <returns>decimal value</returns> 
		public static decimal ToDecimal(object Value)
		{
			return ToDecimal(Value, 0);
		}


		/// <summary> 
		/// Converts to decimal datatype from an arbitrary object. 
		/// </summary> 
		/// <param name="Value">An arbitrary object</param> 
		/// <param name="DefaultValue">Value to be returned if conversion fails or is null.</param>
		/// <returns>decimal value</returns> 
		public static decimal ToDecimal(object Value, decimal DefaultValue)
		{
			try
			{
				return (Value == DBNull.Value || Value == null ? DefaultValue : Convert.ToDecimal(Value));
			}
			catch
			{
				return DefaultValue;
			}
		}

		/// <summary> 
		/// Converts to double datatype from an arbitrary object. 
		/// </summary> 
		/// <remarks>Returns 0 if conversion fails.</remarks>
		/// <param name="Value">An arbitrary object</param> 
		/// <returns>double value</returns> 
		public static double ToDouble(object Value)
		{
			return ToDouble(Value, 0);
		}


		/// <summary> 
		/// Converts to decimal datatype from an arbitrary object. 
		/// </summary> 
		/// <param name="Value">An arbitrary object</param> 
		/// <param name="DefaultValue">Value to be returned if conversion fails or is null.</param>
		/// <returns>double value</returns> 
		public static double ToDouble(object Value, double DefaultValue)
		{
			try
			{
				return (Value == DBNull.Value || Value == null ? DefaultValue : Convert.ToDouble(Value));
			}
			catch
			{
				return DefaultValue;
			}
		}

		/// <summary> 
		/// Converts to single (float) datatype from an arbitrary object. 
		/// </summary> 
		/// <remarks>Returns 0 if conversion fails.</remarks>
		/// <param name="Value">An arbitrary object</param> 
		/// <returns>single (float) value</returns> 
		public static float ToSingle(object Value)
		{
			return ToSingle(Value, 0);
		}


		/// <summary> 
		/// Converts to single (float) datatype from an arbitrary object. 
		/// </summary> 
		/// <param name="Value">An arbitrary object</param> 
		/// <param name="DefaultValue">Value to be returned if conversion fails or is null.</param>
		/// <returns>single (float) value</returns> 
		public static float ToSingle(object Value, float DefaultValue)
		{
			try
			{
				return (Value == DBNull.Value || Value == null ? DefaultValue : Convert.ToSingle(Value));
			}
			catch
			{
				return DefaultValue;
			}
		}

		/// <summary> 
		/// Converts to single (float) datatype from an arbitrary object. 
		/// </summary> 
		/// <param name="Value">An arbitrary object</param> 
		/// <returns>single (float) value</returns> 
		public static float ToFloat(object Value)
		{
			return ToSingle(Value);
		}


		/// <summary> 
		/// Converts to single (float) datatype from an arbitrary object. 
		/// </summary> 
		/// <param name="Value">An arbitrary object</param> 
		/// <param name="DefaultValue">Value to be returned if conversion fails or is null.</param>
		/// <returns>single (float) value</returns> 
		public static float ToFloat(object Value, float DefaultValue)
		{
			return ToSingle(Value, DefaultValue);
		}

		/// <summary> 
		/// Converts to boolean datatype from an arbitrary object. 
		/// </summary> 
		/// <remarks>Returns false if conversion fails.</remarks>
		/// <param name="Value">An arbitrary object</param> 
		/// <returns>Boolean value</returns> 
		public static bool ToBoolean(object Value)
		{
			try
			{
				return (Value == DBNull.Value || Value == null ? false : Boolean.Parse(Value.ToString()));
			}
			catch
			{
				return (Value.ToString() == "0" ? false : true);
			}
		}


		/// <summary> 
		/// Converts to string datatype from an arbitrary object.
		/// </summary> 
		/// <remarks>Returns a blank string if conversion fails.</remarks>
		/// <param name="Value">An arbitrary object</param> 
		/// <returns>string value</returns> 
		public static string ToString(object Value)
		{
			return ToString(Value, "");
		}

		/// <summary> 
		/// Converts to string datatype from an arbitrary object. 
		/// </summary> 
		/// <param name="Value">An arbitrary object</param> 
		/// <param name="DefaultValue">Value to be returned if conversion fails or is null.</param>
		/// <returns>string value</returns> 
		public static string ToString(object Value, string DefaultValue)
		{
			try
			{
				return (Value == DBNull.Value || Value == null ? DefaultValue : Value.ToString());
			}
			catch
			{
				return DefaultValue;
			}
		}

		/// <summary> 
		/// Converts to DateTime datatype from an arbitrary object.
		/// </summary> 
		/// <remarks>Returns DateTime.MinValue if conversion fails.</remarks>
		/// <param name="Value">An arbitrary object</param> 
		/// <returns>DateTime value</returns> 
		public static DateTime ToDateTime(object Value)
		{
			return ToDateTime(Value, DateTime.MinValue);
		}

		/// <summary> 
		/// Converts to DateTime datatype from an arbitrary object. 
		/// </summary> 
		/// <param name="Value">An arbitrary object</param> 
		/// <param name="DefaultValue">Value to be returned if conversion fails or is null.</param>
		/// <returns>DateTime value</returns> 
		public static DateTime ToDateTime(object Value, DateTime DefaultValue)
		{
			try
			{
				return (Value == DBNull.Value || Value == null ? DefaultValue : DateTime.Parse(Value.ToString()));
			}
			catch
			{
				return DefaultValue;
			}
		}

	}
}
