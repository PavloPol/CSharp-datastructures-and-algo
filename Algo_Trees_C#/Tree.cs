using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo_Trees_C_
{
    public class Tree
    {
        public class Node
        {
            internal int value;
            internal Node? left = null;
            internal Node? right = null;

            internal Node(int value)
            {
                this.value = value;
            }
        }

        private Node? root = null;

        public Tree() { }

        public Tree(int value)
        {
            root = new Node(value);
        }

        public void Insert(int value)
        {
            if(root == null)
            {
                root = new Node(value);
            }
            else
            {
                Insert(root, value);
            }
        }

        private void Insert(Node node, int value)
        {
            if(value < node.value)
            {
                if(node.left == null)
                {
                    node.left = new Node(value);
                }
                else
                {
                    Insert(node.left, value);
                }
            }
            else if(value >= node.value) 
            {
                if(node.right == null)
                {
                    node.right = new Node(value);
                }
                else
                {
                    Insert(node.right, value);
                }
            }
        }

        public Node Search(int value)
        {
            return Search(root, value);
        }

        private Node Search(Node node, int value)
        {
            if (node == null) return null;
            if (node.value == value) return node;
            return (value < node.value) ? Search(node.left, value) : Search(node.right, value);
        }

        public Node GetMin()
        {
            return GetMin(root);
        }

        private Node GetMin(Node node)
        {
            if (node == null) return null;
            if (node.left == null) return node;
            return GetMin(node.left);
        }

        public Node GetMax()
        {
            return GetMax(root);
        }

        private Node GetMax(Node node)
        {
            if (node == null) return null;
            if (node.right == null) return node;
            return GetMax(node.right);
        }

        public Node Delete(int value)
        {
            return Delete(root, value);
        }

        private Node Delete(Node node, int value)
        {
            if (node == null) return null;
            else if (value < node.value) node.left = Delete(node.left, value);
            else if (value > node.value) node.right = Delete(node.right, value);
            else
            {
                if(node.left == null || node.right == null)
                {
                    node = (node.left == null) ? node.right : node.left;
                }
                else
                {
                    Node maxLnLeft = GetMax(node.left);
                    node.value = maxLnLeft.value;
                    node.left = Delete(node.left, maxLnLeft.value);
                }
            }
            return node;
        }

        public void PrintTree()
        {
            PrintTree(root);
            Console.WriteLine();
        }

        private void PrintTree(Node node)
        {
            if (node == null) return;
            PrintTree(node.left);
            Console.Write(node.value);
            PrintTree(node.right);
        }

        public void BFS()
        {
            if (root == null)
            {
                return;
            }

            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                Node node = queue.Dequeue();
                Console.Write(node.value + " ");

                if (node.left != null)
                {
                    queue.Enqueue(node.left);
                }

                if (node.right != null)
                {
                    queue.Enqueue(node.right);
                }
            }
            Console.WriteLine();
        }

        public void BFSL()
        {
            if (root == null)
            {
                return;
            }

            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(root);

            while (queue.Count > 0)
            {
                int levelSize = queue.Count;

                while (levelSize > 0)
                {
                    Node node = queue.Dequeue();
                    Console.Write(node.value + " ");

                    if (node.left != null)
                    {
                        queue.Enqueue(node.left);
                    }

                    if (node.right != null)
                    {
                        queue.Enqueue(node.right);
                    }

                    levelSize--;
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void DeleteTree()
        {
            DeleteTree(root);
        }

        private void DeleteTree(Node node)
        {
            if (node == null) return;
            DeleteTree(node.left);
            DeleteTree(node.right);
            Console.Write(node.value + " ");
        }
    }
}
