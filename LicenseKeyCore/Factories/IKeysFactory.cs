using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LicenseKeyCore.Database.Entities;
using LicenseKeyCore.Database;
using LicenseKeyCore.Model;

namespace LicenseKeyCore.Factories
{
	public interface IKeysFactory
	{
		IEnumerable<DataKeys> KeyNameList();
		DataKeys GetById(int keyId);
		Task<DataKeys> AddKey(inputData model);
	}
}
