namespace ToDoList.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class task_update : DbMigration
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
