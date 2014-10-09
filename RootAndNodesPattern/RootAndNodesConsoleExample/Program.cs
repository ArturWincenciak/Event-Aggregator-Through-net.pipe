using System;
using TeoVincent.RootAndNodesConsoleExample.Nodes;
using TeoVincent.RootAndNodesPattern;

namespace TeoVincent.RootAndNodesConsoleExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("OGNIA...");
            Console.WriteLine("Po tej zmianie w kodzie ruszy utomatyczny build...");

            var root = new ExampleTree("teo");
            ANode showMessage = new MessageNode(root, "Powitanie", "To jest startowy węzeł...");
            ANode asterisks = new AsterisPrinterkNode(root, "Gwiazdki", 5);
            ANode question = new YesOrNoMenuNode(root, "Pytanie o gwiazdki", "Czy chcesz aby narysować gwiazdki?");
            ANode notAsterisksMessage = new MessageNode(root, "Gwiazdki nie wybrane", "Nie chciałeś gwiazdek więc nie ma.");
            ANode wrongChoiceMessage = new MessageNode(root, "Błędny wybór.", "Zle wybrałeś. Spróbuj jeszcze raz.");
            ANode endMessage = new MessageNode(root, "Koniec", "Koniec zabawy.");

            showMessage.JoinChildNode(question);
            question.JoinChildNode(YesOrNoMenuNode.YES_OUTPUT, asterisks);
            question.JoinChildNode(YesOrNoMenuNode.NO_OUTPUT, notAsterisksMessage);
            question.JoinChildNode(wrongChoiceMessage);
            wrongChoiceMessage.JoinChildNode(question);
            asterisks.JoinChildNode(endMessage);
            notAsterisksMessage.JoinChildNode(endMessage);

            root.Start(showMessage);

            Console.ReadLine();
        }
    }
}
