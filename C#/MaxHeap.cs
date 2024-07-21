using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class MaxHeap<T>
{
    private Node<T>[] array;
    private int size;

    //Node element that carry data and priority of element
    internal class Node<T>
    {
        internal int priority;
        internal T data;

        public Node(int priority, T data)
        {
            this.priority = priority;
            this.data = data;
        }
    }

    public MaxHeap(int capacity)
    {
        size = 0;
        array = new Node<T>[capacity];
    }

    //return number of elements
    public int Size()
    {
        return size;
    }

    //return number of max elements that it can carry
    public int Capacity()
    {
        return array.Length;
    }

    //check if it's empty
    public bool IsEmpty()
    {
        return Size() == 0;
    }

    //get node's parent index in array
    private int GetParentIndex(int nodeIndex)
    {
        return (nodeIndex - 1) / 2; //if heap elements starts at 1 it  will be: (nodeIndex)/2
    }

    //get node's left child index in array
    private int GetLeftChildIndex(int nodeIndex)
    {
        return (2 * nodeIndex) + 1; //if heap elements starts at 1 it  will be: (2*nodeIndex)
    }

    //get node's right child index in array
    private int GetRightChildIndex(int nodeIndex)
    {
        return (2 * nodeIndex) + 2; //if heap elements starts at 1 it  will be: (2*nodeIndex)+1
    }

    //bubble up the element as long as it's bigger than parent
    private void HeapifyUp(int index)
    {
        Node<T> temp = array[index]; //hold current element value

        while (index > 0 && temp.priority > array[GetParentIndex(index)].priority)
        {
            array[index] = array[GetParentIndex(index)]; //move parent's value down to current element
            index = GetParentIndex(index); //go up
        }

        array[index] = temp; //set current position value to element value
    }

    //bubble down the element as long as it's smaller than children
    private void HeapifyDown(int index)
    {
        Node<T> temp = array[index]; //hold current element value
        int targetChildIndex;

        while (index < size / 2)
        {
            //calculate diff between parent priority and the children
            int leftDiff = array[GetLeftChildIndex(index)].priority - temp.priority;
            int rightDiff = array[GetRightChildIndex(index)].priority - temp.priority;

            //diffs == 0 or less which means that node is bigger than both children break
            if (leftDiff <= 0 && rightDiff <= 0) break;

            //get target child index
            if (leftDiff > rightDiff) targetChildIndex = GetLeftChildIndex(index);
            else targetChildIndex = GetRightChildIndex(index);

            //move child value up to current element
            array[index] = array[targetChildIndex];
            index = targetChildIndex; //go down
        }

        array[index] = temp; //set current position value to element value
    }

    //add element to heap
    public void Add(int priority, T element)
    {
        if (Size() == Capacity()) throw new IndexOutOfRangeException("Heap is full - capacity:" + Capacity());

        Node<T> node = new Node<T>(priority, element);
        int elementIndex = size;
        array[size++] = node;
        HeapifyUp(elementIndex); //move up node
    }

    //delete max value (at root) and return data
    public T Remove()
    {
        if (IsEmpty()) return default;

        Node<T> max = array[0];
        array[0] = array[size - 1]; //swap max value node with last node in heap
        size--;
        HeapifyDown(0); //move down node to a suitable position
        return max.data;
    }

    //check the front of heap
    public T PeekFront()
    {
        if (IsEmpty()) return default;
        return array[0].data;
    }

    //delete all elements
    public void Clear()
    {
        size = 0;
        array = new Node<T>[Capacity()];
        GC.Collect(); // activate garbage collector to delete old array
    }

}
