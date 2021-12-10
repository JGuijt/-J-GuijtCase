using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HerkansingBackEndAPI.Models
{
    public class CursusInstantie
    {
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDatum { get; set; }
        public int CursusId { get; set; }
    }
}



