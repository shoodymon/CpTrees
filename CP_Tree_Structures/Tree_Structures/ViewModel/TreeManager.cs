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
    public enum TreeType
    {
        BinarySearchTree,
        AVLTree,
        RedBlackTree,
        MinHeap,
        MaxHeap,
        BTree,
        BPlusTree,
        TwoThreeFourTree
    }

    public class TreeManager
    {
        private readonly Dictionary<TreeType, ITree> trees;
        private readonly TreeDrawer treeDrawer;
        private readonly Canvas canvas;
        private readonly BTreeDrawer bTreeDrawer;

        public TreeManager(Canvas canvas, TreeDrawer treeDrawer)
        {
            this.canvas = canvas;
            this.treeDrawer = treeDrawer;
            this.bTreeDrawer = new BTreeDrawer();
            trees = new Dictionary<TreeType, ITree>
            {
                { TreeType.BinarySearchTree, new BinarySearchTree() },
                { TreeType.AVLTree, new AVLTree() },
                { TreeType.RedBlackTree, new RedBlackTree() },
                { TreeType.MinHeap, new MinHeap() },
                { TreeType.MaxHeap, new MaxHeap() },
                { TreeType.BTree, new BTree(2) },
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
                treeDrawer.DrawTree(bst, canvas);
            }
            else if (treeType == TreeType.AVLTree && trees[treeType] is AVLTree avl)
            {
                treeDrawer.DrawTree(avl, canvas);
            }
            else if (treeType == TreeType.RedBlackTree && trees[treeType] is RedBlackTree rbt)
            {
                treeDrawer.DrawTree(rbt, canvas);
            }
            else if (treeType == TreeType.MinHeap && trees[treeType] is MinHeap minHeap)
            {
                treeDrawer.DrawTree(minHeap, canvas);
            }
            else if (treeType == TreeType.MaxHeap && trees[treeType] is MaxHeap maxHeap)
            {
                treeDrawer.DrawTree(maxHeap, canvas);
            }
            else if (treeType == TreeType.BTree && trees[treeType] is BTree btree)
            {
                bTreeDrawer.DrawTree(btree, canvas);
            }
            // Добавим аналогичную отрисовку для других деревьев, если потребуется
        }
    }
}
