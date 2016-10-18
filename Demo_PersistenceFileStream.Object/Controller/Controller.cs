using Demo_PersistenceFileStream.Data;
using Demo_PersistenceFileStream.View;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_PersistenceFileStream.Controller
{
    class AppController
    {
        #region FIELDS

        // instantiate the app view
       // private static ConsoleView _appView = new ConsoleView();

        // instantiate model
        public static List<HighScore> highScores = new List<HighScore>();

        List<string> highScoresStringListWrite = new List<string>();

        private static ConsoleView _consoleView = new ConsoleView();

        string highScoreString;

        string dataFile;

        // track app use
        private bool _usingApp;


        #endregion

        #region CONSTRUCTORS

        public AppController()
        {
            InitializeApp();
            UseApp();
        }

        #endregion

        #region METHODS

        /// <summary>
        /// sets up app on initialization
        /// </summary>
        public void InitializeApp()
        {
            _usingApp = true;
        }

        /// <summary>
        /// The main app loop
        /// </summary>
        public void UseApp()
        {
            while (_usingApp)
            {
                ManageStateTasks();
            }

            System.Environment.Exit(1);
        }

        /// <summary>
        /// designates app tasks based on the current view state
        /// </summary>
        public void ManageStateTasks()
        {
            switch (_consoleView.CurrentViewState)
            {
                case ConsoleView.ViewState.WelcomeScreen:
                    _consoleView.DisplayWelcomeScreen();
                    break;
                case ConsoleView.ViewState.MainMenu:
                    _consoleView.DisplayMainMenuScreen();
                    break;
                case ConsoleView.ViewState.DisplayAllRecords:
                    _consoleView.DisplayHighScores(highScores);
                    break;
                case ConsoleView.ViewState.ClearAllRecords:
                    break;
                case ConsoleView.ViewState.AddRecord:
                    break;
                case ConsoleView.ViewState.DeleteRecord:
                    break;
                case ConsoleView.ViewState.UpdateRecord:
                    ProcessUpdateRecord();
                    break;
                case ConsoleView.ViewState.Quit:
                    _consoleView.DisplayQuitScreen();
                    _usingApp = false;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// query, check and potentially update score record
        /// </summary>
        public void ProcessUpdateRecord()
        {
            try
            {
                ReadHighScoresFromTextFile(dataFile);

                // create score data as an array of list index + updated score info
                int[] scoreData = _consoleView.DisplayUpdateRecordScreen(highScores);
                
                // if valid score data is returned, update scorelist and write to file using score data array
                if(scoreData[0] != -1)
                {
                    highScores[scoreData[0]].PlayerScore = scoreData[1];
                    WriteHighScoresToTextFile(highScores, dataFile);
                    _consoleView.DisplayUpdatePrompt(highScores[scoreData[0]]);
                }
            }
            catch (Exception ex)
            {
                _consoleView.DisplayErrorPrompt(ex.Message);
            }
            _consoleView.CurrentViewState = ConsoleView.ViewState.MainMenu;
        }

   
 

        ////TODO - get ReadScores method to sync for sure with correct file
        ///// <summary>
        ///// attempts to read scores from the data file and overwrites the current stored score list
        ///// </summary>
        //private List<HighScore> ReadScores()
        //{
        //    try
        //    {

        //        List<string> scoresStringList = new List<string>();

        //        highScores.Clear();

        //        // read each line and put it into an array and convert the array to a list
        //        scoresStringList = File.ReadAllLines(DataStructure.textFilePath).ToList();

        //        foreach (string highScoreString in scoresStringList)
        //        {
        //            // use the Split method and the delineator on the array to separate each property into an array of properties
        //            string[] properties = highScoreString.Split(DataStructure.delineator);

        //            highScores.Add(new HighScore() { PlayerName = properties[0], PlayerScore = Convert.ToInt32(properties[1]) });

        //        }

        //        return highScores;
        //    }

        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}

        ///// <summary>
        ///// Write all high scores to the data file
        ///// </summary>
        //private void WriteScores()
        //{
        //    try
        //    {

        //        // build the list to write to the text file line by line
        //        foreach (var player in highScores)
        //        {
        //            highScoreString = player.PlayerName + DataStructure.delineator + player.PlayerScore;
        //            highScoresStringListWrite.Add(highScoreString);
        //        }

        //        //File.Delete(DataStructure.textFilePath);
        //        File.WriteAllLines(DataStructure.textFilePath, highScoresStringListWrite);
     
        //    }

        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}

        /// <summary>
        /// clears all high scores from the text file
        /// </summary>
        private void ClearScores()
        {
            foreach (var scoreListing in highScores)
            {
                highScoreString = scoreListing.PlayerName + DataStructure.delineator + scoreListing.PlayerScore;
                highScoresStringListWrite.Add(highScoreString);
            }

            File.WriteAllText(DataStructure.textFilePath, string.Empty);

            

        }
                private void AddRecord()
        {
            try
            {


            }
            catch (Exception)
            {
                throw;
            }
        }
        private void DeleteRecord()
        {

        }



        public List<HighScore> InitializeListOfHighScores()
        {
            List<HighScore> highScoresClassList = new List<HighScore>();

            // initialize the IList of high scores - note: no instantiation for an interface
            highScoresClassList.Add(new HighScore() { PlayerName = "John", PlayerScore = 1296 });
            highScoresClassList.Add(new HighScore() { PlayerName = "Joan", PlayerScore = 345 });
            highScoresClassList.Add(new HighScore() { PlayerName = "Jeff", PlayerScore = 867 });
            highScoresClassList.Add(new HighScore() { PlayerName = "Charlie", PlayerScore = 2309 });

            return highScoresClassList;
        }


        public void WriteHighScoresToTextFile(List<HighScore> highScoreClassLIst, string dataFile)
        {
            string highScoreString;

            List<string> highScoresStringListWrite = new List<string>();

            // build the list to write to the text file line by line
            foreach (var player in highScoreClassLIst)
            {
                highScoreString = player.PlayerName + "," + player.PlayerScore;
                highScoresStringListWrite.Add(highScoreString);
            }

            File.WriteAllLines(dataFile, highScoresStringListWrite);
        }

        public List<HighScore> ReadHighScoresFromTextFile(string dataFile)
        {
            List<string> highScoresStringList = new List<string>();

            

            // read each line and put it into an array and convert the array to a list
            highScoresStringList = File.ReadAllLines(dataFile).ToList();

            foreach (string highScoreString in highScoresStringList)
            {
                // use the Split method and the delineator on the array to separate each property into an array of properties
                string[] properties = highScoreString.Split(DataStructure.delineator);

                highScores.Add(new HighScore() { PlayerName = properties[0], PlayerScore = Convert.ToInt32(properties[1]) });
            }

            return highScores;
        }
        #endregion
    }



}
