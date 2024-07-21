package org.example;

import java.util.Iterator;
import java.util.Spliterator;
import java.util.function.Consumer;

//Queue using array implementation | From Repo: https://github.com/amrk000/Data-Structures-Implementation-Java-CSharp
//by Amrk000 - No license or attribution required

//generic fixed queue of type <T> - implementing iterable is optional
//IMPORTANT: This Queue is based on circular array instead of normal array for efficiency
public class ArrayQueue<T> implements Iterable<T> {
    private T[] array;
    private int front, rear, size;

    public ArrayQueue(int capacity){
        front = rear = -1; //front and rear traversals are -1 if empty
        size = 0;
        array = (T[]) new Object[capacity];
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
        return size() == 0;
    }

    //add element to queue
    public void enqueue(T element){
        //if size equals capacity return with exception that queue is full
        if(size() == capacity()) throw new IndexOutOfBoundsException("Queue is full - capacity:" + capacity());
        if(isEmpty()) front = 0; //set start to 0 if it's first element in queue
        rear = ++rear % capacity(); //rear is always increased by one to go next and value is % by capacity to move in circle so if it's out of index it moves to array start again
        array[rear] = element; //add element at rear position
        size++;
    }

    //remove element from queue and return its value (First in First Out [FIFO])
    public T dequeue(){
        if(isEmpty()) return null;

        T element = array[front]; //get first element from queue front
        array[front] = null; //set empty place to null

        if(front == rear) front = rear = -1; //if front equals rear then there is only one element so, dequeue and set indexes to -1
        else front = ++front % capacity(); //else front is always increased by one to go next and value is % by capacity to move in circle so if it's out of index it moves to array start again
        size--;
        return element;
    }

    //check the front of queue (Next Element to dequeue)
    public T peekFront(){
        if(isEmpty()) return null;
        return array[front];
    }

    //check the rear of queue
    public T peekRear(){
        if(isEmpty()) return null;
        return array[rear];
    }

    //delete all elements
    public void clear(){
        front = rear = -1; //front and rear traversals are -1 if empty
        size = 0;
        array = (T[]) new Object[capacity()]; //set new array
        System.gc(); // activate garbage collector to delete old array
    }

    //foreach iterator
    @Override
    public Iterator<T> iterator() {
        return new Iterator<T>() {
            int i=front, counter=0;

            @Override
            public boolean hasNext() {
                return counter < size;
            }

            @Override
            public T next() {
                counter++;
                i = i%capacity();
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
