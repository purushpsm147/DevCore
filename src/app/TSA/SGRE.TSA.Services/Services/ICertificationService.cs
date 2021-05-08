using SGRE.TSA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public interface ICertificationService
    {
        Task<(bool IsSuccess, IEnumerable<Certification> certifications)> GetCertificationAsync();
        Task<(bool IsSuccess, IEnumerable<Certification> certifications)> GetCertificationAsync(int id);
    }
}
