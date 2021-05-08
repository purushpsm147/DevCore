using SGRE.TSA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRE.TSA.ExternalServices
{
    public interface ITestExternalService
    {
        Task<ExternalServiceResponse<IEnumerable<SegmentDimensionSummary>>> GetByIdAsync(int id);
    }
}
