using System;

namespace TeoVincent.RootAndNodesPattern.Exceptions
{
    public class DuplicateChildNodeException : Exception
    {
        private readonly ANode m_nodeChild;

        public DuplicateChildNodeException(ANode a_nodeChild)
        {
            m_nodeChild = a_nodeChild;
        }

        public ANode NodeChild
        {
            get { return m_nodeChild; }
        }
    }
}