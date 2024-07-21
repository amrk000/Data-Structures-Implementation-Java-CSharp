package org.example;

import java.util.Iterator;
import java.util.Spliterator;
import java.util.function.Consumer;

//Dynamic Array implementation | From Repo: https://github.com/amrk000/Data-Structures-Implementation-Java-CSharp
//by Amrk000 - No license or attribution required

//generic dynamic array of type <T> - implementing iterable is optional
public class DynamicArray<T> implements Iterable<T>{
    private T[] array;
    private int size;
    private final int resizeFactor = 2; //grow & shrink factor default is 2 so array will grow by double and shrink by half

    public DynamicArray(){
        array =(T[]) new Object[1];
        size = 0;
    }

    //increase the size of array depending on resize factor
    private void grow(){
        int newSize = array.length*resizeFactor;
        T[] newArray = (T[]) new Object[newSize];
        //move all elements from old array to new array
        for(int i=0; i<array.length; i++) newArray[i] = array[i];
        array = newArray; // set the default array to be the new one - old array have no reference, so it will be deleted by garbage collector
    }

    //decrease the size of array depending on resize factor
    private void shrink(){
        int newSize = array.length/resizeFactor;
        T[] newArray = (T[]) new Object[newSize];
        //move all elements from old array to new array
        for(int i=0; i<newArray.length; i++) newArray[i] = array[i];
        array = newArray; // set the default array to be the new one - old array have no reference, so it will be deleted by garbage collector
    }

    //shift elements back by 1 step to index
    private void shiftBack(int index){
        for(int i = index; i<size-1; i++){
            array[i] = array[i+1];
            array[i+1] = null;
        }
    }

    //return number of elements
    public int size() {
        return size;
    }

    //check if it's empty
    public boolean isEmpty(){
        return size==0;
    }

    //return element by index
    public T get(int index){
        if(index>=size) throw new IndexOutOfBoundsException("Index "+index+" out of bounds for length "+size);
        return (T) array[index];
    }

    //add element to array
    public void add(T element){
        if(array.length == size) grow(); //if array is full grow
        array[size++] = element; //adds after last element in array and increase size by 1
    }

    //remove element from array by data value
    public void remove(T element){
        //find object and get index
        for(int i=0; i<size; i++){
            if(array[i] == element){
                //shift array elements back
                shiftBack(i);
                size--;
                if(array.length/resizeFactor == size) shrink(); //if array have elements less than resizeFactor multiplication shrink (result will be an array that fits exact elements & full)

                break;
            }
        }
    }

    //remove element from array by index
    public void removeAt(int index){
        if(index>=size) throw new IndexOutOfBoundsException("Index "+index+" out of bounds for length "+size);

        //shift array elements
        shiftBack(index);

        size--;
        if(array.length/resizeFactor == size) shrink(); //if array have elements less than resizeFactor multiplication shrink (result will be an array that fits exact elements & full)

    }

    //delete all elements
    public void clear(){
        array =(T[]) new Object[1]; //set new array with 1 element (will grow later while adding elements)
        size = 0; //reset size
        System.gc(); // activate garbage collector to delete old array
    }

    //return elements as string
    public String toString(){
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.append("[");
        for(int i=0; i<size; i++) stringBuilder.append(array[i].toString() + ",");
        if(!isEmpty()) stringBuilder.deleteCharAt(stringBuilder.length()-1);
        stringBuilder.append("]");
        return stringBuilder.toString();
    }

    //foreach iterator
    @Override
    public Iterator<T> iterator() {
        return new Iterator<T>() {
            int i=0;

            @Override
            public boolean hasNext() {
                return i<size;
            }

            @Override
            public T next() {
                return array[i++];
            }
        };
    }

    @Override
    public void forEach(Consumer<? super T> action) {
        Iterable.super.forEach(action);
    }

    @Override
    public Spliterator<T> spliterator() {
        return Iterable.super.spliterator();
    }
}
