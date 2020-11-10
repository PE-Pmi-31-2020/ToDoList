using System;
using System.Data.SQLite;

namespace TestDB
{
    class Program
    {

        static void Main(string[] args)
        {
            DbTester dbTester = new DbTester();
            dbTester.CreateDataBase();
            dbTester.GenerateData();
            dbTester.ReadData();


            Console.ReadKey();
        }
    }
}
