﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CRMSystem.Domains
{
    public interface ILeadService
    {
        Task<int> SaveLeadAsync(Lead data);


        Task<int> UpdateLeadAsync(Lead data);
        Task<Lead> getLeadByID(int ID);
        Task<List<Lead>> getAllLeads();
    }
}
