namespace LeetCode.Collections;

public class ListNode
{
    public int val;

    public ListNode next;

    public ListNode(int x) { val = x; }
}
public class LinkedListSolution
{
    public ListNode MergeTwoLists(ListNode l1, ListNode l2)
    {
        ListNode? result = null;
        ListNode? currentResult = null;

        while (l1 != null || l2 != null)
        {
            if (l1 == null || l2?.val < l1?.val)
            {
                AssignCurrent(l2!);
                l2 = l2!.next;
                continue;
            }

            AssignCurrent(l1!);
            l1 = l1.next;
        }

        return result!;

        void AssignCurrent(ListNode next)
        {
            var tmpNode = new ListNode(next.val);
            if (currentResult != null)
            {
                currentResult.next = tmpNode;
            }

            currentResult = tmpNode;
            if (result == null)
                result = tmpNode;
        }
    }
}
