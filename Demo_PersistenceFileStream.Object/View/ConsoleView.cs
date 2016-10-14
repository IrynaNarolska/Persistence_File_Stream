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
            WelcomeScreen,
            MainMenu,
            DisplayAllRecords,
            ClearAllRecords,
            AddRecord,
            DeleteRecord,
            UpdateRecord,
            Quit
        }

        #endregion

        #region FIELDS

        private const int NUMBER_OF_MAIN_MENU_CHOICES = 6;

        // constants for cursor positioning
        private const int VERT_CURSOR_POS_ERROR_PROMPT = 10;
        private const int HORIZ_CURSOR_POS_ERROR_PROMPT = 3;

        private ViewState _currentViewState;

        #endregion

        #region PROPERTIES

        public ViewState CurrentViewState
        {
            get { return _currentViewState; }
            set { _currentViewState = value; }
        }

        #endregion

        #region CONSTRUCTORS

        public ConsoleView ()
        {
            _currentViewState = ViewState.WelcomeScreen;
        }

        #endregion

        #region METHODS

        /// <summary>
        /// displays the welcome screen
        /// </summary>
        public void DisplayWelcomeScreen()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\tWelcome to the Deluxe Score Manager App!");
            Console.WriteLine("\n\t---Press any key to continue---");
            Console.CursorVisible = false;
            Console.ReadKey();
            _currentViewState = ViewState.MainMenu;
        }

        /// <summary>
        /// displays the main menu screen
        /// </summary>
        public void DisplayMainMenuScreen()
        {
            Console.Clear();
            Console.WriteLine("\n\tPlease select a menu option from the following:");
            Console.WriteLine("\n\t1. Display all score records");
            Console.WriteLine("\t2. Add score record");
            Console.WriteLine("\t3. Update score record");
            Console.WriteLine("\t4. Delete score record");
            Console.WriteLine("\t5. !!Delete ALL score records!!");
            Console.WriteLine("\n\t6. Quit");

            MainMenuChoice();
        }

        /// <summary>
        /// displays an error message and continue prompt
        /// </summary>
        /// <param name="errorMessage"></param>
        public void DisplayErrorPrompt(string errorMessage)
        {
            Console.SetCursorPosition(HORIZ_CURSOR_POS_ERROR_PROMPT, VERT_CURSOR_POS_ERROR_PROMPT);
            Console.WriteLine("!!***************DATA ERROR ENCOUNTERED***************!!");
            Console.WriteLine(errorMessage);
            Console.WriteLine("\n\tPress any key to continue");

            Console.CursorVisible = false;
            Console.ReadKey();
        }

        /// <summary>
        /// displays the update record screen
        /// </summary>
        public void DisplayUpdateRecordScreen()
        {
        
            //TODO implement update record screen and functionality!

        }

        /// <summary>
        /// obtain and carry out menu selection
        /// </summary>
        private void MainMenuChoice()
        {
            int menuChoice = GetMainMenuChoice();

            switch (menuChoice)
            {
                case 1:
                    // display all score records selected
                    break;
                case 2:
                    // add score record selected
                    break;
                case 3:
                    // update score record selected
                    break;
                case 4:
                    // delete score record selected
                    break;
                case 5:
                    // delete ALL score records selected
                    _currentViewState = ViewState.ClearAllRecords;
                    break;
                case 6:
                    // quit selected
                    _currentViewState = ViewState.Quit;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// gets main menu choice
        /// </summary>
        /// <returns int > main menu choice</returns>
        private int GetMainMenuChoice()
        {
            int menuChoice = -1;

            //TODO add menu choice functionality

            return menuChoice;
        }

        #endregion


    }
}
