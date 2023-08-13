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

    public bool Remove(K key)
    {
        int index = GetBucketIndex(key);
        if (buckets[index] != null)
        {
            var nodeToRemove = buckets[index].Find(node => node.Key.Equals(key));
            if (nodeToRemove != null)
            {
                buckets[index].Remove(nodeToRemove);
                return true;
            }
        }
        return false;
    }
}

class Program
{
    static void Main(string[] args)
    {
        string paragraph = "Paranoids are not paranoid because they are paranoid but " +
                          "because they keep putting themselves deliberately into " +
                          "paranoid avoidable situations";

        MyHashTable<string, int> wordFrequencyTable = new MyHashTable<string, int>(20);

        string[] words = paragraph.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        foreach (string word in words)
        {
            if (!string.IsNullOrEmpty(word))
            {
                string normalizedWord = word.ToLower();
                if (wordFrequencyTable.Get(normalizedWord) == default(int))
                {
                    wordFrequencyTable.Add(normalizedWord, 1);
                }
                else
                {
                    int currentFrequency = wordFrequencyTable.Get(normalizedWord);
                    wordFrequencyTable.Add(normalizedWord, currentFrequency + 1);
                }
            }
        }

        // Remove the word "avoidable" from the hash table
        wordFrequencyTable.Remove("avoidable");

        // Print word frequencies after removal
        foreach (string word in words)
        {
            if (!string.IsNullOrEmpty(word))
            {
                string normalizedWord = word.ToLower();
                Console.WriteLine($"Word: {normalizedWord}, Frequency: {wordFrequencyTable.Get(normalizedWord)}");
            }
        }
    }
}
