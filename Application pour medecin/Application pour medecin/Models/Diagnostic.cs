using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Application_pour_medecin.Models
{
    internal interface IDiagnostic
    {
        float[] Features { get; }
        bool Label { get; }
        void PrintInfo();
    }
    public class Diagnostic : IDiagnostic , INotifyPropertyChanged
    { 
        [Name("cp")]
            public float cp { get; set; }
        [Name("ca")]
        public float ca { get; set; }
        [Name("oldpeak")]
        public float oldpeak { get; set; }
        [Name("thal")]
        public float thal { get; set; }
        [Name("target")]
        public string target { get; set; }
        private string _target = "0";
        private float[] _features = new float[5] { 0, 0, 0, 0, 0 };


        public float[] Features
        {
            get
            { return this._features; }
            set
            {
                this._features = value;
            }
        }
        public Diagnostic()
        {
            this._features = new float[5] { thal, cp, oldpeak, ca, (target == "0") ? 0 : 1 };
        }
        public bool Label { get; set; }
        public void PrintInfo()
        {
            Console.WriteLine($"{thal} + {target}");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
