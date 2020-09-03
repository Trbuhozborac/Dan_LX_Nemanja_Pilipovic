using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Zadatak_1.Commands;
using Zadatak_1.Models;
using Zadatak_1.Views;

namespace Zadatak_1.ViewModels
{
    public class AddNewWorkerViewModel : BaseViewModel
    {
        #region Objects

        AddNewWorker addNewWorker;
        BackgroundWorker bgWorker = new BackgroundWorker();

        #endregion

        #region Constructors

        public AddNewWorkerViewModel(AddNewWorker workerViewOpen)
        {
            Worker = new tblWorker();
            addNewWorker = workerViewOpen;
            NewSector = new tblSector();
            NewLocation = new tblLocation();
            Genders = GetAllGenders();
            Locations = GetAllLocations();
            addNewWorker = workerViewOpen;
        }

        #endregion

        #region Properties

        static int counter = 0;

        private tblWorker worker;

        public tblWorker Worker
        {
            get { return worker; }
            set
            {
                worker = value;
                OnPropertyChanged("Worker");
            }
        }

        private string gender;

        public string Gender
        {
            get { return gender; }
            set
            {
                gender = value;
                OnPropertyChanged("Gender");
            }
        }


        private string sector;

        public string Sector
        {
            get { return sector; }
            set
            {
                sector = value;
                OnPropertyChanged("Sector");
            }
        }

        private tblSector newSector;

        public tblSector NewSector
        {
            get { return newSector; }
            set
            {
                newSector = value;
                OnPropertyChanged("NewSector");
            }
        }


        private List<string> genders;

        public List<string> Genders
        {
            get { return genders; }
            set
            {
                genders = value;
                OnPropertyChanged("Genders");
            }
        }

        private string location;

        public string Location
        {
            get { return location; }
            set
            {
                location = value;
                OnPropertyChanged("Location");
            }
        }

        private tblLocation newLocation;

        public tblLocation NewLocation
        {
            get { return newLocation; }
            set
            {
                newLocation = value;
                OnPropertyChanged("NewLocation");
            }
        }


        private List<string> locations;

        public List<string> Locations
        {
            get { return locations; }
            set
            {
                locations = value;
                OnPropertyChanged("Locations");
            }
        }

        private string dateOfBirth;

        public string DateOfBirth
        {
            get { return dateOfBirth; }
            set
            {
                dateOfBirth = value;
                OnPropertyChanged("DateOfBirth");
            }
        }

        #endregion

        #region Commands

        private ICommand save;
        public ICommand Save
        {
            get
            {
                if (save == null)
                {
                    save = new RelayCommand(param => SaveExecute(), param => CanSaveExecute());
                }
                return save;
            }
        }

        private ICommand close;
        public ICommand Close
        {
            get
            {
                if (close == null)
                {
                    close = new RelayCommand(param => CloseExecute(), param => CanCloseExecute());
                }
                return close;
            }

        }



        #endregion

        #region Functions

        /// <summary>
        /// Gets all genders from Database
        /// </summary>
        /// <returns>All genders</returns>
        private List<string> GetAllGenders()
        {
            List<string> genders = new List<string>();
            genders.Add("Male");
            genders.Add("Female");
            genders.Add("Something");
            return genders;
        }

        /// <summary>
        /// Gets all locations from the Database
        /// </summary>
        /// <returns>All Locations</returns>
        private List<string> GetAllLocations()
        {
            try
            {
                using (WorkersDbEntities db = new WorkersDbEntities())
                {
                    List<tblLocation> list = new List<tblLocation>();
                    list = db.tblLocations.Where(x => x.Id > 0).OrderBy(x => x.Address).ToList();
                    List<string> allLocations = new List<string>();
                    foreach (tblLocation location in list)
                    {
                        string fullLocation = location.Address + "," + location.City + "," + location.Country;
                        allLocations.Add(fullLocation);
                    }
                    return allLocations;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
                return null;
            }
        }

        /// <summary>
        /// Saves created Worker to the database
        /// </summary>
        private void SaveExecute()
        {
            tblLocation location = new tblLocation();
            string[] fullLocation = Location.Split(',');
            string address = fullLocation[0];

            try
            {
                using (WorkersDbEntities db = new WorkersDbEntities())
                {
                    location = db.tblLocations.Where(x => x.Address == address).FirstOrDefault();
                    
                    Worker.FKLocation = location.Id;
                    Worker.Gender = Gender;

                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
            }

            tblSector sector = new tblSector();
            sector.Sector = Sector;
            List<tblSector> sectors = new List<tblSector>();
            try
            {
                using (WorkersDbEntities db = new WorkersDbEntities())
                {
                    foreach (tblSector dbSector in db.tblSectors)
                    {
                        sectors.Add(dbSector);
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
            }

            if (sectors.Count == 0)
            {
                try
                {
                    using (WorkersDbEntities db = new WorkersDbEntities())
                    {
                        db.tblSectors.Add(sector);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message.ToString());

                }

                try
                {
                    using (WorkersDbEntities db = new WorkersDbEntities())
                    {
                        tblSector sector1 = db.tblSectors.Where(x => x.Sector == sector.Sector).FirstOrDefault();
                        Worker.FKSector = sector1.Id;
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
                }

            }
            else
            {
                for (int i = 0; i < sectors.Count; i++)
                {
                    if (sectors[i].Sector == sector.Sector)
                    {
                        Worker.FKSector = sectors[i].Id;
                    }
                    else if (i == sectors.Count - 1 && sectors[i].Sector != sector.Sector)
                    {
                        try

                        {
                            using (WorkersDbEntities db = new WorkersDbEntities())
                            {
                                db.tblSectors.Add(sector);
                                db.SaveChanges();
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
                        }

                        try
                        {
                            using (WorkersDbEntities db = new WorkersDbEntities())
                            {
                                tblSector sector1 = db.tblSectors.Where(x => x.Sector == sector.Sector).FirstOrDefault();
                                Worker.FKSector = sector1.Id;
                                db.SaveChanges();
                            }
                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            SaveWorker();
        }

        /// <summary>
        /// Notify user and log info about created worker
        /// </summary>
        private void WorkerAdded(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                MessageBox.Show("Try another jmbg");
            }
            else
            {
                MessageBox.Show("Worker added Successfully!");
                addNewWorker.Close();
                string location = @"~\..\..\..\Log.txt";
                if (File.Exists(location))
                {
                    string info = $"\n[{DateTime.Now.ToShortDateString()}] [{DateTime.Now.ToShortTimeString()}] [Worker Created]";
                    File.AppendAllText(location, info);
                }
                counter = 0;
            }
        }

        /// <summary>
        /// Saves the created Worker in database
        /// </summary>
        private void SaveWorker()
        {
            try
            {
                using (WorkersDbEntities db = new WorkersDbEntities())
                {
                    if (ValideteJMBG(Worker.JMBG))
                    {
                        if(counter == 0)
                        {
                            Worker.Gender = Gender;
                            db.tblWorkers.Add(Worker);
                            db.SaveChanges();
                            counter++;
                        }
                        WriteWorker();
                    }
                    else
                    {
                        MessageBox.Show("Invalid JMBG");
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message.ToString());
            }
        }

        /// <summary>
        /// Checks if all fields are populated
        /// </summary>
        private bool CanSaveExecute()
        {
            //if (String.IsNullOrEmpty(Worker.Name) || String.IsNullOrEmpty(Worker.Surname) || String.IsNullOrEmpty(Worker.IDCardNumber.ToString()) ||
            //    String.IsNullOrEmpty(Gender) || String.IsNullOrEmpty(Worker.PhoneNumber.ToString()) ||
            //    String.IsNullOrEmpty(Location) ||
            //    String.IsNullOrWhiteSpace(Worker.Name) || String.IsNullOrWhiteSpace(Worker.Surname) || String.IsNullOrWhiteSpace(Worker.IDCardNumber.ToString()) ||
            //    String.IsNullOrWhiteSpace(Gender) || String.IsNullOrWhiteSpace(Worker.PhoneNumber.ToString()) ||
            //    String.IsNullOrWhiteSpace(Location))
            //{
            //    return false;
            //}
            //else
            //{
            //    return true;
            //}
            return true;
        }

        /// <summary>
        /// Close the AddNewWorker window
        /// </summary>
        private void CloseExecute()
        {
            addNewWorker.Close();
        }


        /// <summary>
        /// Checks if user can close the window
        /// </summary>
        private bool CanCloseExecute()
        {
            return true;
        }


        /// <summary>
        /// Checks if jmbg is valid
        /// </summary>
        /// <param name="jmbg">Worker JMBG</param>
        private bool ValideteJMBG(string jmbg)
        {
            if (CheckJMBG(jmbg) == jmbg[12] && jmbg.Length == 13)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Calculate the last digit of jmbg to check if jmbg is valid
        /// </summary>
        /// <param name="jmbg">Worker JMBG</param>
        /// <returns></returns>
        private static char CheckJMBG(string jmbg)
        {
            int k = 0;
            for (int i = 0; i < 6; i++)
            {
                k += (jmbg[i] + jmbg[6 + i] - 96) * (7 - i);
            }
            k = 11 - (k % 11);
            if (k > 9)
            {
                k = 0;
            }

            k += 48;
            char check = Convert.ToChar(k);
            return check;
        }

        private void WriteWorker()
        {
            MessageBox.Show("Worker added Successfully!");
            addNewWorker.Close();
            string location = @"~\..\..\..\Log.txt";
            if (File.Exists(location))
            {
                string info = $"\n[{DateTime.Now.ToShortDateString()}] [{DateTime.Now.ToShortTimeString()}] [Worker Created]";
                File.AppendAllText(location, info);
            }
            counter = 0;
        }

        #endregion
    }
}
