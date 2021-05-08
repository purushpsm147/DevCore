using SGRE.TSA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public interface ISstDesignRequestServices
    {
        Task<(bool IsSuccess, IEnumerable<SstDesignRequest> SstDesignRequest)> GetSstDesignRequestByIdAsync(int SstId);
        Task<(bool IsSuccess, dynamic SstDesignRequest)> PutSstDesignRequestAsync(SstDesignRequest sstDesignRequest);
    }
}
