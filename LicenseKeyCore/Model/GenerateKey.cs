using System;
using LicenseKeyCore.License;
using LicenseKeyCore.Database;
using LicenseKeyCore.Database.Entities;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel;

namespace LicenseKeyCore.Model
{
    public class GenerateKey : IDisposable
    {
        private const int m_ProductCode = 1;
        private DatabaseContext m_db;
        // Flag: Has Dispose already been called?
        bool disposed = false;
        // Instantiate a SafeHandle instance.
        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);


        public GenerateKey()
        {

            m_db = new DatabaseContext();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern.
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
                // Free any other managed objects here.
                //
            }

            disposed = true;
        }

        public DataKeys GenKey(inputData model)
        {
            DataKeys _dataKeys = new DataKeys();
            String _productID = String.Empty;
            // String _productID = "D5F0-1B5F-767D-A3FB-A473-E53C-10CE-AC1D";// ComputerInfo.GetComputerId();

            if (String.IsNullOrEmpty(model.ProductID))
            {
                _productID = ComputerInfo.GetComputerId();
            }else
            {
                _productID = model.ProductID;
            }

            

            KeyManager _km = new KeyManager(_productID);

            KeyValuesClass _kv;

            String _productKey = String.Empty;

            if (model.LicenseType == LicenseType.TRIAL)
            {
                _kv = new KeyValuesClass()
                {
                    Type = LicenseType.TRIAL,
                    Header = Convert.ToByte(9),
                    Footer = Convert.ToByte(6),
                    ProductCode = (byte)m_ProductCode,
                    Edition =model.Edition,
                    Version = 1,
                    Expiration = DateTime.Now.Date.AddDays(model.ExperienceDays)
                };
                if (!_km.GenerateKey(_kv, ref _productKey))
                    return null;
            }
            else
            {
                _kv = new KeyValuesClass()
                {
                    Type = LicenseType.FULL,
                    Header = Convert.ToByte(9),
                    Footer = Convert.ToByte(6),
                    ProductCode = (byte)m_ProductCode,
                    Edition = model.Edition,
                    Version = 1
                };
                if (!_km.GenerateKey(_kv, ref _productKey))
                    return null;
            }

            _dataKeys.ProductID = _productID;
            _dataKeys.Name = model.Name;
            _dataKeys.ProducKey = _productKey;
            _dataKeys.LicenseType = _kv.Type;
            _dataKeys.ExpereienceDays = _kv.Expiration;
            _dataKeys.Edition = _kv.Edition;


            return _dataKeys;
        }

    }//end class
}//end