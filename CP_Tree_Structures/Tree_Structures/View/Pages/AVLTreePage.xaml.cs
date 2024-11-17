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
    /// Логика взаимодействия для AVLTreePage.xaml
    /// </summary>
    public partial class AVLTreePage : Page
    {
        public AVLTreePage()
        {
            InitializeComponent();
        }

        private void AVLtreeCodeEditor_Loaded(object sender, RoutedEventArgs e)
        {
            AVLtreeCodeEditor.Text = @"
    public class AvlTreeNode
    {
        public int Value { get; set; } // полезная нагрузка
        public AvlTreeNode Left { get; set; } // ссылка на левый дочерний узел
        public AvlTreeNode Right { get; set; } // ссылка на правый дочерний узел
        public int BalanceFactor { get; set; } // показатель сбалансированности
        public AvlTreeNode Parent { get; set; } // ссылка на родителя

        public AvlTreeNode(int value, AvlTreeNode parent = null)
        {
            Value = value;
            Parent = parent;
            Left = null;
            Right = null;
            BalanceFactor = 0;
        }
    }
    ";
        }
    }
}
