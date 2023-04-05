using TestletRandomizer.Models;

namespace TestletRandomizer.UnitTests;

public class Helpers
{
    // Could use autofixture
    internal static List<Item> GetRandomizeInputItems()
    {
        var operationalItems = new List<Item>(Constants.NumOfOperational);
        
        for (var i = 0; i < Constants.NumOfOperational; i++)
        {
            operationalItems.Add(new Item { ItemId = $"operational{i+1}", ItemType = ItemTypeEnum.Operational });
        }
        
        var pretestItems = new List<Item>(Constants.NumOfPretest);

        for (var i = 0; i < Constants.NumOfPretest; i++)
        {
            pretestItems.Add(new Item { ItemId = $"pretest{i+1}", ItemType = ItemTypeEnum.Pretest });
        }
        
        var items = operationalItems.Concat(pretestItems).OrderBy(x => Guid.NewGuid()).ToList();
       
        return items;
    }
}