using com.chatclube.UsuarioX;
using System;
using System.Collections.Generic;

namespace com.chatclube.SalaX
{
    public partial class Sala
    {
        public Sala()
        {
            Usuario = new HashSet<Usuario>();
        }

        public int IDSala { get; set; }
        public string Nome { get; set; }
        public int? IDTipo { get; set; }
        public string BSSIDWifi { get; set; }
        public int NumeroUsuariosOnline { get; set; }
        public string NumeroMaxUsuarios { get; set; }
        public string Pais { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Cep { get; set; }
        public string Rua { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int? IDUsuario { get; set; }
        public DateTime DataHora { get; set; }

        public Usuario IDUsuarioNavigation { get; set; }
        public ICollection<Usuario> Usuario { get; set; }
    }
}

