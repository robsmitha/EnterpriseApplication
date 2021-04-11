using Application.Common.Models.SystemConfigurations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.SystemConfigurations.GetSystemConfigurations
{
    public class GetSystemConfigurationsResponse
    {
        public List<SystemConfigurationDto> Configurations { get; set; }
        public GetSystemConfigurationsResponse(List<SystemConfigurationDto> configurations)
        {
            Configurations = configurations;
        }
    }
}
