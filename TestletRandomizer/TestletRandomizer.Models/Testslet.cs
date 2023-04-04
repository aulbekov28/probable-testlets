namespace TestletRandomizer.Models;

public class Testlet
{
    public readonly string TestletId;
    private readonly List<Item> Items;
    public Testlet(string testletId, List<Item> items)
    {
        TestletId = testletId;
        Items = items;
    }
    
    // Items private collection has 6 Operational and 4 Pretest Items. Randomize the order of these items as per the requirement (with TDD)
    // The assignment will be reviewed on the basis of – Tests written first, Correct logic, Well structured & clean readable code.
    public List<Item> Randomize()
    {
        //If taken into account that number of tests is not big then we could ignore the performance side of the problem for now
        //In this case O(n) time complexity and O(n) space complexity 
        
        var pretestItems = Items.Where(i => i.ItemType == ItemTypeEnum.Pretest).ToList();
        var operationalItems = Items.Where(i => i.ItemType == ItemTypeEnum.Operational).ToList();
        var randomizedItems = new List<Item>(Consts.NumOfTest);
        
        var random = new Random();
        
        // Randomly choose from pretests by index
        for (var i = 0; i < Consts.NumOfPretestsAtTheBeginning; i++)
        {
            var index = random.Next(pretestItems.Count);
            randomizedItems.Add(pretestItems[index]);
            pretestItems.RemoveAt(index);
        }
        
        for (var i = 0; i < Consts.NumOfTest - Consts.NumOfPretestsAtTheBeginning; i++)
        {
            var index = random.Next(operationalItems.Count + pretestItems.Count);
            if (index < operationalItems.Count)
            {
                randomizedItems.Add(operationalItems[index]);
                operationalItems.RemoveAt(index); // could try to swap with the last element to avoid resizing
            }
            else
            {
                randomizedItems.Add(pretestItems[index - operationalItems.Count]);
                pretestItems.RemoveAt(index - operationalItems.Count);
            }
        }
        
        return randomizedItems;
    }
}

public class Item
{
    public string ItemId;
    public ItemTypeEnum ItemType;

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType()) {
            return false;
        }

        var other = (Item)obj;
        return ItemId == other.ItemId;
    }

    public override int GetHashCode()
    {
        return ItemId.GetHashCode();
    }
}

public enum ItemTypeEnum
{
    Pretest = 0,
    Operational = 1
}

public static class Consts
{
    public const int NumOfTest = 10;
    public const int NumOfPretest = 4;
    public const int NumOfPretestsAtTheBeginning = 2;
    public const int NumOfOperational = NumOfTest - NumOfPretest;
} 