using System;
using System.Collections.Generic;
using System.Linq;
using ChatClube.Web.Data.Config;
using com.chatclube.Data.Repository.UsuarioX;
using com.chatclube.Repository.Config;
using com.chatclube.UsuarioX;
using Microsoft.AspNetCore.Mvc;

namespace ChatClube.Web.Controllers
{
    [Route("api/[controller]")]
    public class UsuarioController : Controller
    {
        UsuarioRepository usuarioRep;

        public UsuarioController(DBContextCoreSQLServer DBContextCoreSQLServer)
        {

            DBContextCore.DbType = DBContextCoreSQLServer;
            usuarioRep = new UsuarioRepository();
        }

        // GET: api/<controller>
        [HttpGet]
        public List<Usuario> Get()
        {
            var teste = usuarioRep.GetAll().ToList();//.ToList();
            return teste;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public Usuario Get(string IDProfile)
        {
            return  usuarioRep.GetUsuario(IDProfile);//.ToList();
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]Usuario usuario)
        {
            if (usuario != null)
                usuarioRep.SalvarUsuario(usuario);
        }
    }
}