using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tree_Structures.Model
{
    public interface ITreeNode
    {
        int Value { get; set; }
        ITreeNode Left { get; set; }
        ITreeNode Right { get; set; }
    }
}
