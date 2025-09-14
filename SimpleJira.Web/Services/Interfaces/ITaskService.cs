using Data.DTOs;
using Data.DTOs.TaskDTOs;
using Data.Models;

namespace SimpleJira.Web.Services.Interfaces
{
    public interface ITaskService
    {
        Task<GetTaskDTO> CreateTaskAsync(CreateTaskDTO model);
        Task<GetTaskDTO> GetTaskByIDAsync(int id);
        Task<List<GetTaskDTO>> GetAllTasksAsync();
        Task UpdateTaskAsync(UpdateTaskDTO model);
        Task RemoveAsync(int id);
    }

}
