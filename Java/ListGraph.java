package org.example;

import java.util.*;
import java.util.function.Consumer;
//Adjacency List Graph implementation | From Repo: https://github.com/amrk000/Data-Structures-Implementation-Java-CSharp
//by Amrk000 - No license or attribution required

//generic Graph of type <T> - implementing iterable is optional
public class ListGraph<T> implements Iterable<T>{
    HashMap<T, LinkedList<Edge<T>> > adjacencyList; //Hashmap is more efficient than using linked list of linked lists
    private boolean directed;
    private int size;

    public ListGraph(boolean directed){
        adjacencyList = new HashMap<>();
        this.directed = directed;
        size = 0;
    }

    //Edge that carry weight and target vertex
    private static class Edge<T>{
        private int weight;
        private T targetVertex;

        public Edge(T data, int weight){
            this.targetVertex = data;
            this.weight = weight;
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

    //add data vertex to Graph
    public void add(T data){
        adjacencyList.put(data, new LinkedList<>());
        size++;
    }

    //remove data vertex from Graph
    public void remove(T data){
        for(T vertex : adjacencyList.keySet()) removeEdge(vertex,data);
        adjacencyList.remove(data);
        size--;
    }

    //return index of element
    public int indexOf(T data){
        int i=0;
        for(T element : adjacencyList.keySet()){
            if(element == data) break;
            i++;
        }

        return i<size? i : -1; // if i < size then index of item found so return i else it's not found so return -1
    }

    //check edge between two data vertices
    public boolean hasEdge(T from, T to){
        //check all from vertex edges
        for (Edge<T> edge : adjacencyList.get(from)){
            if(edge.targetVertex == to) return true;
        }

        return false;
    }

    //connect weighted edge between two data vertices
    public void setEdge(T from, T to, int weight){
        if(weight<=0) throw new ArithmeticException("Edge weight should be positive number");

        //check if edge is new or not
        if(hasEdge(from, to)){
            //edit edge
            for (Edge<T> edge : adjacencyList.get(from)){
                if(edge.targetVertex == to){
                    edge.weight = weight;
                    break;
                }
            }
        }
        else {
            //create new edge
            adjacencyList.get(from).add(new Edge<>(to, weight)); //connect current to target
            if (!directed && from!= to) adjacencyList.get(to).add(new Edge<>(from, weight)); //if not directed graph connect target to current
        }

    }

    //connect edge between two data vertices
    public void setEdge(T from, T to){
        setEdge(from, to, 1); // if no weight is specified default is 1
    }

    //remove edge between two data vertices
    public void removeEdge(T from, T to){
        for (Edge<T> edge : adjacencyList.get(from)){
            if(edge.targetVertex == to){
                adjacencyList.get(from).remove(edge);
                break;
            }
        }
    }

    //get edge between two data vertices
    public int getEdgeWeight(T from, T to){
        for (Edge<T> edge : adjacencyList.get(from)){
            if(edge.targetVertex == to) return edge.weight;
        }

        return 0;
    }

    //delete all elements
    public void clear(){
        adjacencyList.clear();
        size = 0;
        System.gc(); // activate garbage collector to delete old array
    }

    //return graph as string
    public String toString(){
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.append("[");

        for(T vertex : adjacencyList.keySet()){
            stringBuilder.append(vertex.toString() + " -> {");

            for(Edge<T> edge : adjacencyList.get(vertex)){
                stringBuilder.append(edge.targetVertex.toString() +"("+ edge.weight +"),");
            }
            stringBuilder.append("},");
        }

        if(!isEmpty()) stringBuilder.deleteCharAt(stringBuilder.length()-1);
        stringBuilder.append("]");
        return stringBuilder.toString();
    }

    //Depth First Search (DFS) Traversal that returns list of visited vertices
    public ArrayList<T> getDFSTraversalList(T startVertex) {
        ArrayList<T> visited = new ArrayList<>(); //list of visited vertices
        Stack<T> stack = new Stack<>(); //stack used for DFS traversal

        stack.push(startVertex);

        while (!stack.isEmpty()) {
            T vertex = stack.pop();

            if (!visited.contains(vertex)) {
                visited.add(vertex);
                for (Edge<T> edge : adjacencyList.get(vertex)) stack.push(edge.targetVertex);
            }

        }

        return visited;
    }

    //Breadth First Search (BFS) Traversal that returns list of visited vertices
    public ArrayList<T> getBFSTraversalList(T startVertex) {
        ArrayList<T> visited = new ArrayList<>(); //list of visited vertices
        ArrayList<T> queue = new ArrayList<>(); //queue used for BFS traversal

        queue.addLast(startVertex); //adds to rear
        visited.add(startVertex); //adds to rear

        while (!queue.isEmpty()) {
            T vertex = queue.removeFirst(); //remove from front

            for (Edge<T> edge : adjacencyList.get(vertex)) {

                if (!visited.contains(edge.targetVertex)) {
                    visited.add(edge.targetVertex);
                    queue.add(edge.targetVertex);
                }
            }
        }

        return visited;
    }

    //foreach iterator
    @Override
    public Iterator<T> iterator() {
        return adjacencyList.keySet().iterator();
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
