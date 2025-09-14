using Shared.Constants;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Data.DTOs
{
    public class CreateTaskDTO
    {
        [Required(ErrorMessage = WebConstants.RequiredField)]
        public string Name { get; set; } = null!;
        public string? Priority { get; set; }
        [JsonIgnore]
        public int AuthorId { get; set; }
        public int PerformerId { get; set; }
        public int? ParentTaskId { get; set; }
        public int[] SubtasksIDs { get; set; } = [];
    }
}
