﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Application.Common.Interfaces
{
    public interface ICurrentUserService
    {
        int UserId { get; }
    }
}
