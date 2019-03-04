using RestWithASP.Data.VO;
using RestWithASP.Model;
using System.Collections.Generic;

namespace RestWithASP.Business
{
    public interface IPersonBusiness
    {
        PersonVO Create(PersonVO pessoa);
        PersonVO FindById(long id);
        List<PersonVO> FindAll();
        PersonVO Update(PersonVO pessoa);
        void Delete(long id);

    }
}
