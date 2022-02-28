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
            using(var reader = new StreamReader(@"C:\Users\noure\OneDrive\Bureau\game\samples.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                        {
                            var records = csv.GetRecords<Foo>().ToList();
                        }
        }
    }
    public class Foo
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

    }
}
