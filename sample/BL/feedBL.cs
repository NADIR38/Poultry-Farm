using System;
using System.Collections.Generic;
using Poultary.Interfaces;
using Poultary.BL.Models;
using Poultary.DL;

namespace Poultary.BL.Bl
{
    public class feedBL : feedinterface
    {
        public bool addfeed(feed c)
        {
            if (!IsValidFeed(c, out string error))
            {
                throw new ArgumentException($"Invalid feed data: {error}");
            }
            return feedDl.addfeed(c);
        }

        public bool deletefeed(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Invalid feed batch ID.");
            }
            return feedDl.deletefeed(id);
        }

        public bool updatefeed(feed c)
        {
            if (c.id <= 0)
            {
                throw new ArgumentException("Feed batch ID must be a positive number.");
            }

            if (!IsValidFeed(c, out string error))
            {
                throw new ArgumentException($"Invalid feed data: {error}");
            }

            return feedDl.updatefeed(c);
        }

        public List<feed> getfeed()
        {
            return feedDl.getfeed();
        }

        private bool IsValidFeed(feed c, out string error)
        {
            error = string.Empty;

            if (string.IsNullOrWhiteSpace(c.name))
            {
                error = "Batch name is required.";
                return false;
            }

            

            if (c.price <= 0)
            {
                error = "Price must be a positive number.";
                return false;
            }

            if (c.weight <= 0)
            {
                error = "Weight must be a positive number.";
                return false;
            }

            if (c.quantity <= 0)
            {
                error = "Quantity (in sacks) must be greater than 0.";
                return false;
            }

            if (string.IsNullOrWhiteSpace(c.suppliername))
            {
                error = "Supplier name is required.";
                return false;
            }

            return true;
        }
    }
}
