using ReactNetProyect.BackEnd.Data;
using ReactNetProyect.BackEnd.Data.Models;
using ReactNetProyect.BackEnd.Data.Repositories;
using System.Linq;

namespace ReactNetProyect.BackEnd.Service
{
    public interface IReceiptService 
    {
        IQueryable<Receipt> GetAllReceipts();
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

        public IQueryable<Receipt> GetAllReceipts()
        {

                return  _repository.GetAllReceipts();

        }

        public async Task<Receipt> GetReceiptByIdAsync(int id)
        {

                return await _repository.GetReceiptByIdAsync(id);
        }

        public async Task AddReceiptAsync(Receipt receipt)
        {

                await _repository.AddReceiptAsync(receipt);

        }

        public async Task UpdateReceiptAsync(Receipt receipt)
        {

                await _repository.UpdateReceiptAsync(receipt);

        }

        public async Task DeleteReceiptAsync(int id)
        {
                await _repository.DeleteReceiptAsync(id);
        }
    }
}