using Demo_PersistenceFileStream.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo_PersistenceFileStream.Controller
{
    class AppController
    {
        #region FIELDS

        // instantiate the game view
        private static ConsoleView _gameView = new ConsoleView();

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
        }

        /// <summary>
        /// designates app tasks based on view state
        /// </summary>
        public void ManageStateTasks()
        {
            switch (_gameView.CurrentViewState)
            {
                case ConsoleView.ViewState.WelcomeScreen:
                    break;
                case ConsoleView.ViewState.MainMenu:
                    break;
                case ConsoleView.ViewState.DisplayAllRecords:
                    break;
                case ConsoleView.ViewState.ClearAllRecords:
                    break;
                case ConsoleView.ViewState.AddRecord:
                    break;
                case ConsoleView.ViewState.DeleteRecord:
                    break;
                case ConsoleView.ViewState.UpdateRecord:
                    break;
                case ConsoleView.ViewState.Quit:
                    _usingApp = false;
                    break;
                default:
                    break;
            }
        }

        #endregion
    }

}
