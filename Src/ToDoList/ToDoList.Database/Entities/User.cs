using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.Database.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        [InverseProperty("User")]
        public List<Task> Tasks { get; set; }
        [InverseProperty("Event")]
        public List<Event> Events { get; set; }
    }
}
