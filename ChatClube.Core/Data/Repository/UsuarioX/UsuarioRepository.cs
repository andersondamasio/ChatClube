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

        public void SalvarUsuario(Usuario usuario)
        {
            Usuario usu = GetAll().Where(s => s.IDProfile == usuario.IDProfile).FirstOrDefault();
            if (usu == null)
                Add(usuario);
        }
    }
}
