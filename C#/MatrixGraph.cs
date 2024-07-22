using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Adjacency Matrix Graph implementation | From Repo: https://github.com/amrk000/Data-Structures-Implementation-Java-CSharp
//by Amrk000 - No license or attribution required

//generic Graph of type <T> - implementing iterable is optional
class MatrixGraph<T> : IEnumerable<T>
{
    private int[,] adjacencyMatrix; //matrix that represents edges between vertices
    private T[] vertexData; //array that holds vertices data
    private bool directed;
    private int size;
    public MatrixGraph(int capacity, bool directed)
    {
        adjacencyMatrix = new int [capacity,capacity];
        vertexData = new T[capacity];
        this.directed = directed;
        size = 0;
    }

    //return number of elements
    public int Size()
    {
        return size;
    }

    //return number of max elements that it can carry
    public int Capacity()
    {
        return vertexData.Length;
    }

    //check if it's empty
    public bool isEmpty()
    {
        return size == 0;
    }

    //add data vertex to Graph
    public void Add(T data)
    {
        if (size == Capacity()) throw new ArgumentOutOfRangeException("Graph is full - capacity:" + Capacity());
        vertexData[size++] = data;
    }

    //return element by index
    public T Get(int index)
    {
        if (index >= size) throw new ArgumentOutOfRangeException("Index " + index + " out of bounds for length " + size);
        return vertexData[index];
    }

    //return index of element
    public int IndexOf(T data)
    {
        int i = 0;
        while (i < size)
        {
            if (Comparer.ReferenceEquals(vertexData[i], data)) break;
            i++;
        }

        return i < size ? i : -1; // if i < size then index of item found so return i else it's not found so return -1
    }

    //check between two data vertices
    public bool HasEdge(T from, T to)
    {
        int currentVertexIndex = IndexOf(from);
        int targetVertexIndex = IndexOf(to);

        return adjacencyMatrix[currentVertexIndex,targetVertexIndex] > 0;
    }

    //connect weighted edge between two data vertices
    public void SetEdge(T from, T to, int weight)
    {
        if (weight < 0) throw new ArithmeticException("Edge weight should be positive");

        int currentVertexIndex = IndexOf(from);
        int targetVertexIndex = IndexOf(to);

        if (currentVertexIndex < 0 || targetVertexIndex < 0) return;

        adjacencyMatrix[currentVertexIndex,targetVertexIndex] = weight; //connect current to target
        if (!directed) adjacencyMatrix[targetVertexIndex,currentVertexIndex] = weight; //if not directed graph connect target to current
    }

    //connect edge between two data vertices
    public void SetEdge(T from, T to)
    {
        SetEdge(from, to, 1); // if no weight is specified default is 1
    }

    //remove edge between two data vertices
    public void RemoveEdge(T from, T to)
    {
        SetEdge(from, to, 0); // set weight to 0
    }

    //get edge between two data vertices
    public int GetEdgeWeight(T from, T to)
    {
        int currentVertexIndex = IndexOf(from);
        int targetVertexIndex = IndexOf(to);

        return adjacencyMatrix[currentVertexIndex,targetVertexIndex];
    }

    //delete all elements
    public void Clear()
    {
        adjacencyMatrix = new int[Capacity(),Capacity()]; //set new matrix
        vertexData = new T[Capacity()]; //set new array
        size = 0;
        GC.Collect(); // activate garbage collector to delete old array
    }

    //return graph as string
    public String ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("[");

        for (int i = 0; i < size; i++)
        {
            stringBuilder.Append(vertexData[i].ToString() + " -> {");

            for (int j = 0; j < size; j++)
            {
                int edge = adjacencyMatrix[i,j];
                if (edge > 0)
                {
                    stringBuilder.Append(vertexData[j].ToString() + "(" + edge + "),");
                }
            }
            stringBuilder.Append("},");
        }

        if (!isEmpty()) stringBuilder.Remove(stringBuilder.Length - 1, 1);
        stringBuilder.Append("]");
        return stringBuilder.ToString();
    }

    //foreach iterator
    public IEnumerator<T> GetEnumerator()
    {
        int i = 0;
        while (i < size) yield return vertexData[i++];
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}
