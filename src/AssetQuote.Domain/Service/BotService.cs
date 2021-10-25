using AssetQuote.Domain.Interfaces.Repositories;
using AssetQuote.Domain.Interfaces.Services;
using System;
using System.Threading.Tasks;

namespace AssetQuote.Domain.Service
{
    public class BotService : IBotService
    {
        private readonly IAssetRepository _assetRepository;

        public BotService(IAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
        }

        public async Task<String> StartContact() => await Task.Run(() =>  
        "Olá, você Gostaria de fazer o que? \n 1 - Cadastrar novo ativo \n 2 - Consultar ativos cadastrados \n 3 - Deletar ativo cadastrado" );

        public async Task<string> Start(string texts) => texts switch { 
            _=> await StartContact() 
        }; 
    }
}
