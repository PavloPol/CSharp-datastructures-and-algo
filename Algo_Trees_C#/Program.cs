namespace Algo_Trees_C_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //AVLTree tree = new AVLTree(1, 1);
            //tree.Insert(tree, 2, 2);
            //tree.Insert(tree, 7, 7);
            //tree.Insert(tree, 3, 3);
            //tree.Insert(tree, 6, 6);
            //tree.Insert(tree, 8, 8);
            //tree.Delete(tree, 3);
            //tree.PrintTree(tree);
            Tree tree = new Tree();
            tree.Insert(5);
            tree.Insert(8);
            tree.Insert(9);
            tree.Insert(3);
            tree.Insert(2);
            tree.Insert(1);
            tree.PrintTree();
            tree.BFS();
        }
    }
}
