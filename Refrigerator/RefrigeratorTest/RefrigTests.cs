using NUnit.Framework;
using com.codecool.api;
using System.Collections.Generic;

namespace RefrigeratorTest
{
    public class RefrigeratorTests
    {
        Refigrigator refrig;
        Refigrigator normalRefrig;
        [SetUp]
        public void Setup()
        {
            this.refrig = new MiniRefig("TestRefig");
            refrig.Open();
            normalRefrig = new NormalRefig("TestNormal");
            normalRefrig.Open();
        }

        [Test]
        public void RemoveShelfTest_RemoveZeroIndex_ZeroIsNull()
        {
            refrig.RemoveShelf(0);

            Assert.IsNull(refrig.shelfContainer[0]);
        }

        [Test]
        public void RemoveShelfTest_DoorIsClosed_ThrowExeption()
        {
            refrig.Close();

            Assert.Throws<FridgeIsClosedException>(() => refrig.RemoveShelf(0));
        }

        [Test]
        public void GetFoodFromFridge_TakeAFoodToFridge_GetItBack()
        {
            var foodToTest = new Food(120, 30, "tesztalma");
            refrig.AddFood(foodToTest);

            var result = refrig.GetFoodFromFridge("tesztalma");

            Assert.IsNotNull(result);
            Assert.AreSame(foodToTest, result);
        }

        [Test]
        public void GetFreeSpaceTest_BasicMiniFridge_FreeSpaceEqualMaxFreeSpace()
        {
            var result = refrig.GetFreeSpace();

            Assert.AreEqual(300, result);
        }

        [Test]
        public void AddFoodTest_BigItemToNormalFridge_RemovedShelfNotNull()
        {
            var bigFood = new Food(120, 90, "tesztsor");
            normalRefrig.AddFood(bigFood);

            Assert.IsNotNull(normalRefrig.RemovedShelf);
            Assert.AreEqual(1, normalRefrig.RemovedShelf.ID);
        }

        [Test]
        public void FindEmptySlotsTest_RemoveTwoShelfs_GetBackTheNumbers()
        {
            normalRefrig.RemoveShelf(1);
            normalRefrig.RemoveShelf(3);

            var emptyShelfList = normalRefrig.FindEmptySlots();

            CollectionAssert.AreEqual(new List<int> { 1, 3 }, emptyShelfList);
        }

        [Test]
        public void CheckMaxShelfCapacityTest_MiniAndNormalFridge_Result100And150()
        {
            var miniCap = refrig.CheckMaxShelfCapacity();
            var normalCap = normalRefrig.CheckMaxShelfCapacity();

            Assert.AreEqual(100, miniCap);
            Assert.AreEqual(150, normalCap);
        }

        [Test]
        public void ListFoodTest_AddFoodToTheFridge_GetBackTheList()
        {
            var foodToTest = new Food(120, 30, "tesztalma");
            var foodToTest2 = new Food(120, 30, "tesztalma2");
            var foodToTest3 = new Food(120, 30, "tesztalma3");
            normalRefrig.AddFood(foodToTest);
            normalRefrig.AddFood(foodToTest2);
            normalRefrig.AddFood(foodToTest3);

            var result = normalRefrig.ListFood();

            CollectionAssert.AreEqual(new List<Food> { foodToTest, foodToTest2, foodToTest3 }, result);
        }
    }
}