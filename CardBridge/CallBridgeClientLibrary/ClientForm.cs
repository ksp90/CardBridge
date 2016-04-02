using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using N = System.Net;
using System.IO;
using System.Threading;
using UtilityBox;
using System.Diagnostics;
using System.Configuration;
using TestApplication;
using GameEntity;
using System.Xml;
using System.Net.Sockets;

namespace BroadcastChat
{
    public partial class Client : Form
    {
        static N.Sockets.TcpClient TC;
        Action action;
        List<CardGame> allConferenceForms = new List<CardGame>();
        string currentUser = string.Empty;

        public Client()
        {
            InitializeComponent();
        }

        private void Client_Load(object sender, EventArgs e)
        {
            TC = new N.Sockets.TcpClient();
            try
            {
                XmlDocument settingsXml = new XmlDocument();
                settingsXml.Load("App.config");
                string xpath = string.Format("configuration/appSettings/add[@key='{0}']/@value", "ServerIP");
                string serverIP = settingsXml.SelectSingleNode(xpath).Value;
                //string serverIP = ConfigurationManager.AppSettings["ServerIP"].ToString();
                //string serverIP = "127.0.0.1";
                if (string.IsNullOrEmpty(serverIP))
                {
                    MessageBox.Show("Invalid server ip configuration");
                    Application.Exit();
                }
                TC.Connect(serverIP, 4296);
                ChatMessage loginMessage = new ChatMessage();
                loginMessage.MessageCommand = Command.Login;
                loginMessage.MessageTyp = MessageType.Messages;
                currentUser = Environment.UserName + " (" + (TC.Client.LocalEndPoint.ToString()) + ")";
                loginMessage.Sender = currentUser;
                this.Text = "Call Bridge Client : " + currentUser;
                loginMessage.MessageDetail = Constants.loginMessage;
                StreamWriter SW = new StreamWriter(TC.GetStream());
                SW.WriteLine(loginMessage.ToString());
                SW.Flush();
            }
            catch (SocketException)
            {
                MessageBox.Show("Unreachable server");
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application.Exit();
            }
            Thread t = new Thread(new ThreadStart(run));
            t.Start();
        }

        private void run()
        {
            StreamReader SR = new StreamReader(TC.GetStream());
            while (TC.Connected)
            {
                Application.DoEvents();
                try
                {
                    string msg = SR.ReadLine();
                    ChatMessage message = new ChatMessage(msg);
                    ProcessReceivedMessage(message);
                }
                catch (NullReferenceException)
                {
                    action = () => MessageBox.Show("Unreachable server");
                    action += () => Environment.Exit(0);
                    Invoke(action);
                }
                catch (Exception ex)
                {
                    action = () => MessageBox.Show(ex.Message);
                    action += () => Environment.Exit(0);
                    Invoke(action);
                }
            }
        }

        private void Client_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                this.Hide();
                ChatMessage logoffMessage = new ChatMessage();
                logoffMessage.MessageCommand = Command.Logoff;
                logoffMessage.MessageTyp = MessageType.Warning;
                logoffMessage.Sender = currentUser;
                logoffMessage.Receivers = Constants.allReceiver;
                logoffMessage.MessageDetail = Constants.logoffMessage;
                StreamWriter SW = new StreamWriter(TC.GetStream());
                SW.WriteLine(logoffMessage.ToString());
                SW.Flush();
                for (int i = 0; i < allConferenceForms.Count; i++)
                {
                    if (allConferenceForms[i]!=null && !allConferenceForms[i].IsDisposed)
                    {
                        allConferenceForms[i].Close();
                    }
                }
                TC.Close();
            }
            catch
            { }
            finally
            {
                //Application.DoEvents();
            }
            Environment.Exit(0);
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            if (lstOnlineClients.SelectedItems.Count == 0) 
            {
                MessageBox.Show("Please select clients from list");
                return;
            }
            StringBuilder selectedClients = new StringBuilder();
            List<string> selectedClientsList = new List<string>();
            int numberOfSelectedClients = lstOnlineClients.SelectedItems.Count;
            for (int i = 0; i < numberOfSelectedClients; i++)
            {
                selectedClientsList.Add(lstOnlineClients.SelectedItems[i].ToString());
            }
            selectedClientsList.Add(currentUser);
            selectedClientsList.Sort();
            int numberOfReceivers = numberOfSelectedClients + 1;
            for (int i = 0; i < numberOfReceivers; i++)
            {
                selectedClients.Append(selectedClientsList[i] + Constants.receiverSeperator);
            }
            selectedClients.Remove(selectedClients.Length - 1, 1);
            lstOnlineClients.SelectedItems.Clear();
            for (int i = 0; i < allConferenceForms.Count; i++)
            {
                if (allConferenceForms[i].Text.Equals(selectedClients.ToString()))
                {
                    allConferenceForms[i].Visible = true;
                    return;
                }
            }
            ChatMessage enterConferenceMessage = new ChatMessage();
            enterConferenceMessage.MessageCommand = Command.EnterConference;
            enterConferenceMessage.MessageTyp = MessageType.Messages;
            enterConferenceMessage.Receivers = selectedClients.ToString();
            enterConferenceMessage.Sender = currentUser;
            CallBridge game = new CallBridge(4, 13, new PlayCard());
            List<List<PlayCard>> allPlayersCards = new List<List<PlayCard>>();
            for (int i = 0; i < selectedClientsList.Count; i++)
            {
                allPlayersCards.Add(game.AllPlayers[i].CardInHand);
            }
            //adding color card at last
            allPlayersCards.Add(new List<PlayCard>(){game.AllCards[new Random().Next(52)]});
            enterConferenceMessage.MessageDetail = UtilityBox.UtilityBoxHelper.XmlSerializeToString(allPlayersCards);
            StreamWriter sw = new StreamWriter(TC.GetStream());
            sw.WriteLine(enterConferenceMessage.ToString());
            sw.Flush();
        }

        #region Menustrip function
        private void onlineListToolStripMenuItem_CheckedChanged(object sender, EventArgs e)
        {
            pnlOnlineList.Visible = onlineListToolStripMenuItem.Checked;
        }

        private void onlineListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            onlineListToolStripMenuItem.Checked = !onlineListToolStripMenuItem.Checked;
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutClientChat about = new AboutClientChat();
            about.ShowDialog(this);
        } 
        #endregion

        #region Function to process received message
        private void ProcessReceivedMessage(ChatMessage message)
        {
            if (message.MessageTyp == MessageType.Clientlist)
            {
                MakeClientList(message);
            }
            else if (message.MessageTyp == MessageType.Messages)
            {
                if (message.MessageCommand == Command.Broadcast)
                {
                    ProcessBroadcastMessage(message);
                }
                else if (message.MessageCommand == Command.EnterConference)
                {
                    ProcessEnterConferenceMessage(message);
                }
                else if (message.MessageCommand == Command.LeaveConference)
                {
                    ProcessLeaveConferenceMessage(message);
                }
                else if (message.MessageCommand == Command.Bidding)
                {
                    ProcessBroadcastMessage(message);
                }
            }
            else
            {
                action = () => MessageBox.Show("Something wrong");
                Invoke(action);
            }
        }

        private void MakeClientList(ChatMessage message)
        {
            action = () =>
            {
                string[] allClients = message.MessageDetail.
                    Split(new string[] { Constants.receiverSeperator }, StringSplitOptions.None);
                Array.Sort(allClients);
                lstOnlineClients.Items.Clear();
                for (int i = 0; i < allClients.Length; i++)
                {
                    if (!allClients[i].Equals(currentUser))
                    {
                        lstOnlineClients.Items.Add(allClients[i]);
                    }
                }
            };
            Invoke(action);
        }

        private void ProcessBroadcastMessage(ChatMessage message)
        {
            int allFormsCount = 0;
            bool windowFound = false;
            action = () => allFormsCount = allConferenceForms.Count;
            Invoke(action);
            for (int i = 0; i < allFormsCount; i++)
            {
                string formTitle = string.Empty;
                action = () => formTitle = allConferenceForms[i].Text;
                Invoke(action);
                if (formTitle.Equals(message.Receivers))
                {
                    action = () => allConferenceForms[i].AcceptMessage(message);
                    action += () => allConferenceForms[i].Visible = true;
                    Invoke(action);
                    windowFound = true;
                }
            }
            if (!windowFound)
            {
                CreateChatWindow(message);
            }
        }

        private void ProcessEnterConferenceMessage(ChatMessage message)
        {
            int allFormsCount = 0;
            bool windowAlreadyCreated = false;
            action = () => allFormsCount = allConferenceForms.Count;
            Invoke(action);
            for (int i = 0; i < allFormsCount; i++)
            {
                string formTitle = string.Empty;
                action = () => formTitle = allConferenceForms[i].Text;
                Invoke(action);
                if (formTitle.Equals(message.Receivers))
                {
                    action = () => allConferenceForms[i].AcceptMessage(message);
                    action += () => allConferenceForms[i].Visible = true;
                    Invoke(action);
                    windowAlreadyCreated = true;
                }
            }
            if (!windowAlreadyCreated)
            {
                CreateChatWindow(message);
            }
        }

        private void ProcessLeaveConferenceMessage(ChatMessage message)
        {
            int allFormsCount = 0;
            action = () => allFormsCount = allConferenceForms.Count;
            Invoke(action);
            for (int i = 0; i < allFormsCount; i++)
            {
                string formTitle = string.Empty;
                action = () => formTitle = allConferenceForms[i].Text;
                Invoke(action);
                if (formTitle.Equals(message.Receivers))
                {
                    if (message.Sender.Equals(currentUser))
                    {
                        action = () => allConferenceForms[i].Hide();
                        Invoke(action);
                        return;
                    }
                    else
                    {
                        message.MessageDetail = message.Sender + " left us.";
                        action = () => allConferenceForms[i].AcceptMessage(message); 
                        action += () => allConferenceForms[i].Visible = true;
                        Invoke(action);
                    }
                }
            }
        }

        private void CreateChatWindow(ChatMessage message)
        {
            string[] allPlayers = message.Receivers.Split(new string[] { Constants.receiverSeperator }, StringSplitOptions.RemoveEmptyEntries);
            List<List<PlayCard>> allPlayersCards = new List<List<PlayCard>>();
            allPlayersCards = UtilityBox.UtilityBoxHelper.XmlDeserializeFromString<List<List<PlayCard>>>(message.MessageDetail);

            CardGame game = new CardGame(TC,(PlayerID)Array.IndexOf(allPlayers,currentUser),allPlayersCards[allPlayers.Length][0]);
            if (game.Game.NumberOfPlayers==allPlayers.Length)
            {
                for (int i = 0; i < allPlayers.Length; i++)
                {
                    game.Game.AllPlayers[i].CardInHand = allPlayersCards[i];
                }
            }
            game.DrawBoard();
            action = () => game.Text = message.Receivers;
            action += () => game.CurrentUser = currentUser;
            action += () => game.Visible = true;
            action += () => game.MdiParent = this;
            action += () => allConferenceForms.Add(game);
            action += () => MessageBox.Show("Color card : "+game.Game.ColorCard.CrdName);
            Invoke(action);
        } 
        #endregion
    }
}