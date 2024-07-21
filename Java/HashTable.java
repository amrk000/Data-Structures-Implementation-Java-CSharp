package org.example;

//HashTable implementation | From Repo: https://github.com/amrk000/Data-Structures-Implementation-Java-CSharp
//by Amrk000 - No license or attribution required

//generic HashTable of type <T>
//IMPORTANT: This HashTable is based on separate chaining approach (table bucket is an array of linked lists)
public class HashTable<T> {
    private Node<T>[] tableBucket;
    private int size;
    private final float optimalLoadFactor = 0.75F; //optimal load factor value

    public HashTable(){
        tableBucket = new Node[10];
        size = 0;
    }

    //Node element that carry key, value and reference to next node in linked list chain
    private static class Node<T> {
        private String key;
        private T value;
        private Node<T> next;

        public Node(String key, T value) {
            this.key = key;
            this.value = value;
            next = null;
        }
    }

    //return number of elements
    public int size(){
        return size;
    }

    //check if it's empty
    public boolean isEmpty(){
        return size == 0;
    }

    //return table bucket size
    public int bucketSize(){
        return tableBucket.length;
    }

    //check current table load factor
    public float loadFactor(){
        return (float) size / bucketSize();
    }

    //check if current load factor isn't bigger than optimal to determine if rehashing is needed
    public boolean isLoadFactorOptimal(){
        return loadFactor() <= optimalLoadFactor;
    }

    //return hash code value which made from string key (hashcode is an index of  table bucket array)
    private int hashFunction(String key){
        long keyNumValue = 0;
        for(char c : key.toCharArray()) keyNumValue+=c;
        return (int) (keyNumValue % bucketSize()); //always returns code that doesn't exceed bucket index range
    }

    //increase table bucket size and set new hash codes to elements then reposition them
    private void rehash(){
        Node<T>[] oldBucket = tableBucket; //get temporary copy of table bucket
        tableBucket = new Node[bucketSize()*2]; //set new larger bucket
        size = 0;

        for(int i=0; i<oldBucket.length; i++){
            if(oldBucket[i] == null) continue; //if there is no linked list there continue

            Node<T> current = oldBucket[i]; //get linked list head from old bucket

            //traverse each linked list
            while (current!=null){
                set(current.key, current.value); //add elements from old bucket to new bucket
                current = current.next;
            }
        }
    }

    //add or update element by key
    public void set(String key, T value){
        int hashCode = hashFunction(key); //get hash code from key

        if(tableBucket[hashCode] == null) tableBucket[hashCode] = new Node<>(key, value); //if hash index is empty add head node of linked list
        else {
            Node<T> current = tableBucket[hashCode]; //get head node of linked list

            while (current != null){
                //if key found update value and exit
                if(current.key.equals(key)){
                    current.value = value;
                    return;
                }
                //if last is reached add new node
                if(current.next == null){
                    current.next = new Node<>(key, value);
                    break;
                }

                current = current.next;
            }
        }

        size++;

        if(!isLoadFactorOptimal()) rehash(); //check if rehashing is required
    }

    //get element by key
    public T get(String key){
        int hashCode = hashFunction(key); //get hash code from key

        Node<T> current = tableBucket[hashCode]; //get head node of linked list

        while (current != null){
            if(current.key.equals(key)) return current.value; //element found
            current = current.next;
        }

        return null;
    }

    //remove node by key
    public void remove(String key){
        int hashCode = hashFunction(key); //get hash code from key

        Node<T> current = tableBucket[hashCode]; //get head node of linked list

        if(current.key.equals(key)){
            tableBucket[hashCode] = current.next; //if key equals head remove head
            size--;
            return;
        }

        //search in linked list
        do{
            if(current.next.key.equals(key)) {
                current.next = current.next.next; //remove next of current node if equals target
                size--;
                return;
            }
            current = current.next;
        }
        while (current.next != null);
    }

    //return keys as string
    public String getKeys(){
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.append("[");

        for(Node<T> hashIndex : tableBucket){
            Node<T> current = hashIndex;
            while (current!=null) {
                stringBuilder.append(current.key + ",");
                current = current.next;
            }
        }

        if(!isEmpty()) stringBuilder.deleteCharAt(stringBuilder.length()-1);
        stringBuilder.append("]");
        return stringBuilder.toString();
    }

    //return values as string
    public String getValues(){
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.append("[");

        for(Node<T> hashIndex : tableBucket){
            Node<T> current = hashIndex;
            while (current!=null) {
                stringBuilder.append(current.value.toString() + ",");
                current = current.next;
            }
        }

        if(!isEmpty()) stringBuilder.deleteCharAt(stringBuilder.length()-1);
        stringBuilder.append("]");
        return stringBuilder.toString();
    }

    //return key,value array as string
    public String toString(){
        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.append("[");

        for(Node<T> hashIndex : tableBucket){
            Node<T> current = hashIndex;
            while (current!=null) {
                stringBuilder.append("{"+current.key + "," + current.value.toString() + "},");
                current = current.next;
            }
        }

        if(!isEmpty()) stringBuilder.deleteCharAt(stringBuilder.length()-1);
        stringBuilder.append("]");
        return stringBuilder.toString();
    }

    //clear all elements
    public void clear(){
        size = 0;
        tableBucket = new Node[10];
        System.gc(); // activate garbage collector to delete old array
    }
}
