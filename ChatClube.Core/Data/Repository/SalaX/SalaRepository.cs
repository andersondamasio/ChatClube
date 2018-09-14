using com.chatclube.Repository.Config;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using com.chatclube.SalaX;
using System.Text.RegularExpressions;
using com.chatclube.Data.Repository.Config;
using chatclube.com.Services;

namespace com.chatclube.Repository.SalaX
{
        public class SalaRepository : Repository<Sala>
    {
        public SalaRepository()
        {
        }

        public async Task<List<Sala>> GetSalasAsync()
        {
            return await GetAll().ToListAsync();
        }

        public async Task<int> InsertUpdateSalaWifiAsync(string Nome, string BSSIdWifi)
        {
            try
            {
                Sala sala = GetAll().Where(s => s.BSSIDWifi == BSSIdWifi).FirstOrDefault();
                if (sala != null)
                {
                    if (sala.Nome != Nome)
                    {
                        sala.Nome = Nome;
                        return await UpdateAsync(sala);
                    }
                }
                else
                {
                    sala = new Sala();
                    sala.IDTipo = 2;
                    sala.Nome = Regex.Replace(Nome, @"\""", string.Empty).Truncate(50, false);
                    sala.BSSIDWifi = BSSIdWifi;
                    sala.IDSala = GetAll().Select(s => s.IDSala).DefaultIfEmpty().Max() + 1;
                    return await AddAsync(sala);
                }
            }
            catch(Exception ex) { }
            return 0;
        }


        public void SalvarSala()
        {

            var sala = GetAll().LastOrDefault();
            if (sala == null)
                sala = new Sala { IDSala = 1, Nome = "Sala 1" };
            else
                sala = new Sala { IDSala = sala.IDSala+1, Nome = $"Sala {sala.IDSala + 1}" };

            AddAsync(sala);
        }
    }
}
