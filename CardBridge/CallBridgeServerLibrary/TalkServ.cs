using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using N = System.Net;
using System.IO;
using System.Diagnostics;
using UtilityBox;
using TestApplication;

namespace Server
{
    class TalkServ
    {
        System.Net.Sockets.TcpListener server;
        public static Hashtable handles;
        public static Hashtable handleByConnect;
        public static Hashtable handleToClientName;
        bool listening = true;

        static AppentTextDelegate appendServerMessage;
         LogTextDelegate logTextDelegate;

        public  LogTextDelegate LogTextDelegate
        {
            get { return logTextDelegate; }
            set { logTextDelegate = value; }
        }

        public AppentTextDelegate AppendServerMessage
        {
            get { return appendServerMessage; }
            set { appendServerMessage = value; }
        }


        public TalkServ()
        {
            handles = new Hashtable(100);
            handleByConnect = new Hashtable(100);
            handleToClientName = new Hashtable(100);
            server = new System.Net.Sockets.TcpListener(4296);
        }

        public void StartServer()
        {
            appendServerMessage("Starting server at " + DateTime.Now.ToString());
            listening = true;
            while (listening)
            {
                server.Start();
                if (server.Pending())
                {
                    N.Sockets.TcpClient connection = server.AcceptTcpClient();
                    appendServerMessage("Connected to " + connection.Client.RemoteEndPoint.ToString());
                    Debug.WriteLine("Connection made");
                    BackForth BF = new BackForth(connection);
                }
            }
        }

        public void StopServer()
        {
            listening = false;
            string[] allClients = new string[handles.Count];
            handles.Keys.CopyTo(allClients, 0);
            for (int i = 0; i < handles.Count; i++)
            {
                RemoveClient(allClients[i]);
            }
            server.Stop();
            appendServerMessage("Server stopped at " + DateTime.Now.ToString());
        }

        public static void BroadcastMessage(ChatMessage message)
        {
            StreamWriter SW;
            N.Sockets.TcpClient tc = null;
            string[] receivers = message.Receivers.
                Split(new string[] { Constants.receiverSeperator }, StringSplitOptions.RemoveEmptyEntries);
            string[] allClientHandles = new string[handleToClientName.Keys.Count];
            string[] allClientNames = new string[handleToClientName.Values.Count];
            handleToClientName.Keys.CopyTo(allClientHandles, 0);
            handleToClientName.Values.CopyTo(allClientNames, 0);
            for (int i = 0; i < receivers.Length; i++)
            {
                try
                {
                    int index = Array.IndexOf(allClientNames, receivers[i]);
                    if (index != -1)
                    {
                        tc = (N.Sockets.TcpClient)TalkServ.handles[allClientHandles[index]];
                        if (tc != null)
                        {
                            SW = new StreamWriter(tc.GetStream());
                            SW.WriteLine(message);
                            SW.Flush();
                            appendServerMessage("Message sent : " + message.ToString());
                            SW = null;
                        }
                    }
                }
                catch (Exception e44)
                {
                    Debug.WriteLine("Error during broadcast : " + e44.ToString());
                    TalkServ.RemoveClient((string)TalkServ.handleByConnect[tc]);
                    BroadcastMessage(GetClientListMessage());
                }
            }
        }

        public static string GetAllClientListSeperatedByComma()
        {
            if (handleToClientName.Count == 0)
            {
                return string.Empty;
            }
            StringBuilder allClients = new StringBuilder();
            string[] clients = new string[handleToClientName.Count];
            handleToClientName.Values.CopyTo(clients, 0);
            for (int i = 0; i < handleToClientName.Count; i++)
            {
                allClients.Append(clients[i] + Constants.receiverSeperator);
            }
            allClients.Remove(allClients.Length - 1, 1);
            return allClients.ToString();
        }

        public static string GetAllClientHandlesSeperatedByComma()
        {
            if (handleByConnect.Count == 0)
            {
                return string.Empty;
            }
            StringBuilder allClients = new StringBuilder();
            string[] clients = new string[handleByConnect.Count];
            handleByConnect.Values.CopyTo(clients, 0);
            for (int i = 0; i < handleByConnect.Count; i++)
            {
                allClients.Append(clients[i] + Constants.receiverSeperator);
            }
            allClients.Remove(allClients.Length - 1, 1);
            return allClients.ToString();
        }

        public static ChatMessage GetClientListMessage()
        {
            ChatMessage message = new ChatMessage();
            message.MessageCommand = Command.Broadcast;
            message.MessageTyp = MessageType.Clientlist;
            message.MessageDetail = TalkServ.GetAllClientListSeperatedByComma();
            message.Receivers = message.MessageDetail;
            return message;
        }

        public static void RemoveClient(string clientName)
        {
            if (clientName != null && handles.ContainsKey(clientName))
            {
                N.Sockets.TcpClient client = (N.Sockets.TcpClient)TalkServ.handles[clientName];
                TalkServ.handles.Remove(clientName);
                TalkServ.handleByConnect.Remove(client);
                TalkServ.handleToClientName.Remove(clientName);
                try
                {
                    client.Close();
                }
                catch (Exception e)
                {
                    Debug.WriteLine(e);
                }
            }
        }
    }
}
