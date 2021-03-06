﻿using EFGHermes.SystemPerfomanceManagment.ServerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EFGHermes.SystemPerfomanceManagment.ServerAPI.Interfaces
{
    interface IAgent
    {
        Task<List<Service>> GetServicesAsync();
        Task<Service> GetServiceById(string name);
        void StartService(string name);
        void StopService(string name);
    }
}
