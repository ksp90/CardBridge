using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TestApplication;
using GameEntity;

namespace BroadcastChat
{
    public partial class GameSummary : Form
    {
        public GameSummary()
        {
            InitializeComponent();
        }

        public GameSummary(CallBridge currentGame, PlayerID currentPlayer)
        {
            InitializeComponent();
            //
            lblColorCardValue.Text = currentGame.ColorCard.CrdName.ToString();
            lblYourBid.Text = "You bid for " + currentGame.AllPlayers[(int)currentPlayer].BidPoint.ToString()
                + " hand(s)";
            lblYourCurrentScore.Text = "You won " + currentGame.AllPlayers[(int)currentPlayer].HandWinCount.ToString()
                + " hand(s)";
            lblYourTotalScore.Text = "You : " + currentGame.PlayerResults[(int)currentPlayer].ToString();

            int j = 1;
            for (int i = 0; i < currentGame.NumberOfPlayers; i++)
            {
                if ((PlayerID)i == currentPlayer)
                {
                    continue;
                }
                else
                {
                    gbBiddingDetails.Controls[j].Text = ((PlayerID)i).ToString() + " bid for "
                        + currentGame.AllPlayers[i].BidPoint.ToString() + " hand(s)";
                    gbCurrentScore.Controls[j].Text = ((PlayerID)i).ToString() + " won " + currentGame.AllPlayers[(int)i].HandWinCount.ToString()
                        + " hand(s)";
                    gbTotalScore.Controls[j].Text = ((PlayerID)i).ToString() + " : " + currentGame.PlayerResults[(int)i].ToString();

                    j = j + 1;
                }
            }
            //
        }
    }
}
