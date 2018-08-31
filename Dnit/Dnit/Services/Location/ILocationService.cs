using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Dnit.Core.Services.Location
{
    public interface ILocationService
    {
        Task UpdateUserLocation(Dnit.Core.Models.Location.Location newLocReq, string token);
    }
}
