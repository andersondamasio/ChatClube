using com.chatclube.Repository.Config;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System;

namespace com.chatclube.Repository
{
   public class SalaRepository : Repository<Sala>
    {
        public List<Sala> GetSalas()
        {
            return GetAll().ToList(); 
        }

        public void SalvarSala()
        {
            

           Add(new Sala { Nome = Guid.NewGuid().ToString() });
        }
    }
}
