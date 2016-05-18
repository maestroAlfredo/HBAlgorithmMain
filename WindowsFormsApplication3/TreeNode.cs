using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Data;
using System.Drawing;


namespace HermanBetaAlgorithmAlphaNum
{
    public class TreeNode<T> : IDisposable
    {

        public TreeNode(T Value)
        {
            this.Value = Value;
            Parent = null;
            Children = new TreeNodeList<T>(this);
        }

        public TreeNode(T Value, TreeNode<T> Parent)
        {
            this.Value = Value;
            this.Parent = Parent;
            Children = new TreeNodeList<T>(this);
            
        }

        private TreeNode<T> _Parent;
        public TreeNode<T> Parent
        {
            get { return _Parent; }
            set
            {
                //if the node is the parent return
                if (value == _Parent)
                {
                    return;
                }

                //if this node we're trying to create a parent for already has another parent, remove it from that parents child list. 
                if (_Parent != null)
                {
                    _Parent.Children.Remove(this);
                }

                //if the value isn't null and the value's children list doesn't contain this object, then add the value to the child list
                if (value != null && !value.Children.Contains(this))
                {
                    value.Children.Add(this);
                }

                //set the parent to the node specified
                _Parent = value;
            }
        }

        public TreeNode<T> Root
        {
            get
            {
                //return (Parent == null) ? this : Parent.Root;

                TreeNode<T> node = this;
                while (node.Parent != null)
                {
                    node = node.Parent;
                }
                return node;
            }
        }

        private TreeNodeList<T> _Children;
        public TreeNodeList<T> Children
        {
            get { return _Children; }
            private set { _Children = value; }
        }

        private T _Value;
        public T Value
        {
            get { return _Value; }
            set
            {
                _Value = value;

                if (_Value != null && _Value is ITreeNodeAware<T>)
                {
                    (_Value as ITreeNodeAware<T>).Node = this;
                }
            }
        }
        public int Depth
        {
            get
            {
                //return (Parent == null ? -1 : Parent.Depth) + 1;

                int depth = 0;
                TreeNode<T> node = this;
                while (node.Parent != null)
                {
                    node = node.Parent;
                    depth++;
                }
                return depth;
            }
        }

        public enum TreeTraversalType
        {
            TopDown,
            BottomUp
        }

        private TreeTraversalType _DisposeTraversal = TreeTraversalType.BottomUp;
        public TreeTraversalType DisposeTraversal
        {
            get { return _DisposeTraversal; }
            set { _DisposeTraversal = value; }
        }

        private bool _IsDisposed;
        public bool IsDisposed
        {
            get { return _IsDisposed; }
        }

        public void Dispose()
        {
            CheckDisposed();
            OnDisposing();

            // clean up contained objects (in Value property)
            if (Value is IDisposable)
            {
                if (DisposeTraversal == TreeTraversalType.BottomUp)
                {
                    foreach (TreeNode<T> node in Children)
                    {
                        node.Dispose();
                    }
                }

                (Value as IDisposable).Dispose();

                if (DisposeTraversal == TreeTraversalType.TopDown)
                {
                    foreach (TreeNode<T> node in Children)
                    {
                        node.Dispose();
                    }
                }
            }

            _IsDisposed = true;
        }

        public event EventHandler Disposing;

        protected void OnDisposing()
        {
            if (Disposing != null)
            {
                Disposing(this, EventArgs.Empty);
            }
        }

        public void CheckDisposed()
        {
            if (IsDisposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }

        public override string ToString()
        {
            string Description = string.Empty;
            if (Value != null)
            {
                Description = "[" + Value.ToString() + "] ";
            }

            return Description + "Depth=" + Depth.ToString() + ", Children="
              + Children.Count.ToString();
        }
    }
}
