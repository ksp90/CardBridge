using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using N = System.Net;
using System.Diagnostics;
using UtilityBox;
using TestApplication;

namespace Server
{
    class BackForth
    {
        N.Sockets.TcpClient client;
        System.IO.StreamReader SR;
        System.IO.StreamWriter SW;
        string handle;
        static int counter = 0;

        public BackForth(System.Net.Sockets.TcpClient c)
        {
            client = c;
            Thread t = new Thread(new ThreadStart(init));
            t.Start();
        }

        private string GetHandle()
        {
            return "Client " + (++counter);
        }

        private void run()
        {
            try
            {
                string l = string.Empty;
                while (true)
                {
                    l = SR.ReadLine();
                    ChatMessage message = new ChatMessage(l);
                    ProcessMessage(message);
                }
            }
            catch (Exception e44) 
            { 
                Debug.WriteLine(e44);
                TalkServ.RemoveClient((string)TalkServ.handleByConnect[handle]);
                TalkServ.BroadcastMessage(TalkServ.GetClientListMessage());
            }
        }

        private void init()
        {
            SR = new System.IO.StreamReader(client.GetStream());
            SW = new System.IO.StreamWriter(client.GetStream());
            handle = GetHandle();
            TalkServ.handles.Add(handle, client);
            TalkServ.handleByConnect.Add(client, handle);
            Thread t = new Thread(new ThreadStart(run));
            t.Start();
        }

        private void ProcessMessage(ChatMessage message)
        {
            if (message.MessageCommand == Command.Broadcast)
            {
                ProcessBroadcast(message);
            }
            else if (message.MessageCommand == Command.EnterConference)        
            {
                ProcessEnterConferenceMessage(message);
            }
            else if (message.MessageCommand == Command.LeaveConference)
            { 
            
            }
            else if (message.MessageCommand == Command.Bidding)
            {
                ProcessBroadcast(message);
            }
            else if (message.MessageCommand == Command.Logoff)
            {
                TalkServ.RemoveClient(handle);
                TalkServ.BroadcastMessage(TalkServ.GetClientListMessage());
            }
            else if (message.MessageCommand == Command.Login)
            {
                TalkServ.handleToClientName.Add(handle, message.Sender);
                TalkServ.BroadcastMessage(TalkServ.GetClientListMessage());
            }
        }

        private void ProcessEnterConferenceMessage(ChatMessage message)
        {
            string[] allReceivers = message.Receivers.Split(new string[] { Constants.receiverSeperator },StringSplitOptions.RemoveEmptyEntries);
            if (allReceivers.Length==4)
            {
                TalkServ.BroadcastMessage(message);
            }
        }

        private void ProcessBroadcast(ChatMessage message)
        {
            if (message.Receivers.Equals(Constants.allReceiver))
            {
                message.Receivers = TalkServ.GetAllClientListSeperatedByComma();
            }
            TalkServ.BroadcastMessage(message);
        }
    }
}
