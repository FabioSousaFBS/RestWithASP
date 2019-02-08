using System;
using System.Collections.Generic;
using System.Threading;
using RestWithASP.Model;

namespace RestWithASP.Services.Implementation
{
    public class PersonServiceImplementation : IPersonServices
    {
        private volatile int count;

        public Person Create(Person pessoa)
        {
            return pessoa;
        }

        public void Delete(long id)
        {
            
        }

        public List<Person> FindAll()
        {
            List<Person> persons = new List<Person>();
            for(int i = 0; i < 8; i++)
            {
                Person person = MockPerson(i);
                persons.Add(person);
            }

            return persons;
        }

        private Person MockPerson(int i)
        {
            return new Person
            {
                Id = IncrementAndGet(),
                FirstName = "Person Name " + i ,
                LastName = "Person Lastname " + i,
                Address = "Some address " + i,
                Gender = "Masculino"
            };
        }

        private long IncrementAndGet()
        {
            return Interlocked.Increment(ref count);
        }

        public Person FindById(long id)
        {
            return new Person {
            Id= IncrementAndGet(),
            FirstName="Fabio",
            LastName="Sousa",
            Address="Rua teste, 123",
            Gender = "Masculino"
            };
        }

        public Person Update(Person pessoa)
        {
            return pessoa;
        }
    }
}
