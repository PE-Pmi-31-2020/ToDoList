using System.Data.Entity.Migrations;

namespace ToDoList.DAL.Migrations
{
    public partial class TaskUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "IsDone", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tasks", "IsDone");
        }
    }
}
