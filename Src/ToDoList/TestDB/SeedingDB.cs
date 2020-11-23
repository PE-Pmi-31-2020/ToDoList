using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using ToDoList.Database.EF;
using ToDoList.Database.Entities;

namespace TestDB
{
    public static class SeedingDb
    {
        public static void Initialize(DataBase dataBase)
        {
            if (dataBase.Tasks.Any()) return;
            GenerateData(dataBase);
            dataBase.SaveChanges();
        }
        public static void GenerateData(DataBase dataBase)
        {

            GenerateUsers(dataBase);
            GenerateEvents(dataBase);
            GenerateTasks(dataBase);
        }

        private static void GenerateUsers(DataBase dataBase)
        {

            for (int i = 1; i <= 30; i++)
            {
                dataBase.Users.Add(new User()
                    {UserName = $"user{i}", FullName = $"Name{i} Surname{i}",Password = $"{i}{i}{i}{i}" });
            }

            //dataBase.SaveChanges();
            dataBase.SaveChanges();
        }

        private static void GenerateEvents(DataBase dataBase)
        {

            Random rnd = new Random();
            TimeSpan dateTime = TimeSpan.Zero;

            for (int i = 1; i <= 30; i++)
            {
                dataBase.Events.Add(new Event()
                    {Name = $"Name{i}", Description = $"Description{i}", From = dateTime, To = dateTime, RemindTime = dateTime, UserId = rnd.Next(1, 30)});
            }
            dataBase.SaveChanges();

        }

        private static void GenerateTasks(DataBase dataBase)
        {
            Random rnd = new Random();
            TimeSpan dateTime = TimeSpan.Zero;

            for (int i = 1; i <= 30; i++)
            {
                dataBase.Tasks.Add(new Task()
                    { Name = $"Name{i}", Deadline = dateTime, UserId = rnd.Next(1, 30) });
            }
            dataBase.SaveChanges();

        }

        public static void ReadData(DataBase dataBase)
        {
            Console.WriteLine("----------USERS----------");
            var users = dataBase.Users.AsNoTracking().ToList();
            foreach (var user in users)
            {
                Console.WriteLine($"{user.Id} {user.UserName} {user.FullName} {user.Password}");
            }
           

            Console.WriteLine("----------EVENTS----------");
            var events = dataBase.Events.AsNoTracking().ToList();
            foreach (var eveent in events)
            {
                Console.WriteLine($"{eveent.Id} {eveent.Name} {eveent.Description} {eveent.From} {eveent.To} {eveent.RemindTime} {eveent.UserId}");
            }

            Console.WriteLine("----------TASKS----------");
            var tasks = dataBase.Tasks.AsNoTracking().ToList();
            foreach (var task in tasks)
            {
                Console.WriteLine($"{task.Id} {task.Name} {task.Deadline} {task.UserId}");
            }
        }

        public static void DeleteDb(DataBase dataBase)
        {
            dataBase.Database.EnsureDeleted();
        }

    }
}
