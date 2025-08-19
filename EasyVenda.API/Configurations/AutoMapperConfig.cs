using _EasyVenda.Core.Dtos;
using _EasyVenda.Core.Entities;

namespace _EasyVenda.API.Configurations
{
    public class AutoMapperConfig : AutoMapper.Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Venda, VendaDto>().ReverseMap();
            CreateMap<ItemVenda, ItemVendaDto>().ReverseMap();
        }
    }
}
