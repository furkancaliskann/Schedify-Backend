using AutoMapper;
using Business.Abstract;
using Business.Dtos;
using Business.Exceptions;
using DataAccess.Abstract;
using Entities.Concrete;
using FluentValidation;
using DataAccess.Pagination;

public class TodoService : ITodoService
{
    private readonly ITodoRepository _todoRepository;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateTodoDto> _createTodoValidator;
    private readonly IValidator<UpdateTodoDto> _updateTodoValidator;

    public TodoService(ITodoRepository todoRepository, IMapper mapper, 
        IValidator<CreateTodoDto> createTodoValidator, IValidator<UpdateTodoDto> updateTodoValidator)
    {
        _todoRepository = todoRepository;
        _mapper = mapper;
        _createTodoValidator = createTodoValidator;
        _updateTodoValidator = updateTodoValidator;
    }

    public async Task<PagedResponse<Todo>> GetAllAsync(string userId, PaginationQuery paginationQuery)
    {
        return await _todoRepository.GetAllAsync(userId, paginationQuery);
    }

    public async Task<Todo> GetByIdAsync(string userId, int id)
    {
        var todo = await _todoRepository.GetByIdAsync(id);

        if (todo == null)
        {
            throw new NotFoundException("Todo not found!");
        }

        if (todo.CreatedUserId != userId)
        {
            throw new UnauthorizedAccessException("You do not have permission to view this todo!");
        }

        return todo;
    }

    public async Task AddAsync(CreateTodoDto entity, string userId)
    {
        var validationResult = _createTodoValidator.Validate(entity);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var todo = _mapper.Map<Todo>(entity);
        todo.CreatedAt = DateTime.UtcNow;
        todo.UpdatedAt = DateTime.UtcNow;
        todo.CreatedUserId = userId;
        await _todoRepository.AddAsync(todo);
    }

    public async Task UpdateAsync(string userId, int todoId, UpdateTodoDto updateTodoDto)
    {
        var validationResult = _updateTodoValidator.Validate(updateTodoDto);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var todo = await _todoRepository.GetByIdAsync(todoId);

        if (todo == null)
        {
            throw new NotFoundException("Todo not found!");
        }

        if (todo.CreatedUserId != userId)
        {
            throw new UnauthorizedAccessException("You do not have permission to update this todo!");
        }

        _mapper.Map(updateTodoDto, todo);
        todo.UpdatedAt = DateTime.UtcNow;

        await _todoRepository.UpdateAsync(todo);
    }

    public async Task DeleteAsync(string userId, int id)
    {
        var todo = await _todoRepository.GetByIdAsync(id);

        if (todo is null)
        {
            throw new NotFoundException("Todo not found!");
        }

        if(todo.CreatedUserId != userId)
            throw new UnauthorizedAccessException("You do not have permission to delete this todo!");

        await _todoRepository.DeleteAsync(id);
    }
}
