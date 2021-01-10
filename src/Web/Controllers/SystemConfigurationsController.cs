using System.Threading.Tasks;
using Application.Common.Interfaces;
using Application.SystemConfigurations.GetSystemConfigurations;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Common;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemConfigurationsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAppUserService _user;
        private readonly ILogger<SystemConfigurationsController> _logger;

        public SystemConfigurationsController(IMediator mediator, IAppUserService user, ILogger<SystemConfigurationsController> logger)
        {
            _mediator = mediator;
            _user = user;
            _logger = logger;
        }
        [HttpGet]
        public async Task<GetSystemConfigurationsResponse> Get()
        {
            return await _mediator.Send(new GetSystemConfigurationsQuery());
        }
    }
}