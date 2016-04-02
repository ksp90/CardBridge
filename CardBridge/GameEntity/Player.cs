using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameEntity
{
    public class Player
    {
        List<PlayCard> cardInHand = new List<PlayCard>();
        List<PlayCard> wonCards = new List<PlayCard>();

        public List<PlayCard> WonCards
        {
            get { return wonCards; }
        }

        int bidPoint = 0;

        public int BidPoint
        {
            get { return bidPoint; }
            set { bidPoint = value; }
        }

        int handWinCount = 0;

        public int HandWinCount
        {
            get { return handWinCount; }
            set { handWinCount = value; }
        }

        public List<PlayCard> CardInHand
        {
            get { return cardInHand; }
            set { cardInHand = value; }
        }

        public void AddCardInHand(PlayCard card)
        {
            cardInHand.Add(card);
        }

        public void AddWonHand(List<PlayCard> currentHand)
        {
            wonCards.AddRange(currentHand);
        }
    }
}
