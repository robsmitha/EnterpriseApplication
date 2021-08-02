using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UnitType : TypeEntity
    {
        /// <summary>
        /// Abbreviation (i.e. 4 each)
        /// </summary>
        public string PerUnit { get; set; }
    }
}
