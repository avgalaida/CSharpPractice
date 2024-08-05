namespace WebApp.Utils;

public static class StringSort
{
    public static void QuickSort(char[] array, int low, int high)
    {
        var stack = new Stack<Range>();
        stack.Push(new Range(low, high));

        while (stack.Count > 0)
        {
            var range = stack.Pop();
            low = range.Low;
            high = range.High;

            if (low < high)
            {
                int pivotIndex = Partition(array, low, high);
                stack.Push(new Range(low, pivotIndex - 1));
                stack.Push(new Range(pivotIndex + 1, high));
            }
        }
    }

    private static int Partition(char[] array, int low, int high)
    {
        var mid = low + (high - low) / 2;
        var pivot = array[mid];
        Swap(array, mid, high); 

        var i = low - 1;
        for (int j = low; j < high; j++)
        {
            if (array[j] <= pivot)
            {
                i++;
                Swap(array, i, j);
            }
        }
        Swap(array, i + 1, high);
        return i + 1;
    }

    private static void Swap(char[] array, int i, int j)
    {
        char temp = array[i];
        array[i] = array[j];
        array[j] = temp;
    }

    public struct Range
    {
        public int Low { get; }
        public int High { get; }

        public Range(int low, int high)
        {
            Low = low;
            High = high;
        }
    }

    public static char[] TreeSort(char[] array)
    {
        if (array.Length == 0) return array;

        var root = new TreeNode(array[0]);

        for (int i = 1; i < array.Length; i++)
        {
            root.Insert(array[i]);
        }

        return InOrderTraversal(root, array.Length);
    }

    private static char[] InOrderTraversal(TreeNode root, int size)
    {
        var sortedList = new List<char>(size);
        var stack = new Stack<TreeNode>();
        var currentNode = root;

        while (stack.Count > 0 || currentNode != null)
        {
            if (currentNode != null)
            {
                stack.Push(currentNode);
                currentNode = currentNode.Left;
            }
            else
            {
                currentNode = stack.Pop();
                sortedList.Add(currentNode.Value);
                currentNode = currentNode.Right;
            }
        }

        return sortedList.ToArray();
    }
}

public class TreeNode
{
    public char Value { get; set; }
    public TreeNode Left { get; set; }
    public TreeNode Right { get; set; }

    public TreeNode(char value)
    {
        Value = value;
    }

    public void Insert(char value)
    {
        if (value < Value)
        {
            if (Left == null)
            {
                Left = new TreeNode(value);
            }
            else
            {
                Left.Insert(value);
            }
        }
        else
        {
            if (Right == null)
            {
                Right = new TreeNode(value);
            }
            else
            {
                Right.Insert(value);
            }
        }
    }
}