using ReactNetProyect.BackEnd.Data;
using ReactNetProyect.BackEnd.Data.Models;
using ReactNetProyect.BackEnd.Data.Repositories;

namespace ReactNetProyect.BackEnd.Service
{
    public interface IReceiptService 
    {
        Task<IEnumerable<Receipt>> GetAllReceiptsAsync();
        Task<Receipt> GetReceiptByIdAsync(int id);
        Task AddReceiptAsync(Receipt receipt);
        Task UpdateReceiptAsync(Receipt receipt);
        Task DeleteReceiptAsync(int id);
    }

    public class ReceiptService : IReceiptService
    {
        private readonly ReceiptRepository _repository;

        public ReceiptService(ReceiptRepository receiptRepo)
        {
            _repository = receiptRepo;
        }

        public async Task<IEnumerable<Receipt>> GetAllReceiptsAsync()
        {
            using (_repository)
            {
                return await _repository.GetAllReceiptsAsync();
            }
        }

        public async Task<Receipt> GetReceiptByIdAsync(int id)
        {
            using (_repository)
            {
                return await _repository.GetReceiptByIdAsync(id);
            }
        }

        public async Task AddReceiptAsync(Receipt receipt)
        {
            using (_repository)
            {
                await _repository.AddReceiptAsync(receipt);
            }
        }

        public async Task UpdateReceiptAsync(Receipt receipt)
        {
            using (_repository)
            {
                await _repository.UpdateReceiptAsync(receipt);
            }
        }

        public async Task DeleteReceiptAsync(int id)
        {
            using (_repository)
            {
                await _repository.DeleteReceiptAsync(id);
            }
        }
    }
}