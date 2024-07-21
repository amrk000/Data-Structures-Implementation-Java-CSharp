using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//AVL Tree (Balanced Tree) implementation | From Repo: https://github.com/amrk000/Data-Structures-Implementation-Java-CSharp
//by Amrk000 - No license or attribution required

//generic AVLTree of type <T>
class AVLTree<T>
{
    private Node<T> root;
    private int size;

    public AVLTree()
    {
        root = null;
        size = 0;
    }

    //Node element that carry comparison key, height, data and references to next children in tree chain
    internal class Node<T>
    {
        internal T data;
        internal Node<T> leftChild;
        internal Node<T> rightChild;
        internal int key;
        internal int height; //avl tree uses height in each node

        public Node(int key, T data)
        {
            this.data = data;
            this.leftChild = null;
            this.rightChild = null;
            this.key = key;
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

    //return node height (height is the count of edges)
    private int NodeHeight(Node<T> node)
    {
        if (node == null) return -1;
        return node.height;
    }

    //returns tree height (edges count from root to the deepest leaf)
    public int Height()
    {
        if (root == null) return 0;
        return root.height;
    }

    //update node height value recursively
    private void UpdateNodeHeight(Node<T> node)
    {
        node.height = Math.Max(NodeHeight(node.leftChild), NodeHeight(node.rightChild)) + 1;
    }

    //calculates balance factor of node by comparing heights of sub trees
    //if balance factor > 1 right subtree is heavy, so it will be rotated left
    //if balance factor < -1 left subtree is heavy, so it will be rotated right
    //else it's balanced if == 0 or nearly balanced == 1 or -1
    private int BalanceFactor(Node<T> node)
    {
        return NodeHeight(node.rightChild) - NodeHeight(node.leftChild);
    }

    //rotate right
    private Node<T> RotateRight(Node<T> node)
    {
        Node<T> leftChild = node.leftChild;
        node.leftChild = leftChild.rightChild;

        leftChild.rightChild = node;

        UpdateNodeHeight(node);
        UpdateNodeHeight(leftChild);

        return leftChild;
    }

    //rotate left
    private Node<T> RotateLeft(Node<T> node)
    {
        Node<T> rightChild = node.rightChild;
        node.rightChild = rightChild.leftChild;

        rightChild.leftChild = node;

        UpdateNodeHeight(node);
        UpdateNodeHeight(rightChild);

        return rightChild;
    }

    //rebalance tree which starts at node by rotating left or right
    private Node<T> Rebalance(Node<T> node)
    {
        int nodeBf = BalanceFactor(node);

        if (nodeBf < -1)
        {
            if (BalanceFactor(node.leftChild) <= 0)
            {    
                node = RotateRight(node); // Case 1: rotate right
            }
            else
            {                                
                node.leftChild = RotateLeft(node.leftChild);
                node = RotateRight(node); // Case 2: rotate child left then rotate right
            }
        }
        else if (nodeBf > 1)
        {
            if (BalanceFactor(node.rightChild) >= 0)
            {    
                node = RotateLeft(node); // Case 3: rotate left
            }
            else
            {                                 
                node.rightChild = RotateRight(node.rightChild); // Case 4: rotate child right then rotate right
                node = RotateLeft(node);
            }
        }

        //child is rotated first:
        //Case 2: if left is heavy and child in left have a heavy right subtree
        //Case 4: or if right is heavy and child in right have a heavy left subtree

        return node;
    }

    //add new element to tree recursively
    private Node<T> RecursiveAddition(Node<T> currentNode, Node<T> newNode)
    {
        //if child is null last call in stack returns the newNode to be added to tree
        if (currentNode == null) return newNode;

        if (newNode.key < currentNode.key)
        {
            currentNode.leftChild = RecursiveAddition(currentNode.leftChild, newNode); //move to left child
        }
        else if (newNode.key > currentNode.key)
        {
            currentNode.rightChild = RecursiveAddition(currentNode.rightChild, newNode); //move to right child
        }
        else
        {
            //if key of new element equals key in of node in tree throw exception and it won't be added
            throw new IndexOutOfRangeException("There is already an item with key: " + currentNode.key + ", has Value: " + currentNode.data);
        }

        UpdateNodeHeight(currentNode); //update each node height
        return Rebalance(currentNode); //rebalance node before returning
    }

    //add new element
    public void Add(int key, T element)
    {
        root = RecursiveAddition(root, new Node<T>(key, element));
        size++;
    }

    //get the deepest node in left side in subtree to replace the node that will be removed while deleting data
    private Node<T> GetDeepestLeftNode(Node<T> node)
    {
        return node.leftChild == null ? node : GetDeepestLeftNode(node.leftChild);
    }

    //remove element from tree recursively
    private Node<T> RecursiveRemoval(Node<T> currentNode, int key)
    {

        if (currentNode == null) return null; //reached null child of leaf stop

        if (key < currentNode.key)
        {
            currentNode.leftChild = RecursiveRemoval(currentNode.leftChild, key); //move to left child
        }
        else if (key > currentNode.key)
        {
            currentNode.rightChild = RecursiveRemoval(currentNode.rightChild, key); //move to right child
        }
        else
        {

            //found target node
            if (currentNode.leftChild == null && currentNode.rightChild == null)
            {
                return null; //Case 1: sets current node to null if it's a leaf without children
            }
            else if (currentNode.rightChild == null)
            {
                return currentNode.leftChild; //Case 2: sets current node to left child if it has no right child
            }
            else if (currentNode.leftChild == null)
            {
                return currentNode.rightChild; //Case 3: sets current node to right child if it has no left child
            }

            //Case 4 (node has 2 children): replace the node value with the value of smallest child in right subtree
            Node<T> smallestNode = GetDeepestLeftNode(currentNode.rightChild);
            currentNode.data = smallestNode.data;
            currentNode.key = smallestNode.key;

            //remove the smallest child node in right subtree
            currentNode.rightChild = RecursiveRemoval(currentNode.rightChild, smallestNode.key);

            //Note: Case 4 another approach is to replace node value with the value of biggest child in left subtree
        }

        UpdateNodeHeight(currentNode); //update each node height
        return Rebalance(currentNode); //rebalance node before returning
    }

    //delete node by its key
    public void Remove(int key)
    {
        root = RecursiveRemoval(root, key);
        size--;
    }

    //Depth First Search (DFS) Traversals: inorder, preorder, postorder

    //traverse the tree inorder (Sorted order) recursively and return list of elements
    private void TraverseInOrder(Node<T> node, ArrayList output)
    {
        if (node != null)
        {
            TraverseInOrder(node.leftChild, output);
            output.Add(node.data);
            TraverseInOrder(node.rightChild, output);
        }
    }

    //return list of elements inorder (sorted)
    public ArrayList GetInorderList()
    {
        ArrayList output = new ArrayList();
        TraverseInOrder(root, output);
        return output;
    }

    //traverse the tree preorder recursively and return list of elements
    private void TraversePreorder(Node<T> node, ArrayList output)
    {
        if (node != null)
        {
            output.Add(node.data);
            TraversePreorder(node.leftChild, output);
            TraversePreorder(node.rightChild, output);
        }
    }

    //return list of elements preorder
    public ArrayList GetPreorderList()
    {
        ArrayList output = new ArrayList();
        TraversePreorder(root, output);
        return output;
    }

    //traverse the tree postorder recursively and return list of elements
    private void TraversePostorder(Node<T> node, ArrayList output)
    {
        if (node != null)
        {
            TraversePostorder(node.leftChild, output);
            TraversePostorder(node.rightChild, output);
            output.Add(node.data);
        }
    }

    //return list of elements postorder
    public ArrayList GetPostorderList()
    {
        ArrayList output = new ArrayList();
        TraversePostorder(root, output);
        return output;
    }

    //Breadth First Search (BFS) Traversal: LevelOrder
    //traverse the tree LevelOrder and return list of elements
    private void TraverseLevelOrder(ArrayList output)
    {
        if (root == null) return;

        ArrayList nodes = new ArrayList(); //levels queue
        nodes.Add(root);

        while (nodes.Count > 0)
        {
            Node<T> node = (Node<T>) nodes[0];
            nodes.RemoveAt(0);
            output.Add(node.data);

            if (node.leftChild != null) nodes.Add(node.leftChild);

            if (node.rightChild != null) nodes.Add(node.rightChild);
        }
    }

    //return list of elements LevelOrder
    public ArrayList GetLevelorderList()
    {
        ArrayList output = new ArrayList();
        TraverseLevelOrder(output);
        return output;
    }

    //Depth First Search (DFS)
    private Node<T> Search(Node<T> node, int targetKey)
    {
        if (node == null) return null; //reached null child of leaf stop

        if (targetKey < node.key) return Search(node.leftChild, targetKey); //if target key is less than current move left
        else if (targetKey > node.key) return Search(node.rightChild, targetKey); //else if target key is bigger than current move right

        //else return current node which has key that equals target key
        return node;
    }

    //get data by key
    public T get(int key)
    {
        Node<T> node = Search(root, key);
        if (node == null) return default;
        return node.data;
    }

    //check if tree contain element with key
    public bool contains(int key)
    {
        return get(key) != null;
    }

    //clear all elements
    public void clear()
    {
        size = 0;
        root = null;
        GC.Collect(); // activate garbage collector to delete old array
    }
}

