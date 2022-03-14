using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Application_pour_medecin.Models
{
    public class Patient : INotifyPropertyChanged
    {
        //DIAGNOSTIQUE MISSING !!!!!!

        public event PropertyChangedEventHandler PropertyChanged;

        private string _nom="";

        private string _prénom = "";
        private string _date;
        private string _ville = "";
        private string _genre = "";
        public string Nom
        {
            get { return this._nom; }

            set
            {
                if (this._nom != value)
                {
                    this._nom = value;
                    
                }
            }
        }
        

        public string Prénom
        {
            get { return this._prénom; }

            set
            {
                if (this._prénom != value)
                {
                    this._prénom = value;
                }
            }
        }
        public string Date
        {
            get { return this._date; }

            set
            {
                if (this._date != value)
                {
                    this._date = value;
                }
            }
        }
        public string Ville
        {
            get { return this._ville; }

            set
            {
                if (this._ville != value)
                {
                    this._ville = value;
                }
            }
        }
        public string Genre
        {
            get { return this._genre; }

            set
            {
                if (this._genre != value)
                {
                    this._genre = value;
                }
            }
        }
        public bool IsValid()
        {
            return (!string.IsNullOrEmpty(this._nom) && 
                !string.IsNullOrEmpty(this._prénom)&&
                !string.IsNullOrEmpty(this._date)&&
                !string.IsNullOrEmpty(this._ville)&&
                !string.IsNullOrEmpty(this._genre)
                );
        }
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public Patient() { }
        public Patient(string nom,string prenom)
        {
            this._nom = nom;
            this._prénom = prenom;
        }
        public Patient(string nom, string prenom,string date,string ville,string genre)
        {
            this._nom = nom;
            this._prénom = prenom;
            this._date = date;
            this._genre = genre;
            this._ville = ville;
        }

    }
}
