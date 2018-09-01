using ChatClube.Web.Data;
using com.chatclube.Repository.Config;
using com.chatclube.Repository.SalaX;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ChatClube.Web.Controllers
{
    public class SalaController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<string> Get()
        {

         /*   var dbOptions =
                new DbContextOptionsBuilder<ChatClubeContext>()
                .UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=ChatClube;Trusted_Connection=True;");
                */

           // new ChatClubeContext()
              var teste =  new SalaRepository().GetAll().ToList();

         

            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
            new SalaRepository().InsertUpdateSalaWifi("xy", "asdasd");

        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}