using System;

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
    internal class BienvenueViewModel : INotifyPropertyChanged
    {
        private Window _window;
        public Models.Medecin Medecin { get; set; }
        //public Models.Medecin OldMedecin=Medecin;
        
        
        // POUR INFORMATION
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
                else if (this._radio2IsCheck)
                {
                    selected = "Femme";
                }
                else
                {
                    selected = "Homme";
                }
                return selected;
            }
        }
        
        public ICommand InfoAnnuler { get; private set; }
        public ICommand InfoModifier { get; private set; }

        private Models.Medecin _oldMedecin ;
        public Models.Medecin OldMedecin
        {
            get
            {
                return _oldMedecin ;
            }
            set
            {
                if(_oldMedecin != value)
                {
                    _oldMedecin = value;

                }

            }
        }
        //ATENTION !!!!!!!!!!!!!!!!!!!!!!!
        //POUR DIAGNOSTIQUE
        //ATENTION !!!!!!!!!!!!!!!!!!!!!!!


        public Models.Patient Patient { get; set; }

        Models.Patient Patient1 = new Models.Patient("Test","test","test","Rimouski","Homme");
        private ObservableCollection<Models.Patient> _patients;
        public ObservableCollection<Models.Patient> Patients
        {
            get
            {
                return _patients;
            }
            set
            {
                if (_patients != value)
                {
                    _patients = value;
                    OnPropertyChanged();
                }
            }

        }
        private ObservableCollection<string> _villesPatient;

        public ObservableCollection<string> VillesPatient
        {
            get { return _villesPatient; }
        }

        private bool _radio3IsCheck;
        public bool Radio3IsCheck
        {
            get { return this._radio3IsCheck; }
            set
            {
                this._radio3IsCheck = value;
                this.OnPropertyChanged("Radio3IsCheck");
                this.OnPropertyChanged("TextValue2");
            }
        }

        private bool _radio4IsCheck;
        public bool Radio4IsCheck
        {
            get { return this._radio4IsCheck; }
            set
            {
                this._radio4IsCheck = value;
                this.OnPropertyChanged("Radio4IsCheck");
                this.OnPropertyChanged("TextValue2");
            }
        }
        public string TextValue2
        {
            get
            {
                string selected;
                if (!this._radio3IsCheck && !this._radio4IsCheck)
                {
                    selected = null;
                }
                else if (this._radio4IsCheck)
                {
                    selected = "Femme";
                }
                else
                {
                    selected = "Homme";
                }
                return selected;
            }
        }
        private Models.Patient _selectedPatient;
        public Models.Patient SelectedPatient
        {
            get
            {
                return _selectedPatient;
            }
            set
            {
                if (_selectedPatient != value)
                {
                    _selectedPatient = value;

                }
            }
        }
        private ObservableCollection<string> _douleursThalassemie;

        public ObservableCollection<string> DouleursThalassemie
        {
            get { return _douleursThalassemie; }
        }
        private ObservableCollection<string> _douleursThoracique;

        public ObservableCollection<string> DouleursThoracique
        {
            get { return _douleursThoracique; }
        }

        public ICommand AddDoctorCommand { get; private set; }
        public ICommand QuitterCommand { get; private set; }
        public ICommand OpenAjoutPatientCommand { get; private set; }
        //ATENTION !!!!!!!!!!!!!!!!!!!!!!!
        //POUR CONFIGURATION
        //ATENTION !!!!!!!!!!!!!!!!!!!!!!!

        private string _trainSet;
        private string _testSet;
        
        public ICommand OpenTrainCommand { get; private set; }
        public ICommand OpenTestCommand { get; private set; }
        Models.KNN KNN { get; set; }

        private ObservableCollection<string> _distances;

        public ObservableCollection<string> Distances
        {
            get { return _distances; }
        }

        private int _parametreK = 1;
        public int ParametreK
        {
            get { return _parametreK; }
            set { _parametreK = value; }
        }
        private string _selectedDistance;

        public string SelectedDistance
        {
            get { return _selectedDistance; }
            set { _selectedDistance = value; }
        }
        private int _distance;
        public int Distance
        {
            get { return _distance; }
            set
            {
                if (SelectedDistance == "Euclidienne")
                {
                    _distance = 0;
                }
                else if (SelectedDistance == "Manhattan")
                {
                    _distance = 1;
                }
            }
        }
        private float _accuracy;
        public float Accuracy
        {
            get { return _accuracy; }
            set { _accuracy = value; 
            }
        }
        private string _accuracyString;
        public string AccuracyString
        {
            get { return _accuracyString; }
            set
            {
                _accuracyString = value;
                OnPropertyChanged();

            }
        }

        public ICommand EvaluerCommand { get; private set; }
        public BienvenueViewModel(Window window,Models.Medecin doctor)
        {
            _window = window;
            Medecin = new Models.Medecin(doctor.Nom, doctor.Prénom, doctor.Date, doctor.DateEntrée, doctor.Ville, doctor.Mail, doctor.Genre);
            _oldMedecin = new Models.Medecin(Medecin.Nom,Medecin.Prénom,Medecin.Date,Medecin.DateEntrée,Medecin.Ville,Medecin.Mail,Medecin.Genre);
            
            _villes = new ObservableCollection<string>()
            {
                "Casablanca",
                "Marrakech",
                "Tanger",
                "Rabat",
                "Fez",
                "Rimouski"
            };
            _douleursThalassemie = new ObservableCollection<string>()
            {
                "normale",
                "défaut corrigé",
                "défaut"
            };

            _douleursThoracique = new ObservableCollection<string>()
            {
                "Angine typique",
                "Angine atypique",
                "Douleur non angineuse",
                "Asymptomatique"
            };
            //ATENTION !!!!!!!!!!!!!!!!!!!!!!!
            //POUR INFORMATION
            //ATENTION !!!!!!!!!!!!!!!!!!!!!!!

            if (Medecin.Genre == "Homme")
            {
                Radio1IsCheck = true;
            }else Radio2IsCheck = true;
            InfoAnnuler = new RelayCommand(
                o => true,
                o => AnnulerModif()
                );
            InfoModifier = new RelayCommand(
                o => true,
                o => Modifier()
                );
            //ATENTION !!!!!!!!!!!!!!!!!!!!!!!
            //POUR DIAGNOSTIQUE
            //ATENTION !!!!!!!!!!!!!!!!!!!!!!!

            Patient = new Models.Patient();
            _patients = new ObservableCollection<Models.Patient>();
            Patients.Add(Patient1);
            //Patients.Add(Noureddine);
            //Patients.Add(Yasmine);
            _selectedPatient = new Models.Patient();
            _villesPatient = new ObservableCollection<string>()
            {
                "Casablanca",
                "Marrakech",
                "Tanger",
                "Rabat",
                "Fez",
                "Rimouski"
            };

            
            AddDoctorCommand = new RelayCommand(
                o => true,
                o => AddPatient()
            );

            QuitterCommand = new RelayCommand(
                o => true,
                o => QuitAjoutPatient()
            );

            OpenAjoutPatientCommand = new RelayCommand(
                o => true,
                o => OpenAjoutPatient()
                );
            //POUR CONFIGURATION
            KNN= new Models.KNN();
            _distances = new ObservableCollection<string>()
            {
                "Euclidienne",
                "Manhattan"
            };
            OpenTrainCommand = new RelayCommand(
                o => true,
                o => OpenTrain()
                );
            OpenTestCommand = new RelayCommand(
                o => true,
                o => OpenTest()
                );
            EvaluerCommand = new RelayCommand(
                o => true,
                o => Evaluer()
                );
        }
        //POUR INFORMATION
        private void AnnulerModif()
        {
          
            Medecin.Nom = OldMedecin.Nom;
            MessageBox.Show($"{Medecin.Nom}  +  {OldMedecin.Nom}");
        }
        private void Modifier()
        {
            //OldMedecin=Medecin;
            MessageBox.Show($"{Medecin.Nom}   ++ {OldMedecin.Nom}");
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // POUR DIAGNOSTIQUE

        private void OpenAjoutPatient()
        {
            Patient.Nom = "";
            Patient.Prénom = "";
            Patient.Date = "";
            AjoutPatient ajoutPatient = new AjoutPatient(Medecin);
            ajoutPatient.DataContext = this;
            ajoutPatient.ShowDialog();
            //_window.Close();
        }

        private void QuitAjoutPatient()
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window != _window)
                {
                    window.Close();
                }
            }
        }
        private void AddPatient()
        {
            Patient.Genre = TextValue2;

            if (Patient.IsValid())
            {
                if (Patients.Any(u => u.Nom == Patient.Nom && u.Prénom == Patient.Prénom))
                    MessageBox.Show("L'utilisateur existe !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                else
                {
                    Patients.Add(new Models.Patient(Patient.Nom, Patient.Prénom, Patient.Date, Patient.Ville, Patient.Genre));
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
                MessageBox.Show("Veuillez remplir tous les cases !", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);

        }


        //POUR CONFIGURATION

        private void OpenTrain()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            Nullable<bool> result = dlg.ShowDialog();
            if (result.HasValue)
            {
                MessageBox.Show(dlg.FileName, "Fichier Choisi", MessageBoxButton.OK, MessageBoxImage.Information);
                _trainSet = dlg.FileName;
            }

        }
        private void OpenTest()
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            Nullable<bool> result = dlg.ShowDialog();
            if (result.HasValue)
            {
                MessageBox.Show(dlg.FileName,"Fichier Choisi",MessageBoxButton.OK,MessageBoxImage.Information);
                _testSet = dlg.FileName;
            }

        }
        private void Evaluer()
        {
            MessageBox.Show($"{_testSet} + {_trainSet} + {SelectedDistance} + {ParametreK}");
            KNN.Train(_trainSet,ParametreK,Distance);
            float taux = KNN.Evaluate(_testSet);
            _accuracy = taux;
            MessageBox.Show(_accuracy.ToString());
            _accuracyString = "Taux de reconnaissance : "+ _accuracy.ToString("0.00")+ " %";
            AccuracyString = _accuracyString;
        }
    }
}
