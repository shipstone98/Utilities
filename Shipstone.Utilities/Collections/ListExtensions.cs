using System;
using System.Collections.Generic;

namespace Shipstone.Utilities.Collections
{
    /// <summary>
    /// Provides a set of <c>static</c> (<c>Shared</c> in Visual Basic) methods for querying and modifying objects that implement <see cref="IList{T}" /> and <see cref="IReadOnlyList{T}" />.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Shuffles the order of the elements in the specified range in a list.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <c><paramref name="source" /></c>.</typeparam>
        /// <param name="source">A list of values to shuffle.</param>
        /// <param name="index">The zero-based starting index of the range to shuffle.</param>
        /// <param name="count">The number of elements in the range to shuffle.</param>
        /// <param name="random">The <see cref="Random" /> implementation to use when shuffling elements, or <c>null</c>.</param>
        /// <exception cref="ArgumentException"><c><paramref name="index" /></c> and <c><paramref name="count" /></c> do not denote a valid range of elements in <c><paramref name="source" /></c>.</exception>
        /// <exception cref="ArgumentNullException"><c><paramref name="source" /></c> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><c><paramref name="index" /></c> is less than 0 (zero) -or- <c><paramref name="count" /></c> is less than 0 (zero).</exception>
        public static void Shuffle<T>(
            this IList<T> source,
            int index,
            int count,
            Random random = null
        )
        {
            if (source is null)
            {
                throw new ArgumentNullException(
                    nameof (source),
                    $"{nameof (source)} is null."
                );
            }

            if (index < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof (index),
                    $"{nameof (index)} is less than 0 (zero)."
                );
            }

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof (count),
                    $"{nameof (count)} is less than 0 (zero)."
                );
            }

            if (index + count > source.Count)
            {
                throw new ArgumentException(
                    $"{nameof (index)} and {nameof (count)} do not denote a valid range of elements in {nameof (source)}.",
                    nameof (count)
                );
            }
            
            if (random is null)
            {
                random = Internals._Random;
            }

            for (int i = 0; i < count - 1; i ++)
            {
                int j = random.Next(0, i);
                ListExtensions.Swap(source, index + i, index + j);
            }
        }

        private static void Swap<T>(IList<T> source, int indexA, int indexB)
        {
            T temp = source[indexA];
            source[indexA] = source[indexB];
            source[indexB] = temp;
        }
    }
}
