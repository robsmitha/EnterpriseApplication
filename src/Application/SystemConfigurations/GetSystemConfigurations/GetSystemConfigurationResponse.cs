using Application.Common.Models.SystemConfigurations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.SystemConfigurations.GetSystemConfigurations
{
    public class GetSystemConfigurationsResponse
    {
        public List<SystemConfigurationModel> Configurations { get; set; }
        public GetSystemConfigurationsResponse(List<SystemConfigurationModel> configurations)
        {
            Configurations = configurations;
        }
    }
}
