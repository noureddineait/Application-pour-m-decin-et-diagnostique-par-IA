using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application_pour_medecin.Models
{
    public class Medecin : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private string _nom = "";
        private string _prénom = "";
        private string _date;
        private string _genre = "";
        private string _dateEntré;
        private string _mail = "";
        private string _ville = "";
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

        public string DateEntrée
        {
            get { return this._dateEntré; }

            set
            {
                if (this._dateEntré != value)
                {
                    this._dateEntré = value;
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
        public string Mail
        {
            get { return this._mail; }

            set
            {
                if (this._mail != value)
                {
                    this._mail = value;
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
        public Medecin(string nom, string prenom)
        {
            this._nom = nom;
            this._prénom = prenom;
        }
        public Medecin(string nom, string prenom,string date,string dateEntrée,string ville,string mail,string genre)
        {
            this._nom = nom;
            this._prénom = prenom;
            this._date=date;
            this._dateEntré = dateEntrée;
            this._ville=ville;
            this._mail = mail;
            this._genre=genre;
        }
        public bool IsValid()
        {
            return (
                !string.IsNullOrEmpty(this.Nom) &&
                !string.IsNullOrEmpty(this.Prénom) &&
                !string.IsNullOrEmpty(this.Mail)&&
                !string.IsNullOrEmpty(this.Ville) &&    
                !string.IsNullOrEmpty(this.Date) &&
                !string.IsNullOrEmpty(this.DateEntrée)&&
                !string.IsNullOrEmpty(this.Genre)
            );
        }
        public Medecin(string nom)
        {
            this._nom = nom;
        }
        public Medecin()
        {
            
        }
    }
}
