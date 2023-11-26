namespace RoleAdvisor.Application.Interfaces;

public interface IWriteService<T> where T : class
{
    Task<T> AddEntity(T entity);
    Task<T> UpdateEntity(T entity);
    Task<bool> DeleteEntity(int id);
}
