using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.Database.Entities
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan Deadline { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        //[InverseProperty("Tasks")]
        public User User { get; set; }
    }
}
