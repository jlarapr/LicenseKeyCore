using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LicenseKeyCore.Database;
using LicenseKeyCore.Database.Entities;
using LicenseKeyCore.Model;
using Microsoft.AspNetCore.Http;
using LicenseKeyCore.Factories;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LicenseKeyCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerateController : ControllerBase
    {

        private readonly IKeysFactory m_keysFactory;
        //private readonly DatabaseContext m_db;

        //public GenerateController()
        //{
        //    m_db = new DatabaseContext();
        //}

        //public GenerateController(IKeysFactory keysFactory, DatabaseContext context)
        public GenerateController(IKeysFactory keysFactory)
        {
            //m_db = context;// new DatabaseContext();
            m_keysFactory = keysFactory;
        }

        [HttpGet("getKeyList")]
        [ProducesResponseType(typeof(DataKeys), 200)]
        [ProducesResponseType(typeof(DataKeys), 400)]
        public IActionResult GetAllKeys()
        {
            var result = m_keysFactory.KeyNameList();
            if (result == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, result);
                //return BadRequest((IEnumerable<string>)null);
            }
            return StatusCode(StatusCodes.Status200OK,result);
           // return Ok(result);
        }


        // GET: api/<GenerateController>
        [HttpGet]
        public IActionResult Get()
        {
            //  return m_db.tblDataKeys.ToList();
            var result = m_keysFactory.KeyNameList();
            if (result == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, result);
            }
            return StatusCode(StatusCodes.Status200OK, result);
        }

        // GET api/<GenerateController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            //return m_db.tblDataKeys.Find(id);

            var result = m_keysFactory.GetById(id);
            if (result == null)
            {
                return StatusCode(StatusCodes.Status400BadRequest, result);
                //return BadRequest((IEnumerable<string>)null);
            }
            return StatusCode(StatusCodes.Status200OK, result);
        }

        // POST api/<GenerateController>
        [HttpPost]
        public IActionResult Post([FromBody] inputData model)
        {
            try
            {
                var result = m_keysFactory.AddKey(model);
                if (result == null)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, result);
                    //return BadRequest((IEnumerable<string>)null);
                }
                return StatusCode(StatusCodes.Status200OK, result);

                //DataKeys _dataKeys;
                //using (GenerateKey set = new GenerateKey(m_db))
                //{
                //    _dataKeys = set.GenKey(model);
                //    m_db.tblDataKeys.Add(_dataKeys);
                //    m_db.SaveChanges();
                //}
                //return StatusCode(StatusCodes.Status201Created, _dataKeys);
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
