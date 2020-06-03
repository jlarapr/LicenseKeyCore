using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LicenseKeyCore.Database.Entities;
using LicenseKeyCore.Database;

namespace LicenseKeyCore.Factories
{
	public interface IKeysFactory
	{
		IEnumerable<DataKeys> KeyNameList();
	}
}
