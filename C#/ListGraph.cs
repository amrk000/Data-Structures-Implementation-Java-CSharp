using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Adjacency List Graph implementation | From Repo: https://github.com/amrk000/Data-Structures-Implementation-Java-CSharp
//by Amrk000 - No license or attribution required

//generic Graph of type <T> - implementing iterable is optional
class ListGraph<T> : IEnumerable<T>
{
    Dictionary<T, LinkedList<Edge<T>>> adjacencyList; //Hashmap (Dictionary) is more efficient than using linked list of linked lists
    private bool directed;
    private int size;
    public ListGraph(bool directed)
    {
        adjacencyList = new Dictionary<T, LinkedList<Edge<T>>>();
        this.directed = directed;
        size = 0;
    }

    //Edge that carry weight and target vertex
    internal class Edge<T>
    {
        internal int weight;
        internal T targetVertex;

        public Edge(T data, int weight)
        {
            this.targetVertex = data;
            this.weight = weight;
        }
    }

    //return number of elements
    public int Size()
    {
        return size;
    }

    //check if it's empty
    public bool IsEmpty()
    {
        return size == 0;
    }

    //add data vertex to Graph
    public void Add(T data)
    {
        adjacencyList.Add(data, new LinkedList<Edge<T>>());
        size++;
    }

    //remove data vertex from Graph
    public void Remove(T data)
    {
        foreach (T vertex in adjacencyList.Keys) RemoveEdge(vertex, data);
        adjacencyList.Remove(data);
        size--;
    }

    //return index of element
    public int IndexOf(T data)
    {
        int i = 0;
        foreach (T element in adjacencyList.Keys)
        {
            if (Comparer.ReferenceEquals(element, data)) break;
            i++;
        }

        return i < size ? i : -1; // if i < size then index of item found so return i else it's not found so return -1
    }

    //check edge between two data vertices
    public bool HasEdge(T from, T to)
    {
        //check all from vertex edges
        foreach (Edge<T> edge in adjacencyList[from])
        {
            if (Comparer.ReferenceEquals(edge.targetVertex, to)) return true;
        }

        return false;
    }

    //connect weighted edge between two data vertices
    public void SetEdge(T from, T to, int weight)
    {
        if (weight <= 0) throw new ArithmeticException("Edge weight should be positive number");

        //check if edge is new or not
        if (HasEdge(from, to))
        {
            //edit edge
            foreach (Edge<T> edge in adjacencyList[from])
            {
                if (Comparer.ReferenceEquals(edge.targetVertex, to))
                {
                    edge.weight = weight;
                    break;
                }
            }
        }
        else
        {
            //create new edge
            adjacencyList[from].AddLast(new Edge<T>(to, weight)); //connect current to target
            if (!directed && !Comparer.ReferenceEquals(from, to)) adjacencyList[to].AddLast(new Edge<T>(from, weight)); //if not directed graph connect target to current
        }

    }

    //connect edge between two data vertices
    public void SetEdge(T from, T to)
    {
        SetEdge(from, to, 1); // if no weight is specified default is 1
    }

    //remove edge between two data vertices
    public void RemoveEdge(T from, T to)
    {
        foreach (Edge<T> edge in adjacencyList[from])
        {
            if (Comparer.ReferenceEquals(edge.targetVertex, to))
            {
                adjacencyList[from].Remove(edge);
                break;
            }
        }
    }

    //get edge between two data vertices
    public int GetEdgeWeight(T from, T to)
    {
        foreach (Edge<T> edge in adjacencyList[from])
        {
            if (Comparer.ReferenceEquals(edge.targetVertex, to)) return edge.weight;
        }

        return 0;
    }

    //delete all elements
    public void Clear()
    {
        adjacencyList.Clear();
        size = 0;
        GC.Collect(); // activate garbage collector to delete old array
    }

    //return graph as string
    public String ToString()
    {
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.Append("[");

        foreach (T vertex in adjacencyList.Keys)
        {
            stringBuilder.Append(vertex.ToString() + " -> {");

            foreach (Edge<T> edge in adjacencyList[vertex])
            {
                stringBuilder.Append(edge.targetVertex.ToString() + "(" + edge.weight + "),");
            }
            stringBuilder.Append("},");
        }

        if (!IsEmpty()) stringBuilder.Remove(stringBuilder.Length - 1, 1);
        stringBuilder.Append("]");
        return stringBuilder.ToString();
    }

    //Depth First Search (DFS) Traversal that returns list of visited vertices
    public ArrayList GetDFSTraversalList(T startVertex)
    {
        ArrayList visited = new ArrayList(); //list of visited vertices
        Stack<T> stack = new Stack<T>(); //stack used for DFS traversal

        stack.Push(startVertex);

        while (stack.Count > 0)
        {
            T vertex = stack.Pop();

            if (!visited.Contains(vertex))
            {
                visited.Add(vertex);
                foreach (Edge<T> edge in adjacencyList[vertex]) stack.Push(edge.targetVertex);
            }

        }

        return visited;
    }

    //Breadth First Search (BFS) Traversal that returns list of visited vertices
    public ArrayList GetBFSTraversalList(T startVertex)
    {
        ArrayList visited = new ArrayList(); //list of visited vertices
        Queue<T> queue = new Queue<T>(); //queue used for BFS traversal

        queue.Enqueue(startVertex); //adds to rear
        visited.Add(startVertex); //adds to rear

        while (queue.Count > 0)
        {
            T vertex = queue.Dequeue(); //remove from front

            foreach (Edge<T> edge in adjacencyList[vertex])
            {

                if (!visited.Contains(edge.targetVertex))
                {
                    visited.Add(edge.targetVertex);
                    queue.Enqueue(edge.targetVertex);
                }
            }
        }

        return visited;
    }

    //foreach iterator
    public IEnumerator<T> GetEnumerator()
    {
        return adjacencyList.Keys.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }
}
