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
using Tree_Structures.View.Pages;
using Tree_Structures.ViewModel;

namespace Tree_Structures.View
{
    /// <summary>
    /// Логика взаимодействия для TutorialsWindow.xaml
    /// </summary>
    /// 
    public class TreeTypeItem
    {
        public TreeType TreeType { get; set; }
        public string DisplayName { get; set; }
    }

    public partial class TutorialsWindow : Window
    {
        private readonly Dictionary<TreeType, Page> _treePages;

        public TutorialsWindow()
        {
            InitializeComponent();

            // Создаем и инициализируем коллекцию страниц
            _treePages = new Dictionary<TreeType, Page>
            {
                { TreeType.BinarySearchTree, new BinarySearchTreePage() },
                { TreeType.AVLTree, new AVLTreePage() },
                { TreeType.RedBlackTree, new RBTreePage() },
                { TreeType.MinHeap, new BinaryHeapPage() },
                { TreeType.BTree, new BTreePage() }
            };

            // Заполняем ComboBox
            var treeTypes = new List<TreeTypeItem>
            {
                new TreeTypeItem { TreeType = TreeType.BinarySearchTree, DisplayName = "Бинарное дерево поиска" },
                new TreeTypeItem { TreeType = TreeType.AVLTree, DisplayName = "АВЛ-дерево" },
                new TreeTypeItem { TreeType = TreeType.RedBlackTree, DisplayName = "КЧ-дерево" },
                new TreeTypeItem { TreeType = TreeType.MinHeap, DisplayName = "Двоичная куча" },
                new TreeTypeItem { TreeType = TreeType.BTree, DisplayName = "B-дерево" }
            };

            // Устанавливаем DataContext для привязки и выбираем первый элемент по умолчанию
            TreeTypeComboBox.ItemsSource = treeTypes;
            TreeTypeComboBox.SelectedIndex = 0;
        }

        private void TreeTypeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TreeTypeComboBox.SelectedItem is TreeTypeItem selectedTreeTypeItem &&
                _treePages.ContainsKey(selectedTreeTypeItem.TreeType))
            {
                TutorialFrame.Navigate(_treePages[selectedTreeTypeItem.TreeType]);
            }
        }
    }
}
