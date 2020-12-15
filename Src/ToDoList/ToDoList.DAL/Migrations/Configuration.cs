using System.Data.Entity.Migrations;
using ToDoList.DAL.EF;

namespace ToDoList.DAL.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DataBase>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DataBase context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
