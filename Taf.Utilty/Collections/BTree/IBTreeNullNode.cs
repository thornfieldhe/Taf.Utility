namespace Taf.Utility{
    public class IBTreeNullNode<T> : IBTreeNode<T>{
        public BTreeNodeColor Color => BTreeNodeColor.Black;

        public bool IsNull => true;

        public BTreeNodeColor Colour{
            get => BTreeNodeColor.Black;
            set{ }
        }

        public IBTreeNode<T> Parent{ get; set; }

        public IBTreeNode<T> Left{
            get => null;
            set{ }
        }

        public IBTreeNode<T> Right{
            get => null;
            set{ }
        }

        public T Data{
            get => default;
            set{ }
        }
    }
}
