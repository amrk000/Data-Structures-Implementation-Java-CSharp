package org.example;

import java.util.Iterator;
import java.util.Spliterator;
import java.util.function.Consumer;

//Single Linked List implementation | From Repo: https://github.com/amrk000/Data-Structures-Implementation-Java-CSharp
//by Amrk000 - No license or attribution required

//generic single LinkedList of type <T> - implementing iterable is optional
public class LinkedList<T> implements Iterable<T>{
    private Node<T> head, tail;
    private int size;

    public LinkedList(){
        size = 0;
    }

    //Node element that carry data and reference to next node in linked list chain
    private static class Node<T>{
        private T data;
        private Node<T> next;

        public Node(T data){
            this.data = data;
            this.next = null;
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

    //get node by index
    private Node<T> getNode(int index){
        if(index>=size) throw new IndexOutOfBoundsException("Index "+index+" out of bounds for length "+size);

        //make a node that starts at head and traverse on all nodes one by one to reach target
        int i = 0;
        Node<T> traverse = head;
        do {
            if (i == index) break; //found: exit loop
            traverse = traverse.next;
            i++;
        } while (traverse.next != null);

        return traverse;
    }

    //get data by index
    public T get(int index){
        return getNode(index).data;
    }

    //add new data node after last node using tail
    public void add(T element){
        if(isEmpty()) head = tail = new Node<>(element);
        else {
            Node<T> newNode = new Node<>(element);
            tail.next = newNode;
            tail = newNode;
        }

        size++;
    }

    //add new data node before first node using head
    public void addFirst(T element){
        Node<T> newNode = new Node<>(element);
        newNode.next = head;
        head = newNode;
        size++;
    }

    //add new data node at specific index
    public void add(T element, int index){
        if(index == 0) addFirst(element); //if index= 0 add at head
        else if(index == size-1) add(element); //if index= lastNode add at tail
        else {
            Node<T> newNode = new Node<>(element);
            Node<T> temp = getNode(index - 1); //get the node before position to insert new node after

            newNode.next = temp.next;
            temp.next = newNode;

            size++;
        }
    }

    //delete first node
    public void removeFirst(){
        head = head.next; //move head to next node - first node ref has no object and will be deleted
        size--;
    }

    //delete last node
    public void removeLast(){
        Node<T> temp = getNode(size-2); //get the node before last one
        tail = temp; //move tail to the node before target
        temp.next = null;
        size--;
    }

    //delete node by its value
    public void remove(T element){
        if(isEmpty()) return;

        if(head.data == element) removeFirst(); //if data found at head delete first
        else if(tail.data == element) removeLast(); //if data found at tail delete last
        else if(head.next!=null) {
            //make a node that starts at head and traverse on all nodes one by one to reach target
            Node<T> traverse = head;
            do {
                //Target found next to current node
                if (traverse.next.data == element) {
                    traverse.next = traverse.next.next;
                    size--;
                    break;
                }
                traverse = traverse.next;
            } while (traverse.next != null);
        }
    }

    //delete node by its index
    public void removeAt(int index){
        if(isEmpty()) return;

        if(index == 0) removeFirst(); //if index = 0 at head delete first
        else if(index == size-1) removeLast(); //if index = lastNode at tail delete last
        else {
            Node<T> temp = getNode(index - 1); //get the node before position to delete the node after
            Node<T> target = temp.next;
            temp.next = target.next; //target node is unlinked and will be removed by garbage collector
            size--;
        }
    }

    //clear all elements
    public void clear(){
        head = tail = null; //reset chain
        size = 0;
        System.gc(); // activate garbage collector to delete old chain nodes
    }

    //return elements as string
    public String toString(){
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.append("[");
        for(T element : this) stringBuilder.append(element.toString() + ",");
        if(!isEmpty()) stringBuilder.deleteCharAt(stringBuilder.length()-1);
        stringBuilder.append("]");
        return stringBuilder.toString();
    }

    //foreach iterator
    @Override
    public Iterator<T> iterator() {
        return new Iterator<T>() {
            Node<T> traverse = head;

            @Override
            public boolean hasNext() {
                return traverse!=null;
            }

            @Override
            public T next() {
                T data = traverse.data;
                traverse = traverse.next;
                return data;
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
