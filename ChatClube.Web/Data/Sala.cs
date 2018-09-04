using System;
using System.Collections.Generic;

namespace chatclube.com.Data
{
    public partial class Sala
    {
        public Sala()
        {
            Usuario = new HashSet<Usuario>();
        }

        public int IdSala { get; set; }
        public string Nome { get; set; }
        public int? Idtipo { get; set; }
        public string BssidWifi { get; set; }
        public int NumeroUsuariosOnline { get; set; }
        public int NumeroMaxUsuarios { get; set; }
        public string Pais { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Cep { get; set; }
        public string Rua { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int? Idusuario { get; set; }
        public DateTime DataHora { get; set; }

        public Usuario IdusuarioNavigation { get; set; }
        public ICollection<Usuario> Usuario { get; set; }
    }
}
