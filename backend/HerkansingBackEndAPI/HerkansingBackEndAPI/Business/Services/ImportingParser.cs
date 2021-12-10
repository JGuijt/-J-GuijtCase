using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HerkansingBackEndAPI.DataAcces;
using HerkansingBackEndAPI.Interfaces;
using HerkansingBackEndAPI.Models;
using Microsoft.AspNetCore.Http;


namespace HerkansingBackEndAPI.Services
{
    public class ImportingParser : IImportingParser
    {
        IFormFile file; 
        string[] looseCourses;
        IDatabaseAcces _databaseAcces;

        public ImportingParser(IFormFile file, IDatabaseAcces databaseAcces)
        {
            this.file = file;
            _databaseAcces = databaseAcces;
        }

        public int[] ParseFile()
        {
            int[] counters = { 0, 0 };
            if (file.Length > 0)
            {
                using (var reader = new StreamReader(file.OpenReadStream()))
                {
                    while (reader.Peek() >= 0)
                    {
                        string result = reader.ReadToEnd();
                        looseCourses = result.Split("\n\n");
                    }
                }
                counters = VerzamelBak(looseCourses);
            }
            return counters;
        }

        public int[] VerzamelBak(string[] cursussen)
        {
            int cursusCounter = 0;
            int instantieCounter = 0;
            foreach (string cursus in cursussen)
            {
                if (cursus.Length > 1)
                {
                    string[] splitCourse = SplitRegels(cursus);
                    string titel = ParseTitel(splitCourse);
                    string code = ParseCode(splitCourse);
                    int duur = ParseDuur(splitCourse);
                    DateTime datum = ParseDatum(splitCourse);
                    cursusCounter += SendCursusToDb(titel, duur, code);
                    instantieCounter += SendInstantieToDb(datum, titel);
                }
            }
            int[] updatedCounters = { cursusCounter, instantieCounter };
            return  updatedCounters;
        }

        //Check of cursus al aanwezig is, anders toevoegen aan db        
        public int SendCursusToDb(string titel, int duur, string code)
        {
            int counter = 0;
            Cursus cursus = new Cursus() { Titel = titel, Duur = duur, Code = code };
            if (_databaseAcces.CheckCursusAanwezig(titel))
            {
                return counter;
            }
            else
            {
                _databaseAcces.InsertInDb(cursus);
                counter++;
                return counter;
            }
        }


        //checkt de juiste CursusId, voegt dan toe aan db
        public int SendInstantieToDb(DateTime datum, string titel)
        {
            int counter = 0;
            int gevondenId;
            gevondenId = _databaseAcces.ZoekCursusId(titel);
            CursusInstantie instantie = new CursusInstantie() { StartDatum = datum, CursusId = gevondenId };
            _databaseAcces.InsertInDb(instantie);
            counter++;
            return counter;
        }

        //Split cursus blok in enkele regels
        public string[] SplitRegels(string singleLine)
        {
            string[] splitLines = singleLine.Split("\n");
            return splitLines;
        }
        public string ParseTitel(string[] input)
        {
            string result  = string.Empty;            
            foreach(string line in input)
            {
                if (line.StartsWith("Titel: "))
                {
                    string title = line.Remove(0, 7);
                    result = title;
                }
            }            
            return result;
        }

        public string ParseCode(string[] input)
        {
            string result = string.Empty;
            foreach (string line in input)
            {
                if (line.StartsWith("Cursuscode: "))
                {
                    string code = line.Remove(0, 12);
                    result = code;
                }
            }
            return result;
        }

        public int ParseDuur(string[] input)
        {
            int result = 0;
            foreach (string line in input)
            {
                if (line.StartsWith("Duur: "))
                {
                    string dagen = line.Remove(0, 6);
                    string duur = dagen.Remove(1);
                    result = int.Parse(duur);
                }
            }
            return result;
        }

        public DateTime ParseDatum(string[] input)
        {
            DateTime result = default;
            foreach (string line in input)
            {
                if (line.StartsWith("Startdatum: "))
                {
                    string datum = line.Remove(0, 11);
                    result = DateTime.Parse(datum);
                }
            }
            return result;
        }
    }
}
