using DgmBackendEF.Data.Models;

namespace DgmBackendEF.Data.Repositories
{
    public interface IPersonRepository
    {
        Task<Person> createAttendeeProfile(Person person);
        Task deleteAttendeeProfile(Person person);
        Task<Person> getAttendeeById(int id);
        Task<IEnumerable<Person>> getAttendeeList();
        Task updateAttendeeProfile(Person person);
    }
}