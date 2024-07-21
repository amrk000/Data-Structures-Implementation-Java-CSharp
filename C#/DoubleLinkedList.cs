using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Double Linked List implementation | From Repo: https://github.com/amrk000/Data-Structures-Implementation-Java-CSharp
//by Amrk000 - No license or attribution required

//generic DoubleLinkedList of type <T> - implementing iterable is optional
class DoubleLinkedList<T> : IEnumerable<T>
{
    private Node<T> head, tail;
    private int size;

    public DoubleLinkedList()
    {
        size = 0;
    }

    //Node element that carry data and reference to next & previous node in linked list chain
    internal class Node<T>
    {
        internal T data;
        internal Node<T> next;
        internal Node<T> previous;

        public Node(T data)
        {
            this.data = data;
            this.next = null;
            this.previous = null;
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

    //get node by index
    private Node<T> GetNode(int index)
    {
        if (index >= size) throw new IndexOutOfRangeException("Index " + index + " out of bounds for length " + size);

        //make a node that starts at head and traverse on all nodes one by one to reach target
        int i = 0;
        Node<T> traverse = head;
        do
        {
            if (i == index) break; //found: exit loop
            traverse = traverse.next;
            i++;
        } while (traverse.next != null);

        return traverse;
    }

    //get data by index
    public T Get(int index)
    {
        return GetNode(index).data;
    }

    //add new data node after last node using tail
    public void Add(T element)
    {
        if (IsEmpty()) head = tail = new Node<T>(element);
        else
        {
            Node<T> newNode = new Node<T>(element);
            tail.next = newNode;
            newNode.previous = tail;
            tail = newNode;
        }

        size++;
    }

    //add new data node before first node using head
    public void AddFirst(T element)
    {
        Node<T> newNode = new Node<T>(element);
        newNode.next = head;
        head.previous = newNode;
        head = newNode;
        size++;
    }

    //add new data node at specific index
    public void Add(T element, int index)
    {
        if (index == 0) AddFirst(element);
        else if (index == size - 1) Add(element);
        else
        {
            Node<T> newNode = new Node<T>(element);
            Node<T> temp = GetNode(index); //get the node

            //link new node to prev & next
            newNode.next = temp;
            newNode.previous = temp.previous;

            //link prev & next nodes to new node
            temp.previous.next = newNode;
            temp.previous = newNode;

            size++;
        }
    }

    //delete first node
    public void RemoveFirst()
    {
        head = head.next; //move head to next node - first node ref has no object and will be deleted
        head.previous = null;
        size--;
    }

    //delete last node
    public void RemoveLast()
    {
        tail = tail.previous; //get last node directly and move tail back by previous - last node ref has no object and will be deleted
        tail.next = null;
        size--;
    }

    //delete node by its value
    public void Remove(T element)
    {
        if (IsEmpty()) return;

        if (Comparer.Equals(head.data, element)) RemoveFirst(); //if data found at head delete first
        else if (Comparer.Equals(tail.data, element)) RemoveLast(); //if data found at tail delete last
        else if (head.next != null)
        {
            //make a node that starts at head and traverse on all nodes one by one to reach target
            //no need to stop at the node before the target to delete target like single linked list as u can access next & previous easily
            Node<T> traverse = head;
            while (traverse.next != null)
            {
                //Target found
                if (Comparer.Equals(traverse.data, element))
                {
                    //link next & prev nodes together, So nodes around target are linked and target is out
                    traverse.next.previous = traverse.previous;
                    traverse.previous.next = traverse.next;
                    traverse.next = traverse.previous = null;
                    size--;
                    break;
                }
                traverse = traverse.next;
            }
        }
    }

    //delete node by its index
    public void RemoveAt(int index)
    {
        if (IsEmpty()) return;

        if (index == 0) RemoveFirst(); //if index = 0 at head delete first
        else if (index == size - 1) RemoveLast(); //if index = lastNode at tail delete last
        else
        {
            Node<T> temp = GetNode(index); //get the target node

            //link next & prev nodes together, So nodes around target are linked and target is out
            temp.next.previous = temp.previous;
            temp.previous.next = temp.next;
            temp.next = temp.previous = null;

            size--;
        }
    }

    //clear all elements
    public void Clear()
    {
        head = tail = null; //reset chain
        size = 0;
        GC.Collect(); // activate garbage collector to delete old chain nodes
    }

    //return elements as string
    override
    public String ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("[");
        foreach (T element in this) stringBuilder.Append(element.ToString() + ",");
        if (!IsEmpty()) stringBuilder.Remove(stringBuilder.Length - 1,1);
        stringBuilder.Append("]");
        return stringBuilder.ToString();
    }

    //foreach iterator
    public IEnumerator<T> GetEnumerator()
    {
        Node<T> traverse = head;
        while (traverse != null) {
            T data = traverse.data;
            traverse = traverse.next;
            yield return data;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}

