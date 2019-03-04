using System.Collections.Generic;
using RestWithASP.Data.Converters;
using RestWithASP.Data.VO;
using RestWithASP.Model;
using RestWithASP.Repository.Generic;

namespace RestWithASP.Business.Implementation
{
    public class PersonBusinessImplementation : IPersonBusiness
    {
        private  IRepository<Person> _repository;
        private readonly PersonConverter _converter;

        public PersonBusinessImplementation(IRepository<Person> repository)
        {
            _repository = repository;
            _converter = new PersonConverter();
        }

        public PersonVO Create(PersonVO pessoa)
        {
            var personEntity = _converter.Parse(pessoa);
            personEntity = _repository.Create(personEntity);
            return _converter.Parse(personEntity);
        }

        public void Delete(long id)
        {
            _repository.Delete(id);
        }

        public List<PersonVO> FindAll()
        {
            return _converter.ParseList(_repository.FindAll());
        }
        
        public PersonVO FindById(long id)
        {
            return _converter.Parse(_repository.FindById(id));
        }

        public PersonVO Update(PersonVO pessoa)
        {
            var personEntity = _converter.Parse(pessoa);
            personEntity = _repository.Update(personEntity);
            return _converter.Parse(personEntity);
        }
    }
}
