package org.example;

import java.util.Iterator;
import java.util.Spliterator;
import java.util.function.Consumer;

//Stack using array implementation | From Repo: https://github.com/amrk000/Data-Structures-Implementation-Java-CSharp
//by Amrk000 - No license or attribution required

//generic fixed stack of type <T> - implementing iterable is optional
public class ArrayStack<T>  implements Iterable<T>{
    private T[] array;
    private int size;

    public ArrayStack(int capacity){
        array = (T[]) new Object[capacity];
        size = 0;
    }

    //return number of elements
    public int size(){
        return size;
    }

    //return number of max elements that it can carry
    public int capacity(){
        return array.length;
    }

    //check if it's empty
    public boolean isEmpty(){
        return size == 0;
    }

    //add element to stack
    public void push(T element){
        if(size == capacity()) throw new IndexOutOfBoundsException("Stack is full - capacity:" + capacity());
        array[size++] = element;
    }

    //remove element from stack and return its value (Last in First Out [LIFO])
    public T pop(){
        if(isEmpty()) return null;
        return array[--size];
    }

    //check the top of stack
    public T peek(){
        if(isEmpty()) return null;
        return array[size-1];
    }

    //delete all elements
    public void clear(){
        array = (T[]) new Object[capacity()]; //set new array
        System.gc(); // activate garbage collector to delete old array
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
