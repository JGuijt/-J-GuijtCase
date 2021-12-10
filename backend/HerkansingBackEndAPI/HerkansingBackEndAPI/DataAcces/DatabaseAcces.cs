using HerkansingBackEndAPI.Data;
using HerkansingBackEndAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HerkansingBackEndAPI.DataAcces
{
    public class DatabaseAcces : IDatabaseAcces
    {
        private CursusContext _context;
        public DatabaseAcces(CursusContext context)
        {
            _context = context;
        }

        public int InsertInDb(Cursus cursus)
        {
            _context.Cursussen.Add(cursus);
            _context.SaveChanges();
            return 1;
        }

        public int InsertInDb(CursusInstantie cursusInstantie)
        {
            _context.CursusInstanties.Add(cursusInstantie);
            _context.SaveChanges();
            return 1;
        }

        public bool CheckCursusAanwezig(string titel)
        {
            Cursus x = _context.Cursussen.FirstOrDefault(t => t.Titel == titel);
            if (x != null)
                return true;
            else 
                return false;
        }

        public int ZoekCursusId(string titel)
        {
            var f = _context.Cursussen.FirstOrDefault(t => t.Titel == titel);
            return f.Id;
        }

        public async Task<ActionResult<IEnumerable<CursusView>>> GetCursussen()
        {
            var query = (from c in _context.Cursussen
                           join i in _context.CursusInstanties on c.Id equals i.CursusId
                           select new CursusView
                           {
                               Titel = c.Titel,
                               Duur = c.Duur,
                               StartDatum = i.StartDatum
                           });
            var list = await query.ToListAsync();
            return list;
        }
    }

}
