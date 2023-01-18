using System.Collections.Generic;

namespace Shipstone.Utilities.Collections
{
    /// <summary>
    /// Represents a read-only, paginated collection of elements that can be accessed by index.
    /// </summary>
    /// <typeparam name="T">The type of elements in the paginated list.</typeparam>
    public interface IReadOnlyPaginatedList<out T> : IReadOnlyList<T>
    {
        /// <summary>
        /// Gets a value indicating whether the list contains data on the previous page.
        /// </summary>
        /// <value><c>true</c> if the list contains data on the previous page; otherwise, <c>false</c>.</value>
        bool HasPreviousPage { get; }

        /// <summary>
        /// Gets a value indicating whether the list contains data on the next page.
        /// </summary>
        /// <value><c>true</c> if the list contains data on the next page; otherwise, <c>false</c>.</value>
        bool HasNextPage { get; }

        /// <summary>
        /// Gets the number of pages in the list.
        /// </summary>
        /// <value>The number of pages in the list.</value>
        int PageCount { get; }

        /// <summary>
        /// Gets the zero-based index of the current page in the list.
        /// </summary>
        /// <value>The zero-based index of the current page in the list.</value>
        int PageIndex { get; }

        /// <summary>
        /// Gets the total number of elements in the list.
        /// </summary>
        /// <value>The total number of elements in the list.</value>
        int TotalCount { get; }
    }
}
