// Core
using System;
using System.Collections.Generic;

namespace NathansWay.MonoGame
{
	public static class HelperFunctions 
	{
        private static HashSet<Type> NumericTypes = new HashSet<Type>
        {
                typeof(int),
                typeof(uint),
                typeof(double),
                typeof(decimal)
        };

        public static bool IsNumericType(Type type)
        {
            return NumericTypes.Contains(type) || NumericTypes.Contains(Nullable.GetUnderlyingType(type));
        }

	}
}

