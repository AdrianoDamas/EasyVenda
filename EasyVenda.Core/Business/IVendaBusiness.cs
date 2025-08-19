using _EasyVenda.Core.Dtos;

namespace _EasyVenda.Core.Business
{
    public interface IVendaBusiness
    {
        Task<VendaDto> CriarVendaAsync(VendaDto vendaDto);
        Task<VendaDto?> GetByIdAsync(int id);
        Task AtualizarVendaAsync(VendaDto vendaDto);
        Task CancelarVendaAsync(int id);
        Task CancelarItemAsync(int vendaId, int itemId);
        Task<List<VendaDto>> ObterTodasVendasAsync();
    }

}
