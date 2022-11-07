using Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApiControllers.Services
{
    internal class CurrentUserService : ICurrentUser
    {
        public string Id => "Anonomous";
    }
}
