using System.ComponentModel.DataAnnotations;

namespace Data.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Login { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public List<TaskModel> CreatedTasks { get; set; } = [];
        public List<TaskModel> AssignedTasks { get; set; } = [];
    }
}
