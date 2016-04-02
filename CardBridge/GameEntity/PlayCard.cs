using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml.Serialization;

namespace GameEntity
{
    public class PlayCard : IComparable
    {
        CardName crdName;

        public CardName CrdName
        {
            get { return crdName; }
            set { crdName = value; }
        }

        CardNumber crdNumber;

        public CardNumber CrdNumber
        {
            get { return crdNumber; }
            set { crdNumber = value; }
        }

        string imageName;

        public string ImageName
        {
            get { return imageName; }
            set { imageName = value; }
        }

        string backImage;

        public string BackImage
        {
            get { return backImage; }
            set { backImage = value; }
        }

        bool enableColorPower = true;

        public bool EnableColorPower
        {
            get { return enableColorPower; }
            set { enableColorPower = value; }
        }

        public PlayCard()
        {

        }

        public int CompareTo(object obj)
        {
            if (obj is PlayCard)
            {
                PlayCard tagetCard = obj as PlayCard;
                return (((int)crdName) * 52 + ((int)crdNumber)).
                    CompareTo(((int)tagetCard.CrdName) * 52 + ((int)tagetCard.CrdNumber));
            }
            else
            {
                return -1;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is PlayCard)
            {
                PlayCard tmp = obj as PlayCard;
                return (crdName.Equals(tmp.CrdName) && crdNumber.Equals(tmp.CrdNumber));
            }
            else
            {
                return false;
            }
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public static CardName ResolveCardName(string name)
        {
            if (name.Equals(CardName.Club.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                return CardName.Club;
            }
            else if (name.Equals(CardName.Diamond.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                return CardName.Diamond;
            }
            else if (name.Equals(CardName.Heart.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                return CardName.Heart;
            }

            else if (name.Equals(CardName.Spade.ToString(), StringComparison.CurrentCultureIgnoreCase))
            {
                return CardName.Spade;
            }
            else
            {
                return (CardName)(-1);
            }
        }
    }
}
