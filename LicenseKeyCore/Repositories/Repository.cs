using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LicenseKeyCore.Database;
using LicenseKeyCore.Database.Entities;
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
			throw new System.NotImplementedException();
		}

		public IEnumerable<DataKeys> GetAll()
		{
			return _dbContext.tblDataKeys.ToList();
		}

		public Task<bool> InsertKey(DataKeys key)
		{
			throw new System.NotImplementedException();
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
