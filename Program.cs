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

    public void InOrderTraversal(MyBinaryNode<T> node)
    {
        if (node != null)
        {
            InOrderTraversal(node.Left);
            Console.Write($"{node.Key} ");
            InOrderTraversal(node.Right);
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

        Console.WriteLine("In-order traversal of the BST:");
        bst.InOrderTraversal(bst.Root);
    }
}
