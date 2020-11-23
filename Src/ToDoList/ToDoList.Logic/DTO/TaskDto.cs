using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoList.Logic.DTO
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Deadline { get; set; }
        public int UserId { get; set; }
        public UserDto User { get; set; }
    }
}
