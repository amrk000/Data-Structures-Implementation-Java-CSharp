using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Stack using LinkedList implementation | From Repo: https://github.com/amrk000/Data-Structures-Implementation-Java-CSharp
//by Amrk000 - No license or attribution required

//generic dynamic stack of type <T> - implementing iterable is optional
class ListStack<T> : IEnumerable<T>
{
    private LinkedList<T> list;

    public ListStack()
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

    //add element to stack
    public void Push(T element)
    {
        list.AddLast(element);
    }

    //remove element from stack and return its value (Last in First Out [LIFO])
    public T Pop()
    {
        if (list.Count == 0) return default;
        T last = list.Last();
        list.RemoveLast();
        return last;
    }

    //check the top of stack
    public T Peek()
    {
        if (list.Count == 0) return default;
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
