using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LicenseKeyCore.Database.Entities;
using LicenseKeyCore.Model;

namespace LicenseKeyCore.Repositories
{
	public interface IRepository
	{
		DataKeys GetById(int keyId);
		IEnumerable<DataKeys> GetAll();
		Task<DataKeys> InsertKey(inputData model);
		Task<bool> UpdateKey(DataKeys key);
		Task<bool> DeleteKey(DataKeys key);
	}
}
