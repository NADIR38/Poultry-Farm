using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoultryProject.BL.Models;

namespace PoultryProject.Interfaces
{
    public interface Ifeedinfo
    {
        List<feedinfo> getinfo();
        List<feedinfo> searchinfo(string text);
    }
}
