using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_PersistenceFileStream.View
{
    class ConsoleView
    {

        #region ENUMS

        public enum ViewState
        {
            MainMenu,
            DisplayAllRecords,
            ClearAllRecords,
            AddRecord,
            DeleteRecord,
            UpdateRecord
        }

        #endregion
    }
}
