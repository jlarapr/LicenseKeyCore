using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LicenseKeyCore.Database.Entities;
using LicenseKeyCore.Database;
using LicenseKeyCore.Repositories;

namespace LicenseKeyCore.Factories
{
	public class KeysFactory : IKeysFactory
	{
		private readonly IRepository _repository;

		public KeysFactory(IRepository repository)
		{
			_repository = repository;
		}
		public IEnumerable<DataKeys> KeyNameList()
		{
			var result = _repository.GetAll();
			return result.ToList();
		}
	}
}
