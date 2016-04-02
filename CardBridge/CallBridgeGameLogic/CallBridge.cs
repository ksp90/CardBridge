using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using GameEntity;

namespace TestApplication
{
    public class CallBridge
    {
        const string folderName = "Resources";

        List<PlayCard> allCards;

        public List<PlayCard> AllCards
        {
            get { return allCards; }
        }

        List<PlayCard> suffledCards = new List<PlayCard>();

        PlayCard colorCard = new PlayCard() { CrdName = CardName.Heart };

        public PlayCard ColorCard
        {
            get { return colorCard; }
        }

        PlayCard currentPlayingCard;

        public PlayCard CurrentPlayingCard
        {
            get { return currentPlayingCard; }
        }

        int[] playerResults;

        public int[] PlayerResults
        {
            get { return playerResults; }
        }

        string[] playerNames;

        public string[] PlayerNames
        {
            get { return playerNames; }
            set { playerNames = value; }
        }

        PlayerID playerTurn;

        public PlayerID PlayerTurn
        {
            get { return playerTurn; }
        }

        int numberOfCardsPerPlayer = 13;

        public int NumberOfCardsPerPlayer
        {
            get { return numberOfCardsPerPlayer; }
        }

        int numberOfPlayers = 4;

        public int NumberOfPlayers
        {
            get { return numberOfPlayers; }
        }

        Player[] allPlayers;

        public Player[] AllPlayers
        {
            get { return allPlayers; }
            set { allPlayers = value; }
        }

        SortedList<PlayerID, PlayCard> handDownCards = new SortedList<PlayerID, PlayCard>();

        int highestBid = 1;

        public int HighestBid
        {
            get { return highestBid; }
        }

        bool bidSettled = false;

        bool currentRoundEndFlag = false;

        public bool CurrentRoundEndFlag
        {
            get { return currentRoundEndFlag; }
        }

        public bool BidSettled
        {
            get { return bidSettled; }
        }

        PlayerID bidWonner = PlayerID.Player1;

        int numberOfbids = 0;

        public CallBridge(int numberOfPlayers, int numberOfCardsPerPlayer,PlayCard colorCard)
        {
            this.numberOfPlayers = numberOfPlayers;
            this.numberOfCardsPerPlayer = numberOfCardsPerPlayer;
            playerResults = new int[numberOfPlayers];
            playerNames = new string[numberOfPlayers];
            this.colorCard = colorCard;
            InitializeAllCards();
            SuffleCards();
            InitializePlayers();
            playerTurn = PlayerID.Player1;
        }

        public void NewGame(PlayCard colorCard)
        {
            this.colorCard = colorCard;
            SuffleCards();
            InitializePlayers();
            bidSettled = false;
            currentRoundEndFlag = false;
            bidWonner = playerTurn;
            numberOfbids = 0;
        }

        private void InitializeAllCards()
        {
            string suffixName = string.Empty;
            allCards = new List<PlayCard>();
            allCards.Capacity = 52;

            for (int i = 1; i <= 10; i++)
            {
                if (i == 1)
                {
                    suffixName = "A";
                }
                else
                {
                    suffixName = i.ToString();
                }

                allCards.Add(new PlayCard()
                {
                    CrdName = CardName.Club,
                    CrdNumber = (CardNumber)i,
                    ImageName = (folderName + "/" + CardName.Club.ToString().ToLower() + "_" + suffixName + ".png"),
                    BackImage = (folderName + "/backCardImage.png")
                });
                allCards.Add(new PlayCard()
                {
                    CrdName = CardName.Diamond,
                    CrdNumber = (CardNumber)i,
                    ImageName = (folderName + "/" + CardName.Diamond.ToString().ToLower() + "_" + suffixName + ".png"),
                    BackImage = (folderName + "/backCardImage.png")
                });
                allCards.Add(new PlayCard()
                {
                    CrdName = CardName.Heart,
                    CrdNumber = (CardNumber)i,
                    ImageName = (folderName + "/" + CardName.Heart.ToString().ToLower() + "_" + suffixName + ".png"),
                    BackImage = (folderName + "/backCardImage.png")
                });
                allCards.Add(new PlayCard()
                {
                    CrdName = CardName.Spade,
                    CrdNumber = (CardNumber)i,
                    ImageName = (folderName + "/" + CardName.Spade.ToString().ToLower() + "_" + suffixName + ".png"),
                    BackImage = (folderName + "/backCardImage.png")
                });
            }

            for (int i = 0; i < 4; i++)
            {
                allCards.Add(new PlayCard()
                {
                    CrdName = (CardName)i,
                    CrdNumber = CardNumber.Jack,
                    ImageName = (folderName + "/" + ((CardName)i).ToString().ToLower() + "_J" + ".png"),
                    BackImage = (folderName + "/backCardImage.png")
                });
                allCards.Add(new PlayCard()
                {
                    CrdName = (CardName)i,
                    CrdNumber = CardNumber.Queen,
                    ImageName = (folderName + "/" + ((CardName)i).ToString().ToLower() + "_Q" + ".png"),
                    BackImage = (folderName + "/backCardImage.png")
                });
                allCards.Add(new PlayCard()
                {
                    CrdName = (CardName)i,
                    CrdNumber = CardNumber.King,
                    ImageName = (folderName + "/" + ((CardName)i).ToString().ToLower() + "_K" + ".png"),
                    BackImage = (folderName + "/backCardImage.png")
                });
            }
        }

        private void InitializePlayers()
        {
            allPlayers = new Player[numberOfPlayers];
            for (int i = 0; i < numberOfPlayers; i++)
            {
                allPlayers[i] = new Player();
            }
            for (int i = 0; i < numberOfCardsPerPlayer; i++)
            {
                for (int j = 0; j < numberOfPlayers; j++)
                {
                    allPlayers[j].AddCardInHand(suffledCards[0]);
                    suffledCards.RemoveAt(0);
                }
            }
        }

        private void SuffleCards()
        {
            suffledCards.Clear();
            List<int> indexes = new List<int>();
            Random rnd = new Random();
            for (int i = 0; i < 52; i++)
            {
                indexes.Add(i);
            }

            while (indexes.Count != 0)
            {
                int randIndex = rnd.Next(0, indexes.Count);
                suffledCards.Add(allCards[indexes[randIndex]]);
                indexes.RemoveAt(randIndex);
            }
        }

        public bool MakeTurnPlay(PlayerID player, PlayCard playedCard)
        {
            bool res = false;
            if (player == playerTurn)
            {
                handDownCards.Add(player, playedCard);

                allPlayers[(int)player].CardInHand.Remove(playedCard);
                if (handDownCards.Count==1)
                {
                    currentPlayingCard = playedCard;
                }
                if (handDownCards.Count == numberOfPlayers)
                {
                    //make dicission who will win this hand here
                    playerTurn = CalculateScore();
                    allPlayers[(int)playerTurn].HandWinCount++;
                    res = true;
                    if (allPlayers[(int)playerTurn].CardInHand.Count==0)
                    {
                        //determine score
                        DeterMineScoreForPlayers();
                        currentRoundEndFlag = true;
                    }
                }
                else
                {
                    playerTurn = ((int)player == numberOfPlayers-1) ? (PlayerID.Player1) : ((PlayerID)(((int)player) + 1));
                }
            }
            return res;
        }

        private PlayerID CalculateScore()
        {
            PlayerID winner;
            SortedList<int, PlayerID> scoreList = new SortedList<int, PlayerID>();
            for (int i = 0; i < numberOfPlayers; i++)
            {
                PlayCard card = handDownCards[(PlayerID)i];
                int tempScore = (int)card.CrdNumber;

                //check for other cards but not color card
                if (card.CrdName != currentPlayingCard.CrdName && card.CrdName != colorCard.CrdName)
                {
                    tempScore = -1 * (int)card.CrdNumber;
                }
                if (card.CrdName == colorCard.CrdName && card.EnableColorPower)
                {
                    tempScore += 20;
                }
                if (card.CrdNumber == CardNumber.Ace) //ace should be highest scorer
                {
                    tempScore += 15;
                }
                while (true)
                {
                    try
                    {
                        scoreList.Add(tempScore, (PlayerID)i);
                        break;
                    }
                    catch (Exception)
                    {
                        //same card when-- no available playing card
                        tempScore--;
                    }
                }
            }
            winner = scoreList.Values[numberOfPlayers-1];
            for (int i = 0; i < numberOfPlayers; i++)
            {
                allPlayers[(int)winner].WonCards.Add(handDownCards[(PlayerID)i]);
            }
            handDownCards = new SortedList<PlayerID, PlayCard>();
            return winner;
        }

        public bool BidIt(PlayerID player, int bidNum)
        {
            bool bidEndFlag = false;
            //passing
            #region 29 style
            //if (bidNum == highestBid && bidPassCounter < numberOfPlayers-1)
            //{
            //    bidPassCounter++;
            //    playerTurn = ((int)player == numberOfPlayers - 1) ? (PlayerID.Player1) : ((PlayerID)(((int)player) + 1));
            //    return bidEndFlag;
            //}
            //if (bidPassCounter == numberOfPlayers - 1)
            //{
            //    bidEndFlag = true;
            //    bidSettled = true;
            //    playerTurn = player;
            //    return bidEndFlag;
            //}
            //bidWonner = player;
            //highestBid = bidNum; 
            #endregion
            numberOfbids++;
            allPlayers[(int)player].BidPoint = bidNum;
            if (numberOfbids==numberOfPlayers)
            {
                bidSettled = true;
                bidEndFlag = true;
            }
            playerTurn = ((int)player == numberOfPlayers - 1) ? (PlayerID.Player1) : ((PlayerID)(((int)player) + 1));
            return bidEndFlag;
        }

        private void DeterMineScoreForPlayers()
        {
            for (int i = 0; i < numberOfPlayers; i++)
            {
                if ((allPlayers[i].HandWinCount - allPlayers[i].BidPoint)>2)
                {
                    playerResults[i] -= allPlayers[i].HandWinCount;
                }
                else if (allPlayers[i].HandWinCount >= allPlayers[i].BidPoint)
                {
                    playerResults[i] += allPlayers[i].HandWinCount;
                }
                else
                {
                    playerResults[i] -= allPlayers[i].BidPoint;
                }
            }
        }

        public bool IsValidCardForPlay(PlayCard card, PlayerID player)
        {
            bool res = false;
            if (handDownCards.Count==0)
            {
                //first player innitiating play
                res = true;
            }
            else
            {
                bool isSameCardFound = false;
                for (int i = 0; i < allPlayers[(int)player].CardInHand.Count; i++)
                {
                    if (currentPlayingCard.CrdName == allPlayers[(int)player].CardInHand[i].CrdName)
                    {
                        isSameCardFound = true;
                        break;
                    }
                }
                if (currentPlayingCard.CrdName == card.CrdName)
                {
                    res = true;
                }
                if (!isSameCardFound)
                {
                    //no same card found all cards are valis
                    res = true;
                }
            }
            return res;
        }
    }
}
