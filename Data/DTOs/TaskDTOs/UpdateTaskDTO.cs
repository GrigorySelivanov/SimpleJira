using Shared.Constants;
using System.ComponentModel.DataAnnotations;

namespace Data.DTOs
{
    public class UpdateTaskDTO
    {
        [Required(ErrorMessage = WebConstants.RequiredField)]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? State { get; set; }
        public string? Priority { get; set; }
        public int? PerformerId { get; set; }
        public int? ParentTaskId { get; set; }
        public int[] SubtasksIDs { get; set; } = [];
    }
}
