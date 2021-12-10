using HerkansingBackEndAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HerkansingBackEndAPI.DataAcces
{
    public interface IDatabaseAcces
    {
        //Task<int> InsertInDb(Cursus cursus);
        //Task<int> InsertInDb(CursusInstantie cursusInstantie);
        int InsertInDb(Cursus cursus);
        int InsertInDb(CursusInstantie cursusInstantie);
        Task<ActionResult<IEnumerable<CursusView>>> GetCursussen();
        bool CheckCursusAanwezig(string titel);
        int ZoekCursusId(string titel);        
    }
}
