using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace GameEntity
{
    #region CardNumber
    public enum CardNumber
    {
        Ace = 1,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten,
        Jack,
        Queen,
        King
    }
    #endregion

    #region CardName
    public enum CardName
    {
        Club,
        Spade,
        Heart,
        Diamond
    }
    #endregion

    public enum PlayerID
    {
        Player1,
        Player2, 
        Player3,
        Player4
    }
}
