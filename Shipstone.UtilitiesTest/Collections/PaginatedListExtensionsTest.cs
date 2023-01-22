using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Shipstone.Utilities.Collections;

namespace Shipstone.UtilitiesTest.Collections
{
    [TestClass]
    public class PaginatedListExtensionsTest
    {
#region ToPaginatedList method
        [TestMethod]
        public void TestToPaginatedList_Invalid_ConverterNull()
        {
            // Arrange
            const int COUNT = 100;
            int[] array = Internals.CreateArray(COUNT);
            IQueryable<int> query = array.AsQueryable();
            IReadOnlyPaginatedList<int> source = query.ToPaginatedList(0, 10);

            // Act
            ArgumentException ex =
                Assert.ThrowsException<ArgumentNullException>(() =>
                    PaginatedListExtensions.ToPaginatedList<int, String>(
                        source,
                        null!
                    ));

            // Assert
            Assert.AreEqual(
                "converter is null. (Parameter 'converter')",
                ex.Message
            );

            Assert.AreEqual("converter", ex.ParamName);
        }

        [TestMethod]
        public void TestToPaginatedList_Invalid_SourceNull()
        {
            // Arrange
            Func<String, int> converter = Int32.Parse;

            // Act
            ArgumentException ex =
                Assert.ThrowsException<ArgumentNullException>(() =>
                    PaginatedListExtensions.ToPaginatedList(null!, converter));

            // Assert
            Assert.AreEqual(
                "source is null. (Parameter 'source')",
                ex.Message
            );

            Assert.AreEqual("source", ex.ParamName);
        }

        [TestMethod]
        public void TestToPaginatedList_Valid_EqualTypes()
        {
            // Arrange
            const int INDEX = 1, COUNT = 10;
            int[] array = Internals.CreateArray(COUNT * COUNT);
            IQueryable<int> query = array.AsQueryable();

            IReadOnlyPaginatedList<int> source =
                query.ToPaginatedList(INDEX, COUNT);

            Func<int, int> converter = i => i;
            int[] result = new int[COUNT];

            for (int i = 0; i < COUNT; i ++)
            {
                result[i] = converter(array[INDEX * COUNT + i]);
            }

            // Act
            PaginatedList<int> list = source.ToPaginatedList(converter);

            // Assert
            list.AssertEqual(
                result,
                0,
                COUNT,
                INDEX,
                COUNT,
                COUNT * COUNT,
                true,
                true
            );
        }

        [TestMethod]
        public void TestToPaginatedList_Valid_SourceEmpty()
        {
            // Arrange
            int[] array = Array.Empty<int>();
            IQueryable<int> query = array.AsQueryable();
            IReadOnlyPaginatedList<int> source = query.ToPaginatedList(0, 0);
            Func<int, String> converter = i => i.ToString();
            String[] result = Array.Empty<String>();

            // Act
            PaginatedList<String> list = source.ToPaginatedList(converter);

            // Assert
            list.AssertEqual(result, 0, 0, 0, 1, 0, false, false);
        }

        [TestMethod]
        public void TestToPaginatedList_Valid_SourceNotEmpty()
        {
            // Arrange
            const int INDEX = 1, COUNT = 10;
            int[] array = Internals.CreateArray(COUNT * COUNT);
            IQueryable<int> query = array.AsQueryable();

            IReadOnlyPaginatedList<int> source =
                query.ToPaginatedList(INDEX, COUNT);

            Func<int, String> converter = i => i.ToString();
            String[] result = new String[COUNT];

            for (int i = 0; i < COUNT; i ++)
            {
                result[i] = converter(array[INDEX * COUNT + i]);
            }

            // Act
            PaginatedList<String> list = source.ToPaginatedList(converter);

            // Assert
            list.AssertEqual(
                result,
                0,
                COUNT,
                INDEX,
                COUNT,
                COUNT * COUNT,
                true,
                true
            );
        }
#endregion
    }
}
