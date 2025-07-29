using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Poultary.BL.Models;
using Poultary.Interfaces;

namespace Poultary.BL.Bl
{
    public class mortalityBl : mortalityinterface
    {
        public bool addmortality(mortality m)
        {
            if (m == null) return false;
            if (m.count < 0) return false;
            if (string.IsNullOrWhiteSpace(m.reason)) return false;


            return DL.mortalityDL.addmortality(m);
        }

        public List<mortality> getmortality()
        {
            return DL.mortalityDL.GetMortalities();
        }

        public bool updatemortality(mortality m)
        {
            if (m == null) return false;
            if (m.mortalityId <= 0) return false;
            if (m.batchId <= 0) return false;
            if (m.count < 0) return false;
            if (m.date == default) return false;
            if (string.IsNullOrWhiteSpace(m.reason)) return false;

            return DL.mortalityDL.updatemortality(m);
        }

        public bool deletemortality(int mortalityId)
        {
            if (mortalityId <= 0) return false;

            return DL.mortalityDL.deletemortality(mortalityId);
        }
        public List<string> getbatchnames(string text)
        {
            return DL.mortalityDL.GetBatchNames(text);
        }
        
        public List<mortality> SearchMortalityByDate(DateTime date)
        {
            if (date == default) return new List<mortality>();

            return DL.mortalityDL.SearchMortalityByDate(date);
        }
        public List<mortality> SearchMortality(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return new List<mortality>();

            return DL.mortalityDL.SearchMortalities(input.Trim());
        }

    }
}
