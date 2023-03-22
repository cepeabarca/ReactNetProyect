using ReactNetProyect.BackEnd.Data.Models;
using ReactNetProyect.BackEnd.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactNetProyect.BackEnd.Service
{
    public interface ICurrencyService
    {
        Task<IEnumerable<Currency>> GetAllCurrenciesAsync();
        Task<Currency> GetCurrencyByIdAsync(int id);
    }

    public class CurrencyService : ICurrencyService
    {
        private readonly CurrencyRepository _repository;

        public CurrencyService(CurrencyRepository currencyRepo)
        {
            _repository = currencyRepo;
        }

        public async Task<IEnumerable<Currency>> GetAllCurrenciesAsync()
        {
            using (_repository)
            {
                return await _repository.GetAllCurrenciesAsync();
            }
        }

        public async Task<Currency> GetCurrencyByIdAsync(int id)
        {
            using (_repository)
            {
                return await _repository.GetCurrencyByIdAsync(id);
            }
        }
    }
}
