using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree_Structures.Model
{
    public class AVLNode : ITreeNode
    {
        public int Value { get; set; }
        public ITreeNode Left { get; set; }
        public ITreeNode Right { get; set; }
        public int Height { get; set; }

        public AVLNode(int value)
        {
            Value = value;
            Height = 1;
            Left = null;
            Right = null;
        }
    }

    public class AVLTree : ITree
    {
        public ITreeNode root { get; private set; }

        private int Height(ITreeNode node)
        {
            if (node == null) return 0;
            return ((AVLNode)node).Height;
        }

        private int BalanceFactor(ITreeNode node)
        {
            if (node == null) return 0;
            return Height(node.Left) - Height(node.Right);
        }

        private void UpdateHeight(AVLNode node)
        {
            node.Height = 1 + Math.Max(Height(node.Left), Height(node.Right));
        }

        private ITreeNode RightRotate(ITreeNode y)
        {
            AVLNode x = (AVLNode)y.Left;
            AVLNode T2 = (AVLNode)x.Right;

            x.Right = y;
            y.Left = T2;

            UpdateHeight((AVLNode)y);
            UpdateHeight(x);

            return x;
        }

        private ITreeNode LeftRotate(ITreeNode x)
        {
            AVLNode y = (AVLNode)x.Right;
            AVLNode T2 = (AVLNode)y.Left;

            y.Left = x;
            x.Right = T2;

            UpdateHeight((AVLNode)x);
            UpdateHeight(y);

            return y;
        }

        public void Insert(int value)
        {
            root = Insert(root, value);
            Console.WriteLine($"Inserted node with value {value}");
        }

        private ITreeNode Insert(ITreeNode node, int value)
        {
            if (node == null)
                return new AVLNode(value);

            if (value < node.Value)
                node.Left = Insert(node.Left, value);
            else if (value > node.Value)
                node.Right = Insert(node.Right, value);
            else
                return node; // Дублирующиеся значения не допускаются

            UpdateHeight((AVLNode)node);

            int balance = BalanceFactor(node);

            // Левый-Левый случай
            if (balance > 1 && value < node.Left.Value)
                return RightRotate(node);

            // Правый-Правый случай
            if (balance < -1 && value > node.Right.Value)
                return LeftRotate(node);

            // Левый-Правый случай
            if (balance > 1 && value > node.Left.Value)
            {
                node.Left = LeftRotate(node.Left);
                return RightRotate(node);
            }

            // Правый-Левый случай
            if (balance < -1 && value < node.Right.Value)
            {
                node.Right = RightRotate(node.Right);
                return LeftRotate(node);
            }

            return node;
        }

        public void Delete(int value)
        {
            root = Delete(root, value);
            Console.WriteLine($"Deleted node with value {value}");
        }

        private ITreeNode Delete(ITreeNode node, int value)
        {
            if (node == null)
                return node;

            if (value < node.Value)
                node.Left = Delete(node.Left, value);
            else if (value > node.Value)
                node.Right = Delete(node.Right, value);
            else
            {
                // Узел с одним или без детей
                if (node.Left == null || node.Right == null)
                {
                    ITreeNode temp = node.Left ?? node.Right;
                    if (temp == null)
                    {
                        temp = node;
                        node = null;
                    }
                    else
                        node = temp;
                }
                else
                {
                    // Узел с двумя детьми
                    ITreeNode temp = FindMin(node.Right);
                    node.Value = temp.Value;
                    node.Right = Delete(node.Right, temp.Value);
                }
            }

            if (node == null)
                return node;

            UpdateHeight((AVLNode)node);

            int balance = BalanceFactor(node);

            // Левый-Левый случай
            if (balance > 1 && BalanceFactor(node.Left) >= 0)
                return RightRotate(node);

            // Левый-Правый случай
            if (balance > 1 && BalanceFactor(node.Left) < 0)
            {
                node.Left = LeftRotate(node.Left);
                return RightRotate(node);
            }

            // Правый-Правый случай
            if (balance < -1 && BalanceFactor(node.Right) <= 0)
                return LeftRotate(node);

            // Правый-Левый случай
            if (balance < -1 && BalanceFactor(node.Right) > 0)
            {
                node.Right = RightRotate(node.Right);
                return LeftRotate(node);
            }

            return node;
        }

        private ITreeNode FindMin(ITreeNode node)
        {
            ITreeNode current = node;
            while (current.Left != null)
                current = current.Left;
            return current;
        }

        public bool Search(int value)
        {
            return Search(root, value);
        }

        private bool Search(ITreeNode node, int value)
        {
            if (node == null)
                return false;
            if (value == node.Value)
                return true;
            if (value < node.Value)
                return Search(node.Left, value);
            return Search(node.Right, value);
        }
    }
}
