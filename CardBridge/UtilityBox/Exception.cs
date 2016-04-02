using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UtilityBox
{

    public class InvalidMessageException : Exception
    {
        public InvalidMessageException()
        {

        }
        public InvalidMessageException(string msg)
            : base(msg)
        {

        }
    }
}
