using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HL7SampleListener
{
    class Program
    {
        private static readonly byte[] Localhost = { 127, 0, 0, 1 };
        private const int Port = 1302;

        static void Main(string[] args)
        {
            System.Net.IPAddress address = new IPAddress(Localhost);
            System.Net.IPEndPoint endPoint = new IPEndPoint(address, Port);

            try
            {
                // Create a thread for listening to a port.
                Subscriber subscriber = new Subscriber(endPoint);
                System.Threading.Thread listnerThread = new Thread(new ThreadStart(subscriber.Listen));
                listnerThread.Start();
                // Create another thread for sending HL7 messages
                // Send Message so that the listening port catches it.
                //Publisher publisher = new Publisher(Localhost, Port);
                //Thread senderThread = new Thread(new ThreadStart(publisher.Send));
                //senderThread.Start();
       

                
            }
            catch (Exception e)
            {
                // Exception handling
                Console.WriteLine("An unexpected exception occured: {0}", e.Message);
            }
        }
    }
}
