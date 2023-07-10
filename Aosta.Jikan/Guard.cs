using FastEnumUtility;

namespace Aosta.Jikan;

public static class Guard
{
	public static void IsDefaultEndpoint(string? endpoint, string methodName)
	{
		if (endpoint?.Equals(JikanConfiguration.DefaultEndpoint) ?? true)
		{
			throw new NotSupportedException($"Operation {methodName} is not supported on the default endpoint.");
		}
	}

	public static void IsNotNullOrWhiteSpace(string arg, string argumentName)
	{
		if (string.IsNullOrWhiteSpace(arg))
		{
			throw new JikanParameterValidationException("Argument can't be null or whitespace.", argumentName);
		}
	}

	public static void IsNotNull(object arg, string argumentName)
	{
		if (arg == null)
		{
			throw new JikanParameterValidationException("Argument can't be a null.", argumentName);
		}
	}

	public static void IsLongerThan(string arg, int min, string argumentName)
	{
		if (string.IsNullOrWhiteSpace(arg) || arg.Length <= min)
		{
			throw new JikanParameterValidationException($"Argument must be at least {min} characters long", argumentName);
		}
	}

	public static void IsGreaterThanZero(long arg, string argumentName)
	{
		if (arg < 1)
		{
			throw new JikanParameterValidationException("Argument must be a natural number greater than 0.", argumentName);
		}
	}
		
	public static void IsLessOrEqualThan(long arg, long max, string argumentName)
	{
		if (arg > max)
		{
			throw new JikanParameterValidationException($"Argument must not be greater than {max}.", argumentName);
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

		throw new JikanParameterValidationException(message, argumentName);
	}

	public static void IsValidEnum<TEnum>(TEnum arg, string argumentName) where TEnum : struct, Enum
	{
		if (!FastEnum.IsDefined(arg))
		{
			throw new JikanParameterValidationException("Enum value must be valid", argumentName);
		}
	}
		
	public static void IsLetter(char character, string argumentName)
	{
		if (!char.IsLetter(character))
		{
			throw new JikanParameterValidationException("Character must be a letter", argumentName);
		}
	}
}