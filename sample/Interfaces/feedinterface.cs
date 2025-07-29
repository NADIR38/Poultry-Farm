using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Poultary.BL.Models;

namespace Poultary.Interfaces
{
   public interface feedinterface
    {
        bool addfeed(feed c);
        bool updatefeed(feed c);
        bool deletefeed(int id);
        List<feed> getfeed();

    }
}
