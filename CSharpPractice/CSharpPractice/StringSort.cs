namespace CSharpPractice
{
    public static class StringSort
    {
        public static void QuickSort(char[] array, int low, int high)
        {
            var stack = new Stack<int>();
            stack.Push(low);
            stack.Push(high);

            while (stack.Count > 0)
            {
                high = stack.Pop();
                low = stack.Pop();

                int pivotIndex = Partition(array, low, high);

                if (pivotIndex - 1 > low)
                {
                    stack.Push(low);
                    stack.Push(pivotIndex - 1);
                }

                if (pivotIndex + 1 < high)
                {
                    stack.Push(pivotIndex + 1);
                    stack.Push(high);
                }
            }
        }

        private static int Partition(char[] array, int low, int high)
        {
            char pivot = array[high];
            int i = low - 1;
            for (int j = low; j < high; j++)
            {
                if (array[j] < pivot)
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
}