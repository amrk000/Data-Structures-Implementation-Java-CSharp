using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Priority Queue using MaxHeap implementation | From Repo: https://github.com/amrk000/Data-Structures-Implementation-Java-CSharp
//by Amrk000 - No license or attribution required

//generic fixed priority queue of type <T>
//IMPORTANT: This Queue is based on MaxHeap
class PriorityQueue<T>
{
    private MaxHeap<T> heap;

    public PriorityQueue(int capacity)
    {
        heap = new MaxHeap<T>(capacity);
    }

    //return number of elements
    public int Size()
    {
        return heap.Size();
    }

    //return number of max elements that it can carry
    public int Capacity()
    {
        return heap.Capacity();
    }

    //check if it's empty
    public bool IsEmpty()
    {
        return heap.IsEmpty();
    }

    //add element to queue
    public void Enqueue(int priority, T element)
    {
        heap.Add(priority, element);
    }

    //remove element from queue and return its value (First in First Out [FIFO])
    public T Dequeue()
    {
        if (heap.IsEmpty()) return default;
        return heap.Remove(); //removes from root (max priority first)
    }

    //check the front of queue (Next Element to dequeue)
    public T PeekFront()
    {
        if (IsEmpty()) return default;
        return heap.PeekFront();
    }

    //delete all elements
    public void Clear()
    {
        heap.Clear();
    }

}
