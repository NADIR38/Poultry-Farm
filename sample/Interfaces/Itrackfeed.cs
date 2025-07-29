using PoultryProject.BL.Models;
using System.Collections.Generic;
using System;

public interface ITrackfeed
{
    bool addtrack(trackfeed g);
    bool deletetrack(int id);
    bool updatetrack(trackfeed g);
    List<trackfeed> getAllTracks();

    // New methods
    List<string> GetChickBatchNames();
    List<trackfeed> searchTrackFeeds(string batchName);
}
