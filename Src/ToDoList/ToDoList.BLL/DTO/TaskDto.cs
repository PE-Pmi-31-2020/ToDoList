using System;

namespace ToDoList.BLL.DTO
{
    public class TaskDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TimeSpan Deadline { get; set; }
        public int UserId { get; set; }
        public UserDto User { get; set; }
    }
}
