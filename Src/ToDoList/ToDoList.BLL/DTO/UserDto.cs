using System.Collections.Generic;

namespace ToDoList.BLL.DTO
{
    public class UserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Password { get; set; }
        public List<TaskDto> Tasks { get; set; }
        public List<EventDto> Events { get; set; }
    }
}
