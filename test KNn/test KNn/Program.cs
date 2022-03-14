using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using System.Globalization;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
namespace test_KNn
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            /*using(var reader = new StreamReader(@"C:\Users\noure\OneDrive\Bureau\game\train.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                        {
                KNN kNN = new KNN();
                var records = csv.GetRecords<Foo>().ToList();
                Diagnostic diagnostic = new Diagnostic(records[0].thal, records[0].cp, records[0].oldpeak, records[0].ca, records[0].target);
                //diagnostic.PrintInfo();
                Diagnostic diagnostic2 = new Diagnostic(records[2].thal, records[2].cp, records[2].oldpeak, records[2].ca, records[2].target);
                //diagnostic2.PrintInfo();
                float distanceEuc = kNN.EuclideanDistance(diagnostic, diagnostic2);
                float distanceMan = kNN.ManhattanDistance(diagnostic, diagnostic2);
                //Console.WriteLine(distanceEuc);
                //Console.WriteLine(distanceMan);

            }*/
            KNN kNN2 = new KNN();

            List<Diagnostic> records2 = kNN2.ImportSamples(@"C:\Users\noure\OneDrive\Bureau\game\train.csv");
            Diagnostic testFoo = new Diagnostic() { thal = records2[1].thal,cp=records2[1].cp , ca=records2[1].ca,oldpeak=records2[1].oldpeak,target=records2[1].target};
            testFoo.PrintInfo();
            /*Diagnostic diagnostic3 = new Diagnostic(records2[1].thal, records2[1].cp, records2[1].oldpeak, records2[1].ca, records2[1].target);
            diagnostic3.PrintInfo();
            Diagnostic diagnostic4 = new Diagnostic(records2[0].thal, records2[0].cp, records2[0].oldpeak, records2[0].ca, records2[0].target);
            diagnostic4.PrintInfo();
            float distanceEuc2 = kNN2.EuclideanDistance(diagnostic3, diagnostic4);
            float distanceMan2 = kNN2.ManhattanDistance(diagnostic3, diagnostic4);*/
            //Console.WriteLine(distanceEuc2);
            //
            //Console.WriteLine(distanceMan2);
            List<bool> test = new List<bool>();
            test.Add(true);
            test.Add(false);
            test.Add(false);
            test.Add(true);
            List<float> distances = new List<float>();
            distances.Add(1);
            distances.Add(3);
            distances.Add(0);
            distances.Add(4);
            kNN2.ShellSort(distances,test);
            List<float> distancestest = distances.GetRange(0,2);
            //kNN.ShellSort(distances, test);     
            //Console.WriteLine("test " + distancestest[2].ToString());
            //bool predicts = kNN2.Predict(diagnostic4);
            //Console.WriteLine("predicts : " +predicts.ToString());
            //Console.WriteLine(diagnostic4.Target);
            float test23 = kNN2.Evaluate("test");
            Console.WriteLine(test23);

            Diagnostic diagnostic = new Diagnostic() {thal=1,cp=0,ca=1,oldpeak=0.6f };
            bool predicts = kNN2.Predict(diagnostic);

            Console.WriteLine(predicts);
            Console.ReadKey();
        }
    }
    public sealed class DiagnosticMap : ClassMap<Diagnostic>
    {
        public DiagnosticMap()
        {
            AutoMap(CultureInfo.InvariantCulture);
            Map(m => m.Label).Ignore();
        }
    }
    public class Diagnostic : IDiagnostic
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
    }
    internal interface IKNN
    {
        /* main methods */
        void Train(string filename_train_samples_csv, int k = 1, int distance = 1);
        float Evaluate(string filename_test_samples_csv);
        bool Predict(Diagnostic sample_to_predict);
        /* utils */
        float EuclideanDistance(Diagnostic first_sample, Diagnostic second_sample);
        float ManhattanDistance(Diagnostic first_sample, Diagnostic second_sample);
        bool Vote(List<bool> sorted_labels);
        void ShellSort(List<float> distances, List<bool> labels);
        List<Diagnostic> ImportSamples(string filename_samples_csv);
        
    }
    class KNN : IKNN
    {
        public bool Predict(Diagnostic sample_to_predict)
        {
            List<bool> labels;
            List<float> distances = new List<float>();
            List<Diagnostic> trainSet = ImportSamples(@"C:\Users\noure\OneDrive\Bureau\game\train.csv");
            List<Diagnostic> EvaluationSet = ImportSamples(@"C:\Users\noure\OneDrive\Bureau\game\test.csv");

            labels = new List<bool>(trainSet.Count);
            bool result;
            for (int i = 0; i < trainSet.Count; i++)
            {
                Diagnostic sampleTrain = new Diagnostic() { thal = trainSet[i].thal, cp = trainSet[i].cp, oldpeak = trainSet[i].oldpeak, ca = trainSet[i].ca, target = trainSet[i].target };
                distances.Add(EuclideanDistance(sample_to_predict, sampleTrain));
                labels.Add(trainSet[i].target == "0" ? false : true);
            }
            ShellSort(distances, labels);

            result = Vote(labels.GetRange(0, 6));
            return result;
        }
        
        public void Train(string filename_train_samples_csv, int k = 1, int distance = 1)
        {

        }
        public float Evaluate(string filename_test_samples_csv)
        {
            int k = 6;
            int distance = 1;
            List<bool> labels;
            List<float> distances = new List<float>();
            List<Diagnostic> trainSet = ImportSamples(@"C:\Users\noure\OneDrive\Bureau\game\train.csv");
            List<Diagnostic> EvaluationSet = ImportSamples(@"C:\Users\noure\OneDrive\Bureau\game\test.csv");
            
            labels = new List<bool>(trainSet.Count);
            bool result;
            float taux=0;
            foreach (Diagnostic foo in EvaluationSet)
            {
                distances.Clear();
                Diagnostic sampleFoo = new Diagnostic() { thal=foo.thal, cp=foo.cp, oldpeak=foo.oldpeak, ca=foo.ca, target=foo.target };
                labels.Clear();

                for (int i = 0; i < trainSet.Count; i++)
                {
                    
                    Diagnostic sampleTrain = new Diagnostic() { thal=trainSet[i].thal, cp=trainSet[i].cp, oldpeak=trainSet[i].oldpeak, ca=trainSet[i].ca, target=trainSet[i].target };
                    distances.Add(EuclideanDistance(sampleFoo,sampleTrain));
                    labels.Add(trainSet[i].target=="0"?false:true);
                }
                ShellSort(distances, labels);                
                result = Vote(labels.GetRange(0, 5));               
                
                if(result==(foo.target == "0" ? false : true))
                {
                    taux ++;
                }
            }

            return taux/EvaluationSet.Count * 100;
        }
        public float EuclideanDistance(Diagnostic first_sample, Diagnostic second_sample)
        {
            float distanceThal = (float)(Math.Pow((first_sample.thal - second_sample.thal),2));
            float distanceThor = (float)(Math.Pow((first_sample.cp - second_sample.cp),2));
            float distanceDep = (float)(Math.Pow((first_sample.oldpeak - second_sample.oldpeak),2));
            float distanceVai = (float)(Math.Pow((first_sample.ca - second_sample.ca),2));
            return (float)(Math.Sqrt(distanceThal + distanceThor + distanceDep + distanceVai));
        }
        public float ManhattanDistance(Diagnostic first_sample, Diagnostic second_sample)
        {
            float distanceThal = Math.Abs(first_sample.thal - second_sample.thal);
            float distanceThor = Math.Abs(first_sample.cp - second_sample.cp);
            float distanceDep = Math.Abs(first_sample.oldpeak - second_sample.oldpeak);
            float distanceVai = Math.Abs(first_sample.ca - second_sample.ca);
            return distanceThal+ distanceThor+ distanceDep+ distanceVai;
        }
        public void ShellSort(List<float> distances, List<bool> labels)
        {
            int i, j, pos;
            float temp;
            bool temp2;
            pos = 3;
            while (pos > 0)
            {
                for (i = 0; i < distances.Count(); i++)
                {
                    j = i;
                    temp = distances[i];
                    temp2 = labels[i];
                    while ((j >= pos) && (distances[j - pos] > temp))
                    {
                        distances[j] = distances[j - pos];
                        labels[j]= labels[j - pos];
                        j = j - pos;
                    }
                    distances[j] = temp;
                    labels[j] = temp2;
                }
                if (pos / 2 != 0)
                    pos = pos / 2;
                else if (pos == 1)
                    pos = 0;
                else
                    pos = 1;
            }
        }
        public bool Vote(List<bool> sorted_labels)
        {
            int _true = 0;
            int _false = 0;
            foreach (bool v in sorted_labels)
            {
                if (v == false){
                    _false++;
                }
                else
                {
                    _true++;
                }
            }
            if (_true > _false)
            {
                return true;
            }
            else if(_false > _true)
            {
                return false;
            }
            else
            {
                var random = new Random();
                var randomBool = random.Next(2) == 1;
                return randomBool;
            }

        }
        public List<Diagnostic> ImportSamples(string test)
        {
            using (var reader = new StreamReader(test))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<DiagnosticMap>();
                var records = csv.GetRecords<Diagnostic>().ToList();

                return records;
            }
        }
    }
    internal interface IDiagnostic
    {
        float[] Features { get; }
        bool Label { get; }
        void PrintInfo();
    }

    /*class Diagnostic : IDiagnostic {
        private float _thalassémie = 0.00f;
        private float _thoracique = 0.00f;
        private float _depression = 0.00f;
        private float _vaisseaux = 0.00f;
        private string _target = "0";
        private float[] _features = new float[5] { 0, 0, 0, 0,0 };
        public Diagnostic(float thalassémie, float thoracique, float depression, float vaisseaux,string target)
        {
            this._thalassémie = thalassémie;
            this._vaisseaux=vaisseaux;
            this._thoracique=thoracique;
            this._depression = depression;
            this._target = target;
            this._features = new float[5] { thalassémie, thoracique, depression, vaisseaux, (target=="0")?0:1 };
        }
        public string Target
        {
            get { return this._target; }

            set
            {
                if (this._target != value)
                {
                    this._target = value;
                }
            }
        }
        public float Thalassémie
        {
            get { return this._thalassémie; }

            set
            {
                if (this._thalassémie != value)
                {
                    this._thalassémie = value;
                }
            }
        }

        public float Thoracique
        {
            get { return this._thoracique; }

            set
            {
                if (this._thoracique != value)
                {
                    this._thoracique = value;
                }
            }
        }
        public float Depression
        {
            get { return this._depression; }

            set
            {
                if (this._depression != value)
                {
                    this._depression = value;
                }
            }
        }
        public float Vaisseaux
        {
            get { return this._vaisseaux; }

            set
            {
                if (this._vaisseaux != value)
                {
                    this._vaisseaux = value;
                }
            }
        }

        public float[] Features {
            get
            { return this._features; }
            set
            {
                this._features=value;
            }
        }
        public bool Label { get; set; }
        public void PrintInfo() { 
            Console.WriteLine (this._thalassémie.ToString() + "  " + this._thoracique.ToString() + "  " + this._depression.ToString("0.000") + "  " + this._vaisseaux.ToString() + "  " + Target.ToString()); }
        };*/
}
