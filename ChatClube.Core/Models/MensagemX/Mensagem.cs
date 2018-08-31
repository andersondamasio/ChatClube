using com.chatclube.SalaX;
using System;
using System.Collections.Generic;
using System.Text;

namespace com.chatclube.Models.MensagemX
{
    public class Mensagem
    {
        public int IdMensagem { get; set; }
        public string IdUsuario { get; set; }
        public int IdEnvia { get; set; }
        public int? IdRecebe { get; set; }
        /// <summary>
        ///1 = enviado, 2 recebido pelo servidor, 3 = recebido pelo destinatario  
        /// </summary>
        public int IdStatus { get; set; }
        public string Imagem { get; set; }
        public string descricao { get; set; }
        /// <summary>
        /// 1 = Texto, 2 Imagem, 3 = Video, 4 = Audio, 5 = usuario entrou 
        /// </summary>
        public int Tipo { get; set; }
        public bool? Bloqueado { get; set; }
        public DateTime DataHora { get; set; }
        public Sala Sala { get; set; }
      
        public bool Resposta { get; set; }
        public bool EstaNaSala { get; set; }
    }
}
