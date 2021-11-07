namespace AssetQuote.Domain.Entities.Enuns
{
    public enum BotStep
    {
        Default = -1,
        Start = 999,
        NewAsset = 1,
        ConsultAsset = 2,
        RemoveAsset = 3,

        CreantingAsset,
        ConsultingAsset,
        RemovingAsset,
        ConfirmRemoveAsset
    }
}
