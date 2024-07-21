package org.example;

import java.util.Iterator;
import java.util.LinkedList;
import java.util.Spliterator;
import java.util.function.Consumer;

//Stack using LinkedList implementation | From Repo: https://github.com/amrk000/Data-Structures-Implementation-Java-CSharp
//by Amrk000 - No license or attribution required

//generic dynamic stack of type <T> - implementing iterable is optional
public class ListStack<T>  implements Iterable<T>{
    private LinkedList<T> list;

    public ListStack(){
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

    //add element to stack
    public void push(T element){
        list.add(element);
    }

    //remove element from stack and return its value (Last in First Out [LIFO])
    public T pop(){
        if(list.isEmpty()) return null;
        return list.removeLast();
    }

    //check the top of stack
    public T peek(){
        if(list.isEmpty()) return null;
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
