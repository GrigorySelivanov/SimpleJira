using Data.DTOs.UserDTOs;

namespace Data.DTOs.TaskDTOs
{
    public class GetTaskDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string State { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public UserShortDTO? Author { get; set; }
        public UserShortDTO? Performer { get; set; }
        public GetTaskShortDTO? ParentTask { get; set; }
        public List<GetTaskShortDTO>? Subtasks { get; set; } = [];
    }
}
