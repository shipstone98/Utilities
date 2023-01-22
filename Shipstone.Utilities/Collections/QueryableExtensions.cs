using System;
using System.Linq;

namespace Shipstone.Utilities.Collections
{
    /// <summary>
    /// Provides a set of <c>static</c> (<c>Shared</c> in Visual Basic) methods for querying objects that implement <see cref="IQueryable{T}" />.
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// Creates a <see cref="PaginatedList{T}" /> from a page in the specified <see cref="IQueryable{T}" />.
        /// </summary>
        /// <typeparam name="T">The type of the elements of <c><paramref name="source" /></c>.</typeparam>
        /// <param name="source">The <see cref="IQueryable{T}" /> to create a <see cref="PaginatedList{T}" /> from.</param>
        /// <param name="pageIndex">The zero-based index of the page in <c><paramref name="source" /></c> to copy elements from.</param>
        /// <param name="maxCount">The maximum number of elements to copy from <c><paramref name="source" /></c> (i.e. the number of elements on each page).</param>
        /// <returns>A <see cref="PaginatedList{T}" /> that contains elements copied from the specified page in <c><paramref name="source" /></c>.</returns>
        /// <exception cref="ArgumentException"><c><paramref name="pageIndex" /></c> and <c><paramref name="maxCount" /></c> do not denote a valid range of elements in <c><paramref name="source" /></c> -or- <c><paramref name="source" /></c> is not empty and <c><paramref name="maxCount" /></c> is equal to 0 (zero).</exception>
        /// <exception cref="ArgumentNullException"><c><paramref name="source" /></c> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><c><paramref name="pageIndex" /></c> is less than 0 (zero) -or- <c><paramref name="maxCount" /></c> is less than 0 (zero).</exception>
        public static PaginatedList<T> ToPaginatedList<T>(
            this IQueryable<T> source,
            int pageIndex,
            int maxCount
        )
        {
            if (source is null)
            {
                throw new ArgumentNullException(
                    nameof (source),
                    $"{nameof (source)} is null."
                );
            }

            if (pageIndex < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof (pageIndex),
                    $"{nameof (pageIndex)} is less than 0 (zero)."
                );
            }

            if (maxCount < 0)
            {
                throw new ArgumentOutOfRangeException(
                    nameof (maxCount),
                    $"{nameof (maxCount)} is less than 0 (zero)."
                );
            }

            int totalCount = source.Count();
            int pageCount;
            T[] array;
            int toSkip = pageIndex * maxCount;

            if (totalCount == 0)
            {
                if (pageIndex > 0)
                {
                    throw new ArgumentException(
                        $"{nameof (source)} is empty and {nameof (pageIndex)} is greater than 0 (zero).",
                        "pageIndex"
                    );
                }

                array = Array.Empty<T>();
                pageCount = 1;
            }

            else
            {
                if (maxCount == 0)
                {
                    throw new ArgumentException(
                        $"{nameof (source)} is not empty and {nameof (maxCount)} is equal to 0 (zero).",
                        "maxCount"
                    );
                }

                if (toSkip >= totalCount)
                {
                    throw new ArgumentException(
                        $"{nameof (pageIndex)} and {nameof (maxCount)} do not denote a valid range of elements in {nameof (source)}.",
                        "maxCount"
                    );
                }

                array = source
                    .Skip(toSkip)
                    .Take(maxCount)
                    .ToArray();

                pageCount = (int) Math.Ceiling(totalCount / (double) maxCount);
            }

            return new PaginatedList<T>(
                array,
                pageIndex,
                pageCount,
                totalCount
            );
        }
    }
}
