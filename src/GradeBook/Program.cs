using System;
using System.Collections.Generic;

namespace GradeBook
{
    class Program
    {
        static void Main(string[] args)
        {

            IBook book = new DiskBook("Scott's gradebook");
            var grade = 0.0;

            book.GradeAdded += OnGradeAdded;

            Console.Write("Enter a grade or 'q' to quit: ");
            var input = Console.ReadLine();

            EnterGrades(book, ref grade, ref input);

            var stats = book.GetStatistics();

            Console.WriteLine($"The lowest grade is {stats.Low:N2}");
            Console.WriteLine($"The highest grade is {stats.High:N2}");
            Console.WriteLine($"The average is {stats.Average:N2}");
            Console.WriteLine($"The letter is {stats.Letter}");

        }

        private static void EnterGrades(IBook book, ref double grade, ref string input)
        {
            while (!input.Equals("q") && !input.Equals("Q"))
            {
                try
                {
                    grade = double.Parse(input);
                    book.AddGrade(grade);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }

                Console.Write("Enter a grade or 'q' to quit: ");
                input = Console.ReadLine();
            }
        }

        static void OnGradeAdded(object sender, EventArgs e)
        {
            Console.WriteLine("A grade was added");
        }
    }
}
