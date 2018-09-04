using System;
using System.Collections.Generic;

namespace chatclube.com.Data
{
    public partial class Usuario
    {
        public Usuario()
        {
            Sala = new HashSet<Sala>();
        }

        public int Idusuario { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public int? Idsala { get; set; }
        public string Idprofile { get; set; }
        public string Apelido { get; set; }
        public string Email { get; set; }
        public string Locale { get; set; }
        public string Sexo { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string Pais { get; set; }
        public string CidadeEstado { get; set; }
        public int? NumeroDenuncias { get; set; }
        public bool Verificado { get; set; }
        public DateTime? DataHoraOnline { get; set; }
        public DateTime? DataHoraUltimaAtualizacao { get; set; }
        public string Token { get; set; }
        public string ConnectionId { get; set; }
        public string VersaoAplicativo { get; set; }
        public string BssidWifi { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string IdsSalaNotificar { get; set; }
        public DateTime DataHora { get; set; }

        public Sala IdsalaNavigation { get; set; }
        public ICollection<Sala> Sala { get; set; }
    }
}
