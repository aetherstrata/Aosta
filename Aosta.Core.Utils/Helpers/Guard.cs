using Aosta.Core.Utils.Exceptions;
using FastEnumUtility;

namespace Aosta.Core.Utils.Helpers;

public static class Guard
{
	public static void IsNotNullOrWhiteSpace(string arg, string argumentName)
	{
		if (string.IsNullOrWhiteSpace(arg))
		{
			throw new ParameterValidationException("Argument can't be null or whitespace.", argumentName);
		}
	}

	public static void IsNotNull(object arg, string argumentName)
	{
		if (arg == null)
		{
			throw new ParameterValidationException("Argument can't be a null.", argumentName);
		}
	}

	public static void IsLongerThan(string arg, int min, string argumentName)
	{
		if (string.IsNullOrWhiteSpace(arg) || arg.Length <= min)
		{
			throw new ParameterValidationException($"Argument must be at least {min} characters long", argumentName);
		}
	}

	public static void IsGreaterThanZero(long arg, string argumentName)
	{
		if (arg < 1)
		{
			throw new ParameterValidationException("Argument must be a natural number greater than 0.", argumentName);
		}
	}
		
	public static void IsLessOrEqualThan(long arg, long max, string argumentName)
	{
		if (arg > max)
		{
			throw new ParameterValidationException($"Argument must not be greater than {max}.", argumentName);
		}
	}

	public static void IsValid<T>(Func<T, bool> isValidFunc, T arg, string argumentName, string? message = null)
	{
		if (isValidFunc(arg))
		{
			return;
		}

		if (string.IsNullOrWhiteSpace(message))
		{
			message = "Argument is not valid.";
		}

		throw new ParameterValidationException(message, argumentName);
	}

	public static void IsValidEnum<TEnum>(TEnum arg, string argumentName) where TEnum : Enum
	{
		if (!Enum.IsDefined(typeof(TEnum), arg))
		{
			throw new ParameterValidationException("Enum value must be valid", argumentName);
		}
	}
		
	public static void IsLetter(char character, string argumentName)
	{
		if (!char.IsLetter(character))
		{
			throw new ParameterValidationException("Character must be a letter", argumentName);
		}
	}
}