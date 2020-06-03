using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LicenseKeyCore.Database.Entities;
using LicenseKeyCore.Database;
using LicenseKeyCore.Repositories;
using LicenseKeyCore.Model;

namespace LicenseKeyCore.Factories
{
	public class KeysFactory : IKeysFactory
	{
		private readonly IRepository _repository;

		public KeysFactory(IRepository repository)
		{
			_repository = repository;
		}

        public Task<DataKeys> AddKey(inputData model)
        {
			var result = _repository.InsertKey(model);
			return result;
        }

        public DataKeys GetById(int keyId)
        {
			var result = _repository.GetById(keyId);
			return result;
        }

        public IEnumerable<DataKeys> KeyNameList()
		{
			var result = _repository.GetAll();
			return result.ToList();
		}
	}
}
