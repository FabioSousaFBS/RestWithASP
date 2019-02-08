using RestWithASP.Model;
using System.Collections.Generic;

namespace RestWithASP.Services
{
    public interface IPersonServices
    {
        Person Create(Person pessoa);
        Person FindById(long id);
        List<Person> FindAll();
        Person Update(Person pessoa);
        void Delete(long id);




    }
}
