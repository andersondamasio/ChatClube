using ChatClube.Web.Data.Config;
using com.chatclube.Data.Repository.Config;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace com.chatclube.Repository.Config
{
    public class Repository<T> : IRepository<T>
       where T : class
    {
        private DbSet<T> dbSet;

        #region Singleton
        public DbContext DBContext => GetDBContext();
        #endregion

        private DbContext GetDBContext()
        {
            switch (DBContextCore.DBContextType)
            {
                case DBContextType.SQLite:
                    {
                       if(DBContextCore.DbType == null)
                            DBContextCore.DbType = new DBContextCoreSQLite();
                        return ((DBContextCoreSQLite)DBContextCore.DbType); ;
                    }
                case DBContextType.SQLServer:
                    return ((DBContextCoreSQLServer)DBContextCore.DbType);
                default:
                    throw new Exception("Nenhum Provedor encontrado.");
            }
        }

        public Repository()
        {
            this.dbSet = DBContext.Set<T>();
        }

        public async System.Threading.Tasks.Task<int> AddAsync(T item)
        {
            var dataHora = item.GetType().GetProperties().Where(s => s.Name.ToLower().EndsWith("datahora")).FirstOrDefault();
            if (dataHora != null)
                dataHora.SetValue(item, DateTime.Now, null);

            if (DBContextCore.DBContextType == DBContextType.SQLServer)
            {
                var primarykey = GetKey(item);
                if (primarykey != null)
                    primarykey.SetValue(item, null, null);
            }

            await dbSet.AddAsync(item);

            return await SaveChangesAsync();
        }

        public async System.Threading.Tasks.Task<int> UpdateAsync(T item)
        {
            var ultimaAtualizacao = DBContext.GetType().GetProperties().Where(s => s.Name.ToLower().EndsWith("ultimaatualizacao")).FirstOrDefault();
            if (ultimaAtualizacao != null)
                ultimaAtualizacao.SetValue(DBContext, DateTime.Now, null);
            return await SaveChangesAsync();
        }

  

        public IQueryable<T> GetAll()
        {
            return dbSet;
        }

        public async System.Threading.Tasks.Task<int> RemoveAsync(T item)
        {
            dbSet.Remove(item);
            return await SaveChangesAsync();
        }

        private async System.Threading.Tasks.Task<int> SaveChangesAsync()
        {
            try
            {
                int save = await DBContext.SaveChangesAsync();

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

        private PropertyInfo GetKey(T entity)
        {
            var keyName = DBContext.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties
                .Select(x => x.Name).Single();
            return entity.GetType().GetProperty(keyName);
        }

    }
}
