using BlockchainApp.Models;

namespace BlockchainApp.Services.MiningServ
{
    public interface IMiningService
    {
        Task<Block> MineBlockAsync();
    }
}
