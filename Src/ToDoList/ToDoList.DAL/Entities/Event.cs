using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ToDoList.Database.Entities
{
    public class Event
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }
        public TimeSpan RemindTime { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        //[InverseProperty("Events")]
        public User User { get; set; }
    }
}
