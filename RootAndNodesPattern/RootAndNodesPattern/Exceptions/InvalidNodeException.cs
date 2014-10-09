using System;

namespace TeoVincent.RootAndNodesPattern.Exceptions
{
    public class InvalidNodeException : Exception
    {
        private readonly ANode m_nodeChild;

        public InvalidNodeException(ANode a_nodeChild)
        {
            m_nodeChild = a_nodeChild;
        }

        public ANode NodeChild
        {
            get { return m_nodeChild; }
        }
    }
}