using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_PersistenceFileStream.Controller
{
    class GameException
    {
        //Inherit from main Exception class
        public class PositionChoiceOutOfRangeException : Exception
        {
            //use this constructor to set our own message.
            public PositionChoiceOutOfRangeException(string message) : base(message)
            {
            }
        }

       
    }
}
