using Application.Common.Interfaces;
using Application.Common.Mappings;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Queries.SystemConfigurations.GetSystemConfigurations
{
    public class GetSystemConfigurationsQuery : IRequest<GetSystemConfigurationsQuery.Response>
    {
        public class Handler : IRequestHandler<GetSystemConfigurationsQuery, Response>
        {
            private readonly IApplicationDbContext _context;
            private IMapper _mapper;

            public Handler(
                IApplicationDbContext context,
                IMapper mapper
                )
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<Response> Handle(GetSystemConfigurationsQuery request, CancellationToken cancellationToken)
            {
                var configurations = _mapper.Map<List<SystemConfigurationDto>>(await _context.SystemConfigurations.AsNoTracking().ToListAsync());
                return new Response(configurations);
            }
        }

        public class Response
        {
            public List<SystemConfigurationDto> Configurations { get; set; }
            public Response(List<SystemConfigurationDto> configurations)
            {
                Configurations = configurations;
            }
        }

        public class SystemConfigurationDto : IMapFrom<SystemConfiguration>
        {
            public string Name { get; set; }
            public string Value { get; set; }
            public bool? Enabled { get; set; }
        }
    }
}
