using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LicenseKeyCore.Database;
using LicenseKeyCore.Database.Entities;
using LicenseKeyCore.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using RepoDb;
using RepoDb.Enumerations;
using RepoDb.Interfaces;

namespace LicenseKeyCore.Repositories
{
	public class Repository : ControllerBase, IRepository
	{
		private readonly DatabaseContext _dbContext;

		public Repository(DatabaseContext databaseContext)
		{
			_dbContext = databaseContext;
		}
		public DataKeys GetById(int keyId)
		{
            return _dbContext.tblDataKeys.Find(keyId);
        }

        public IEnumerable<DataKeys> GetAll()
		{
			return _dbContext.tblDataKeys.ToList();
		}

		public Task<DataKeys> InsertKey(inputData model)
		{
			DataKeys dataKeys;
			using (GenerateKey set = new GenerateKey(_dbContext))
            {
				dataKeys = set.GenKey(model);
				_dbContext.tblDataKeys.Add(dataKeys);
				 _dbContext.SaveChanges();
            }
			return Task.FromResult(dataKeys);

		}

		public Task<bool> UpdateKey(DataKeys key)
		{
			throw new System.NotImplementedException();
		}

		public Task<bool> DeleteKey(DataKeys key)
		{
			throw new System.NotImplementedException();
		}
	}
}
