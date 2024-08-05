namespace Algo_Trees_C_
{
    public class RBTree
    {
        public enum ColorEnum { RED, BLACK };
        public class Node
        {
            internal int value;
            internal Node? left = null;
            internal Node? right = null;
            internal Node? parent = null;
            internal ColorEnum color = ColorEnum.BLACK;

            public Node(int value)
            {
                this.value = value;
                this.color = ColorEnum.RED;
            }
        }

        private Node? root = null;

        public RBTree() { }

        public RBTree(int value)
        {
            root = new Node(value);
            root.color = ColorEnum.BLACK;
        }

        public void Insert(int value)
        {
            if (root == null)
            {
                root = new Node(value);
                root.color = ColorEnum.BLACK;
            }
            else
            {
                Insert(root, value);
            }
        }

        private void Insert(Node node, int value)
        {
            if (value > node.value)
            {
                if (node.right == null)
                {
                    node.right = new Node(value);
                    node.right.parent = node;
                    InsertFix(node.right);
                }
                else
                {
                    Insert(node.right, value);
                }
            }
            else
            {
                if (node.left == null)
                {
                    node.left = new Node(value);
                    node.left.parent = node;
                    InsertFix(node.left);
                }
                else
                {
                    Insert(node.left, value);
                }
            }
        }

        private void InsertFix(Node node)
        {
            Node parent = null;
            Node grandparent = null;

            while ((node != root) && (node.color == ColorEnum.RED) && (node.parent.color == ColorEnum.RED))
            {
                parent = node.parent;
                grandparent = parent.parent;

                // Case A: Parent of node is left child of Grandparent of node
                if (parent == grandparent.left)
                {
                    Node uncle = grandparent.right;

                    // Case 1: The uncle of node is also red, only recoloring required
                    if (uncle != null && uncle.color == ColorEnum.RED)
                    {
                        grandparent.color = ColorEnum.RED;
                        parent.color = ColorEnum.BLACK;
                        uncle.color = ColorEnum.BLACK;
                        node = grandparent;
                    }
                    else
                    {
                        // Case 2: node is right child of its parent, left-rotation required
                        if (node == parent.right)
                        {
                            LeftRotation(parent);
                            node = parent;
                            parent = node.parent;
                        }

                        // Case 3: node is left child of its parent, right-rotation required
                        RightRotation(grandparent);
                        ColorEnum tempColor = parent.color;
                        parent.color = grandparent.color;
                        grandparent.color = tempColor;
                        node = parent;
                    }
                }

                // Case B: Parent of node is right child of Grandparent of node
                else
                {
                    Node uncle = grandparent.left;

                    // Case 1: The uncle of node is also red, only recoloring required
                    if (uncle != null && uncle.color == ColorEnum.RED)
                    {
                        grandparent.color = ColorEnum.RED;
                        parent.color = ColorEnum.BLACK;
                        uncle.color = ColorEnum.BLACK;
                        node = grandparent;
                    }
                    else
                    {
                        // Case 2: node is left child of its parent, right-rotation required
                        if (node == parent.left)
                        {
                            RightRotation(parent);
                            node = parent;
                            parent = node.parent;
                        }

                        // Case 3: node is right child of its parent, left-rotation required
                        LeftRotation(grandparent);
                        ColorEnum tempColor = parent.color;
                        parent.color = grandparent.color;
                        grandparent.color = tempColor;
                        node = parent;
                    }
                }
            }

            root.color = ColorEnum.BLACK;
        }

        public void Delete(int value)
        {
            // Find the node to be deleted
            Node node = Search(root, value);
            if (node == null) return;

            Node y = node;
            Node x;
            ColorEnum originalColor = y.color;

            if (node.left == null)
            {
                // Case: Node has no left child
                x = node.right;
                Transplant(node, node.right);
            }
            else if (node.right == null)
            {
                // Case: Node has no right child
                x = node.left;
                Transplant(node, node.left);
            }
            else
            {
                // Case: Node has two children
                y = GetMin(node.right);  // Find the minimum node in the right subtree
                originalColor = y.color;
                x = y.right;
                if (y.parent == node)
                {
                    if (x != null) x.parent = y;
                }
                else
                {
                    Transplant(y, y.right);
                    y.right = node.right;
                    y.right.parent = y;
                }
                Transplant(node, y);
                y.left = node.left;
                y.left.parent = y;
                y.color = node.color;
            }

            // If the deleted node was black, fix the tree
            if (originalColor == ColorEnum.BLACK)
            {
                DeleteFix(x);
            }
        }

        private void DeleteFix(Node x)
        {
            while (x != root && (x == null || x.color == ColorEnum.BLACK))
            {
                if (x == x.parent.left)
                {
                    Node w = x.parent.right;
                    if (w.color == ColorEnum.RED)
                    {
                        // Case 1: x's sibling w is red
                        w.color = ColorEnum.BLACK;
                        x.parent.color = ColorEnum.RED;
                        LeftRotation(x.parent);
                        w = x.parent.right;
                    }

                    if ((w.left == null || w.left.color == ColorEnum.BLACK) &&
                        (w.right == null || w.right.color == ColorEnum.BLACK))
                    {
                        // Case 2: x's sibling w is black, and both of w's children are black
                        w.color = ColorEnum.RED;
                        x = x.parent;
                    }
                    else
                    {
                        if (w.right == null || w.right.color == ColorEnum.BLACK)
                        {
                            // Case 3: x's sibling w is black, w's left child is red, and w's right child is black
                            if (w.left != null) w.left.color = ColorEnum.BLACK;
                            w.color = ColorEnum.RED;
                            RightRotation(w);
                            w = x.parent.right;
                        }

                        // Case 4: x's sibling w is black, and w's right child is red
                        w.color = x.parent.color;
                        x.parent.color = ColorEnum.BLACK;
                        if (w.right != null) w.right.color = ColorEnum.BLACK;
                        LeftRotation(x.parent);
                        x = root;
                    }
                }
                else
                {
                    Node w = x.parent.left;
                    if (w.color == ColorEnum.RED)
                    {
                        // Case 1: x's sibling w is red
                        w.color = ColorEnum.BLACK;
                        x.parent.color = ColorEnum.RED;
                        RightRotation(x.parent);
                        w = x.parent.left;
                    }

                    if ((w.right == null || w.right.color == ColorEnum.BLACK) &&
                        (w.left == null || w.left.color == ColorEnum.BLACK))
                    {
                        // Case 2: x's sibling w is black, and both of w's children are black
                        w.color = ColorEnum.RED;
                        x = x.parent;
                    }
                    else
                    {
                        if (w.left == null || w.left.color == ColorEnum.BLACK)
                        {
                            // Case 3: x's sibling w is black, w's right child is red, and w's left child is black
                            if (w.right != null) w.right.color = ColorEnum.BLACK;
                            w.color = ColorEnum.RED;
                            LeftRotation(w);
                            w = x.parent.left;
                        }

                        // Case 4: x's sibling w is black, and w's left child is red
                        w.color = x.parent.color;
                        x.parent.color = ColorEnum.BLACK;
                        if (w.left != null) w.left.color = ColorEnum.BLACK;
                        RightRotation(x.parent);
                        x = root;
                    }
                }
            }

            if (x != null) x.color = ColorEnum.BLACK;
        }

        public Node GetMin()
        {
            return GetMin(root);
        }

        private Node GetMin(Node node)
        {
            while (node.left != null)
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

        private void Transplant(Node u, Node v)
        {
            if (u.parent == null)
            {
                root = v;
            }
            else if (u == u.parent.left)
            {
                u.parent.left = v;
            }
            else
            {
                u.parent.right = v;
            }
            if (v != null)
            {
                v.parent = u.parent;
            }
        }

        public Node Search(int value)
        {
            return Search(root, value);
        }

        private Node Search(Node node, int value)
        {
            while (node != null && value != node.value)
            {
                if (value > node.value)
                {
                    node = node.right;
                }
                else
                {
                    node = node.left;
                }
            }
            return node;
        }

        public void LeftRotation(Node x)
        {
            Node y = x.right;
            Node p = x.parent;

            if (y.left != null)
            {
                y.left.parent = x;
            }

            x.right = y.left;
            y.left = x;
            x.parent = y;

            if (p == null)
            {
                root = y;
            }
            else if (x == p.left)
            {
                p.left = y;
            }
            else
            {
                p.right = y;
            }

            y.parent = p;
        }

        public void RightRotation(Node y)
        {
            Node x = y.left;
            Node p = y.parent;

            if (x.right != null)
            {
                x.right.parent = y;
            }

            y.left = x.right;
            x.right = y;
            y.parent = x;

            if (p == null)
            {
                root = x;
            }
            else if (y == p.right)
            {
                p.right = x;
            }
            else
            {
                p.left = x;
            }

            x.parent = p;
        }

        public void PrintTree()
        {
            PrintTree(root);
        }

        private void PrintTree(Node node)
        {
            if (node == null) return;
            PrintTree(node.left);
            Console.Write(node.value + " ");
            PrintTree(node.right);
        }
    }

}
