using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Shipstone.Utilities.Collections;

namespace Shipstone.UtilitiesTest.Collections
{
    [TestClass]
    public class ListExtensionsTest
    {
        [TestMethod]
        public void TestShuffle_Invalid_CountLessThanZero()
        {
            // Arrange
            int[] array = Array.Empty<int>();
            IEnumerable<int> counts = new int[] { Int32.MinValue, -1 };

            foreach (int count in counts)
            {
                // Act
                ArgumentException ex =
                    Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                        ListExtensions.Shuffle(array, 0, count));

                // Assert
                Assert.AreEqual(
                    "count is less than 0 (zero). (Parameter 'count')",
                    ex.Message
                );

                Assert.AreEqual("count", ex.ParamName);
            }

            Assert.AreEqual(0, array.Length);
        }

        [TestMethod]
        public void TestShuffle_Invalid_IndexLessThanZero()
        {
            // Arrange
            int[] array = Array.Empty<int>();
            IEnumerable<int> indices = new int[] { Int32.MinValue, -1 };

            foreach (int index in indices)
            {
                // Act
                ArgumentException ex =
                    Assert.ThrowsException<ArgumentOutOfRangeException>(() =>
                        ListExtensions.Shuffle(array, index, 0));

                // Assert
                Assert.AreEqual(
                    "index is less than 0 (zero). (Parameter 'index')",
                    ex.Message
                );

                Assert.AreEqual("index", ex.ParamName);
            }

            Assert.AreEqual(0, array.Length);
        }

        [TestMethod]
        public void TestShuffle_Invalid_InvalidRange()
        {
            const int ARRAY_LENGTH = 10;
            int[] array = Internals.CreateArray(ARRAY_LENGTH);
            int[] copy = new int[ARRAY_LENGTH];
            Array.Copy(array, copy, ARRAY_LENGTH);
            int index = 0, count = ARRAY_LENGTH + 1;

            while (count >= 0)
            {
                // Act
                ArgumentException ex =
                    Assert.ThrowsException<ArgumentException>(() =>
                        ListExtensions.Shuffle(array, index, count));

                // Assert
                Assert.AreEqual(
                    "index and count do not denote a valid range of elements in source. (Parameter 'count')",
                    ex.Message
                );

                Assert.AreEqual("count", ex.ParamName);
                ++ index;
                -- count;
            }

            for (int i = 0; i < ARRAY_LENGTH; i ++)
            {
                Assert.AreEqual(copy[i], array[i]);
            }
        }

        [TestMethod]
        public void TestShuffle_Invalid_SourceNull()
        {
            // Act
            ArgumentException ex =
                Assert.ThrowsException<ArgumentNullException>(() =>
                    ListExtensions.Shuffle(null as IList<Object>, 0, 0));

            // Assert
            Assert.AreEqual(
                "source is null. (Parameter 'source')",
                ex.Message
            );

            Assert.AreEqual("source", ex.ParamName);
        }

        [TestMethod]
        public void TestShuffle_Valid_Empty()
        {
            // Arrange
            int[] array = Array.Empty<int>();

            // Act
            ListExtensions.Shuffle(array, 0, 0);

            // Assert
            Assert.AreEqual(0, array.Length);
        }

        [TestMethod]
        public void TestShuffle_Valid_RandomNotNull()
        {
            // Arrange
            const int ARRAY_LENGTH = 10;
            const int INDEX = 1;
            const int COUNT = ARRAY_LENGTH - 2;
            const int SEED = 100;
            int[] arrayA = Internals.CreateArray(ARRAY_LENGTH);
            int[] arrayB = new int[ARRAY_LENGTH];
            Array.Copy(arrayA, arrayB, ARRAY_LENGTH);
            ISet<int> remainingA = new HashSet<int>(arrayA);
            ISet<int> remainingB = new HashSet<int>(arrayB);
            remainingA.Remove(1);
            remainingA.Remove(1);
            remainingB.Remove(ARRAY_LENGTH);
            remainingB.Remove(ARRAY_LENGTH);
            Random randomA = new(SEED);
            Random randomB = new(SEED);

            // Act
            ListExtensions.Shuffle(arrayA, INDEX, COUNT, randomA);
            ListExtensions.Shuffle(arrayB, INDEX, COUNT, randomB);

            // Assert
            Assert.AreEqual(1, arrayA[0]);
            Assert.AreEqual(1, arrayB[0]);
            Assert.AreEqual(ARRAY_LENGTH, arrayA[^1]);
            Assert.AreEqual(ARRAY_LENGTH, arrayB[^1]);

            for (int i = 0; i < COUNT; i ++)
            {
                Assert.IsTrue(remainingA.Remove(arrayA[INDEX + i]));
                Assert.IsTrue(remainingB.Remove(arrayB[INDEX + i]));
            }

            for (int i = 0; i < ARRAY_LENGTH; i ++)
            {
                Assert.IsTrue(arrayA[i] == arrayB[i]);
            }
        }

        [TestMethod]
        public void TestShuffle_Valid_RandomNull()
        {
            // Arrange
            const int ARRAY_LENGTH = 10;
            const int INDEX = 1;
            const int COUNT = ARRAY_LENGTH - 2;
            int[] array = Internals.CreateArray(ARRAY_LENGTH);
            ISet<int> remaining = new HashSet<int>(array);
            remaining.Remove(1);
            remaining.Remove(ARRAY_LENGTH);

            // Act
            ListExtensions.Shuffle(array, INDEX, COUNT);

            // Assert
            Assert.AreEqual(1, array[0]);
            Assert.AreEqual(ARRAY_LENGTH, array[^1]);

            for (int i = 0; i < COUNT; i ++)
            {
                Assert.IsTrue(remaining.Remove(array[INDEX + i]));
            }
        }
    }
}
