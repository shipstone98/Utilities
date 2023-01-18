using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Shipstone.Utilities.Collections;

namespace Shipstone.UtilitiesTest.Collections
{
    [TestClass]
    public class QueryableExtensionsTest
    {
#region ToPaginatedList method
#region Invalid parameters
        [TestMethod]
        public void TestToPaginatedList_Invalid_CountEqualToZero_SourceNotEmpty()
        {
            // Arrange
            int[] array = Internals.CreateArray(1);
            IQueryable<int> query = array.AsQueryable();

            // Act
            Exception ex = Assert.ThrowsException<ArgumentException>(() =>
                QueryableExtensions.ToPaginatedList(query, 0, 0));

            // Assert
            Assert.AreEqual(
                "source is not empty and maxCount is equal to 0 (zero).",
                ex.Message
            );
        }

        [TestMethod]
        public void TestToPaginatedList_Invalid_CountLessThanZero()
        {
            // Arrange
            IEnumerable<int> counts = new int[] { Int32.MinValue, -1 };
            IQueryable<int> query = counts.AsQueryable();

            foreach (int count in counts)
            {
                // Act
                ArgumentException ex =
                    Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                        QueryableExtensions.ToPaginatedList(query, 0, count));

                // Assert
                Assert.AreEqual("maxCount", ex.ParamName);
            }
        }

        [TestMethod]
        public void TestToPaginatedList_Invalid_IndexLessThanZero()
        {
            // Arrange
            IEnumerable<int> indices = new int[] { Int32.MinValue, -1 };
            IQueryable<int> query = indices.AsQueryable();

            foreach (int index in indices)
            {
                // Act
                ArgumentException ex =
                    Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                        QueryableExtensions.ToPaginatedList(query, index, 0));

                // Assert
                Assert.AreEqual("pageIndex", ex.ParamName);
            }
        }

        [TestMethod]
        public void TestToPaginatedList_Invalid_InvalidRange()
        {
            // Arrange
            const int COUNT = 10;
            int[] array = Internals.CreateArray(COUNT);
            IQueryable<int> query = array.AsQueryable();
            ISet<Tuple<int, int>> indices = new HashSet<Tuple<int, int>>();

            for (int i = 0; i < COUNT; i ++)
            {
                indices.Add(new(i + 1, COUNT - i));
            }

            foreach (Tuple<int, int> index in indices)
            {
                // Act
                Exception ex = Assert.ThrowsException<ArgumentException>(() =>
                    QueryableExtensions.ToPaginatedList(
                        query,
                        index.Item1,
                        index.Item2
                    ));

                // Assert
                Assert.AreEqual(
                    "pageIndex and maxCount do not denote a valid range of elements in source.",
                    ex.Message
                );
            }
        }

        [TestMethod]
        public void TestToPaginatedList_Invalid_SourceNull()
        {
            // Act
            ArgumentException ex =
                Assert.ThrowsException<ArgumentNullException>(() =>
                    QueryableExtensions.ToPaginatedList<Object>(null, 0, 0));

            // Assert
            Assert.AreEqual("source", ex.ParamName);
        }
#endregion

#region Valid parameters
        [TestMethod]
        public void TestToPaginatedList_Valid_FirstPage_ContainsEqualToCount()
        {
            // Arrange
            const int INDEX = 0;
            const int COUNT = 10;
            int[] array = Internals.CreateArray(COUNT);
            IQueryable<int> query = array.AsQueryable();

            // Act
            PaginatedList<int> list =
                QueryableExtensions.ToPaginatedList(query, INDEX, COUNT);

            // Assert
            list.AssertEqual(
                array,
                0,
                COUNT,
                INDEX,
                1,
                COUNT,
                false,
                false
            );
        }

        [TestMethod]
        public void TestToPaginatedList_Valid_FirstPage_ContainsLessThanCount()
        {
            // Arrange
            const int INDEX = 0;
            const int COUNT = 10;
            int[] array = Internals.CreateArray(COUNT - 1);
            IQueryable<int> query = array.AsQueryable();

            // Act
            PaginatedList<int> list =
                QueryableExtensions.ToPaginatedList(query, INDEX, COUNT);

            // Assert
            list.AssertEqual(
                array,
                0,
                COUNT - 1,
                INDEX,
                1,
                COUNT - 1,
                false,
                false
            );
        }

        [TestMethod]
        public void TestToPaginatedList_Valid_FirstPage_ContainsMoreThanCount()
        {
            // Arrange
            const int INDEX = 0;
            const int COUNT = 10;
            int[] array = Internals.CreateArray(COUNT + 1);
            IQueryable<int> query = array.AsQueryable();

            // Act
            PaginatedList<int> list =
                QueryableExtensions.ToPaginatedList(query, INDEX, COUNT);

            // Assert
            list.AssertEqual(
                array,
                0,
                COUNT,
                INDEX,
                2,
                COUNT + 1,
                false,
                true
            );
        }

        [TestMethod]
        public void TestToPaginatedList_Valid_LastPage_ContainsEqualToCount()
        {
            // Arrange
            const int INDEX = 1;
            const int COUNT = 10;
            int[] array = Internals.CreateArray(COUNT * 2);
            IQueryable<int> query = array.AsQueryable();

            // Act
            PaginatedList<int> list =
                QueryableExtensions.ToPaginatedList(query, INDEX, COUNT);

            // Assert
            list.AssertEqual(
                array,
                COUNT,
                COUNT,
                INDEX,
                2,
                COUNT * 2,
                true,
                false
            );
        }

        [TestMethod]
        public void TestToPaginatedList_Valid_LastPage_ContainsLessThanCount()
        {
            // Arrange
            const int INDEX = 2;
            const int COUNT = 10;
            int[] array = Internals.CreateArray(COUNT * 3 - 1);
            IQueryable<int> query = array.AsQueryable();

            // Act
            PaginatedList<int> list =
                QueryableExtensions.ToPaginatedList(query, INDEX, COUNT);

            // Assert
            list.AssertEqual(
                array,
                COUNT * 2,
                COUNT - 1,
                INDEX,
                3,
                COUNT * 3 - 1,
                true,
                false
            );
        }

        [TestMethod]
        public void TestToPaginatedList_Valid_MiddlePage()
        {
            // Arrange
            const int INDEX = 1;
            const int COUNT = 10;
            int[] array = Internals.CreateArray(COUNT * 3);
            IQueryable<int> query = array.AsQueryable();

            // Act
            PaginatedList<int> list =
                QueryableExtensions.ToPaginatedList(query, INDEX, COUNT);

            // Assert
            list.AssertEqual(
                array,
                COUNT,
                COUNT,
                INDEX,
                3,
                COUNT * 3,
                true,
                true
            );
        }
#endregion
#endregion
    }
}
