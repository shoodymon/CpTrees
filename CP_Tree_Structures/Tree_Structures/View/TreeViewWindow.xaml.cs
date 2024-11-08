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
using System.Windows.Shapes;
using Tree_Structures.Model;

namespace Tree_Structures.View
{
    /// <summary>
    /// Логика взаимодействия для TreeViewWindow.xaml
    /// </summary>
    public partial class TreeViewWindow : Window
    {
        private ITree currentTree;
        private Dictionary<string, ITree> trees;
        public TreeViewWindow()
        {
            InitializeComponent();
            trees = new Dictionary<string, ITree>()
            {
                { "Бинарное дерево поиска", new BinarySearchTree() },
                { "АВЛ-дерево", new AVLTree() },
                { "КЧ-дерево", new RedBlackTree() },
                { "B-дерево", new BTree() },
                { "B+ дерево", new BPlusTree() },
                { "2-3-4 дерево", new TwoThreeFourTree() },
            };
            currentTree = trees["Бинарное дерево поиска"];
            foreach (var treeType in trees.Keys)
            {
                TreeTypeComboBox.Items.Add(treeType);
            }
            TreeTypeComboBox.SelectedItem = "Бинарное дерево поиска";
        }

        private void AddNodeButton_Click(object sender, RoutedEventArgs e)
        {
            //int value = int.Parse(ValueTextBox.Text);
            //currentTree.Insert(value);
            //RedrawTree();
        }

        private void DeleteNodeButton_Click(object sender, RoutedEventArgs e)
        {
            //int value = int.Parse(ValueTextBox.Text);
            //currentTree.Delete(value);
            //RedrawTree();
        }

        private void SearchNodeButton_Click(object sender, RoutedEventArgs e)
        {
            //int value = int.Parse(ValueTextBox.Text);
            //var node = currentTree.Find(value);
            //// Визуализация найденного узла на полотне
        }

        private void RedrawTree()
        {
            //// Очистить полотно и перерисовать дерево
            //TreeCanvas.Children.Clear();
            //TreeDrawer.DrawTree(currentTree, TreeCanvas);
        }

        private void TreeTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedTreeType = TreeTypeComboBox.SelectedItem as string;

            // Обновляем текущее дерево, если выбор валиден
            if (selectedTreeType != null && trees.ContainsKey(selectedTreeType))
            {
                currentTree = trees[selectedTreeType];
            }
        }
    }
}
