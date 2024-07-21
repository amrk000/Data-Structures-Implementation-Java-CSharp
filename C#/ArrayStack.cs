using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Stack using array implementation | From Repo: https://github.com/amrk000/Data-Structures-Implementation-Java-CSharp
//by Amrk000 - No license or attribution required

//generic fixed stack of type <T> - implementing iterable is optional
public class ArrayStack<T> : IEnumerable<T>
{
    private T[] array;
    private int size;

    public ArrayStack(int capacity)
    {
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
        return size == 0;
    }

    //add element to stack
    public void Push(T element)
    {
        if (size == Capacity()) throw new IndexOutOfRangeException("Stack is full - capacity:" + Capacity());
        array[size++] = element;
    }

    //remove element from stack and return its value (Last in First Out [LIFO])
    public T Pop()
    {
        if (IsEmpty()) return default;
        return array[--size];
    }

    //check the top of stack
    public T Peek()
    {
        if (IsEmpty()) return default;
        return array[size - 1];
    }

    //delete all elements
    public void Clear()
    {
        array = new T[Capacity()]; //set new array
        GC.Collect(); // activate garbage collector to delete old array
    }

    //foreach iterator
    public IEnumerator<T> GetEnumerator()
    {
        int i = 0;

        while (i < size) yield return array[i++];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}
