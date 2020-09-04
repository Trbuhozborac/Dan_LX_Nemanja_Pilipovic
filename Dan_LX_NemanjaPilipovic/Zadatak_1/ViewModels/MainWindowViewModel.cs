using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Commands;
using Zadatak_1.Models;
using Zadatak_1.Views;

namespace Zadatak_1.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        #region Objects

        MainWindow main;
        BackgroundWorker bgWorker = new BackgroundWorker();

        #endregion

        #region Constructors

        public MainWindowViewModel(MainWindow mainOpen)
        {
            main = mainOpen;
            WriteLocations();
            AllInfoAboutWorker = GetAllInfoAboutWorkers();
        }

        #endregion

        #region Properties

        private List<vwWorker> allInfoAboutWorker;

        public List<vwWorker> AllInfoAboutWorker
        {
            get { return allInfoAboutWorker; }
            set
            {
                allInfoAboutWorker = value;
                OnPropertyChanged("AllInfoAboutWorker");
            }
        }

        private vwWorker viewWorker;

        public vwWorker ViewWorker
        {
            get { return viewWorker; }
            set
            {
                viewWorker = value;
                OnPropertyChanged("ViewWorker");
            }
        }

        #endregion

        #region Commands

        private ICommand addWorker;

        public ICommand AddWorker
        {
            get
            {
                if (addWorker == null)
                {
                    addWorker = new RelayCommand(param => AddNewWorker(), param => CanAddNewWorker());
                }
                return addWorker;
            }
        }

        private ICommand delete;
        public ICommand Delete
        {
            get
            {
                if (delete == null)
                {
                    delete = new RelayCommand(param => DeleteWorker(), param => CanDeleteWorker());
                }
                return delete;
            }
        }

        private ICommand update;
        public ICommand Update
        {
            get
            {
                if (update == null)
                {
                    update = new RelayCommand(param => UpdateWorker(), param => CanUpdateWorker());
                }
                return update;
            }
        }

        #endregion

        #region Functions

        /// <summary>
        /// Gets all info about Workers
        /// </summary>
        /// <returns>All Workers and info about them</returns>
        private List<vwWorker> GetAllInfoAboutWorkers()
        {
            try
            {
                using (WorkersDbEntities db = new WorkersDbEntities())
                {
                    List<vwWorker> allInfoAboutWorkers = new List<vwWorker>();
                    allInfoAboutWorkers = db.vwWorkers.OrderBy(X => X.Id).ToList();
                    AllInfoAboutWorker = allInfoAboutWorkers;
                    return AllInfoAboutWorker;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
                return null;
            }
        }


        /// <summary>
        /// Gets all Locations form txt file and write them into the database
        /// </summary>
        private void WriteLocations()
        {
            using (WorkersDbEntities db = new WorkersDbEntities())
            {
                List<tblLocation> locations = new List<tblLocation>();
                foreach (tblLocation location1 in db.tblLocations)
                {
                    locations.Add(location1);
                }
                if (locations.Count == 0)
                {
                    string location = @"~\..\..\..\Locations.txt";
                    if (File.Exists(location))
                    {
                        string[] allLocations = File.ReadAllLines(location);
                        foreach (string oneLocation in allLocations)
                        {
                            string[] separeted = oneLocation.Split(',');
                            string address = separeted[0];
                            string city = separeted[1];
                            string country = separeted[2];
                            tblLocation newLocation = new tblLocation();
                            newLocation.Address = address;
                            newLocation.City = city;
                            newLocation.Country = country;
                            using (WorkersDbEntities dbase = new WorkersDbEntities())
                            {
                                dbase.tblLocations.Add(newLocation);
                                dbase.SaveChanges();
                            }
                        }
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine("File not found");
                    }
                }
            }

        }

        /// <summary>
        /// Open new window for creating new Worker
        /// </summary>
        private void AddNewWorker()
        {
            AddNewWorker card = new AddNewWorker();
            card.ShowDialog();
            AllInfoAboutWorker = GetAllInfoAboutWorkers();
        }

        /// <summary>
        /// Checks if User can add new worker
        /// </summary>
        /// <returns></returns>
        private bool CanAddNewWorker()
        {
            return true;
        }

        /// <summary>
        /// Deletes Selected Worker
        /// </summary>
        private void DeleteWorker()
        {
            try
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete Confirmation", MessageBoxButton.YesNo);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    using (WorkersDbEntities db = new WorkersDbEntities())
                    {
                        bgWorker.DoWork += WorkerDelete;
                        bgWorker.RunWorkerCompleted += WorkerDeleted;
                        bgWorker.RunWorkerAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Checks if Worker can be deleted
        /// </summary>
        /// <returns></returns>
        private bool CanDeleteWorker()
        {
            return true;
        }

        /// <summary>
        /// Deletes Worker from database
        /// </summary>
        private void WorkerDelete(object sender, DoWorkEventArgs e)
        {
            try
            {
                using (WorkersDbEntities db = new WorkersDbEntities())
                {
                    Thread.Sleep(2000);
                    tblWorker worker = db.tblWorkers.Where(x => x.Id == viewWorker.Id).FirstOrDefault();
                    db.tblWorkers.Remove(worker);
                    db.SaveChanges();
                    AllInfoAboutWorker = GetAllInfoAboutWorkers();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Notify User that Worker is deleted, also log info into txt file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void WorkerDeleted(object sender, RunWorkerCompletedEventArgs e)
        {
            MessageBox.Show("Worker Deleted Successfully!");
            string location = @"~\..\..\..\Log.txt";
            if (File.Exists(location))
            {
                string info = $"\n[{DateTime.Now.ToShortDateString()}] [{DateTime.Now.ToShortTimeString()}] [Worker Deleted]";
                File.AppendAllText(location, info);
            }
        }

        /// <summary>
        /// Open new window for updating Worker
        /// </summary>
        private void UpdateWorker()
        {
            UpdateCurrentWorker card = new UpdateCurrentWorker(FindWorker());
            card.ShowDialog();
            AllInfoAboutWorker = GetAllInfoAboutWorkers();

        }


        /// <summary>
        /// Checks if User can update Worker
        /// </summary>
        /// <returns></returns>
        private bool CanUpdateWorker()
        {
            return true;
        }

        /// <summary>
        /// Find Worker thats being updated
        /// </summary>
        /// <returns>Worker thats being updated</returns>
        private tblWorker FindWorker()
        {
            try
            {
                tblWorker worker = new tblWorker();
                using (WorkersDbEntities db = new WorkersDbEntities())
                {
                    worker = db.tblWorkers.Where(x => x.Id == ViewWorker.Id).FirstOrDefault();
                    return worker;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
                return null;
            }
        }


        #endregion

    }
}
