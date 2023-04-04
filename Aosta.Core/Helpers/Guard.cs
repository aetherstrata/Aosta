using Aosta.Core.Exceptions;

namespace Aosta.Core.Helpers;

internal static class Guard
{
	internal static void IsNotNullOrWhiteSpace(string arg, string argumentName)
	{
		if (string.IsNullOrWhiteSpace(arg))
		{
			throw new AostaValidationException("Argument can't be null or whitespace.", argumentName);
		}
	}

	internal static void IsNotNull(object arg, string argumentName)
	{
		if (arg == null)
		{
			throw new AostaValidationException("Argument can't be a null.", argumentName);
		}
	}

	internal static void IsLongerThan(string arg, int min, string argumentName)
	{
		if (string.IsNullOrWhiteSpace(arg) || arg.Length <= min)
		{
			throw new AostaValidationException($"Argument must be at least {min} characters long", argumentName);
		}
	}

	internal static void IsGreaterThanZero(long arg, string argumentName)
	{
		if (arg < 1)
		{
			throw new AostaValidationException("Argument must be a natural number greater than 0.", argumentName);
		}
	}
		
	internal static void IsLessOrEqualThan(long arg, long max, string argumentName)
	{
		if (arg > max)
		{
			throw new AostaValidationException($"Argument must not be greater than {max}.", argumentName);
		}
	}

	internal static void IsValid<T>(Func<T, bool> isValidFunc, T arg, string argumentName, string? message = null)
	{
		if (isValidFunc(arg))
		{
			return;
		}

		if (string.IsNullOrWhiteSpace(message))
		{
			message = "Argument is not valid.";
		}

		throw new AostaValidationException(message, argumentName);
	}

	internal static void IsValidEnum<TEnum>(TEnum arg, string argumentName) where TEnum : Enum
	{
		if (!Enum.IsDefined(typeof(TEnum), arg))
		{
			throw new AostaValidationException("Enum value must be valid", argumentName);
		}
	}
		
	internal static void IsLetter(char character, string argumentName)
	{
		if (!char.IsLetter(character))
		{
			throw new AostaValidationException("Character must be a letter", argumentName);
		}
	}
}