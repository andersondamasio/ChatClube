using com.chatclube.Repository.Config;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace com.chatclube.Repository
{
    public class SalaRepository : Repository<Sala>
    {
        public async Task<List<Sala>> GetSalasAsync()
        {
            return await GetAll().ToListAsync();
        }

        public void SalvarSala()
        {

            var sala = GetAll().LastOrDefault();
            if (sala == null)
                sala = new Sala { IdSala = 1, Nome = "Sala 1" };
            else
                sala = new Sala { IdSala = sala.IdSala+1, Nome = $"Sala {sala.IdSala + 1}" };

            Add(sala);
        }
    }
}
