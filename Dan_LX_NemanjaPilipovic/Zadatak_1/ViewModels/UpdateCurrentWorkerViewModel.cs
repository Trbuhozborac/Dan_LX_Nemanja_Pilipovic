using System.Collections.Generic;
using Zadatak_1.Models;
using Zadatak_1.Views;

namespace Zadatak_1.ViewModels
{
    public class UpdateCurrentWorkerViewModel : BaseViewModel
    {
        #region Objects

        UpdateCurrentWorker updateWorkerView;

        #endregion

        #region Constructors

        public UpdateCurrentWorkerViewModel(UpdateCurrentWorker updateWorkerViewOpen, tblWorker worker)
        {
            updateWorkerView = updateWorkerViewOpen;
            Worker = worker;
        }

        #endregion

        #region Properties

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

        #endregion
    }
}
