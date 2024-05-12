using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.InteropServices;

namespace BearsEngine;

public static class Ensure
{
    /// <summary>
    /// Ensures that the specified argument is greater than zero.
    /// </summary>
    /// <param name="argument">The argument.</param>
    /// <param name="argumentName">Name of the argument.</param>
    /// <exception cref="ArgumentNullException"></exception>
    [DebuggerStepThrough]
    public static void ArgumentPositive(float argument, string argumentName)
    {
        if (argument <= 0)
        {
            throw new ArgumentOutOfRangeException(argumentName);
        }
    }

    /// <summary>
    /// Ensures that the specified argument is not less than zero.
    /// </summary>
    /// <param name="argument">The argument.</param>
    /// <param name="argumentName">Name of the argument.</param>
    /// <exception cref="ArgumentNullException"></exception>
    [DebuggerStepThrough]
    public static void ArgumentNotNegative(float argument, string argumentName)
    {
        if (argument < 0)
        {
            throw new ArgumentOutOfRangeException(argumentName);
        }
    }

    /// <summary>
    /// Ensures that the specified argument is within the requested range.
    /// </summary>
    /// <param name="argument">The argument.</param>
    /// <param name="argumentName">Name of the argument.</param>
    /// <param name="minValue">The minimum accepted value. Equal this is OK.</param>
    /// <param name="maxValue">The maximum value. Equal this is not OK.</param>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    [DebuggerStepThrough]
    public static void ArgumentInRange(float argument, string argumentName, float minValue, float maxValue)
    {
        if (argument < minValue || argument >= maxValue)
        {
            throw new ArgumentOutOfRangeException(argumentName);
        }
    }

    /// <summary>
    /// Ensures that the specified argument is not null.
    /// </summary>
    /// <param name="argument">The argument.</param>
    /// <param name="argumentName">Name of the argument.</param>
    /// <exception cref="ArgumentNullException"></exception>
    [DebuggerStepThrough]
    public static void ArgumentNotNull([NotNull]object? argument, string argumentName)
    {
        if (argument is null)
        {
            throw new ArgumentNullException(argumentName);
        }
    }

    /// <summary>
    /// Ensures that the specified string argument is not null or empty.
    /// </summary>
    /// <param name="argument">The argument.</param>
    /// <param name="argumentName">Name of the argument.</param>
    /// <exception cref="ArgumentNullException"></exception>
    [DebuggerStepThrough]
    public static void ArgumentNotNullOrEmpty([NotNull]string? argument, string argumentName)
    {
        if (string.IsNullOrEmpty(argument))
        {
            throw new ArgumentException("Argument is null or empty", argumentName);
        }
    }

    /// <summary>
    /// Ensures that the specified collection is not null or empty.
    /// </summary>
    /// <typeparam name="T">The type of element in the collection.</typeparam>
    /// <param name="collection">The collection.</param>
    /// <exception cref="InvalidOperationException"></exception>
    [DebuggerStepThrough]
    public static void ArgumentCollectionNotNullOrEmpty<T>(IEnumerable<T> collection, string argumentName)
    {
        if (collection is null)
        {
            throw new ArgumentNullException(argumentName);
        }

        if (!collection.Any())
        {
            throw new ArgumentException("Collection was empty", argumentName);
        }
    }

    /// <summary>
    /// Ensures that the specified collection is not null or empty.
    /// </summary>
    /// <typeparam name="T">The type of element in the collection.</typeparam>
    /// <param name="collection">The collection.</param>
    /// <exception cref="InvalidOperationException"></exception>
    [DebuggerStepThrough]
    public static void CollectionNotNullOrEmpty<T>([NotNull] IEnumerable<T> collection)
    {
        if (collection is null)
        {
            throw new NullReferenceException();
        }

        if (!collection.Any())
        {
            throw new InvalidOperationException();
        }
    }

    /// <summary>
    /// Ensures that the specified key is available in the specified dictionary
    /// </summary>
    /// <param name="dictionary">The dictionary</param>
    /// <param name="key">The key</param>
    /// <exception cref="KeyNotFoundException"></exception>
    [DebuggerStepThrough]
    public static void DictionaryKeyExists<TKey,TValue>(IDictionary<TKey,TValue> dictionary, TKey key)
    {
        NotNull(dictionary);
        NotNull(key);

        if (!dictionary.ContainsKey(key))
            throw new KeyNotFoundException(key!.ToString());
    }

    /// <summary>
    /// Ensures the specified file exists
    /// </summary>
    /// <param name="fileName">The file path, including the extension.</param>
    [DebuggerStepThrough]
    public static void FileExists(string fileName)
    {
        if (!File.Exists(fileName))
            throw new FileNotFoundException(fileName);
    }

    /// <summary>
    /// Ensures that the specified value is not null.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <exception cref="NullReferenceException"></exception>
    [DebuggerStepThrough]
    public static void NotNull([NotNull]object? value)
    {
        if (value is null)
        {
            throw new NullReferenceException();
        }
    }

    /// <summary>
    /// Tests a value to ensure it's within a permitted range
    /// </summary>
    /// <param name="value"></param>
    /// <param name="min">The minimum value acceptable (inclusively).</param>
    /// <param name="max">The maximum value acceptable (inclusively).</param>
    /// <exception cref="InvalidOperationException"></exception>
    [DebuggerStepThrough]
    public static void IsInRange(int value, int minAllowed, int maxAllowed)
    {
        if (value < minAllowed || value > maxAllowed)
        {
            throw new InvalidOperationException($"Value {value} was out of accepted range ({minAllowed}-{maxAllowed})");
        }
    }

    /// <summary>
    /// Tests a value to ensure it's within a permitted range
    /// </summary>
    /// <param name="value"></param>
    /// <param name="min">The minimum value acceptable (inclusively).</param>
    /// <param name="max">The maximum value acceptable (inclusively).</param>
    /// <exception cref="InvalidOperationException"></exception>
    [DebuggerStepThrough]
    public static void IsInRange(float value, float minAllowed, float maxAllowed)
    {
        if (value < minAllowed || value > maxAllowed)
        {
            throw new InvalidOperationException($"Value {value} was out of accepted range ({minAllowed}-{maxAllowed})");
        }
    }

    /// <summary>
    /// Ensures that the specified value is null.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <exception cref="NullReferenceException"></exception>
    [DebuggerStepThrough]
    public static void IsNull(object? value)
    {
        if (value is not null)
        {
            throw new InvalidOperationException();
        }
    }

    /// <summary>
    /// Ensures that the specified value or expression is true
    /// </summary>
    [DebuggerStepThrough]
    public static void That(bool value)
    {
        if (!value)
        {
            throw new InvalidOperationException();
        }
    }
}
