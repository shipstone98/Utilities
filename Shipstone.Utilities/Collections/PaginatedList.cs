using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Shipstone.Utilities.Collections
{
    /// <summary>
    /// Represents a read-only, paginated collection of elements that can be accessed by index.
    /// </summary>
    /// <typeparam name="T">The type of elements in the paginated list.</typeparam>
    public class PaginatedList<T>
        : ReadOnlyCollection<T>, IReadOnlyPaginatedList<T>
    {
        private readonly int _PageCount;
        private readonly int _PageIndex;
        private readonly int _TotalCount;

        /// <summary>
        /// Gets a value indicating whether the <see cref="PaginatedList{T}" /> contains data on the previous page.
        /// </summary>
        /// <value><c>true</c> if the <see cref="PaginatedList{T}" /> contains data on the previous page; otherwise, <c>false</c>.</value>
        public bool HasNextPage => this._PageIndex < this._PageCount - 1;

        /// <summary>
        /// Gets a value indicating whether the <see cref="PaginatedList{T}" /> contains data on the next page.
        /// </summary>
        /// <value><c>true</c> if the <see cref="PaginatedList{T}" /> contains data on the next page; otherwise, <c>false</c>.</value>
        public bool HasPreviousPage => this._PageIndex > 0;

        /// <summary>
        /// Gets the number of pages in the <see cref="PaginatedList{T}" />.
        /// </summary>
        /// <value>The number of pages in the <see cref="PaginatedList{T}" />.</value>
        public int PageCount => this._PageCount;

        /// <summary>
        /// Gets the zero-based index of the current page in the <see cref="PaginatedList{T}" />.
        /// </summary>
        /// <value>The zero-based index of the current page in the <see cref="PaginatedList{T}" />.</value>
        public int PageIndex => this._PageIndex;

        /// <summary>
        /// Gets the total number of elements in the <see cref="PaginatedList{T}" />.
        /// </summary>
        /// <value>The total number of elements in the <see cref="PaginatedList{T}" />.</value>
        public int TotalCount => this._TotalCount;

        internal PaginatedList(
            IList<T> list,
            int pageIndex,
            int pageCount,
            int totalCount
        ) : base(list)
        {
            this._PageCount = pageCount;
            this._PageIndex = pageIndex;
            this._TotalCount = totalCount;
        }
    }
}
