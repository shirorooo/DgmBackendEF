using DgmBackendEF.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DgmBackendEF.Data.Repositories;

public class PersonRepository : IPersonRepository
{
    private readonly PersonContext _ctx;


    public PersonRepository(PersonContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<IEnumerable<Person>> getAttendeeList()
    {
        return await _ctx.Person.ToListAsync();
    }

    public async Task<Person> getAttendeeById(int id)
    {
        return await _ctx.Person.AsNoTracking().FirstOrDefaultAsync(person => person.Id == id);
    }

    public async Task<Person> createAttendeeProfile(Person person)
    {
        _ctx.Person.Add(person);
        await _ctx.SaveChangesAsync();
        return person;
    }

    public async Task updateAttendeeProfile(Person person)
    {
        _ctx.Person.Update(person);
        await _ctx.SaveChangesAsync();
    }

    public async Task deleteAttendeeProfile(Person person)
    {
        _ctx.Person.Remove(person);
        await _ctx.SaveChangesAsync();
    }
}
