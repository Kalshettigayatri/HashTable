using System;

class MyBinaryNode<T> where T : IComparable<T>
{
    public T Key { get; set; }
    public MyBinaryNode<T> Left { get; set; }
    public MyBinaryNode<T> Right { get; set; }

    public MyBinaryNode(T key)
    {
        Key = key;
        Left = null;
        Right = null;
    }
}

class BinarySearchTree<T> where T : IComparable<T>
{
    public MyBinaryNode<T> Root { get; private set; }

    public BinarySearchTree()
    {
        Root = null;
    }

    public void Add(T key)
    {
        Root = AddRecursive(Root, key);
    }

    private MyBinaryNode<T> AddRecursive(MyBinaryNode<T> currentNode, T key)
    {
        if (currentNode == null)
        {
            return new MyBinaryNode<T>(key);
        }

        int compareResult = key.CompareTo(currentNode.Key);
        if (compareResult < 0)
        {
            currentNode.Left = AddRecursive(currentNode.Left, key);
        }
        else if (compareResult > 0)
        {
            currentNode.Right = AddRecursive(currentNode.Right, key);
        }

        return currentNode;
    }

    public bool Search(T key)
    {
        return SearchRecursive(Root, key);
    }

    private bool SearchRecursive(MyBinaryNode<T> currentNode, T key)
    {
        if (currentNode == null)
        {
            return false;
        }

        int compareResult = key.CompareTo(currentNode.Key);
        if (compareResult == 0)
        {
            return true;
        }
        else if (compareResult < 0)
        {
            return SearchRecursive(currentNode.Left, key);
        }
        else
        {
            return SearchRecursive(currentNode.Right, key);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        BinarySearchTree<int> bst = new BinarySearchTree<int>();
        bst.Add(56);
        bst.Add(30);
        bst.Add(70);
        bst.Add(22);
        bst.Add(40);
        bst.Add(60);
        bst.Add(95);
        bst.Add(11);
        bst.Add(65);
        bst.Add(3);
        bst.Add(16);
        bst.Add(63);
        bst.Add(67);

        int searchValue = 63;
        if (bst.Search(searchValue))
        {
            Console.WriteLine($"{searchValue} was found in the binary tree.");
        }
        else
        {
            Console.WriteLine($"{searchValue} was not found in the binary tree.");
        }
    }
}
