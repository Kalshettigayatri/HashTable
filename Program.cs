using System;
using System.Collections.Generic;

class MyMapNode<K, V>
{
    public K Key { get; set; }
    public V Value { get; set; }
    public MyMapNode<K, V> Next { get; set; }
}

class MyHashTable<K, V>
{
    private LinkedList<MyMapNode<K, V>>[] buckets;

    public MyHashTable(int size)
    {
        buckets = new LinkedList<MyMapNode<K, V>>[size];
    }

    private int GetBucketIndex(K key)
    {
        int hashCode = key.GetHashCode();
        int index = hashCode % buckets.Length;
        return Math.Abs(index);
    }

    public void Add(K key, V value)
    {
        int index = GetBucketIndex(key);
        if (buckets[index] == null)
        {
            buckets[index] = new LinkedList<MyMapNode<K, V>>();
        }

        foreach (var node in buckets[index])
        {
            if (node.Key.Equals(key))
            {
                node.Value = value;
                return;
            }
        }

        var newNode = new MyMapNode<K, V> { Key = key, Value = value };
        buckets[index].AddLast(newNode);
    }

    public V Get(K key)
    {
        int index = GetBucketIndex(key);
        if (buckets[index] != null)
        {
            foreach (var node in buckets[index])
            {
                if (node.Key.Equals(key))
                {
                    return node.Value;
                }
            }
        }
        return default(V);
    }
}

class Program
{
    static void Main(string[] args)
    {
        string sentence = "To be or not to be";
        string[] words = sentence.Split(' ');

        MyHashTable<string, int> wordFrequencyTable = new MyHashTable<string, int>(10);

        foreach (string word in words)
        {
            if (!string.IsNullOrEmpty(word))
            {
                if (wordFrequencyTable.Get(word) == default(int))
                {
                    wordFrequencyTable.Add(word, 1);
                }
                else
                {
                    int currentFrequency = wordFrequencyTable.Get(word);
                    wordFrequencyTable.Add(word, currentFrequency + 1);
                }
            }
        }

        // Print word frequencies
        foreach (string word in words)
        {
            if (!string.IsNullOrEmpty(word))
            {
                Console.WriteLine($"Word: {word}, Frequency: {wordFrequencyTable.Get(word)}");
            }
        }
    }
}
