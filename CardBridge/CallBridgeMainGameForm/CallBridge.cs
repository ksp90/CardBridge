using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using GameEntity;
using System.Net.Sockets;
using System.Threading;
using BroadcastChat;
using UtilityBox;

namespace TestApplication
{
    public partial class CardGame : Form
    {
        CallBridge game;

        public CallBridge Game
        {
            get { return game; }
            set { game = value; }
        }

        PlayerID currentPlayerID = PlayerID.Player3;
        int numberOfPlayers = 0;
        int numberOfCardsPerPlayer = 0;
        string currentUser;

        public string CurrentUser
        {
            get { return currentUser; }
            set { currentUser = value; }
        }

        TcpClient TC;

        public CardGame()
        {
            InitializeComponent();
            numberOfPlayers = 4;
            numberOfCardsPerPlayer = 13;
            game = new CallBridge(numberOfPlayers, numberOfCardsPerPlayer, new PlayCard());
            MakeBoard();
            DrawBoard();
        }

        public CardGame(TcpClient tc, PlayerID playerId, PlayCard colorCard)
        {
            InitializeComponent();
            numberOfPlayers = 4;
            numberOfCardsPerPlayer = 13;
            TC = tc;
            currentPlayerID = playerId;
            lblColorCardValue.Text = colorCard.CrdName.ToString();
            game = new CallBridge(numberOfPlayers, numberOfCardsPerPlayer, colorCard);
            MakeBoard();
        }

        private void MakeBoard()
        {
            for (int k = 0; k < numberOfPlayers; k++)
            {
                game.AllPlayers[k].CardInHand.Sort();
                for (int l = 0; l < numberOfCardsPerPlayer; l++)
                {
                    PictureBox pictureBox = new PictureBox();
                    if (k % 2 == 0)
                    {
                        pictureBox.Location = new System.Drawing.Point((8 + (l * 25)), 23);
                    }
                    else
                    {
                        pictureBox.Location = new System.Drawing.Point(8, (23 + (l * 25)));
                    }
                    pictureBox.Size = new System.Drawing.Size(80, 100);
                    pictureBox.TabIndex = 0;
                    pictureBox.TabStop = false;
                    //adding event handler only for current player
                    if (k == (int)currentPlayerID)
                    {
                        pictureBox.Click += new System.EventHandler(this.pictureBox_Click);
                    }
                    gbMainGamePanel.Controls[k].Controls.Add(pictureBox);
                }
            }
        }

        private void DisplayPlayerTurn()
        {
            if (game.PlayerTurn == currentPlayerID)
            {
                lblTurnValue.Text = "Your";
            }
            else
            {
                lblTurnValue.Text = game.PlayerTurn.ToString();
            }
        }

        private void UpdateScoreInscreen()
        {
            lblCurrentScoreValue.Text = game.AllPlayers[(int)currentPlayerID].HandWinCount.ToString();
            lblTotalScoreValue.Text = game.PlayerResults[(int)currentPlayerID].ToString();
        }

        public void DrawBoard()
        {
            for (int j = 0; j < game.NumberOfPlayers; j++)
            {
                game.AllPlayers[j].CardInHand.Sort();
                for (int i = 0; i < game.AllPlayers[j].CardInHand.Count; i++)
                {
                    if (j == (int)currentPlayerID)
                    {
                        ((PictureBox)gbMainGamePanel.Controls[j].Controls[i]).Image = Image.FromFile(game.AllPlayers[j].CardInHand[i].ImageName.ToString());
                    }
                    else
                    {
                        ((PictureBox)gbMainGamePanel.Controls[j].Controls[i]).Image = Image.FromFile(game.AllPlayers[j].CardInHand[i].BackImage);
                    }

                    ((PictureBox)gbMainGamePanel.Controls[j].Controls[i]).Visible = true;
                }
            }
            pictureBox1.Image = null;
            pictureBox2.Image = null;
            pictureBox3.Image = null;
            pictureBox4.Image = null;
            DisplayPlayerTurn();
        }

        private void RedrawHand(PlayerID player, PlayCard playedCard, bool endCurrentHand)
        {
            Panel workingPanel = null;
            PictureBox workingPicturebox = null;
            switch (player)
            {
                case PlayerID.Player1:
                    workingPanel = panel1;
                    workingPicturebox = pictureBox1;
                    break;
                case PlayerID.Player2:
                    workingPanel = panel2;
                    workingPicturebox = pictureBox2;
                    break;
                case PlayerID.Player3:
                    workingPanel = panel3;
                    workingPicturebox = pictureBox3;
                    break;
                case PlayerID.Player4:
                    workingPanel = panel4;
                    workingPicturebox = pictureBox4;
                    break;
            }
            for (int i = 0; i < workingPanel.Controls.Count; i++)
            {
                workingPanel.Controls[i].Visible = false;
            }
            for (int i = 0; i < game.AllPlayers[(int)player].CardInHand.Count; i++)
            {
                if (player == currentPlayerID)
                {
                    ((PictureBox)workingPanel.Controls[i]).Image = Image.FromFile(game.AllPlayers[(int)player].CardInHand[i].ImageName);
                }
                else
                {
                    ((PictureBox)workingPanel.Controls[i]).Image = Image.FromFile(game.AllPlayers[(int)player].CardInHand[i].BackImage);
                }
                workingPanel.Controls[i].Visible = true;
            }
            workingPicturebox.Image = Image.FromFile(playedCard.ImageName);
            workingPicturebox.Visible = true;

            if (endCurrentHand)
            {
                //pictureBox1.Visible = false;
                //pictureBox2.Visible = false;
                //pictureBox3.Visible = false;
                //pictureBox4.Visible = false;
                new Thread(new ThreadStart(PickupHands)).Start();
            }


            DisplayPlayerTurn();
        }

        private void PickupHands()
        {
            foreach (Control control in panel5.Controls)
            {
                Thread.Sleep(200);
                control.Visible = false;
            }
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            if (!game.BidSettled)
            {
                MessageBox.Show("First settled the bid");
                return;
            }
            if (game.PlayerTurn != currentPlayerID)
            {
                MessageBox.Show("Not Your turn. Its " + game.PlayerTurn + "'s turn.");
                return;
            }
            PictureBox gen = (PictureBox)sender;
            int index = -1;
            if (gen.Parent.Equals(panel1))
            {
                index = panel1.Controls.IndexOf(gen);
            }
            else if (gen.Parent.Equals(panel2))
            {
                index = panel2.Controls.IndexOf(gen);
            }
            else if (gen.Parent.Equals(panel3))
            {
                index = panel3.Controls.IndexOf(gen);
            }
            else if (gen.Parent.Equals(panel4))
            {
                index = panel4.Controls.IndexOf(gen);
            }
            if (index != -1)
            {
                if (game.IsValidCardForPlay(game.AllPlayers[(int)currentPlayerID].CardInHand[index], currentPlayerID))
                {
                    SendNetworkPacket(game.AllPlayers[(int)currentPlayerID].CardInHand[index]);
                }
                else
                {
                    MessageBox.Show("Please use "+game.CurrentPlayingCard.CrdName.ToString()+" card");
                }
            }
        }

        private void CardGame_Load(object sender, EventArgs e)
        {
        }

        public void AcceptMessage(ChatMessage message)
        {
            Application.DoEvents();
            string[] allPlayers = message.Receivers.Split(new string[] { Constants.receiverSeperator }, 
                StringSplitOptions.RemoveEmptyEntries);
            PlayerID playerID = (PlayerID)Array.IndexOf(allPlayers, message.Sender);
            if (message.MessageTyp == MessageType.Messages)
            {
                #region Biddinglogic
                if (message.MessageCommand == Command.Bidding)
                {
                    if (game.BidIt(playerID, int.Parse(message.MessageDetail)))
                    {
                        //bid ended
                        pnlBid.Enabled = false;
                        btnMore.Enabled = true;
                    }
                    DisplayPlayerTurn();
                    return;
                } 
                #endregion
                #region NewGameRequsted
                if (message.MessageCommand == Command.EnterConference)
                {
                    //new game requested
                    List<List<PlayCard>> allPlayersCards = new List<List<PlayCard>>();
                    allPlayersCards = UtilityBox.UtilityBoxHelper.XmlDeserializeFromString<List<List<PlayCard>>>(message.MessageDetail);
                    game.NewGame(allPlayersCards[numberOfPlayers][0]);
                    if (game.NumberOfPlayers == allPlayers.Length)
                    {
                        for (int i = 0; i < allPlayers.Length; i++)
                        {
                            game.AllPlayers[i].CardInHand = allPlayersCards[i];
                        }
                    }
                    this.DrawBoard();
                    pnlBid.Enabled = true;
                    lblColorCardValue.Text = game.ColorCard.CrdName.ToString();
                    DisplayPlayerTurn();
                    MessageBox.Show("Color card : " + game.ColorCard.CrdName);
                    return;
                } 
                #endregion
                //make requested move
                PlayCard playedCard = UtilityBoxHelper.XmlDeserializeFromString<PlayCard>(message.MessageDetail);
                bool endCurrentHand = game.MakeTurnPlay(playerID, playedCard);
                RedrawHand(playerID, playedCard, endCurrentHand);
                UpdateScoreInscreen();
                #region Request for new game
                //raise request for new game
                if (game.CurrentRoundEndFlag && playerID == currentPlayerID)
                {
                    //new game
                    ChatMessage enterConferenceMessage = new ChatMessage();
                    enterConferenceMessage.MessageCommand = Command.EnterConference;
                    enterConferenceMessage.MessageTyp = MessageType.Messages;
                    enterConferenceMessage.Receivers = message.Receivers;
                    enterConferenceMessage.Sender = currentUser;
                    PlayCard colorCard = game.AllCards[new Random().Next(52)];
                    game.NewGame(colorCard);
                    List<List<PlayCard>> allPlayersCards = new List<List<PlayCard>>();
                    for (int i = 0; i < allPlayers.Length; i++)
                    {
                        allPlayersCards.Add(game.AllPlayers[i].CardInHand);
                    }
                    //adding color card at last
                    allPlayersCards.Add(new List<PlayCard>() { colorCard });
                    enterConferenceMessage.MessageDetail = UtilityBox.UtilityBoxHelper.XmlSerializeToString(allPlayersCards);
                    StreamWriter sw = new StreamWriter(TC.GetStream());
                    sw.WriteLine(enterConferenceMessage.ToString());
                    sw.Flush();
                }
                #endregion
            }
        }

        private void CardGame_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                StreamWriter sw = new StreamWriter(TC.GetStream());
                ChatMessage message = new ChatMessage();
                message.MessageCommand = Command.LeaveConference;
                message.MessageTyp = MessageType.Messages;
                message.Receivers = this.Text;
                message.Sender = currentUser;
                sw.WriteLine(message.ToString());
                sw.Flush();
            }
            catch
            {
                Environment.Exit(0);
            }
            e.Cancel = true;
        }

        private void SendNetworkPacket(PlayCard playedCard)
        {
            ChatMessage message = new ChatMessage();
            message.Sender = currentUser;
            message.Receivers = this.Text;
            message.MessageCommand = Command.Broadcast;
            message.MessageTyp = MessageType.Messages;
            message.MessageDetail = UtilityBoxHelper.XmlSerializeToString(playedCard);
            StreamWriter sw = new StreamWriter(TC.GetStream());
            sw.WriteLine(message.ToString());
            sw.Flush();
            sw = null;
        }

        private void btnBid_Click(object sender, EventArgs e)
        {
            if (game.PlayerTurn != currentPlayerID)
            {
                MessageBox.Show("Not Your turn. Its " + game.PlayerTurn + "'s turn.");
                return;
            }
            ChatMessage message = new ChatMessage();
            message.Sender = currentUser;
            message.Receivers = this.Text;
            message.MessageCommand = Command.Bidding;
            message.MessageTyp = MessageType.Messages;
            message.MessageDetail = nudBid.Value.ToString();
            StreamWriter sw = new StreamWriter(TC.GetStream());
            sw.WriteLine(message.ToString());
            sw.Flush();
            sw = null;
            pnlBid.Enabled = false;
        }

        private void btnMore_Click(object sender, EventArgs e)
        {
            GameSummary summary = new GameSummary(game, currentPlayerID);
            summary.ShowDialog(this);
        }
    }
}
