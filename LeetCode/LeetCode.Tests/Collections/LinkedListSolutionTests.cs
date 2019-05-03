using LeetCode.Collections;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LeetCode.Tests.Collections
{
    public class LinkedListSolutionTests
    {
        [Theory]
        [InlineData("1->2->4", "1->3->4", "1->1->2->3->4->4")]
        [InlineData("1->2", "1->3->4", "1->1->2->3->4")]
        [InlineData("1->2", "1", "1->1->2")]
        public void MergeTwoLists_ShouldSucceed(string str1, string str2, string expected)
        {
            // Arrange
            var l1 = CreateList(str1);
            var l2 = CreateList(str2);

            // Act
            var resultList = new LinkedListSolution().MergeTwoLists(l1, l2);

            // Assert
            Assert.Equal(expected, ListToString(resultList));
        }

        ListNode CreateList(string str)
        {
            var values = str.Split("->");

            var head = new ListNode(int.Parse(values[0]));
            var current = head;

            for (int i = 1; i < values.Length; ++i)
            {
                var newNode = new ListNode(int.Parse(values[i]));
                current.next = newNode;
                current = current.next;
            }

            return head;
        }

        string ListToString(ListNode head)
        {
            var resultString = $"{head?.val}";

            while (head.next != null)
            {
                resultString += "->" + head.next.val;
                head = head.next;
            }
            return resultString;
        }
    }
}
