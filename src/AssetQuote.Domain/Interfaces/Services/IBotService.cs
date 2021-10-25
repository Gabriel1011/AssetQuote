using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetQuote.Domain.Interfaces.Services
{
    public interface IBotService
    {
        public Task<String> StartContact();
    }
}
