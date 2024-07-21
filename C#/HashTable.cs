using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//HashTable implementation | From Repo: https://github.com/amrk000/Data-Structures-Implementation-Java-CSharp
//by Amrk000 - No license or attribution required

//generic HashTable of type <T>
//IMPORTANT: This HashTable is based on separate chaining approach (table bucket is an array of linked lists)
class HashTable<T>
{
    private Node<T>[] tableBucket;
    private int size;
    private const float optimalLoadFactor = 0.75F; //optimal load factor value

    public HashTable()
    {
        tableBucket = new Node<T>[10];
        size = 0;
    }

    //Node element that carry key, value and reference to next node in linked list chain
    internal class Node<T>
    {
        internal String key;
        internal T value;
        internal Node<T> next;

        public Node(String key, T value)
        {
            this.key = key;
            this.value = value;
            next = null;
        }
    }

    //return number of elements
    public int Size()
    {
        return size;
    }

    //check if it's empty
    public bool IsEmpty()
    {
        return size == 0;
    }

    //return table bucket size
    public int BucketSize()
    {
        return tableBucket.Length;
    }

    //check current table load factor
    public float LoadFactor()
    {
        return (float)size / BucketSize();
    }

    //check if current load factor isn't bigger than optimal to determine if rehashing is needed
    public bool isLoadFactorOptimal()
    {
        return LoadFactor() <= optimalLoadFactor;
    }

    //return hash code value which made from string key (hashcode is an index of  table bucket array)
    private int HashFunction(String key)
    {
        long keyNumValue = 0;
        foreach (char c in key.ToCharArray()) keyNumValue += c;
        return (int)(keyNumValue % BucketSize()); //always returns code that doesn't exceed bucket index range
    }

    //increase table bucket size and set new hash codes to elements then reposition them
    private void Rehash()
    {
        Node<T>[] oldBucket = tableBucket; //get temporary copy of table bucket
        tableBucket = new Node<T>[BucketSize() * 2]; //set new larger bucket
        size = 0;

        for (int i = 0; i < oldBucket.Length; i++)
        {
            if (oldBucket[i] == null) continue; //if there is no linked list there continue

            Node<T> current = oldBucket[i]; //get linked list head from old bucket

            //traverse each linked list
            while (current != null)
            {
                Set(current.key, current.value); //add elements from old bucket to new bucket
                current = current.next;
            }
        }
    }

    //add or update element by key
    public void Set(String key, T value)
    {
        int hashCode = HashFunction(key); //get hash code from key

        if (tableBucket[hashCode] == null) tableBucket[hashCode] = new Node<T>(key, value); //if hash index is empty add head node of linked list
        else
        {
            Node<T> current = tableBucket[hashCode]; //get head node of linked list

            while (current != null)
            {
                //if key found update value and exit
                if (current.key.Equals(key))
                {
                    current.value = value;
                    return;
                }
                //if last is reached add new node
                if (current.next == null)
                {
                    current.next = new Node<T>(key, value);
                    break;
                }

                current = current.next;
            }
        }

        size++;

        if (!isLoadFactorOptimal()) Rehash(); //check if rehashing is required
    }

    //get element by key
    public T Get(String key)
    {
        int hashCode = HashFunction(key); //get hash code from key

        Node<T> current = tableBucket[hashCode]; //get head node of linked list

        while (current != null)
        {
            if (current.key.Equals(key)) return current.value; //element found
            current = current.next;
        }

        return default;
    }

    //remove node by key
    public void Remove(String key)
    {
        int hashCode = HashFunction(key); //get hash code from key

        Node<T> current = tableBucket[hashCode]; //get head node of linked list

        if (current.key.Equals(key))
        {
            tableBucket[hashCode] = current.next; //if key equals head remove head
            size--;
            return;
        }

        //search in linked list
        do
        {
            if (current.next.key.Equals(key))
            {
                current.next = current.next.next; //remove next of current node if equals target
                size--;
                return;
            }
            current = current.next;
        }
        while (current.next != null);
    }

    //return keys as string
    public String GetKeys()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("[");

        foreach (Node<T> hashIndex in tableBucket)
        {
            Node<T> current = hashIndex;
            while (current != null)
            {
                stringBuilder.Append(current.key + ",");
                current = current.next;
            }
        }

        if (!IsEmpty()) stringBuilder.Remove(stringBuilder.Length - 1, 1);
        stringBuilder.Append("]");
        return stringBuilder.ToString();
    }

    //return values as string
    public String GetValues()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("[");

        foreach (Node<T> hashIndex in tableBucket)
        {
            Node<T> current = hashIndex;
            while (current != null)
            {
                stringBuilder.Append(current.value.ToString() + ",");
                current = current.next;
            }
        }

        if (!IsEmpty()) stringBuilder.Remove(stringBuilder.Length - 1, 1);
        stringBuilder.Append("]");
        return stringBuilder.ToString();
    }

    //return key,value array as string
    override
    public String ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("[");

        foreach (Node<T> hashIndex in tableBucket)
        {
            Node<T> current = hashIndex;
            while (current != null)
            {
                stringBuilder.Append("{" + current.key + "," + current.value.ToString() + "},");
                current = current.next;
            }
        }

        if (!IsEmpty()) stringBuilder.Remove(stringBuilder.Length - 1, 1);
        stringBuilder.Append("]");
        return stringBuilder.ToString();
    }

    //clear all elements
    public void clear()
    {
        size = 0;
        tableBucket = new Node<T>[10];
        GC.Collect(); // activate garbage collector to delete old array
    }
}
