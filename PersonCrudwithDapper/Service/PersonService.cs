using PersonCrudwithDapper.DTO;
using PersonCrudwithDapper.IRepository;

namespace PersonCrudwithDapper.Service
{
    public class PersonService
    {
        private readonly IPersonRepository _personRepository;

        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        public async Task<int> CreatePersonAsync(Person person)
        {
            return await _personRepository.CreateAsync(person);
        }

        public async Task<Person> GetPersonByIdAsync(int id)
        {
            return await _personRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Person>> GetAllPeopleAsync()
        {
            return await _personRepository.GetAllAsync();
        }

        public async Task<int> UpdatePersonAsync(Person person)
        {
            return await _personRepository.UpdateAsync(person);
        }

        public async Task<int> DeletePersonAsync(int id)
        {
            return await _personRepository.DeleteAsync(id);
        }
    }
}
