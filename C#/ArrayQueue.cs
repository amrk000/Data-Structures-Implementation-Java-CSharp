using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Queue using array implementation | From Repo: https://github.com/amrk000/Data-Structures-Implementation-Java-CSharp
//by Amrk000 - No license or attribution required

//generic fixed queue of type <T> - implementing iterable is optional
//IMPORTANT: This Queue is based on circular array instead of normal array for efficiency
public class ArrayQueue<T> : IEnumerable<T>
{
    private T[] array;
    private int front, rear, size;

    public ArrayQueue(int capacity)
    {
        front = rear = -1; //front and rear traversals are -1 if empty
        array = new T[capacity];
        size = 0;
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

    //add element to queue
    public void Enqueue(T element)
    {
        //if size equals capacity return with exception that queue is full
        if (Size() == Capacity()) throw new IndexOutOfRangeException("Queue is full - capacity:" + Capacity());
        if (IsEmpty()) front = 0; //set start to 0 if it's first element in queue
        rear = ++rear % Capacity(); //rear is always increased by one to go next and value is % by capacity to move in circle so if it's out of index it moves to array start again
        array[rear] = element; //add element at rear position
        size++;
    }

    //remove element from queue and return its value (First in First Out [FIFO])
    public T Dequeue()
    {
        if (IsEmpty()) return default;

        T element = array[front]; //get first element from queue front
        array[front] = default; //set empty place to null

        if (front == rear) front = rear = -1; //if front equals rear then there is only one element so, dequeue and set indexes to -1
        else front = ++front % Capacity(); //else front is always increased by one to go next and value is % by capacity to move in circle so if it's out of index it moves to array start again
        size--;
        return element;
    }

    //check the front of queue (Next Element to dequeue)
    public T PeekFront()
    {
        if (IsEmpty()) return default;
        return array[front];
    }

    //check the rear of queue
    public T PeekRear()
    {
        if (IsEmpty()) return default;
        return array[rear];
    }

    //delete all elements
    public void Clear()
    {
        front = rear = -1; //front and rear traversals are -1 if empty
        size = 0;
        array = new T[Capacity()]; //set new array
        GC.Collect(); // activate garbage collector to delete old array
    }

    //foreach iterator
    public IEnumerator<T> GetEnumerator()
    {
        int i = front, counter = 0;
        while (counter < size) {
            counter++;
            i = i % Capacity();
            yield return array[i++];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}


