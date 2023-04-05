namespace TestletRandomizer.Models;

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