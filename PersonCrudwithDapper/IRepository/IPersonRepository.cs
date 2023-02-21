using PersonCrudwithDapper.DTO;

namespace PersonCrudwithDapper.IRepository
{
    public interface IPersonRepository
    {
        Task<int> CreateAsync(Person person);
        Task<Person> GetByIdAsync(int id);
        Task<IEnumerable<Person>> GetAllAsync();
        Task<int> UpdateAsync(Person person);
        Task<int> DeleteAsync(int id);
    }
}
