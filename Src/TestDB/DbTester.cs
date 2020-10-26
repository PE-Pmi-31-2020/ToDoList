using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;


namespace TestDB
{
    class DbTester
    {
        static string connectionString = @"Data Source=../../../../DataBase.db";
        static SQLiteConnection con = new SQLiteConnection(connectionString);

        protected void ClearDb()
        {
            using var cmd = new SQLiteCommand(con);
            cmd.CommandText = "delete from Tasks";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "delete from Events";
            cmd.ExecuteNonQuery();
            cmd.CommandText = "delete from Users";
            cmd.ExecuteNonQuery();
        }

        public void GenerateData()
        {
            con.Open();

            ClearDb();

            GenerateUsers();
            GenerateEvents();
            GenerateTasks();

            con.Close();
        }

        protected void GenerateUsers()
        {
            using var cmd = new SQLiteCommand(con);

            for (int i = 1; i <= 30; i++)
            {
                cmd.CommandText = $"INSERT INTO 'Users'(UserName, FullName, Password) VALUES('user{i}','Name{i} Surname{i}', '{i}{i}{i}{i}')";
                cmd.ExecuteNonQuery();
            }
        }

        protected void GenerateEvents()
        {
            using var cmd = new SQLiteCommand(con);

            Random rnd = new Random();
            DateTime dateTime = new DateTime();

            for (int i = 1; i <= 30; i++)
            {
                cmd.CommandText = $"INSERT INTO 'Events'(Name, Description, Time, UserId) VALUES('Name{i}','Description{i}', '{dateTime}', '{rnd.Next(1,30)}')";
                cmd.ExecuteNonQuery();
            }
        }

        protected void GenerateTasks()
        {
            using var cmd = new SQLiteCommand(con);

            Random rnd = new Random();
            DateTime dateTime = new DateTime();

            for (int i = 1; i <= 30; i++)
            {
                cmd.CommandText = $"INSERT INTO 'Tasks'(Name, Deadline, UserId) VALUES('Name{i}', '{dateTime}', '{rnd.Next(1, 30)}')";
                cmd.ExecuteNonQuery();
            }
        }

        public void ReadData()
        { 
            con.Open();

            string stm = "SELECT * FROM Users";
            string stm2 = "SELECT * FROM Events";
            string stm3 = "SELECT * FROM Tasks";

            using var cmd = new SQLiteCommand(stm, con);
            using var cmd2 = new SQLiteCommand(stm2, con);
            using var cmd3 = new SQLiteCommand(stm3, con);

            using SQLiteDataReader rdr = cmd.ExecuteReader();
            using SQLiteDataReader rdr2 = cmd2.ExecuteReader();
            using SQLiteDataReader rdr3 = cmd3.ExecuteReader();

            Console.WriteLine("----------USERS----------");
            while (rdr.Read())
            {
                Console.WriteLine($"{rdr.GetInt32(0)} {rdr.GetString(1)} {rdr.GetString(2)} {rdr.GetString(3)}");
            }

            Console.WriteLine("----------EVENTS----------");
            while (rdr2.Read())
            {
                Console.WriteLine($"{rdr2.GetInt32(0)} {rdr2.GetString(1)} {rdr2.GetString(2)} {rdr2.GetString(3)} {rdr2.GetInt32(4)}");
            }

            Console.WriteLine("----------TASKS----------");
            while (rdr3.Read())
            {
                Console.WriteLine($"{rdr3.GetInt32(0)} {rdr3.GetString(1)} {rdr3.GetString(2)} {rdr3.GetInt32(3)}");
            }
            con.Close();
        }

        public void CreateDataBase()
        {
            SQLiteConnection.CreateFile($"../../../../DataBase.db");

            SQLiteConnection newDb = new SQLiteConnection($"Data Source=../../../../DataBase.db;Version=3;");
            newDb.Open();

            using var cmd = new SQLiteCommand(newDb);
            
            cmd.CommandText = @"CREATE TABLE Users (Id INTEGER PRIMARY KEY, UserName TEXT NOT NULL UNIQUE, FullName TEXT, Password TEXT);";
            cmd.ExecuteNonQuery();
            cmd.CommandText = @"CREATE TABLE Events (Id INTEGER PRIMARY KEY, Name TEXT NOT NULL, Description TEXT, Time DATETIME, UserID INTEGER REFERENCES Users(Id) NOT NULL);";
            cmd.ExecuteNonQuery();
            cmd.CommandText = @"CREATE TABLE Tasks (Id INTEGER PRIMARY KEY, Name TEXT NOT NULL, Deadline DATETIME, UserID INTEGER REFERENCES Users(Id) NOT NULL);";
            cmd.ExecuteNonQuery();


            newDb.Close();
        }
    }
}
