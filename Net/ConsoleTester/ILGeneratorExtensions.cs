﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection.Emit;
using System.Reflection;

namespace ConsoleTester
{
	static public class ILGeneratorExtensions
	{
		static public void EmitCall(this ILGenerator il, MethodInfo method)
		{
			if (method.IsFinal || !method.IsVirtual)
			{
				il.EmitCall(OpCodes.Call, method, null);
			}
			else
			{
				il.EmitCall(OpCodes.Callvirt, method, null);
			}
		}

		static public void EmitLiteralLoad(this ILGenerator il, int value)
		{
			switch (value)
			{
				case -1:
					il.Emit(OpCodes.Ldc_I4_M1);
					return;
				case 0:
					il.Emit(OpCodes.Ldc_I4_0);
					return;
				case 1:
					il.Emit(OpCodes.Ldc_I4_1);
					return;
				case 2:
					il.Emit(OpCodes.Ldc_I4_2);
					return;
				case 3:
					il.Emit(OpCodes.Ldc_I4_3);
					return;
				case 4:
					il.Emit(OpCodes.Ldc_I4_4);
					return;
				case 5:
					il.Emit(OpCodes.Ldc_I4_5);
					return;
				case 6:
					il.Emit(OpCodes.Ldc_I4_6);
					return;
				case 7:
					il.Emit(OpCodes.Ldc_I4_7);
					return;
				case 8:
					il.Emit(OpCodes.Ldc_I4_8);
					return;
			}

			if (value > -129 && value < 128)
				il.Emit(OpCodes.Ldc_I4_S, (SByte)value);
			else
				il.Emit(OpCodes.Ldc_I4, value);
		}

		static public void EmitJumpTo(this ILGenerator il, Label label)
		{
			il.Emit(OpCodes.Br_S, label);
		}

		static public void EmitLiteralLoad(this ILGenerator il, bool value)
		{
			if (value)
				il.Emit(OpCodes.Ldc_I4_1);
			else
				il.Emit(OpCodes.Ldc_I4_0);
		}

		static public void EmitLiteralLoad(this ILGenerator il, long value)
		{
			il.Emit(OpCodes.Ldc_I8, value);
		}

		static public void EmitLiteralLoad(this ILGenerator il, float value)
		{
			il.Emit(OpCodes.Ldc_R4, value);
		}

		static public void EmitLiteralLoad(this ILGenerator il, double value)
		{
			il.Emit(OpCodes.Ldc_R8, value);
		}

		static public void EmitLiteralLoad(this ILGenerator il, string value)
		{
			il.Emit(OpCodes.Ldstr, value);
		}

		static public void EmitLiteralLoad(this ILGenerator il, IntPtr value)
		{
			if (IntPtr.Size == 4)
				il.Emit(OpCodes.Ldc_I4, value.ToInt32());
			else if (IntPtr.Size == 8)
				il.Emit(OpCodes.Ldc_I8, value.ToInt64());
		}
	}
}
