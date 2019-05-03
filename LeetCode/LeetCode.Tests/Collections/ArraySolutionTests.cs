using LeetCode.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LeetCode.Tests.Collections
{
    public class ArraySolutionTests
    {
        [Theory]
        [InlineData(new[] { 1, 1, 2 }, 2, new[] { 1, 2 })]
        [InlineData(new[] { 0, 0, 1, 1, 1, 2, 2, 3, 3, 4 }, 5, new[] { 0, 1, 2, 3, 4 })]
        [InlineData(new[] { 1, 2, 3 }, 3, new[] { 1, 2, 3 })]
        [InlineData(new int[0], 0, new int[0])]
        public void RemoveDuplicates_ShouldSucceed(int[] array, int expectedSize, int[] expectedArray)
        {
            // Act 
            var size = new ArraySolution().RemoveDuplicates(array);

            // Assert
            Assert.Equal(expectedSize, size);

            for (int i = 0; i < expectedSize; ++i)
            {
                Assert.Equal(expectedArray[i], array[i]);
            }
        }
    }
}
