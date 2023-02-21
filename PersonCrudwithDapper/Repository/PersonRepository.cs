using Dapper;
using PersonCrudwithDapper.IRepository;
using System.Data.SqlClient;
using System.Data;
using PersonCrudwithDapper.DTO;

namespace PersonCrudwithDapper.Repository
{
    public class PersonRepository : IPersonRepository
    {
        private readonly string _connectionString;

        public PersonRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> CreateAsync(Person person)
        {
            using var connection = new SqlConnection(_connectionString);
            var parameters = new DynamicParameters();
            parameters.Add("@FirstName", person.FirstName);
            parameters.Add("@Gender", person.Gender);
            parameters.Add("@Country", person.Country);
            parameters.Add("@State", person.State);
            parameters.Add("@City", person.City);
            parameters.Add("@DateOfBirth", person.DateOfBirth);
            parameters.Add("@Hobbies", person.Hobbies);
            parameters.Add("@AcceptTermsAndCondition", person.AcceptTermsAndCondition);
            parameters.Add("@Id", dbType: DbType.Int32, direction: ParameterDirection.Output);

            await connection.ExecuteAsync(
                "dbo.sp_AddPerson",
                parameters,
                commandType: CommandType.StoredProcedure);

            return parameters.Get<int>("@Id");
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);

            return await connection.QuerySingleOrDefaultAsync<Person>(
                "dbo.sp_GetPersonById",
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            using var connection = new SqlConnection(_connectionString);

            return await connection.QueryAsync<Person>(
                "dbo.sp_GetAllPersons",
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> UpdateAsync(Person person)
        {
            using var connection = new SqlConnection(_connectionString);
            var parameters = new DynamicParameters();
            parameters.Add("@Id", person.Id);
            parameters.Add("@FirstName", person.FirstName);
            parameters.Add("@Gender", person.Gender);
            parameters.Add("@Country", person.Country);
            parameters.Add("@State", person.State);
            parameters.Add("@City", person.City);
            parameters.Add("@DateOfBirth", person.DateOfBirth);
            parameters.Add("@Hobbies", person.Hobbies);
            parameters.Add("@AcceptTermsAndCondition", person.AcceptTermsAndCondition);

            return await connection.ExecuteAsync(
                "dbo.sp_UpdatePerson",
                parameters,
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> DeleteAsync(int id)
        {
            using var connection = new SqlConnection(_connectionString);
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id);

            return await connection.ExecuteAsync(
                "dbo.sp_DeletePerson",
                parameters,
                commandType: CommandType.StoredProcedure);
        }
    }

}
