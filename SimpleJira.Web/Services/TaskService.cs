using AutoMapper;
using Data.DTOs;
using Data.DTOs.TaskDTOs;
using Data.Models;
using DataAccess.Repositories.Interfaces;
using SimpleJira.Web.Services.Interfaces;

namespace SimpleJira.Web.Services
{
    public class TaskService(
        ITaskRepository taskRepository,
        IUserRepository userRepository,
        ILogger<TaskService> logger,
        IMapper mapper
        ) : ITaskService
    {
        private readonly ITaskRepository _taskRepository = taskRepository;
        private readonly IUserRepository _userRepository = userRepository;
        private readonly ILogger<TaskService> _logger = logger;
        private readonly IMapper _mapper = mapper;

        #region CRUD metods

        /// <summary>
        /// Получение всех задач.
        /// </summary>
        /// <returns>Список DTO задач.</returns>
        public async Task<List<GetTaskDTO>> GetAllTasksAsync()
        {
            var tasks = await _taskRepository.GetFullAllAsync(true);
            return tasks.Select(t => _mapper.Map<GetTaskDTO>(t)).ToList();
        }

        /// <summary>
        /// Получение задачи по ID.
        /// </summary>
        /// <param name="id">ID задачи.</param>
        /// <returns>DTO задачи.</returns>
        /// <exception cref="KeyNotFoundException">При отсутствии задачи в базе</exception>
        public async Task<GetTaskDTO> GetTaskByIDAsync(int id)
        {
            var task = await _taskRepository.GetFullByIdAsync(id, true) ?? throw new KeyNotFoundException($"Задача с id = {id} не найдена в базе.");
            return _mapper.Map<GetTaskDTO>(task);
        }

        /// <summary>
        /// Метод по созданию задачи.
        /// </summary>
        /// <param name="model">DTO-модель создания задачи.</param>
        /// <returns>DTO созданной задачи.</returns>
        /// <exception cref="KeyNotFoundException">При отсутствии некоторых связных элементов в базе</exception>
        public async Task<GetTaskDTO> CreateTaskAsync(CreateTaskDTO model)
        {
            var task = new TaskModel
            {
                Name = model.Name,
                State = TaskStates.New,
                Author = await _userRepository.GetByIdAsync(model.AuthorId) ?? throw new KeyNotFoundException($"Автор(id = {model.AuthorId}) не найден."),
                Performer = await _userRepository.GetByIdAsync(model.PerformerId) ?? throw new KeyNotFoundException($"Исполнитель(id = {model.PerformerId}) не найден."),
                
            };

            if (Enum.TryParse<TaskPriority>(model.Priority, true, out var newPriority))
                task.Priority = newPriority;

            if (model.SubtasksIDs.Length != 0)
                task.Subtasks = await _taskRepository.GetAllAsync(model.SubtasksIDs);

            if (model.ParentTaskId.HasValue)
                task.ParentTask = await _taskRepository.GetByIdAsync(model.ParentTaskId.Value) ?? throw new KeyNotFoundException($"Родительская задача(id = {model.ParentTaskId.Value}) не найдена.");

            await _taskRepository.CreateAsync(task);
            await _taskRepository.SaveAsync();

            return _mapper.Map<GetTaskDTO>(task);
        }

        /// <summary>
        /// Метод по обновлению задачи.
        /// </summary>
        /// <param name="model">DTO обновленной задачи.</param>
        /// <exception cref="KeyNotFoundException">При отсутствии некоторых связных элементов в базе</exception>
        public async Task UpdateTaskAsync(UpdateTaskDTO model)
        {
            var existTask = await _taskRepository.GetByIdAsync(model.Id) ?? throw new KeyNotFoundException("Задача не найдена");

            existTask.Name = model.Name;

            if (model.PerformerId.HasValue && existTask.PerformerId != model.PerformerId)
            {
                existTask.Performer = await _userRepository.GetByIdAsync(model.PerformerId.Value) ?? throw new KeyNotFoundException($"Исполнитель(id = {model.PerformerId}) не найден");
            }

            if (Enum.TryParse<TaskPriority>(model.Priority, true, out var newPriority))
                existTask.Priority = newPriority;

            if (model.SubtasksIDs.Length != 0)
            {
                existTask.Subtasks = GetNewTaskListAsync(existTask.Subtasks, await _taskRepository.GetAllAsync(model.SubtasksIDs));
            }

            _taskRepository.Update(existTask);
            await _taskRepository.SaveAsync();
        }

        /// <summary>
        /// Удаление задачи.
        /// </summary>
        /// <param name="id">ID задачи</param>
        public async Task RemoveAsync(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            if (task == null)
            {
                _logger.LogWarning("Попытка удалить задачу с id = {}, которой нет в базе", id);
                return;
            }

            _taskRepository.Remove(task);
            await _taskRepository.SaveAsync();
        }

        #endregion

        #region Private metods

        /// <summary>
        /// Метод для переопределения списка задач. Находит пересечение задач, задачи, которые больше не нужны(их удаляет) 
        /// и новые задачи(их добавляет).
        /// </summary>
        /// <param name="oldTasks">Список старых задач.</param>
        /// <param name="newTasks">Список новых задач.</param>
        /// <returns>Актуальный список задач.</returns>
        public List<TaskModel> GetNewTaskListAsync(List<TaskModel> oldTasks, List<TaskModel> newTasks)
        {
            var addedTasks = newTasks.ExceptBy(oldTasks.Select(u => u.Id), u => u.Id).ToList();

            var tasksWithoutChanges = oldTasks.IntersectBy(newTasks.Select(u => u.Id), u => u.Id);
            var currentTasks = tasksWithoutChanges.Concat(addedTasks);
            var removedTasks = oldTasks.Except(currentTasks).ToList();

            _taskRepository.RemoveRange(removedTasks);

            return currentTasks.ToList();
        }

        #endregion

    }
}
