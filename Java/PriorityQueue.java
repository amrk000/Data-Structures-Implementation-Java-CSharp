package org.example;

import java.util.Iterator;
import java.util.LinkedList;
import java.util.Spliterator;
import java.util.function.Consumer;

//Priority Queue using MaxHeap implementation | From Repo: https://github.com/amrk000/Data-Structures-Implementation-Java-CSharp
//by Amrk000 - No license or attribution required

//generic fixed priority queue of type <T>
//IMPORTANT: This Queue is based on MaxHeap
public class PriorityQueue<T>{
    private MaxHeap<T> heap;

    public PriorityQueue(int capacity){
        heap = new MaxHeap<>(capacity);
    }

    //return number of elements
    public int size(){
        return heap.size();
    }

    //return number of max elements that it can carry
    public int capacity(){
        return heap.capacity();
    }

    //check if it's empty
    public boolean isEmpty(){
        return heap.isEmpty();
    }

    //add element to queue
    public void enqueue(int priority, T element){
       heap.add(priority, element);
    }

    //remove element from queue and return its value (First in First Out [FIFO])
    public T dequeue(){
        if(heap.isEmpty()) return null;
        return heap.remove(); //removes from root (max priority first)
    }

    //check the front of queue (Next Element to dequeue)
    public T peekFront(){
        if(isEmpty()) return null;
        return heap.peekFront();
    }

    //delete all elements
    public void clear(){
        heap.clear();
    }
    
}
