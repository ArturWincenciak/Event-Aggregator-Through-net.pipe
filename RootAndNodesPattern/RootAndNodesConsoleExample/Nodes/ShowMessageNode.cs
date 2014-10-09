using System;
using TeoVincent.RootAndNodesPattern;

namespace TeoVincent.RootAndNodesConsoleExample.Nodes
{
    public class MessageNode : ANode
    {
        private readonly string m_message;

        public MessageNode(Root a_root, string a_name, string a_message) 
            : base(a_root, a_name)
        {
            m_message = a_message;
        }

        protected override void OnEntry()
        {
            Console.WriteLine(m_message);
            Finish();
        }
    }
}
