using System;
using ToDoList.Database.EF;

namespace TestDB
{
    class Program
    {
        static void Main(string[] args)
        {
            var dataBase = new DataBase();
            SeedingDb.Initialize(dataBase);
            SeedingDb.ReadData(dataBase);
           //SeedingDb.DeleteDb(dataBase);
        }
    }
}
