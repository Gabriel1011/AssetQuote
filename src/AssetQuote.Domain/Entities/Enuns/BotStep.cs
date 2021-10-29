using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetQuote.Domain.Entities.Enuns
{
    public enum BotStep
    {
        Default = -1,
        Start = 0,
        NewAsset = 1,
        ConsultAsset = 2,
        RemoveAsset = 3,
        CreantingAsset = 11,
        ConsultingAsset = 22,
        RemovingAsset = 33,
        ConfirmNewAsset = 111,
        ConfirmRemoveAsset = 333
    }
}
