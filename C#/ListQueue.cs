using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Queue using linkedList implementation | From Repo: https://github.com/amrk000/Data-Structures-Implementation-Java-CSharp
//by Amrk000 - No license or attribution required

//generic dynamic queue of type <T> - implementing iterable is optional
class ListQueue<T> : IEnumerable<T>
{
    private LinkedList<T> list;

    public ListQueue()
    {
        list = new LinkedList<T>();

    }

    //return number of elements
    public int Size()
    {
        return list.Count;
    }

    //check if it's empty
    public bool IsEmpty()
    {
        return list.Count == 0;
    }

    //add element to queue
    public void Enqueue(T element)
    {
        list.AddLast(element);
    }

    //remove element from queue and return its value (First in First Out [FIFO])
    public T Dequeue()
    {
        if (list.Count == 0) return default;
        T first = list.First();
        list.RemoveFirst();
        return first;
    }

    //check the front of queue (Next Element to dequeue)
    public T PeekFront()
    {
        if (IsEmpty()) return default;
        return list.First();
    }

    //check the rear of queue
    public T PeekRear()
    {
        if (IsEmpty()) return default;
        return list.Last();
    }

    //delete all elements
    public void Clear()
    {
        list.Clear();
    }

    //foreach iterator
    public IEnumerator<T> GetEnumerator()
    {
        return list.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}
