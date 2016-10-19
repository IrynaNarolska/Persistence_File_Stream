using Demo_PersistenceFileStream.Controller;
using Demo_PersistenceFileStream.Data;
using System;
using System.Collections.Generic;
using System.IO;
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
        private const int VERT_CURSOR_POS_ERROR_PROMPT = 14;
        private const int HORIZ_CURSOR_POS_ERROR_PROMPT = 3;

        private const int VERT_CURSOR_POS_MENU_PROMPT = 11;
        private const int HORIZ_CURSOR_POS_MENU_PROMPT = 2;

        private ViewState _currentViewState;

        AppController _appController;
        private List<HighScore> highScoresClassListWrite;

        #endregion

        #region PROPERTIES

        public ViewState CurrentViewState
        {
            get { return _currentViewState; }
            set { _currentViewState = value; }
        }

        #endregion

        #region CONSTRUCTORS

        public ConsoleView()
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
            Console.WriteLine("\t5. !!Clear ALL score records!!");
            Console.WriteLine("\n\t6. Quit");

            MainMenuChoice();
        }

        /// <summary>
        /// displays the quit screen
        /// </summary>
        public void DisplayQuitScreen()
        {
            Console.Clear();
            Console.WriteLine("\n\n\n\tThanks for using the Deluxe Score Manager App!");
            Console.WriteLine("\n\t---Press any key to quit---");
            Console.CursorVisible = false;
            Console.ReadKey();
        }

        /// <summary>
        /// displays the updated score prompt
        /// </summary>
        /// <param name="updatedScore"></param>
        public void DisplayUpdatePrompt(HighScore updatedScore)
        {
            Console.CursorVisible = false;
            Console.WriteLine("\tScore for " + updatedScore.PlayerName + " changed to " + updatedScore.PlayerScore);
            Console.Write("\n\tPress any key to return to main menu");
            Console.ReadKey();
        }

        /// <summary>
        /// displays an error message and continue prompt
        /// </summary>
        /// <param name="errorMessage"></param>
        public void DisplayErrorPrompt(string errorMessage)
        {
            Console.SetCursorPosition(HORIZ_CURSOR_POS_ERROR_PROMPT, VERT_CURSOR_POS_ERROR_PROMPT);
            Console.WriteLine("!!***************DATA ERROR ENCOUNTERED***************!!");
            Console.WriteLine("  " + errorMessage);
            Console.WriteLine("\n\tPress any key to continue");

            Console.CursorVisible = false;
            Console.ReadKey();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="highScores"></param>
        /// <returns>list placement and upated score data</returns>
        public int[] DisplayUpdateRecordScreen(List<HighScore> highScores)
        {
            int[] updatedScoreData = { -1, -1 };

            bool selectingPlayer = true;

            // find a valid player from saves
            while (selectingPlayer)
            {
                Console.Clear();
                Console.CursorVisible = true;
                string promptMessage = "";

                Console.WriteLine("\n\tPlease input the name of the player whos score you wish to update");
                Console.Write("\tor press <Enter> to return to the main menu: ");
                string userName = Console.ReadLine();

                if (userName == "")
                {
                    selectingPlayer = false;
                    break;
                }

                // check through all player scores, replace player score if the name entered matches
                bool playerFound = false;

                for (int i = 0; i < highScores.Count; i++)
                {
                    if (highScores[i].PlayerName == userName)
                    {
                        playerFound = true;

                        bool enteringNewScore = true;

                        // enter new player score
                        while (enteringNewScore)
                        {
                            int newScore = -1;

                            Console.Clear();
                            Console.WriteLine("\n\tCurrently " + userName + " has a score of: " + highScores[i].PlayerScore);
                            Console.WriteLine("\n\tPlease input the updated score for " + userName);
                            Console.Write("\tor press <enter> to select a diffent player: ");
                            string userScore = Console.ReadLine();

                            if (userScore == "") { enteringNewScore = false; }
                            else if (int.TryParse(userScore, out newScore) && newScore >= 0)
                            {
                                selectingPlayer = false;
                                enteringNewScore = false;
                                updatedScoreData[0] = i;
                                updatedScoreData[1] = newScore;
                            }

                            else
                            {
                                Console.CursorVisible = false;
                                Console.WriteLine("\n\tValid scores must be postive integers!");
                                Console.Write("\tPress any key to continue");
                                Console.ReadKey();
                                Console.CursorVisible = true;
                            }
                        }

                        i = highScores.Count;
                    }
                }

                Console.CursorVisible = false;
                if (!playerFound)
                {
                    Console.WriteLine("\n\n\tSorry, no player with that name is on record");
                    Console.Write("\tPress any key to try another name");
                    Console.ReadKey();
                }
            }

            return updatedScoreData;
        }
        //TO DO: Iryna will try to finish this tonight. 
        /// <summary>
        /// Adding a record
        /// </summary>
        public string DisplayAddRecordScreen()
        {
            bool addingRecord = true;

            while (addingRecord)
            {
                Console.Clear();
                Console.CursorVisible = true;

                Console.WriteLine("Please enter the name of the player that you want to add.");
                Console.Write("\tor press <Enter> to return to the main menu: ");
                string addName = Console.ReadLine();
                if (addName == "")
                {
                    break;
                }
                Console.WriteLine("Now enter " + addName + "'s score.");
                string addScore = Console.ReadLine();
            }

            _appController.WriteScores();
            return addedRecord;
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
                    _currentViewState = ViewState.DisplayAllRecords;
                    // display all score records selected
                    break;
                case 2:
                    // add score record selected
                    _currentViewState = ViewState.AddRecord;
                    break;
                case 3:
                    // update score record selected
                    _currentViewState = ViewState.UpdateRecord;
                    break;
                case 4:
                    // delete score record selected
                    _currentViewState = ViewState.DeleteRecord;
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
            bool choosing = true;
            while (choosing)
            {
                Console.SetCursorPosition(HORIZ_CURSOR_POS_MENU_PROMPT, VERT_CURSOR_POS_MENU_PROMPT);
                Console.Write("\n\tSelect a menu option between 1 and " + NUMBER_OF_MAIN_MENU_CHOICES + ":  ");
                // check for valid integer from readKey, and make sure integer is in range
                if (int.TryParse(Console.ReadKey().KeyChar.ToString(), out menuChoice) && menuChoice > 0 && menuChoice <= NUMBER_OF_MAIN_MENU_CHOICES)
                {
                    choosing = false;
                }
                else
                {
                    Console.WriteLine("\b \n\n    ***Valid menu choices are numbers 1 - " + NUMBER_OF_MAIN_MENU_CHOICES + "***");
                }
            }

            return menuChoice;
        }

        public void DisplayHighScores(List<HighScore> highScores)
        {

            highScoresClassListWrite = _appController.InitializeListOfHighScores();

            foreach (HighScore player in highScores)
            {
                Console.WriteLine("Player: {0}\tScore: {1}", player.PlayerName, player.PlayerScore);
            }
        }

        //private void ObjectListReadWrite(string dataFile)
        //{
        //    List<HighScore> highScoresClassListWrite = new List<HighScore>();

        //    List<string> highScoresStringListRead = new List<string>(); ;
        //    List<HighScore> highScoresClassListRead = new List<HighScore>(); 

        //    // initialize a list of HighScore objects
        //    highScoresClassListWrite = _appController.InitializeListOfHighScores();

        //    Console.WriteLine("The following high scores will be added to Data.txt.\n");
        //    // display list of high scores objects
        //    DisplayHighScores(highScoresClassListWrite);

        //    Console.WriteLine("\nAdd high scores to text file. Press any key to continue.\n");
        //    Console.ReadKey();

        //    // build the list of strings and write to the text file line by line
        //   _appController.WriteScores();

        //    Console.WriteLine("High scores added successfully.\n");

        //    Console.WriteLine("Read into a string of HighScore and display the high scores from data file. Press any key to continue.\n");
        //    Console.ReadKey();


        //    // build the list of HighScore class objects from the list of strings
        //    highScoresClassListRead = _appController.ReadScores();

        //    // display list of high scores objects
        //    DisplayHighScores(highScoresClassListRead);
        //}


        #endregion


    }
}
