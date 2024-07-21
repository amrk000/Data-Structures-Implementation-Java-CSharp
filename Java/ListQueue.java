package org.example;

import java.util.Iterator;
import java.util.LinkedList;
import java.util.Spliterator;
import java.util.function.Consumer;

//Queue using linkedList implementation | From Repo: https://github.com/amrk000/Data-Structures-Implementation-Java-CSharp
//by Amrk000 - No license or attribution required

//generic dynamic queue of type <T> - implementing iterable is optional
public class ListQueue<T> implements Iterable<T> {
    private LinkedList<T> list;

    public ListQueue(){
       list = new LinkedList<>();
    }

    //return number of elements
    public int size(){
        return list.size();
    }

    //check if it's empty
    public boolean isEmpty(){
        return list.isEmpty();
    }

    //add element to queue
    public void enqueue(T element){
       list.add(element);
    }

    //remove element from queue and return its value (First in First Out [FIFO])
    public T dequeue(){
        if(list.isEmpty()) return null;
        return list.removeFirst();
    }

    //check the front of queue (Next Element to dequeue)
    public T peekFront(){
        if(isEmpty()) return null;
        return list.getFirst();
    }

    //check the rear of queue
    public T peekRear(){
        if(isEmpty()) return null;
        return list.getLast();
    }

    //delete all elements
    public void clear(){
        list.clear();
    }

    //foreach iterator
    @Override
    public Iterator<T> iterator() {
        return list.iterator();
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
