using com.chatclube.Repository.Config;
using com.chatclube.UsuarioX;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace com.chatclube.SalaX
{
    public partial class Sala : IEntity
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
        public int NumeroMaxUsuarios { get; set; }
        public string Pais { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Cep { get; set; }
        public string Rua { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int? IDUsuario { get; set; }
        public DateTime DataHora { get; set; }

        [NotMapped]
        public Usuario IDUsuarioNavigation { get; set; }
        [NotMapped]
        public ICollection<Usuario> Usuario { get; set; }
        public int Id { get; set; }
    }
}

