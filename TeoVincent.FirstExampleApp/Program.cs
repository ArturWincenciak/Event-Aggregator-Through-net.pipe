#region Licence
// The MIT License (MIT)
// 
// Copyright (c) 2014 TeoVincent Artur Wincenciak
// TeoVincent.EventAggregator2013
// TeoVincent.FirstExampleApp
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
#endregion
using System;

using TeoVincent.EventAggregator.Client;
using TeoVincent.EventAggregator.Common.Events.Tts;
using TeoVincent.FirstExampleApp.Listeners;

namespace TeoVincent.FirstExampleApp
{
    class Program
    {
        static void Main(string[] a_args)
        {
            Console.WriteLine("FirstExampleApp starting ...");

            EventAggregatorClient.Instance.SubscribePlugin("FirstExampleApp");
            var v = new MyAnotherExampleEventListener();
            EventAggregatorClient.Instance.Subscribe(v);
            EventAggregatorClient.Instance.Subscribe(new MyExampleEventListener());
            EventAggregatorClient.Instance.Subscribe(new AllExampleEventListener());

            while(true)
            {
                Console.WriteLine("Press 'i' to send new IVR tree or press 'w' to generate new wave...\n");
                ConsoleKeyInfo key = Console.ReadKey();
                if (key.KeyChar.Equals('i'))
                {
                    var e = new AddedNewIvrEvent {Identificator = "Some unique ID for example."};
                    EventAggregatorClient.Instance.GlobalPublish(e);
                }
                else if(key.KeyChar.Equals('w'))
                {
                    var e = new DesiredWavesEvent();
                    e.Content.Add("Some text for convert to wave.");
                    e.Content.Add("Some today information.");
                    e.Content.Add("Another sentence.");
                    EventAggregatorClient.Instance.GlobalPublish(e);
                }
                else
                    break;
            }

            Console.WriteLine("... FirstExampleApp started.");
            Console.ReadKey();
        }
    }
}
