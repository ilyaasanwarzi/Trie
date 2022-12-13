﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrieTernaryTree
{
    public interface IContainer<T>
    {
        void MakeEmpty();
        bool Empty();
        int Size();
    }

    //-------------------------------------------------------------------------

    public interface ITrie<T> : IContainer<T>
    {
        bool Insert(string key, T value);
        T Value(string key);
    }

    //-------------------------------------------------------------------------

    class Trie<T> : ITrie<T>
    {
        private Node root;                 // Root node of the Trie
        private int size;                  // Number of values in the Trie

        class Node
        {
            public char ch;                // Character of the key
            public T value;                // Value at Node; otherwise default
            public Node low, middle, high; // Left, middle, and right subtrees

            // Node
            // Creates an empty Node
            // All children are set to null
            // Time complexity:  O(1)

            public Node(char ch)
            {
                this.ch = ch;
                value = default(T);
                low = middle = high = null;
            }
        }

        // Trie
        // Creates an empty Trie
        // Time complexity:  O(1)
        //start a publie trie
        public Trie()
        {
            MakeEmpty();
            size = 0;
        }

        //start public bool
        public bool Remove(string key){
            return Remove(ref root, key, 0);
        }
        //start private bool

        private bool Remove(ref Node p, string key, int i){

            while (p != null)
            {
                
                if (key[i] < p.ch)
                    p = p.low;

                          
                else if (key[i] > p.ch)
                    p = p.high;

                else
                {
                    
                    if (++i == key.Length){
                        p.value = default(T);
                        return true;
                    }
                      
                    p = p.middle;
                }
            }
            return false;
        }

        // Contains
        // Returns true if the given key is found in the Trie; false otherwise
        // Time complexity:  O(d) where d is the depth of the trie
        //stat public bool with string key
        public bool Contains(string key)
        {
            int i = 0;
            Node p = root;

            while (p != null)
            {
                // Search for current character of the key in left subtree
                if (key[i] < p.ch)
                    p = p.low;

                // Search for current character of the key in right subtree           
                else if (key[i] > p.ch)
                    p = p.high;

                else // if (p.ch == key[i])
                {
                    // Return true if the key is associated with a non-default value; false otherwise 
                    if (++i == key.Length){
                        return !p.value.Equals(default(T));
                    }
                    // Move to next character of the key in the middle subtree   
                    p = p.middle;
                }
            }
            return false;        // Key too long  
        }

        // Public Insert
        // Calls the private Insert which carries out the actual insertion
        // Returns true if successful; false otherwise
         //start public bool with string
        public bool Insert(string key, T value)
        {
            return Insert(ref root, key, 0, value);
        }

        // Private Insert
        // Inserts the key/value pair into the Trie
        // Returns true if the insertion was successful; false otherwise
        // Note: Duplicate keys are ignored
        // Time complexity:  O(n+L) where n is the number of nodes and 
        //                                L is the length of the given key
        //start bool with references, strings and values as well as if else statements
        private bool Insert(ref Node p, string key, int i, T value)
        {
            if (p == null)
                p = new Node(key[i]);

            // Current character of key inserted in left subtree
            if (key[i] < p.ch)
                return Insert(ref p.low, key, i, value);

            // Current character of key inserted in right subtree
            else if (key[i] > p.ch)
                return Insert(ref p.high, key, i, value);

            else if (i + 1 == key.Length)
            // Key found
            {
                // But key/value pair already exists
                if (!p.value.Equals(default(T))) //this means there is already a value at that key, and that is NOT allowed.
                    return false;
                else
                {
                    // Place value in node
                    p.value = value;
                    size++;
                    return true;
                }
            }

            else
                // Next character of key inserted in middle subtree
                return Insert(ref p.middle, key, i + 1, value);
        }

        // Value
        // Returns the value associated with a key; otherwise default
        // Time complexity:  O(d) where d is the depth of the trie
        //start public valie for string
        public T Value(string key)
        {
            int i = 0;
            Node p = root;

            while (p != null)
            {
                // Search for current character of the key in left subtree
                if (key[i] < p.ch)
                    p = p.low;

                // Search for current character of the key in right subtree           
                else if (key[i] > p.ch)
                    p = p.high;

                else // if (p.ch == key[i])
                {
                    // Return the value if all characters of the key have been visited 
                    if (++i == key.Length)
                        return p.value;

                    // Move to next character of the key in the middle subtree   
                    p = p.middle;
                }
            }
            return default(T);   // Key too long
        }

        // MakeEmpty
        // Creates an empty Trie
        // Time complexity:  O(1)
        //start public void for time complexity
        public void MakeEmpty()
        {
            root = null;
        }

        // Empty
        // Returns true if the Trie is empty; false otherwise
        // Time complexity:  O(1)
        /start public bool
        public bool Empty()
        {
            return root == null;
        }

        // Size
        // Returns the number of Trie values
        // Time complexity:  O(1)
        //start public int and return size
        public int Size()
        {
            return size;
        }

        // Public Print
        // Calls private Print to carry out the actual printing
        //start public void to output key values
        public void Print()
        {
            Print(root, "");
        }

        // Private Print
        // Outputs the key/value pairs ordered by keys
        // Time complexity:  O(n) where n is the number of nodes
        //start private void with node and string
        private void Print(Node p, string key)
        {
            if (p != null)
            {
                Print(p.low, key);
                if (!p.value.Equals(default(T))){
                    Console.WriteLine(key + p.ch + " " + p.value);
                }
                Print(p.middle, key + p.ch);
                Print(p.high, key);
            }
        }
    }
    //start a new class program
    class Program
    {
        static void Main(string[] args)
        {
            Trie<int> T = new Trie<int>();

            T.Insert("bag", 10);
            T.Insert("bat", 20);
            T.Insert("cab", 70);
            T.Insert("bagel", 30);
            T.Insert("beet", 40);
            T.Insert("abc", 60);

            T.Print();
            Console.WriteLine(T.Size());

            Console.WriteLine(T.Value("abc"));
            Console.WriteLine(T.Value("beet"));
            Console.WriteLine(T.Value("a"));

            Console.WriteLine(T.Contains("baet"));
            Console.WriteLine(T.Contains("beet"));
            Console.WriteLine(T.Contains("abc"));

            Console.WriteLine(T.Remove("beet"));
            
            Console.WriteLine(T.Contains("beet"));

            T.Print();
            Console.ReadKey();
        }
            //console writelines to output whatever should be printed
    }
}
