#region Licence
// The MIT License (MIT)
// 
// Copyright (c) 2014 TeoVincent Artur Wincenciak
// TeoVincent.EventAggregator2013
// TeoVincent.TTSExampleApp
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
using TeoVincent.EventAggregator.Common;
using TeoVincent.EventAggregator.Common.Events.Tts;

namespace TeoVincent.TTSExampleApp
{
    internal class TTSListener
        : IListener<AddedNewIvrEvent>
        , IListener<DesiredWavesEvent>
    {

        public void Handle(AddedNewIvrEvent receivedEvent)
        {
            Console.WriteLine(string.Format("\nNew IVR detected ... {0}", receivedEvent.Identificator));
            Console.WriteLine("IVR facory start to create all node of this tree.");
        }

        public void Handle(DesiredWavesEvent receivedEvent)
        {
            Console.WriteLine(string.Format("\nDesire waves ... "));
            foreach (var wave in receivedEvent.Content)
                Console.WriteLine(string.Format("Generating '{0}' waves ... ", wave));
        }
    }
}