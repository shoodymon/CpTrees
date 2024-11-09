using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree_Structures.Model
{
    public class TreeNode: ITreeNode
    {
        public int Value { get; set; }
        public ITreeNode Left { get; set; }
        public ITreeNode Right { get; set; }

        public TreeNode(int value)
        {
            Value = value;
            Left = null;
            Right = null;
        }
    }

    internal class BinarySearchTree : ITree
    {
        public ITreeNode root { get; private set; }

        public void Insert(int value)
        {
            root = Insert(root, value);
            Console.WriteLine($"Inserted node with value {value}");
        }

        private ITreeNode Insert(ITreeNode node, int value)
        {
            if (node == null)
            {
                node = new TreeNode(value);
            }
            else if (value < node.Value)
            {
                node.Left = Insert(node.Left, value);
            }
            else if (value > node.Value)
            {
                node.Right = Insert(node.Right, value);
            }
            // Если значение уже существует в дереве, ничего не делаем
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
            {
                return null;
            }
            else if (value < node.Value)
            {
                node.Left = Delete(node.Left, value);
            }
            else if (value > node.Value)
            {
                node.Right = Delete(node.Right, value);
            }
            else
            {
                // Узел найден, начинаем процесс удаления
                if (node.Left == null)
                {
                    return node.Right;
                }
                else if (node.Right == null)
                {
                    return node.Left;
                }
                else
                {
                    // У узла есть оба потомка
                    // Находим наименьший узел в правом поддереве (или наибольший в левом поддереве)
                    TreeNode minRight = (TreeNode)FindMin(node.Right);
                    // Копируем значение найденного узла в текущий узел
                    node.Value = minRight.Value;
                    // Рекурсивно удаляем найденный узел
                    node.Right = Delete(node.Right, minRight.Value);
                }
            }
            return node;
        }

        private ITreeNode FindMin(ITreeNode node)
        {
            while (node.Left != null)
            {
                node = (TreeNode)node.Left;
            }
            return node;
        }

        public bool Search(int value)
        {
            return Search(root, value);
        }

        private bool Search(ITreeNode node, int value)
        {
            if (node == null)
            {
                return false;
            }
            else if (node.Value == value)
            {
                return true;
            }
            else if (value < node.Value)
            {
                return Search(node.Left, value);
            }
            else
            {
                return Search(node.Right, value);
            }
        }
    }
}
