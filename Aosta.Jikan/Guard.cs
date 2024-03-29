﻿using System.Numerics;
using Aosta.Common.Extensions;
using Aosta.Jikan.Exceptions;

using FastEnumUtility;

namespace Aosta.Jikan;

internal static class Guard
{
	internal static void IsSingleFlag<T>(T e, string paramName) where T : struct, Enum
	{
		if (!e.HasSingleFlag())
		{
			throw new JikanParameterValidationException("Flag enum is not supported for this operation.", paramName);
		}
	}

	internal static void IsDefaultEndpoint(string? endpoint, string methodName)
	{
		if (JikanConfiguration.DEFAULT_ENDPOINT.Equals(endpoint))
		{
			throw new NotSupportedException($"Operation {methodName} is not supported on the default endpoint.");
		}
	}

	internal static void IsNotNullOrWhiteSpace(string? arg, string argumentName)
	{
		if (string.IsNullOrWhiteSpace(arg))
		{
			throw new JikanParameterValidationException("Argument can't be null or whitespace.", argumentName);
		}
	}

	internal static void IsNotNull(object arg, string argumentName)
	{
		if (arg == null)
		{
			throw new JikanParameterValidationException("Argument can't be a null.", argumentName);
		}
	}

	internal static void IsLongerThan(string arg, int min, string argumentName)
	{
		if (string.IsNullOrWhiteSpace(arg) || arg.Length <= min)
		{
			throw new JikanParameterValidationException($"Argument must be at least {min} characters long", argumentName);
		}
	}

	internal static void IsGreaterThanZero<T>(T arg, string argumentName) where T : INumber<T>
	{
		if (arg <= T.Zero )
		{
			throw new JikanParameterValidationException("Argument must be greater than 0.", argumentName);
		}
	}

    internal static void IsGreaterOrEqualToZero<T>(T arg, string argumentName) where T : INumber<T>
    {
        if (arg < T.Zero )
        {
            throw new JikanParameterValidationException("Argument must be greater or equal to 0.", argumentName);
        }
    }

	internal static void IsLessOrEqualThan<T>(T arg, T max, string argumentName) where T : INumber<T>
	{
		if (arg > max)
		{
			throw new JikanParameterValidationException($"Argument must be less or equal than {max}.", argumentName);
		}
	}

	internal static void IsValid<T>(Func<T, bool> isValidFunc, T arg, string argumentName, string message = "Argument is not valid.")
	{
		if (!isValidFunc(arg))
		{
			throw new JikanParameterValidationException(message, argumentName);
		}
	}

	internal static void IsValidEnum<TEnum>(TEnum arg, string argumentName) where TEnum : struct, Enum
	{
		if (!FastEnum.IsDefined(arg))
		{
			throw new JikanParameterValidationException("Enum value must be valid", argumentName);
		}
	}

	internal static void IsLetter(char character, string argumentName)
	{
		if (!char.IsLetter(character))
		{
			throw new JikanParameterValidationException("Character must be a letter", argumentName);
		}
	}
}
