using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tree_Structures.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для BinarySearchTreePage.xaml
    /// </summary>
    public partial class BinarySearchTreePage : Page
    {
        public BinarySearchTreePage()
        {
            InitializeComponent();
        }

        private void BSTreeCodeEditor_Loaded(object sender, RoutedEventArgs e)
        {
            BSTreeCodeEditor.Text = @"
    public class BinaryTreeNode
    {
        public BinaryTreeNode Left { get; set; }  // ссылка на левый дочерний узел
        public BinaryTreeNode Right { get; set; } // ссылка на правый дочерний
        public BinaryTreeNode Parent { get; set; } // ссылка на родителя
        public int Value { get; set; } // полезная нагрузка

        // Конструктор для создания нового узла
        public BinaryTreeNode(int value, BinaryTreeNode parent = null)
        {
            Left = null;
            Right = null;
            Parent = parent;
            Value = value;
        }
    }
    ";
        }

        private void BSTreeAddNodeCodeEditor_Loaded(object sender, RoutedEventArgs e)
        {
            BSTreeAddNodeCodeEditor.Text = @"
    public class BinaryTreeNode
    {
        // ...

        // Публичный метод для добавления нового узла в дерево
        public void InsertNode(int value)
        {
            // Начинаем вставку с текущего узла (this), передаем его в приватный метод рекурсивно
            InsertNode(value, this);
        }

        // Приватный рекурсивный метод для вставки нового узла в дерево
        private void InsertNode(int value, BinaryTreeNode parentNode)
        {
            // Сравниваем значение, которое нужно вставить, с текущим значением узла
            if (value < parentNode.Value)
            {
                // Если новое значение меньше, чем значение текущего узла,
                // проверяем левый дочерний узел.
                if (parentNode.Left == null)
                {
                    // Если левого дочернего узла нет, создаем новый узел и присваиваем его левому потомку
                    parentNode.Left = new BinaryTreeNode(value, parentNode);  // Новый узел с родителем
                }
                else
                {
                    // Если левый узел существует, рекурсивно вызываем вставку для левого поддерева
                    InsertNode(value, parentNode.Left);
                }
            }
            // Если значение больше, чем значение текущего узла, вставляем в правое поддерево
            else if (value > parentNode.Value)
            {
                // Проверяем правый дочерний узел.
                if (parentNode.Right == null)
                {
                    // Если правого дочернего узла нет, создаем новый узел и присваиваем его правому потомку
                    parentNode.Right = new BinaryTreeNode(value, parentNode);  // Новый узел с родителем
                }
                else
                {
                    // Если правый узел существует, рекурсивно вызываем вставку для правого поддерева
                    InsertNode(value, parentNode.Right);
                }
            }
            // Если значение уже существует в дереве (равно текущему узлу), ничего не делаем
        }
    }
    ";
        }

        private void BSTreeDelNodeCodeEditor_Loaded(object sender, RoutedEventArgs e)
        {
            BSTreeDelNodeCodeEditor.Text = @"
    public class BinaryTreeNode
    {
        // ...

        // Публичный метод для удаления узла с заданным значением
        public BinaryTreeNode RemoveNode(int value)
        {
            return RemoveNode(value, this);
        }

        // Приватный рекурсивный метод для удаления узла
        private BinaryTreeNode RemoveNode(int value, BinaryTreeNode node)
        {
            // Если узел равен null, возвращаем null (не найдено)
            if (node == null) return null;

            // Если значение меньше текущего узла, ищем в левом поддереве
            if (value < node.Value)
            {
                node.Left = RemoveNode(value, node.Left);
            }
            // Если значение больше текущего узла, ищем в правом поддереве
            else if (value > node.Value)
            {
                node.Right = RemoveNode(value, node.Right);
            }
            else
            {
                // Если узел найден, проверяем его детей

                // Если у узла нет левого поддерева, возвращаем правого ребенка
                if (node.Left == null) return node.Right;

                // Если у узла нет правого поддерева, возвращаем левого ребенка
                if (node.Right == null) return node.Left;
            }

            // Когда оба поддерева присутствуют, нужно найти наименьший элемент в правом поддереве
            BinaryTreeNode original = node;
            node = node.Right;

            // Находим самый левый узел в правом поддереве (минимальный элемент)
            while (node.Left != null)
            {
                node = node.Left;
            }

            // Переносим правое поддерево
            node.Right = RemoveMin(original.Right);
            // Переносим левое поддерево
            node.Left = original.Left;

            return node; // Возвращаем обновленный узел
        }

        // Приватный метод для удаления минимального элемента в дереве
        private BinaryTreeNode RemoveMin(BinaryTreeNode node)
        {
            // Если у узла нет левого ребенка, возвращаем его правого ребенка (удаляем узел)
            if (node.Left == null)
            {
                return node.Right;
            }

            // Иначе рекурсивно удаляем минимальный элемент в левом поддереве
            node.Left = RemoveMin(node.Left);
            return node; // Возвращаем обновленный узел
        }
    }
    ";
        }

        private void BSTreeSearchNodeCodeEditor_Loaded(object sender, RoutedEventArgs e)
        {
            BSTreeSearchNodeCodeEditor.Text = @"
    public class BinaryTreeNode
    {
        // ...

        // Публичный метод для поиска узла по значению
        public BinaryTreeNode FindNode(int value)
        {
            // Начинаем поиск с текущего узла
            BinaryTreeNode node = this;

            // Пока текущий узел не равен null, продолжаем искать
            while (node != null)
            {
                // Если значение текущего узла совпадает с искомым, возвращаем этот узел
                if (value == node.Value) 
                {
                    return node;
                }

                // Если искомое значение меньше текущего узла, идем в левое поддерево
                if (value < node.Value)
                {
                    node = node.Left;
                }
                // Если искомое значение больше текущего узла, идем в правое поддерево
                else if (value > node.Value)
                {
                    node = node.Right;
                }
            }

            // Если узел не найден, возвращаем null
            return null;
        }
    }
    ";
        }
    }
}
