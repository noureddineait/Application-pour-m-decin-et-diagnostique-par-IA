using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Application_pour_medecin.ViewModels
{
    internal class MainViewModel : INotifyPropertyChanged
    {
        private Window _window; 
        public Models.Medecin Medecin { get; set; }

        private ObservableCollection<Models.Medecin> _doctors;
        public ObservableCollection<Models.Medecin> Doctors
        {
            get
            {
                return _doctors;
            }
            set
            {
                if (_doctors != value) {
                    _doctors = value;
                    OnPropertyChanged();
                }
            }
            
        }
        Models.Medecin Noureddine = new Models.Medecin("Noureddine", "AIT EL HAJ","09-10-2000","10-12-2021","Casablanca","nourlhaaj@gmail.com","Homme");
        Models.Medecin Yasmine = new Models.Medecin("Yasmine", "Outtassi","21-10-1999","10-12-2021","Fez","OuttassiYasmine@gmail.com","Femme");

        private ObservableCollection<string> _villes;

        public ObservableCollection<string> Villes
        {
            get { return _villes; }
        }

        private bool _radio1IsCheck;
        public bool Radio1IsCheck
        {
            get { return this._radio1IsCheck; }
            set
            {
                this._radio1IsCheck = value;
                this.OnPropertyChanged("Radio1IsCheck");
                this.OnPropertyChanged("TextValue");
            }
        }

        private bool _radio2IsCheck;
        public bool Radio2IsCheck
        {
            get { return this._radio2IsCheck; }
            set
            {
                this._radio2IsCheck = value;
                this.OnPropertyChanged("Radio1IsCheck");
                this.OnPropertyChanged("TextValue");
            }
        }
        public string TextValue
        {
            get
            {
                string selected;
                if (!this._radio1IsCheck && !this._radio2IsCheck)
                {
                    selected = null;
                }
                else if (this._radio2IsCheck){
                    selected = "Femme";
                }
                else
                {
                    selected = "Homme";
                }
                return selected;
            }
        }
        private Models.Medecin _selectDoctor;
        public Models.Medecin SelectedDoctor
        {
            get
            {
                return _selectDoctor;
            }
            set
            {
                if (_selectDoctor != value)
                {
                    _selectDoctor = value;

                }
            }
        }
        public ICommand AddDoctorCommand { get; private set; }
        public ICommand ConnexionCommand { get; private set; }
        //public ICommand MyCommand { get; private set; }

        public ICommand QuitterCommand { get; private set; }
        public ICommand OpenCreation { get; private set; }

        
        public MainViewModel(Window window)
        {
            _window = window;
            Medecin = new Models.Medecin();
            _doctors = new ObservableCollection<Models.Medecin>();
            Doctors.Add(Noureddine);
            Doctors.Add(Yasmine);
            _selectDoctor = new Models.Medecin();
            _villes = new ObservableCollection<string>()
            {
                "Casablanca",
                "Marrakech",
                "Tanger",
                "Rabat",
                "Fez",
                "Rimouski"
            };

            ConnexionCommand = new RelayCommand(
                o => true,
                o =>SeConnecter()
                );
            AddDoctorCommand = new RelayCommand(
                o => true,
                o => AddDoctor()
            );

            QuitterCommand = new RelayCommand (
                o=>true,
                o => quitCreation()
            );

            OpenCreation = new RelayCommand(
                o => true,
                o => openCreation()
                );
            
        }
        private void SeConnecter()
        {
            if (SelectedDoctor.IsValid())
            {
                Bienvenue_Dr bienvenue_Dr = new Bienvenue_Dr(SelectedDoctor);
                bienvenue_Dr.Show();
                _window.Close();
            }
                      
        }
        private void openCreation()
        {
            Medecin.Nom = "";
            Medecin.Prénom = "";
            Medecin.Ville = "";
            Medecin.Date = "";
            Medecin.DateEntrée = "";
            Medecin.Mail = "";
            Creation creation = new Creation();
            creation.DataContext = this;
            creation.ShowDialog();
            //_window.Close();
            
        }
        
        private void quitCreation()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window != _window)
                {
                    window.Close();
                }
            }

        }
        private void AddDoctor()
        {
            Medecin.Genre = TextValue;

            if (Medecin.IsValid())
            {
                if (Doctors.Any(u => u.Nom == Medecin.Nom && u.Prénom == Medecin.Prénom))
                    MessageBox.Show("L'utilisateur existe !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    Doctors.Add(new Models.Medecin(Medecin.Nom, Medecin.Prénom, Medecin.Date, Medecin.DateEntrée, Medecin.Ville, Medecin.Mail,Medecin.Genre));
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window != _window)
                        {
                            window.Close();
                        }
                    }
                }
            }
            else
                MessageBox.Show("Veuillez remplir tous les cases !", "Erreur",MessageBoxButton.OK,MessageBoxImage.Error);
            
            //Connexion connexion = new Connexion();
            //connexion.DataContext = this;


            //connexion.Show();
            //_window.Close();


        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
