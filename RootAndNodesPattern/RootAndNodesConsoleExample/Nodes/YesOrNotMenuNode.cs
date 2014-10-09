using System;
using TeoVincent.RootAndNodesPattern;

namespace TeoVincent.RootAndNodesConsoleExample.Nodes
{
    public class YesOrNoMenuNode : ANode
    {
        public const string YES_OUTPUT = "YES";
        public const string NO_OUTPUT = "NO";
        
        private readonly string m_question;

        public YesOrNoMenuNode(Root a_root, string a_name, string a_question) 
            : base(a_root, a_name)
        {
            m_question = a_question;
            
            AddNodeOutput(new OutputNode(YES_OUTPUT));
            AddNodeOutput(new OutputNode(NO_OUTPUT));
        }

        protected override void OnEntry()
        {
            Console.WriteLine(m_question);
            Console.WriteLine("YES = Y, NO = N");
            var answer = Console.ReadKey();
            
            switch (answer.KeyChar)
            {
                case 'Y':
                    Finish(YES_OUTPUT);
                    break;
                case 'N':
                    Finish(NO_OUTPUT);
                    break;
                default:
                    Console.WriteLine("Niepoprawna odpowiedź...");
                    Finish();
                    break;
            }
        }
    }
}
