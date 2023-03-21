using AutoMapper;
using ReactNetProyect.BackEnd.Data.Models;
using ReactNetProyect.Shared.DTO;

namespace ReactNetProyect.BackEnd.API.Utils
{
    public class MappingProfile: Profile
    {
        public MappingProfile() 
        {
            CreateMap<Receipt, ReceiptDTO>().ReverseMap();
            CreateMap<CreateReceiptDTO, Receipt>().ReverseMap();
        }
    }
}
