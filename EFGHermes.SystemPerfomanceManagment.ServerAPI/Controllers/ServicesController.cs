﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EFGHermes.SystemPerfomanceManagment.ServerAPI.Models;
using Microsoft.AspNetCore.SignalR;
using EFGHermes.SystemPerfomanceManagment.ServerAPI.Hubs;

namespace EFGHermes.SystemPerfomanceManagment.ServerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        private readonly ServerContext _context;
        private IHubContext<UIHub> _hub;

        public ServicesController(ServerContext context, IHubContext<UIHub> hub)
        {
            _context = context;
            _hub = hub;
        }
        // Example
        public IActionResult Get()
        {
           

            return Ok(new { Message = "Request Completed" });
        }
        // GET: api/Services
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServiceDTO>>> GetServices()
        {
            var result = await _context.Services
                .Include(s => s.OutgoingServices)
                .Include(s => s.IngoingServices)
                .ToArrayAsync();

            var result2 = result
                .Select(s => new ServiceDTO
                {
                    Id = s.Id,
                    IP = s.IP,
                    Port = s.Port,
                    DBConnectionString = s.DBConnectionString,
                    ServiceStatus = s.ServiceStatus,
                    DisplayName = s.DisplayName,
                    IngoingServicesIds = s.IngoingServicesIds,
                    OutgoingServicesIds = s.OutgoingServicesIds
                }).ToArray();
                

            return result2;
        }

        // GET: api/Services/5
        [HttpGet("{name}")]
        public async Task<ActionResult<Service>> GetService(string name)
        {
            var service = await _context.Services.FindAsync(name);

            if (service == null)
            {
                return NotFound();
            }
            await _hub.Clients.All.SendAsync("getServices");
            return service;
        }


        private bool ServiceExists(string name)
        {
            return _context.Services.Any(e => e.DisplayName == name);
        }
    }
}
