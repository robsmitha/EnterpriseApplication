using Application.SystemConfigurations.GetSystemConfigurations;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SystemConfigurationsController : ControllerBase
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
        public async Task<GetSystemConfigurationsResponse> Get()
        {
            return await _mediator.Send(new GetSystemConfigurationsQuery());
        }
    }
}
