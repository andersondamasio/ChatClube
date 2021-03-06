﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatClube.Web.Data.Config;
using com.chatclube.Data.Repository.Config;
using com.chatclube.Repository.Config;
using com.chatclube.Repository.SalaX;
using com.chatclube.SalaX;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace chatclube.com.Controllers
{
    [Route("api/[controller]")]
    public class SalaController : Controller
    {
        public SalaController(DBContextCoreSQLServer DBContextCoreSQLServer)
        {

            DBContextCore.DbType = DBContextCoreSQLServer;
        }

        // GET: api/<controller>
        [HttpGet]
        public List<Sala> Get()
        {
            var teste = new SalaRepository().GetAll().ToList();//.ToList();
            return teste;
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]Sala sala)
        {
            if (sala != null)
                 new SalaRepository().InsertUpdateSalaWifiAsync(sala.Nome, sala.BSSIDWifi);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
