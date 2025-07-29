using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poultary.BL.Models;

namespace Poultary.Interfaces
{
    public interface mortalityinterface
    {
        bool addmortality(mortality m);
            List<mortality> getmortality();
        bool updatemortality(mortality m);
        bool deletemortality(int mortalityId);
        List<string> getbatchnames(string text);
        List<mortality> SearchMortalityByDate(DateTime date);
        List<mortality> SearchMortality(string input);
    }
}