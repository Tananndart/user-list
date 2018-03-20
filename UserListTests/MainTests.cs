using NUnit.Framework;
using System.Collections.Generic;

namespace UserListTests
{
    [TestFixture]
    public class UserListTests
    {
        private static TestObject[] GetTestObjectArray(int[] array)
        {
            TestObject[] objects = new TestObject[array.Length];

            for (int i = 0; i < array.Length; ++i)
                objects[i] = new TestObject(array[i]);

            return objects;
        }

        public static IEnumerable<IGenericTestCase> TestCases()
        {
            yield return new GenericTestCase<int>(
                array : new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 },
                insertItems : new int[] { 10, 100, 1000 }, 
                removeItems : new int[] { 4, 2, 3 },
                removeIndexes : new int[] { 1, 2, 3 });

            yield return new GenericTestCase<string>(
                array : new string[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" },
                insertItems : new string[] { "ten", "eleven" }, 
                removeItems : new string[] { "two", "four", "six" },
                removeIndexes: new int[] { 1, 2, 3 });

            yield return new GenericTestCase<TestObject>(
                array: GetTestObjectArray(new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 }),
                insertItems: GetTestObjectArray(new int[] { 10, 100, 1000 }),
                removeItems: GetTestObjectArray(new int[] { 4, 2, 3 }),
                removeIndexes: new int[] { 1, 2, 3 });
        }

        [Test]
        [TestCaseSource("TestCases")]
        public void Construct_WithCollection_ShouldEqualsItems(IGenericTestCase testCase)
        {
            testCase.Construct_WithCollection_ShouldEqualsItems();
        }

        [Test]
        [TestCaseSource("TestCases")]
        public void Construct_WithEnumerable_ShouldValid(IGenericTestCase testCase)
        {
            testCase.Construct_WithEnumerable_ShouldValid();
        }

        [Test]
        [TestCaseSource("TestCases")]
        public void Construct_WithCapacity_ShouldValid(IGenericTestCase testCase)
        {
            testCase.Construct_WithCapacity_ShouldValid();
        }

        [Test]
        [TestCaseSource("TestCases")]
        public void SetCapacity_ShouldSaveItems(IGenericTestCase testCase)
        {
            testCase.SetCapacity_ShouldSaveItems();
        }

        [Test]
        [TestCaseSource("TestCases")]
        public void Add_ShouldAddItem(IGenericTestCase testCase)
        {
            testCase.Add_ShouldAddItem();
        }

        [Test]
        [TestCaseSource("TestCases")]
        public void Clear_ShouldRemoveAllItems(IGenericTestCase testCase)
        {
            testCase.Clear_ShouldRemoveAllItems();
        }

        [Test]
        [TestCaseSource("TestCases")]
        public void Remove_ByIndex_ShouldRemoveItem(IGenericTestCase testCase)
        {
            testCase.Remove_ByIndex_ShouldRemoveItem();
        }

        [Test]
        [TestCaseSource("TestCases")]
        public void Remove_ByItem_ShouldRemoveItem(IGenericTestCase testCase)
        {
            testCase.Remove_ByItem_ShouldRemoveItem();
        }

        [Test]
        [TestCaseSource("TestCases")]
        public void Insert_ShouldInsertItem(IGenericTestCase testCase)
        {
            testCase.Insert_ShouldInsertItem();
        }
    }
}
