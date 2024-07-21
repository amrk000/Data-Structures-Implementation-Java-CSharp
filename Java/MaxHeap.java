package org.example;

//MaxHeap using array implementation | From Repo: https://github.com/amrk000/Data-Structures-Implementation-Java-CSharp
//by Amrk000 - No license or attribution required

//generic fixed MaxHeap of type <T>
public class MaxHeap<T> {
    private Node[] array;
    private int size;

    //Node element that carry data and priority of element
    private static class Node<T>{
        private int priority;
        private T data;

        public Node(int priority, T data){
            this.priority = priority;
            this.data = data;
        }
    }

    public MaxHeap(int capacity) {
        this.size = 0;
        array = new Node[capacity];
    }

    //return number of elements
    public int size() {
        return size;
    }

    //return number of max elements that it can carry
    public int capacity() {
        return array.length;
    }

    //check if it's empty
    public boolean isEmpty(){
        return size() == 0;
    }

    //get node's parent index in array
    private int getParentIndex(int nodeIndex) {
        return (nodeIndex-1)/2; //if heap elements starts at 1 it  will be: (nodeIndex)/2
    }

    //get node's left child index in array
    private int getLeftChildIndex(int nodeIndex) {
        return (2*nodeIndex)+1; //if heap elements starts at 1 it  will be: (2*nodeIndex)
    }

    //get node's right child index in array
    private int getRightChildIndex(int nodeIndex) {
        return (2*nodeIndex)+2; //if heap elements starts at 1 it  will be: (2*nodeIndex)+1
    }

    //bubble up the element as long as it's bigger than parent
    private void heapifyUp(int index) {
        Node temp = array[index]; //hold current element value

        while(index>0 && temp.priority > array[getParentIndex(index)].priority){
            array[index] = array[getParentIndex(index)]; //move parent's value down to current element
            index = getParentIndex(index); //go up
        }

        array[index] = temp; //set current position value to element value
    }

    //bubble down the element as long as it's smaller than children
    private void heapifyDown(int index) {
        Node temp = array[index]; //hold current element value
        int targetChildIndex;

        while(index < size/2){
            //calculate diff between parent priority and the children
            int leftDiff = array[getLeftChildIndex(index)].priority - temp.priority;
            int rightDiff = array[getRightChildIndex(index)].priority - temp.priority;

            //diffs == 0 or less which means that node is bigger than both children break
            if(leftDiff <= 0 && rightDiff <= 0) break;

            //get target child index
            if(leftDiff > rightDiff) targetChildIndex = getLeftChildIndex(index);
            else targetChildIndex = getRightChildIndex(index);

            //move child value up to current element
            array[index] = array[targetChildIndex];
            index = targetChildIndex; //go down
        }

        array[index] = temp; //set current position value to element value
    }

    //add element to heap
    public void add(int priority, T element) {
        if(size() == capacity()) throw new IndexOutOfBoundsException("Heap is full - capacity:" + capacity());

        Node<T> node = new Node<>(priority, element);
        int elementIndex = size;
        array[size++] = node;
        heapifyUp(elementIndex); //move up node
    }

    //delete max value (at root) and return data
    public T remove(){
        if(isEmpty()) return null;

        Node max = array[0];
        array[0] = array[size-1]; //swap max value node with last node in heap
        size--;
        heapifyDown(0); //move down node to a suitable position
        return (T) max.data;
    }

    //check the front of heap
    public T peekFront(){
        if(isEmpty()) return null;
        return (T) array[0].data;
    }

    //delete all elements
    public void clear(){
        size = 0;
        array = new Node[capacity()];
        System.gc(); // activate garbage collector to delete old array
    }

}
