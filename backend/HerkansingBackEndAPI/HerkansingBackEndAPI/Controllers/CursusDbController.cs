using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HerkansingBackEndAPI.Data;
using HerkansingBackEndAPI.Models;
using HerkansingBackEndAPI.DataAcces;
using HerkansingBackEndAPI.Services;

namespace HerkansingBackEndAPI.Controllers
{
    //Controller voor de database
    [Route("api/[controller]")]
    [ApiController]
    public class CursusDbController : ControllerBase
    {
        private IDatabaseAcces _databaseAcces;

        public CursusDbController(IDatabaseAcces databaseAcces)
        {
            _databaseAcces = databaseAcces;
        }

        // GET: api/CursusDb
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CursusView>>> GetCursussen()
        {
            return await _databaseAcces.GetCursussen();
        }

        //POST api/CursusDb
        //[HttpPost]
        // public async Task<IActionResult> Upload()
        //{
        //    //Uitlezen van geupload bestand, splitten in losse cursussen
        //    var file = Request.Form.Files[0];
        //    var reader = new ImportingParser(file, _databaseAcces);
        //    reader.ParseFile();

        //    //juiste return 
        //    return new OkResult();
        //}


        [HttpPost]
        public async Task<int[]> Upload()
        {
            //Uitlezen van geupload bestand, splitten in losse cursussen
            var file = Request.Form.Files[0];
            var reader = new ImportingParser(file, _databaseAcces);
            //reader.ParseFile();
            var x = reader.ParseFile();

            //juiste return 
            return x;
        }




    }
}


