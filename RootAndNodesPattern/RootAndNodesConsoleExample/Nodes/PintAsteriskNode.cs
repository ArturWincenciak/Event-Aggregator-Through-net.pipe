using System;
using TeoVincent.RootAndNodesPattern;

namespace TeoVincent.RootAndNodesConsoleExample.Nodes
{
    public class AsterisPrinterkNode : ANode
    {
        private readonly int m_count;

        public AsterisPrinterkNode(Root a_root, string a_name, int a_count) 
            : base(a_root, a_name)
        {
            m_count = a_count;
        }

        protected override void OnEntry()
        {
            for(int i = 0; i < m_count; i++)
                Console.Write("*");

            Finish();
        }
    }
}