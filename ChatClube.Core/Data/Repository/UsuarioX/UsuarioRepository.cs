using com.chatclube.Repository.Config;
using com.chatclube.UsuarioX;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace com.chatclube.Data.Repository.UsuarioX
{
    public class UsuarioRepository : Repository<Usuario>
    {
        public UsuarioRepository()
        {
        }

        public Usuario GetUsuario(string IDProfile)
        {
            return GetAll().Where(s => s.IDProfile == IDProfile).FirstOrDefault();
        }

        public async Task<Usuario> GetUsuarioAsync(string IDProfile)
        {
            return await GetAll().Where(s => s.IDProfile == IDProfile).FirstOrDefaultAsync();
        }

        public async Task SalvarUsuarioAsync(Usuario usuario)
        {
            Usuario usu = await GetAll().Where(s => s.IDProfile == usuario.IDProfile).FirstOrDefaultAsync();
            if (usu == null)
            {
                usuario.IDUsuario = GetAll().Select(s => s.IDUsuario).DefaultIfEmpty().Max() + 1;
                await AddAsync(usuario);
            }
        }
    }
}
