using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicDataBusinessLayer.Services.ServiceResults
{
    public enum ServiceResultStatus
    {
        Success = 0,
        NotFound = 1,
        BadData = 2,
        ServerError = 3
    }


}
