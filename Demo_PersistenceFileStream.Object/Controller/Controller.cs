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
        private static ConsoleView _appView = new ConsoleView();

        // instantiate model
        private static List<HighScore> _highScores = new List<HighScore>();

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
            switch (_appView.CurrentViewState)
            {
                case ConsoleView.ViewState.WelcomeScreen:
                    _appView.DisplayWelcomeScreen();
                    break;
                case ConsoleView.ViewState.MainMenu:
                    _appView.DisplayMainMenuScreen();
                    break;
                case ConsoleView.ViewState.DisplayAllRecords:
                    WriteScores();
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
                    _appView.DisplayQuitScreen();
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
                ReadScores();

                // create score data as an array of list index + updated score info
                int[] scoreData = _appView.DisplayUpdateRecordScreen(_highScores);
                
                // if valid score data is returned, update scorelist and write to file using score data array
                if(scoreData[0] != -1)
                {
                    _highScores[scoreData[0]].PlayerScore = scoreData[1];
                    WriteScores();
                    _appView.DisplayUpdatePrompt(_highScores[scoreData[0]]);
                }
            }
            catch (Exception ex)
            {
                _appView.DisplayErrorPrompt(ex.Message);
            }
            _appView.CurrentViewState = ConsoleView.ViewState.MainMenu;
        }

//TODO - get ReadScores method to sync for sure with correct file
        /// <summary>
        /// attempts to read scores from the data file and overwrites the current stored score list
        /// </summary>
        private void ReadScores()
        {
            try
            {
                List<string> scoresStringList = new List<string>();

                _highScores.Clear();

                // read each line and put it into an array and convert the array to a list
                scoresStringList = File.ReadAllLines(DataStructure.textFilePath).ToList();

                foreach (string highScoreString in scoresStringList)
                {
                    // use the Split method and the delineator on the array to separate each property into an array of properties
                    string[] properties = highScoreString.Split(DataStructure.delineator);

                    _highScores.Add(new HighScore() { PlayerName = properties[0], PlayerScore = Convert.ToInt32(properties[1]) });
                }
            }

            catch (Exception)
            {
                throw;
            }

        }
//TODO - get WriteScores method to sync for sure with the right file
        /// <summary>
        /// Write all high scores to the data file
        /// </summary>
        private void WriteScores()
        {
            try
            {
                string highScoreString;

                List<string> highScoresStringListWrite = new List<string>();

                // build the list to write to the text file line by line
                foreach (var scoreListing in _highScores)
                {
                    highScoreString = scoreListing.PlayerName + DataStructure.delineator + scoreListing.PlayerScore;
                    highScoresStringListWrite.Add(highScoreString);
                }

                File.Delete(DataStructure.textFilePath);
                File.WriteAllLines(DataStructure.textFilePath, highScoresStringListWrite);
     
            }

            catch (Exception)
            {
                throw;
            }

        }

        private void ClearScores()
        {

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
        #endregion
    }

}
