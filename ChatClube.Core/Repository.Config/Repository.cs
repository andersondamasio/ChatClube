using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace com.chatclube.Repository.Config
{
   public class Repository<T> : IRepository<T> where T : class
    {
         ChatClubeContext chatClubeContext;
        private DbSet<T> dbSet;

        public Repository()
        {
            this.chatClubeContext = new ChatClubeContext();
            this.dbSet = chatClubeContext.Set<T>();
        }

        public int Add(T item)
        {
            var dataHora = item.GetType().GetProperties().Where(s => s.Name.EndsWith("dataHora")).FirstOrDefault();
            if (dataHora != null)
                dataHora.SetValue(item, DateTime.Now, null);
            dbSet.Add(item);
            return SaveChanges();
        }

        public int Update(T item)
        {
            var ultimaAtualizacao = chatClubeContext.GetType().GetProperties().Where(s => s.Name.EndsWith("ultimaAtualizacao")).FirstOrDefault();
            if (ultimaAtualizacao != null)
                ultimaAtualizacao.SetValue(chatClubeContext, DateTime.Now, null);
            return SaveChanges();
        }

        public T Get(string id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<T> GetAll()
        {
            return dbSet;
        }

        public int Remove(T item)
        {
            dbSet.Remove(item);
            return SaveChanges();
        }

        private int SaveChanges()
        {
            try
            {
                int save = chatClubeContext.SaveChanges();

                return save;
            }
            catch (Exception e)
            {
                string mensagemErro = string.Empty;
                throw new Exception(e.Message);
                /*foreach (var eve in e.EntityValidationErrors)
                {
                    mensagemErro = string.Format("Entity do tipo \"{0}\" em estado \"{1}\" tem os seguintes erros:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        mensagemErro += System.Environment.NewLine + string.Format("Entity do tipo \"{0}\" em estado \"{1}\"tem os seguintes erros:",
                       eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        mensagemErro += string.Format("- Propriedade: \"{0}\", Erro: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }*/
                //throw new DbEntityValidationException(mensagemErro, e.EntityValidationErrors);
            }
        }
    }
}
