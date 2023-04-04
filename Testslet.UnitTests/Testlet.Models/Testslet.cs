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
    
    //Items private collection has 6 Operational and 4 Pretest Items. Randomize the order of these items as per the requirement (with TDD)
    //The assignment will be reviewed on the basis of – Tests written first, Correct logic, Well structured & clean readable code.
    public List<Item> Randomize()
    {
        throw new NotImplementedException();
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