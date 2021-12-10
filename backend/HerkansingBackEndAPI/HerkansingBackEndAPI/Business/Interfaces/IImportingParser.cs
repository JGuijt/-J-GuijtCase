using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HerkansingBackEndAPI.Interfaces
{
    interface IImportingParser
    {
        //void ParseFile();
        int[] ParseFile();

        //void VerzamelBak(string[] cursussen);
        int[] VerzamelBak(string[] cursussen);
        string[] SplitRegels(string singleLine);
        //void SendCursusToDb(string titel, int duur, string code);
        int SendCursusToDb(string titel, int duur, string code);
        //void SendInstantieToDb(DateTime datum, string titel);
        public int SendInstantieToDb(DateTime datum, string titel);
        string ParseTitel(string[] input);
        int ParseDuur(string[] input);
        DateTime ParseDatum(string[] input);
    }
}
