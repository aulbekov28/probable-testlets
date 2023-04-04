using TestletRandomizer.Models;

namespace TestletRandomizer.UnitTests;

public class Helpers
{
    internal static List<Item> GetRandomizeInputItems()
    {
        var operationalItems = new List<Item>(Consts.NumOfOperational);
        
        for (var i = 0; i < Consts.NumOfOperational; i++)
        {
            operationalItems.Add(new Item { ItemId = $"operational{i+1}", ItemType = ItemTypeEnum.Operational });
        }
        
        var pretestItems = new List<Item>(Consts.NumOfPretest);

        for (var i = 0; i < Consts.NumOfPretest; i++)
        {
            pretestItems.Add(new Item { ItemId = $"pretest{i+1}", ItemType = ItemTypeEnum.Pretest });
        }
        
        var items = operationalItems.Concat(pretestItems).OrderBy(x => Guid.NewGuid()).ToList();
       
        return items;
    }
}