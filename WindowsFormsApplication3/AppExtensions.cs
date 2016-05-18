using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HermanBetaAlgorithmAlphaNum
{
    public static class TreeHelpers
    {
        public static IEnumerable<TreeItem> GetAncestors<TreeItem>(TreeItem item, Func<TreeItem, TreeItem> getParentFunc)
        {
            if (getParentFunc == null)
            {
                throw new ArgumentNullException("getParentFunc");
            }
            if (ReferenceEquals(item, null)) yield break;
            for (TreeItem curItem = getParentFunc(item); !ReferenceEquals(curItem, null); curItem = getParentFunc(curItem))
            {
                yield return curItem;
            }
        }
    }
}
