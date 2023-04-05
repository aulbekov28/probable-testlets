using TestletRandomizer.Models;

namespace TestletRandomizer.UnitTests;

public class TestsletTests
{
    [Fact]
    public void Randomize_Should_Return_Same_Input_Length()
    {
        // Arrange
        var testletId = "testlet1";
        var items = Helpers.GetRandomizeInputItems();
        var testlet = new Testlet(testletId, items);
        
        // Act
        var randomizedItems = testlet.Randomize();
        
        // Assert
        Assert.Equal(items.Count, randomizedItems.Count);
    }
    
    [Fact]
    public void Randomize_Should_Return_2_Pretests_At_The_Beginning()
    {
        // Arrange
        var testletId = "testlet1";
        var testlet = new Testlet(testletId, Helpers.GetRandomizeInputItems());
        
        // Act
        var randomizedItems = testlet.Randomize();
        
        // Assert
        Assert.Equal(Constants.NumOfPretestsAtTheBeginning, randomizedItems.Take(2).Count(x => x.ItemType == ItemTypeEnum.Pretest));
    }
    
    [Fact]
    public void Randomize_Should_Not_Return_Any_Duplicates()
    {
        // Arrange
        var testletId = "testlet1";
        var testlet = new Testlet(testletId, Helpers.GetRandomizeInputItems());
        
        // Act
        var randomizedItems = testlet.Randomize();
        
        // Assert
        Assert.Equal(randomizedItems.Count, randomizedItems.Distinct().Count());

    }
    
    [Fact]
    public void Randomize_Should_Not_Modify_Input()
    {
        // Arrange
        var testletId = "testlet1";
        var items = Helpers.GetRandomizeInputItems();
        var originalItems = new List<Item>(items);
        
        var testlet = new Testlet(testletId, items);
        
        // Act
        _ = testlet.Randomize();
        
        // Assert
        Assert.Equal(originalItems, items);
    }
    
    [Fact]
    public void Randomize_Should_Return_Mix_Of_Tests()
    {
        // Arrange
        var testletId = "testlet1";
        var testlet = new Testlet(testletId, Helpers.GetRandomizeInputItems());
        
        // Act
        var randomizedItems = testlet.Randomize();
        
        
        var pretestCount = 0;
        var operationalCount = 0;
        
        // Assert
        foreach (var item in randomizedItems.Skip(2))
        {
            if (item.ItemType == ItemTypeEnum.Pretest)
            {
                pretestCount++;
            }
            else if (item.ItemType == ItemTypeEnum.Operational)
            {
                operationalCount++;
            }
        }
        

        Assert.Equal(Constants.NumOfPretest - Constants.NumOfPretestsAtTheBeginning, pretestCount);
        Assert.Equal(Constants.NumOfOperational, operationalCount); 
    }
    
    [Fact]
    public void Randomize_Should_Randomize_Data()
    {
        // Arrange
        var testletId = "testlet1";
        var testlet = new Testlet(testletId, Helpers.GetRandomizeInputItems());

        // Act
        var previousOrder = testlet.Randomize();
        for (var i = 0; i < 10; i++)
        {
            var randomizedOrder = testlet.Randomize();
            Assert.NotEqual(previousOrder, randomizedOrder); // review 
            previousOrder = randomizedOrder;
        }
    }
    
    [Fact]
    public void CreateTestlet_Should_Not_Throw_Exception()
    {
        // Arrange
        var testletId = "testlet1";
        var items = Helpers.GetRandomizeInputItems();

        // Act
        var exception = Record.Exception(() => new Testlet(testletId, items));
        
        // Assert
        Assert.Null(exception);
    }
    
        
    [Fact]
    public void CreateTestlet_When_Incorrect_Input_Lenght_Should_Throw_Exception()
    {
        // Arrange
        var testletId = "testlet1";
        var items = Helpers.GetRandomizeInputItems();
        items.Add(new Item());
        
        // Act
        var exception = Record.Exception(() => new Testlet(testletId, items));
        
        // Assert
        Assert.NotNull(exception);
        Assert.Contains("Incorrect length", exception.Message);
    }
    
    [Fact]
    public void CreateTestlet_When_Incorrect_Input_Types_Should_Throw_Exception()
    {
        // Arrange
        var testletId = "testlet1";
        var items = Helpers.GetRandomizeInputItems();
        foreach (var item in items)
        {
            item.ItemType = ItemTypeEnum.Pretest;
        }
        
        // Act
        var exception = Record.Exception(() => new Testlet(testletId, items));
        
        // Assert
        Assert.NotNull(exception);
        Assert.Contains("Incorrect number of items types", exception.Message);
    }
}