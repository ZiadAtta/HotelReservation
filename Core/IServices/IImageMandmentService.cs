using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Ecom.core.Services
{
    public interface IImageMandmentService
    {
        Task<List<string>> AddImageAsync(IFormFileCollection files, string src);
        void DeleteImageAsync(string src);
    }
}
