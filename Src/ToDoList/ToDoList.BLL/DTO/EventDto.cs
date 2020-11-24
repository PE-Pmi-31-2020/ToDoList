using System;

namespace ToDoList.BLL.DTO
{
    public class EventDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public TimeSpan From { get; set; }
        public TimeSpan To { get; set; }
        public TimeSpan RemindTime { get; set; }
        public int UserId { get; set; }
        public UserDto User { get; set; }
    }
}
