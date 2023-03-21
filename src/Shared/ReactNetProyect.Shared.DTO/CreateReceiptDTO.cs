using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReactNetProyect.Shared.DTO
{
    public class CreateReceiptDTO
    {

        public string Provider { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public string? Comment { get; set; }

        public int CurrencyId { get; set; }
    }
}
