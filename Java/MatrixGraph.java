package org.example;

import java.util.Iterator;
import java.util.Spliterator;
import java.util.function.Consumer;

//Adjacency Matrix Graph implementation | From Repo: https://github.com/amrk000/Data-Structures-Implementation-Java-CSharp
//by Amrk000 - No license or attribution required

//generic Graph of type <T> - implementing iterable is optional
public class MatrixGraph <T> implements Iterable<T>{
    private int[][] adjacencyMatrix; //matrix that represents edges between vertices
    private T[] vertexData; //array that holds vertices data
    private boolean directed;
    private int size;

    public MatrixGraph(int capacity, boolean directed){
        adjacencyMatrix = new int[capacity][capacity];
        vertexData = (T[]) new Object[capacity];
        this.directed = directed;
        size = 0;
    }


    //return number of elements
    public int size() {
        return size;
    }

    //return number of max elements that it can carry
    public int capacity(){
        return vertexData.length;
    }

    //check if it's empty
    public boolean isEmpty(){
        return size==0;
    }

    //add data vertex to Graph
    public void add(T data){
        if(size == capacity()) throw new IndexOutOfBoundsException("Graph is full - capacity:" + capacity());
        vertexData[size++] = data;
    }

    //return element by index
    public T get(int index){
        if(index>=size) throw new IndexOutOfBoundsException("Index "+index+" out of bounds for length "+size);
        return vertexData[index];
    }

    //return index of element
    public int indexOf(T data){
        int i=0;
        while (i<size){
            if(vertexData[i] == data) break;
            i++;
        }

        return i<size? i : -1; // if i < size then index of item found so return i else it's not found so return -1
    }

    //check between two data vertices
    public boolean hasEdge(T from, T to){
        int currentVertexIndex = indexOf(from);
        int targetVertexIndex = indexOf(to);

        return adjacencyMatrix[currentVertexIndex][targetVertexIndex] > 0;
    }

    //connect weighted edge between two data vertices
    public void setEdge(T from, T to, int weight){
        if(weight<0) throw new ArithmeticException("Edge weight should be positive");

        int currentVertexIndex = indexOf(from);
        int targetVertexIndex = indexOf(to);

        if(currentVertexIndex < 0 || targetVertexIndex < 0) return;

        adjacencyMatrix[currentVertexIndex][targetVertexIndex] = weight; //connect current to target
        if(!directed) adjacencyMatrix[targetVertexIndex][currentVertexIndex] = weight; //if not directed graph connect target to current
    }

    //connect edge between two data vertices
    public void setEdge(T from, T to){
        setEdge(from, to, 1); // if no weight is specified default is 1
    }

    //remove edge between two data vertices
    public void removeEdge(T from, T to){
        setEdge(from, to, 0); // set weight to 0
    }

    //get edge between two data vertices
    public int getEdgeWeight(T from, T to){
        int currentVertexIndex = indexOf(from);
        int targetVertexIndex = indexOf(to);

        return adjacencyMatrix[currentVertexIndex][targetVertexIndex];
    }

    //delete all elements
    public void clear(){
        adjacencyMatrix = new int[capacity()][capacity()]; //set new matrix
        vertexData = (T[]) new Object[capacity()]; //set new array
        size = 0;
        System.gc(); // activate garbage collector to delete old array
    }

    //return graph as string
    public String toString(){
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.append("[");

        for(int i=0; i<size; i++){
            stringBuilder.append(vertexData[i].toString() + " -> {");

            for(int j=0; j<size; j++){
                int edge = adjacencyMatrix[i][j];
                if(edge>0){
                    stringBuilder.append(vertexData[j].toString() +"("+ edge +"),");
                }
            }
            stringBuilder.append("},");
        }

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
                return vertexData[i++];
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
