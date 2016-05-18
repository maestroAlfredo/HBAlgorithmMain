using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoltageDropCalculatorApplication
{
    public class TreeNodeList<T> : List<TreeNode<T>>
    {
        public TreeNode<T> Parent;
 
    public TreeNodeList(TreeNode<T> Parent)
    {
        this.Parent = Parent;
    }
 
    //hides/overides the behaviour of the list.Add(TreeNode) method
    public  new TreeNode<T> Add(TreeNode<T> Node)
    {
        base.Add(Node); //adds the object to the list
        Node.Parent = Parent; //makes the Parent of the node
        return Node;
    }
 
    public TreeNode<T> Add(T Value)
    {
        return Add(new TreeNode<T>(Value));
    }
 

    public override string ToString()
    {
        return "Count=" + Count.ToString();
    }        

    }
}
