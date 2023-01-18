using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Shipstone.Utilities.Collections;

namespace Shipstone.UtilitiesTest
{
    internal static class Internals
    {
        internal static void AssertEqual<T>(
            this PaginatedList<T> list,
            IReadOnlyList<T> source,
            int sourceIndex,
            int count,
            int pageIndex,
            int pageCount,
            int totalCount,
            bool hasPreviousPage,
            bool hasNextPage
        )
        {
            for (int i = 0; i < count; i ++)
            {
                Assert.AreEqual(source[sourceIndex + i], list[i]);
            }

            Assert.AreEqual(count, list.Count);
            Assert.AreEqual(hasNextPage, list.HasNextPage);
            Assert.AreEqual(hasPreviousPage, list.HasPreviousPage);
            Assert.AreEqual(pageCount, list.PageCount);
            Assert.AreEqual(pageIndex, list.PageIndex);
            Assert.AreEqual(totalCount, list.TotalCount);
        }

        internal static int[] CreateArray(int count)
        {
            int[] array = new int[count];

            for (int i = 0; i < count; i ++)
            {
                array[i] = i + 1;
            }

            return array;
        }
    }
}
