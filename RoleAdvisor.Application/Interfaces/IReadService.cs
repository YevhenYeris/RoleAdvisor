namespace RoleAdvisor.Application.Interfaces;

public interface IReadService<T> where T : class
{
    Task<T> GetEntityById(int id);
    Task<List<T>> GetAllEntities();
}
