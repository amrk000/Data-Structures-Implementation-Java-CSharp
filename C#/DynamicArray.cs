using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Dynamic Array implementation | From Repo: https://github.com/amrk000/Data-Structures-Implementation-Java-CSharp
//by Amrk000 - No license or attribution required

//generic dynamic array of type <T> - implementing iterable is optional
class DynamicArray<T> : IEnumerable<T>
{
    private T[] array;
    private int size;
    private const int resizeFactor = 2; //grow & shrink factor default is 2 so array will grow by double and shrink by half

    public DynamicArray()
    {
        array = new T[1];
        size = 0;
    }

    //increase the size of array depending on resize factor
    private void Grow()
    {
        int newSize = array.Length * resizeFactor;
        T[] newArray = new T[newSize];
        //move all elements from old array to new array
        for (int i = 0; i < array.Length; i++) newArray[i] = array[i];
        array = newArray; // set the default array to be the new one - old array have no reference, so it will be deleted by garbage collector
    }

    //decrease the size of array depending on resize factor
    private void Shrink()
    {
        int newSize = array.Length / resizeFactor;
        T[] newArray = new T[newSize];
        //move all elements from old array to new array
        for (int i = 0; i < newArray.Length; i++) newArray[i] = array[i];
        array = newArray; // set the default array to be the new one - old array have no reference, so it will be deleted by garbage collector
    }

    //shift elements back by 1 step to index
    private void ShiftBack(int index)
    {
        for (int i = index; i < size - 1; i++)
        {
            array[i] = array[i + 1];
            array[i + 1] = default;
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

    //return element by index
    public T Get(int index)
    {
        if (index >= size) throw new IndexOutOfRangeException("Index " + index + " out of bounds for length " + size);
        return (T)array[index];
    }

    //add element to array
    public void Add(T element)
    {
        if (array.Length == size) Grow(); //if array is full grow
        array[size++] = element; //adds after last element in array and increase size by 1
    }

    //remove element from array by data value
    public void Remove(T element)
    {
        //find object and get index
        for (int i = 0; i < size; i++)
        {
            if (Comparer.Equals(array[i], element))
            {
                //shift array elements back
                ShiftBack(i);
                size--;
                if (array.Length / resizeFactor == size) Shrink(); //if array have elements less than resizeFactor multiplication shrink (result will be an array that fits exact elements & full)

                break;
            }
        }
    }

    //remove element from array by index
    public void RemoveAt(int index)
    {
        if (index >= size) throw new IndexOutOfRangeException("Index " + index + " out of bounds for length " + size);

        //shift array elements
        ShiftBack(index);

        size--;
        if (array.Length / resizeFactor == size) Shrink(); //if array have elements less than resizeFactor multiplication shrink (result will be an array that fits exact elements & full)

    }

    //delete all elements
    public void Clear()
    {
        array = new T[1]; //set new array with 1 element (will grow later while adding elements)
        size = 0; //reset size
        GC.Collect(); // activate garbage collector to delete old array
    }

    //return elements as string
    override
    public String ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("[");
        for (int i = 0; i < size; i++) stringBuilder.Append(array[i].ToString() + ",");
        if (!IsEmpty()) stringBuilder.Remove(stringBuilder.Length - 1,1);
        stringBuilder.Append("]");
        return stringBuilder.ToString();
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

