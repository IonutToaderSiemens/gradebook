using System;
using System.Collections.Generic;
using System.IO;

namespace GradeBook
{
    class DiskBook : Book
    {
        private List<double> grades;

        public DiskBook(string name) : base(name)
        {
            grades = new List<double>();
            Name = name;
        }

        public override event GradeAddedDelegate GradeAdded;

        public override void AddGrade(double grade)
        {
            using(var writer = File.AppendText($"{Name}.txt"))
            {
                writer.WriteLine(grade);
                if(GradeAdded != null)
                {
                    GradeAdded(this, new EventArgs());
                }
            }
            
        }

        public override Statistics GetStatistics()
        {
            var result = new Statistics();
            using(var reader = File.OpenText($"{Name}.txt"))
            {
                string line = "";
                while((line = reader.ReadLine()) != null)
                {
                    var grade = double.Parse(line);
                    result.Add(grade);
                }
            }

            return result;
        }
    }
}