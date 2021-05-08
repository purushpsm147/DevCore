using SGRE.TSA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGRE.TSA.Services.Services
{
    public interface ICurrencyService
    {
        Task<(bool IsSuccess, IEnumerable<Currency> currencyResult)> GetCurrencyAsync();
        Task<(bool IsSuccess, IEnumerable<Currency> currencyResult)> GetCurrencyAsync(int id);
    }
}
