using System.Collections.Generic;
using TeoVincent.RootAndNodesPattern.Exceptions;

namespace TeoVincent.RootAndNodesPattern
{
    public class Root
    {
        private readonly string m_name;
        protected ANode m_activeNode;
        protected ANode m_startNode;
        protected List<ANode> m_ownNodes;

        protected Root(string a_name)
        {
            m_name = a_name;
            m_ownNodes = new List<ANode>();
        }

        public void Start(ANode a_node)
        {
            SetStartNode(a_node);
            
            if (m_startNode == null)
                throw new NotSetStartNodeException();

            m_activeNode = m_startNode;
            m_activeNode.Entry();
        }
        
        public override string ToString()
        {
            return GetType().Name + " <-> " + m_name;
        }

        internal void AddNode(ANode a_node)
        {
            m_ownNodes.Add(a_node);
        }

        internal void OnFinishNode(string a_nodeOutputName)
        {
            var nextNode = m_activeNode.GetChildNode(a_nodeOutputName);

            if (nextNode == null)
            {
                OnFinish();
                return;
            }

            m_activeNode = nextNode;
            nextNode.Entry();
        }

        protected virtual void OnFinish() { }

        private void SetStartNode(ANode a_node)
        {
            var n = m_ownNodes.Find(a_a => a_a == a_node);

            if (n == null)
                throw new InvalidNodeException(a_node);

            m_startNode = n;
        }
    }
}