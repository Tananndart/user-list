using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UserListTests
{
    [TestClass]
    public class UserListTests
    {
        private const int TEST_CAPACITY = 100;

        private const int TEST_ARRAY_LENGTH = 10;

        private void CheckLengthArray<T>(T[] array)
        {
            if (array.Length != TEST_ARRAY_LENGTH)
                throw new ArgumentException($"Length {nameof(array)} not equal TEST_ARRAY_LENGTH");
        }

        private int[] GetIntArray()
        {
            int[] array =  { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            CheckLengthArray(array);

            return array;
        }

        private string[] GetStringArray()
        {
            string[] array = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            CheckLengthArray(array);

            return array;
        }

        private TestObject[] GetTestObjectArray()
        {
            TestObject[] objects = new TestObject[TEST_ARRAY_LENGTH];

            for (int i = 0; i < TEST_ARRAY_LENGTH; ++i)
                objects[i] = new TestObject(i);

            return objects;
        }

        [TestMethod]
        public void Construct_WithCollection_ShouldEqualsItems()
        {
            GenericTests.Construct_WithCollection_ShouldEqualsItems(GetIntArray());
            GenericTests.Construct_WithCollection_ShouldEqualsItems(GetStringArray());
            GenericTests.Construct_WithCollection_ShouldEqualsItems(GetTestObjectArray());
        }

        [TestMethod]
        public void Construct_WithEnumerable_ShouldValid()
        {
            GenericTests.Construct_WithEnumerable_ShouldValid(GetIntArray());
            GenericTests.Construct_WithEnumerable_ShouldValid(GetStringArray());
            GenericTests.Construct_WithEnumerable_ShouldValid(GetTestObjectArray());
        }

        [TestMethod]
        public void Construct_WithCapacity_ShouldValid()
        {
            GenericTests.Construct_WithCapacity_ShouldValid<int>(TEST_CAPACITY);
            GenericTests.Construct_WithCapacity_ShouldValid<TestObject>(TEST_CAPACITY);
        }

        [TestMethod]
        public void SetCapacity_ShouldSaveItems()
        {
            GenericTests.SetCapacity_ShouldSaveItems(GetIntArray(), TEST_CAPACITY);
            GenericTests.SetCapacity_ShouldSaveItems(GetStringArray(), TEST_CAPACITY);
            GenericTests.SetCapacity_ShouldSaveItems(GetTestObjectArray(), TEST_CAPACITY);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Indexator_ShouldThrowArgumentOutOfRange()
        {
            GenericTests.Indexator_ShouldThrowArgumentOutOfRange(GetIntArray());
        }

        [TestMethod]
        public void Add_ShouldAddItem()
        {
            GenericTests.Add_ShouldAddItem(GetIntArray());
            GenericTests.Add_ShouldAddItem(GetStringArray());
            GenericTests.Add_ShouldAddItem(GetTestObjectArray());
        }

        [TestMethod]
        public void Clear_ShouldRemoveAllItems()
        {
            GenericTests.Clear_ShouldRemoveAllItems(GetIntArray());
            GenericTests.Clear_ShouldRemoveAllItems(GetStringArray());
            GenericTests.Clear_ShouldRemoveAllItems(GetTestObjectArray());
        }

        [TestMethod]
        public void Remove_ByIndex_ShouldRemoveItem()
        {
            int[] indexes =  { 2, 3, 5, 1 };

            GenericTests.Remove_ByIndex_ShouldRemoveItem(GetIntArray(), indexes);
            GenericTests.Remove_ByIndex_ShouldRemoveItem(GetStringArray(), indexes);
            GenericTests.Remove_ByIndex_ShouldRemoveItem(GetTestObjectArray(), indexes);
        }

        [TestMethod]
        public void Remove_ByItem_ShouldRemoveItem()
        {
            int[] intArray = GetIntArray();
            int[] intRemoveItems = { intArray[3], intArray[6] };

            string[] strArray = GetStringArray();
            string[] strRemoveItems = { strArray[2], strArray[5] };

            TestObject[] objArray = GetTestObjectArray();
            TestObject[] objRemoveItems = { objArray[0], objArray[4] };

            GenericTests.Remove_ByItem_ShouldRemoveItem(intArray, intRemoveItems);
            GenericTests.Remove_ByItem_ShouldRemoveItem(strArray, strRemoveItems);
            GenericTests.Remove_ByItem_ShouldRemoveItem(objArray, objRemoveItems);
        }

        [TestMethod]
        public void Insert_ShouldInsertItem()
        {
            int insertIndex = 3;

            int[] intItems = { 123, 456, 768 };
            string[] strItems = { "item_1", "item_2" };
            TestObject[] objItems = { new TestObject(100), new TestObject(200) };

            GenericTests.Insert_ShouldInsertItem(GetIntArray(), intItems, insertIndex);
            GenericTests.Insert_ShouldInsertItem(GetStringArray(), strItems, insertIndex);
            GenericTests.Insert_ShouldInsertItem(GetTestObjectArray(), objItems, insertIndex);
        }
    }
}
