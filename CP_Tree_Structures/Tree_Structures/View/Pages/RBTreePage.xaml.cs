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
    /// Логика взаимодействия для RBTreePage.xaml
    /// </summary>
    public partial class RBTreePage : Page
    {
        public RBTreePage()
        {
            InitializeComponent();
        }

        private void RBTreeCodeEditor_Loaded(object sender, RoutedEventArgs e)
        {
            RBTreeCodeEditor.Text = @"
    public class RBTreeNode
    {
        // Ссылка на левый дочерний узел
        public RBTreeNode Left { get; set; }

        // Ссылка на правый дочерний узел
        public RBTreeNode Right { get; set; }

        // Цвет узла. Если true — узел красный, если false — узел черный
        public bool IsRed { get; set; }

        // Ссылка на родительский узел
        public RBTreeNode Parent { get; set; }

        // Значение узла
        public int Value { get; set; }

        // Конструктор для создания нового узла с заданным значением и ссылкой на родителя
        public RBTreeNode(int value, RBTreeNode parent = null)
        {
            Value = value;           // Устанавливаем значение узла
            Parent = parent;          // Устанавливаем ссылку на родителя, если он есть
            Left = null;              // Инициализируем левый дочерний узел как null
            Right = null;             // Инициализируем правый дочерний узел как null
            IsRed = true;             // По умолчанию новый узел является красным (можно поменять, если начальный узел черный)
        }
    }
    ";
        }
    }
}
