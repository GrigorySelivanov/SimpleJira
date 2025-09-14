using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public enum TaskStates
    {
        New = 0,
        InProgress = 1,
        Done = 2
    }

    public enum TaskPriority
    {
        Low = 0,
        Medium = 1,
        High = 2
    }
    public class TaskModel
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public TaskStates State { get; set; } = TaskStates.New;
        public TaskPriority? Priority { get; set; }
        public int? AuthorId { get; set; }
        [ForeignKey(nameof(AuthorId))]
        public required User Author { get; set; }
        public int? PerformerId { get; set; }
        [ForeignKey(nameof(PerformerId))]
        public User? Performer { get; set; }
        public int? ParentTaskId { get; set; }
        [ForeignKey(nameof(ParentTaskId))]
        public TaskModel? ParentTask { get; set; }
        public List<TaskModel> Subtasks { get; set; } = [];
    }
}
