using System;
using System.Collections.Generic;
//using System.Windows.Forms;
using PoultryProject.BL.Models;
using PoultryProject.DL;
namespace PoultryProject.BL.Bl
{
    public class trackfeedBL : ITrackfeed
    {
        private readonly trackfeedDL dl = new trackfeedDL();

        public bool addtrack(trackfeed g)
        {
            if (string.IsNullOrWhiteSpace(g.name))
            {
                //MessageBox.Show("Batch name is required.");
                return false;
            }

            if (g.sacksUsed <= 0)
            {
                //MessageBox.Show("Sacks used must be greater than 0.");
                return false;
            }

            return dl.addtrack(g);
        }

        public bool deletetrack(int id)
        {
            if (id <= 0)
                return false;

            return dl.deleteTrack(id);
        }

        public bool updatetrack(trackfeed g)
        {
            if (g.id <= 0 || !IsValid(g)) return false;

            return dl.updateTrack(g);
        }

        public List<trackfeed> getAllTracks()
        {
            return dl.getAllTracks();
        }

        public List<string> GetChickBatchNames()
        {
            return dl.GetChickBatchNames();
        }

        public List<trackfeed> searchTrackFeeds(string batchName)
        {
            return  trackfeedDL .SearchTrackFeeds(batchName);
        
            }

        // 🔒 Centralized validation method
        private bool IsValid(trackfeed g)
        {
            if (string.IsNullOrWhiteSpace(g.name))
                return false;

            if (g.sacksUsed <= 0)
                return false;

            if (g.date == default(DateTime))
                return false;

            return true;
        }
    }
}
