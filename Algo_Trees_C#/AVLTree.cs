using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algo_Trees_C_
{
    public class AVLTree
    {
        public class Node 
        {
            internal int value;
            internal int height = 0;
            internal Node? left = null;
            internal Node? right = null;

            public Node(int value)
            {
                this.value = value;
            }
        }

        private Node? root = null;

        public AVLTree() { }
        
        public AVLTree(int value)
        {
            root = new Node(value);
        }

        private int GetHeight(Node node)
        {
            return (node == null) ? -1 : node.height;
        }

        private void UpdateHeight(Node node)
        {
            node.height = Math.Max(GetHeight(node.left) , GetHeight(node.right)) + 1;
        }

        private int GetBalance(Node node)
        {
            return (node == null) ? 0 : GetHeight(node.right) - GetHeight(node.left);
        }

        private void Swap(Node a, Node b)
        {
            int a_value = a.value;
            a.value = b.value;
            b.value = a_value;
        }

        private void RightRotate(Node node)
        {
            Swap(node, node.left);
            Node buffer = node.right;
            node.right = node.left;
            node.left = node.right.left;
            node.right.left = node.right.right;
            node.right.right = buffer;
            UpdateHeight(node.right);
            UpdateHeight(node);
        }

        private void LeftRotate(Node node)
        {
            Swap(node, node.right);
            Node buffer = node.left;
            node.left = node.right;
            node.right = node.left.right;
            node.left.right = node.left.left;
            node.left.left = buffer;
            UpdateHeight(node.left);
            UpdateHeight(node);
        }

        private void Balance(Node node)
        {
            int balance = GetBalance(node);
            if(balance == -2)
            {
                if(GetBalance(node.left) == 1) LeftRotate(node.left);
                RightRotate(node);
            }
            else if(balance == 2)
            {
                if (GetBalance(node.right) == -1) RightRotate(node.right);
                LeftRotate(node);
            }
        }

        public void Insert(int value)
        {
            if (root == null)
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
            if (value < node.value)
            {
                if (node.left == null)
                {
                    node.left = new Node(value);
                }
                else
                {
                    Insert(node.left, value);
                }
            }
            else if (value >= node.value)
            {
                if (node.right == null)
                {
                    node.right = new Node(value);
                }
                else
                {
                    Insert(node.right, value);
                }
            }

            UpdateHeight(node);
            Balance(node);
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
            while(node.left != null)
            {
                node = node.left;
            }
            return node;
        }
        
        public Node GetMax()
        {
            return GetMax(root);
        }

        private Node GetMax(Node node)
        {
            while(node.right != null)
            {
                node = node.right;
            }
            return node;
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
                if (node.left == null || node.right == null)
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
            if(node != null)
            {
                UpdateHeight(node);
                Balance(node);
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
            Console.Write(node.value + " ");
            PrintTree(node.right);
        }

        /// write bfs for tree
        
        public void DeleteTree()
        {
            DeleteTree(root);
        }

        public void DeleteTree(Node node)
        {
            if (node == null) return;
            DeleteTree(node.left);
            DeleteTree(node.right);
            Console.Write(node.value);
        }
    }
}
