using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Tree_Structures.Model;
using Tree_Structures.View;

namespace Tree_Structures.ViewModel
{
    public enum TreeType { BinarySearchTree, AVLTree, RedBlackTree, BTree, BPlusTree, TwoThreeFourTree }

    public class TreeManager
    {
        private readonly Dictionary<TreeType, ITree> trees;
        private readonly TreeDrawer treeDrawer;
        private readonly Canvas canvas;

        public TreeManager(Canvas canvas, TreeDrawer treeDrawer)
        {
            this.canvas = canvas;
            this.treeDrawer = treeDrawer;
            trees = new Dictionary<TreeType, ITree>
            {
                { TreeType.BinarySearchTree, new BinarySearchTree() },
                { TreeType.AVLTree, new AVLTree() },
                { TreeType.RedBlackTree, new RedBlackTree() },
                //{ TreeType.BTree, new BTree() },
                //{ TreeType.BPlusTree, new BPlusTree() },
                //{ TreeType.TwoThreeFourTree, new TwoThreeFourTree() }
            };
        }

        public void Insert(int value, TreeType treeType)
        {
            if (trees.TryGetValue(treeType, out ITree tree))
            {
                tree.Insert(value);
                RedrawTree(treeType);
            }
            else
            {
                throw new ArgumentException("Unsupported tree type!");
            }
        }

        public void Delete(int value, TreeType treeType)
        {
            if (trees.TryGetValue(treeType, out ITree tree))
            {
                tree.Delete(value);
                RedrawTree(treeType);
            }
            else
            {
                throw new ArgumentException("Unsupported tree type!");
            }
        }

        public bool Search(int value, TreeType treeType)
        {
            return trees.TryGetValue(treeType, out ITree tree) && tree.Search(value);
        }

        private void RedrawTree(TreeType treeType)
        {
            canvas.Children.Clear();

            if (treeType == TreeType.BinarySearchTree && trees[treeType] is BinarySearchTree bst)
            {
                treeDrawer.DrawTree(bst, canvas); // Используем метод DrawTree
            }
            // Добавим аналогичную отрисовку для других деревьев, если потребуется
        }
    }
}
