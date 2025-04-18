using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    internal class Program
    {

        public class Employee
        {
            public string FirstName;
            public string LastName;
            public  virtual void printname()
            {
                Console.WriteLine("Employee Calss: " + FirstName + " : " +  LastName);
            }
        }
        public class PartTimeEmployee : Employee
        {
            public override void printname()
            {
                Console.WriteLine("" + FirstName + " : " + LastName + " - Contractor");
            }
        }

        public class FullTimeEmployee : Employee
        {
            public override void printname()
            {

                Console.WriteLine("" + FirstName + " : " + LastName + " - FullTime Emplyee");
            }
        }

        public class Passvalue
        {
            public void Add(List<int> Name)
            {

                Name.ForEach(x => Console.WriteLine(x));
            }
        }

        public class Passvalue2
        {
            public void Add2(int[] ar)
            {
                foreach (int k in ar)
                {
                    Console.WriteLine("Even Numbers");
                    if (k % 2 == 0)
                    {
                        Console.WriteLine(k);
                    }
                }
            }
        }

        static void Main(string[] args)
        {

            Passvalue passvalue = new Passvalue();
            Passvalue2 passvalue2 = new Passvalue2();

            int[,] firstArray = { { 1, 2 }, { 3, 4 } };
            int[,] secondArray = { { 5, 6 }, { 7, 8 } };


            int[] ar = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

 
            List<int> names2 = new List<int>() { 1, 2, 3, 4, 9, -7 };
            //passvalue.Add(names2);
            passvalue2.Add2(ar);

            int maxID2 = 33;
            bool negetiveNumber2 = false;

      

        FullTimeEmployee FTemployee = new FullTimeEmployee();
        FTemployee.FirstName = "Waseem";
        FTemployee.LastName = "Haider";
        FTemployee.printname();

        PartTimeEmployee PTemployee = new PartTimeEmployee();
        PTemployee.FirstName = "Waqar";
        PTemployee.LastName = "Haider";
        PTemployee.printname();




            List<int> names = new List<int>() { 1, 2, 3, 4, 9, -7 };
            int maxID = 33;
            bool negetiveNumber = false;



            if (names.Count > maxID)
            {
                for (int i = 0; i < names.Count; i++)
                {

                    if (names[i] < 0)
                    {
                        negetiveNumber = true;
                        Console.WriteLine("Negetive number found");
                    }
                }

                Console.WriteLine("Max DI is value is less than total count");
            }



  
            


            names.ForEach(Console.WriteLine);

            Console.ReadLine();
        }
    }
}
