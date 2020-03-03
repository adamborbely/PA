namespace com.codecool.api
{

    public class NormalRefig : Refigrigator
    {
        public NormalRefig(string name)
        {
            Name = name;
            numOfShelfs = 6;
            shelfContainer = new Shelf[numOfShelfs];
            for (int i = 0; i < numOfShelfs; i++)
            {
                shelfContainer[i] = new Shelf(i + 1, 150, Size.Normal);
            }
            fridgeSize = Size.Normal;
        }
        public NormalRefig() { }

        public override Shelf CoolBigItem(Food food)
        {
            for (int i = 1; i < shelfContainer.Length; i++)//start from 1 because if the first fit cannot take the upper one out  
            {
                if (shelfContainer[i] != null && shelfContainer[i - 1] != null && shelfContainer[i].GetFreeSpace() > food.Size && shelfContainer[i - 1].NotContainsBigItem())
                {
                    shelfContainer[i].AddFood(food);
                    return RemoveShelf(i - 1);
                }

            }
            throw new FridgeIsFullException();
        }

        public override bool CoolSmallItem(Food food)
        {
            foreach (var shelf in shelfContainer)
            {
                if (shelf != null)
                {
                    if (shelf.AddFood(food))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
