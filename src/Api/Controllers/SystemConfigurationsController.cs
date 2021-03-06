﻿using Application.Queries.SystemConfigurations.GetSystemConfigurations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [Route("[controller]")]
    [Authorize]
    public class SystemConfigurationsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ILogger<SystemConfigurationsController> _logger;
        public SystemConfigurationsController(
            ILogger<SystemConfigurationsController> logger,
            IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<GetSystemConfigurationsQuery.Response> Get()
        {
            return await _mediator.Send(new GetSystemConfigurationsQuery());
        }
    }
}
