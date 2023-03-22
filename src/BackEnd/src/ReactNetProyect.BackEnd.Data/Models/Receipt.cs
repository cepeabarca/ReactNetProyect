using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ReactNetProyect.BackEnd.Data.Models
{
    public class Receipt
    {
        public int Id { get; set; }
        public string Provider { get; set; }
        public decimal Amount { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public string? Comment { get; set; }

        public int CurrencyId { get; set; }

        [JsonIgnore]
        public Currency Currency { get; set; }
    }
}
