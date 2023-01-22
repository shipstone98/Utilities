using System;

namespace Shipstone.Utilities.Collections
{
    /// <summary>
    /// Provides a set of <c>static</c> (<c>Shared</c> in Visual Basic) methods for querying objects that implement <see cref="IReadOnlyPaginatedList{T}" />.
    /// </summary>
    public static class PaginatedListExtensions
    {
        /// <summary>
        /// Creates a new <see cref="PaginatedList{T}" /> that contains elements copied from a specified <see cref="IReadOnlyPaginatedList{T}" />.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <c><paramref name="source" /></c>.</typeparam>
        /// <typeparam name="TOutput">The type of the elements of the new <see cref="PaginatedList{T}" />.</typeparam>
        /// <param name="source">The <see cref="IReadOnlyPaginatedList{T}" /> to copy elements from.</param>
        /// <param name="converter">The conversion function to use when copying elements.</param>
        /// <returns>A <see cref="PaginatedList{T}" /> that contains elements copied from the specified <c><paramref name="source" /></c>.</returns>
        /// <exception cref="ArgumentNullException"><c><paramref name="source" /></c> is <c>null</c> -or- <c><paramref name="converter" /></c> is <c>null</c>.</exception>
        public static PaginatedList<TOutput> ToPaginatedList<T, TOutput>(
            this IReadOnlyPaginatedList<T> source,
            Func<T, TOutput> converter
        )
        {
            if (source is null)
            {
                throw new ArgumentNullException(
                    nameof (source),
                    $"{nameof (source)} is null."
                );
            }

            if (converter is null)
            {
                throw new ArgumentNullException(
                    nameof (converter),
                    $"{nameof (converter)} is null."
                );
            }

            TOutput[] output = new TOutput[source.Count];

            for (int i = 0; i < output.Length; i ++)
            {
                output[i] = converter(source[i]);
            }

            return new PaginatedList<TOutput>(
                output,
                source.PageIndex,
                source.PageCount,
                source.TotalCount
            );
        }
    }
}
