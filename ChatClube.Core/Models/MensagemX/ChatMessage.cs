using System;
using System.Collections.Generic;
using System.Text;

namespace com.chatclube.Models
{
    public class Mensagem
    {
        public String Descricao { get; set; }
        public UserType Tipo { get; set; }
        public Status Status { get; set; }
        public long Hora { get; set; }
    }
}
