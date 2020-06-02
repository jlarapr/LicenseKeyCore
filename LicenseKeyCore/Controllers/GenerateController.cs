using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LicenseKeyCore.Database;
using LicenseKeyCore.Database.Entities;
using LicenseKeyCore.Model;
using Microsoft.AspNetCore.Http;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LicenseKeyCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerateController : ControllerBase
    {

        private readonly DatabaseContext m_db;

        public GenerateController()
        {
            m_db = new DatabaseContext();
        }

        // GET: api/<GenerateController>
        [HttpGet]
        public IEnumerable<DataKeys> Get()
        {
            return m_db.tblDataKeys.ToList();
        }

        // GET api/<GenerateController>/5
        [HttpGet("{id}")]
        public DataKeys Get(int id)
        {
            return m_db.tblDataKeys.Find(id);
        }

        // POST api/<GenerateController>
        [HttpPost]
        public IActionResult Post([FromBody] inputData model)
        {
            try
            {
                DataKeys _dataKeys;
                using (GenerateKey set = new GenerateKey())
                {
                    _dataKeys = set.GenKey(model);
                    m_db.tblDataKeys.Add(_dataKeys);
                    m_db.SaveChanges();
                }
                return StatusCode(StatusCodes.Status201Created, _dataKeys);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }

        // PUT api/<GenerateController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }
    }
}
