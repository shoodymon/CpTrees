using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree_Structures.Model
{
    public interface IBTreeNode : ITreeNode
    {
        List<int> Keys { get; set; }
        List<IBTreeNode> Children { get; set; }
        bool IsLeaf { get; set; }
    }
}
