using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PoultryProject.BL.Models;
using PoultryProject.Interfaces;

namespace PoultryProject.BL.Bl
{
    public class feedinfoBL : Ifeedinfo
    {
        public List<feedinfo> getinfo()
        {
            try
            {
                return PoultryProject.DL.feedinfoDL.getinfo();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in feedinfoBL: " + ex.Message);
                return new List<feedinfo>();
            }
        }

        public List<feedinfo> searchinfo(string text)
        {
            try
            {
                return PoultryProject.DL.feedinfoDL.SearchFeedInfo(text);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in feedinfoBL: " + ex.Message);
                return new List<feedinfo>();
            }
        }
    }
}