//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ChatClube.Web.Data
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sala
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Sala()
        {
            this.Usuario1 = new HashSet<Usuario>();
        }
    
        public int IdSala { get; set; }
        public string Nome { get; set; }
        public Nullable<int> IDTipo { get; set; }
        public string BSSIdWifi { get; set; }
        public int NumeroUsuariosOnline { get; set; }
        public int NumeroMaxUsuarios { get; set; }
        public string Pais { get; set; }
        public string Estado { get; set; }
        public string Cidade { get; set; }
        public string Cep { get; set; }
        public string Rua { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public Nullable<int> IDUsuario { get; set; }
        public System.DateTime DataHora { get; set; }
    
        public virtual Usuario Usuario { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Usuario> Usuario1 { get; set; }
    }
}
