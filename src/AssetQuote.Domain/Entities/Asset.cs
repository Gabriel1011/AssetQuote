namespace AssetQuote.Domain.Entities
{
  public class Asset
    {
      public Asset( string name)
      {
        Name = name;
      }

      public int Id { get; private set; }
      public string Name { get; private set; }
    }
}
