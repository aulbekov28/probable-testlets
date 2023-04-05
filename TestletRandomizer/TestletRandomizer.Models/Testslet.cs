namespace TestletRandomizer.Models;

public class Testlet
{
    public readonly string TestletId;
    private readonly List<Item> Items;
    
    public Testlet(string testletId, List<Item> items)
    {
        ValidateItems(items);
        TestletId = testletId;
        Items = items;
    }
    
    // Items private collection has 6 Operational and 4 Pretest Items. Randomize the order of these items as per the requirement (with TDD)
    // The assignment will be reviewed on the basis of – Tests written first, Correct logic, Well structured & clean readable code.
    // If taken into account that number of tests is not big then we could ignore the performance side of the problem for now
    // In this case O(n) time complexity and O(n) space complexity 
    public List<Item> Randomize()
    {
        var pretestItems = Items.Where(i => i.ItemType == ItemTypeEnum.Pretest).ToList();
        var operationalItems = Items.Where(i => i.ItemType == ItemTypeEnum.Operational).ToList();
        var randomizedItems = new List<Item>(Constants.NumOfTest);
        
        var random = new Random();
        
        // Randomly choose 2 items from pretests by random index
        for (var i = 0; i < Constants.NumOfPretestsAtTheBeginning; i++)
        {
            var index = random.Next(pretestItems.Count);
            randomizedItems.Add(pretestItems[index]);
            pretestItems.RemoveAt(index);
        }
        
        for (var i = 0; i < Constants.NumOfTest - Constants.NumOfPretestsAtTheBeginning; i++)
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

    private static void ValidateItems(List<Item> items)
    {
        if (items.Count != Constants.NumOfTest)
        {
            throw new Exception("Incorrect length");
        }

        if (Constants.NumOfPretest != items.Count(x => x.ItemType == ItemTypeEnum.Pretest))
        {
            throw new Exception("Incorrect number of items types");
        } 
    }
}