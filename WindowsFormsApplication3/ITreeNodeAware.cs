using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HermanBetaAlgorithmAlphaNum
{
    public interface ITreeNodeAware<T>
    {
        TreeNode<T> Node { get; set; }
    }
}
