using Application.Common.Mappings;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Common.Models.SystemConfigurations
{
    public class SystemConfigurationDto : IMapFrom<SystemConfiguration>
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public bool? Enabled { get; set; }
    }
}
