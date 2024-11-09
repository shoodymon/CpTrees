using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree_Structures.Model
{
    public interface ITree
    {
        ITreeNode root { get; } // Корень дерева, нужен для доступа к узлам
        void Insert(int value);
        void Delete(int value);
        bool Search(int value);
    }
}
