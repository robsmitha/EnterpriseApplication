using Application.Common.Interfaces;
using Application.Common.Models.SystemConfigurations;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.SystemConfigurations.GetSystemConfigurations
{
    public class GetSystemConfigurationsQuery : IRequest<GetSystemConfigurationsResponse>
    {

        public class GetSystemConfigurationsQueryHandler : IRequestHandler<GetSystemConfigurationsQuery, GetSystemConfigurationsResponse>
        {
            private readonly IApplicationDbContext _context;
            private IMapper _mapper;

            public GetSystemConfigurationsQueryHandler(
                IApplicationDbContext context,
                IMapper mapper
                )
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<GetSystemConfigurationsResponse> Handle(GetSystemConfigurationsQuery request, CancellationToken cancellationToken)
            {
                var configurations = _mapper.Map<List<SystemConfigurationModel>>(await _context.SystemConfigurations.AsNoTracking().ToListAsync());
                return new GetSystemConfigurationsResponse(configurations);
            }
        }
    }
}
