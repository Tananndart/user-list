using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserList;


/*
 * + 1. Tests from UserList<int>
 * 2. Tests from UserList<MyClass>
 * 3. Refact tests for all this
 */

namespace UserListTests
{
    [TestClass]
    public class UserListTests
    {
        [TestMethod]
        public void Construct_WithCollection_ShouldEqualsItems()
        {
            int[] array = { 0, 1, 2, 3, 4, 5 };

            UserList<int> list = new UserList<int>(array); 

            Assert.AreEqual(list.Count, array.Length);
            for (int i = 0; i < list.Count; ++i)
                Assert.AreEqual(list[i], array[i]);
        }

        [TestMethod]
        public void Construct_WithEnumerable_ShouldValid()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void Construct_WithCapacity_ShouldValid()
        {
            const int CAPACITY = 500;

            UserList<int> list = new UserList<int>(CAPACITY);

            Assert.AreEqual(list.Capacity, CAPACITY);
        }

        [TestMethod]
        public void SetCapacity_ShouldSaveItems()
        {
            const int CAPACITY = 500;
            int[] array = { 0, 1, 2, 3, 4, 5 };

            UserList<int> list = new UserList<int>(array)
            {
                Capacity = CAPACITY
            };

            Assert.AreEqual(list.Count, array.Length);
            Assert.AreEqual(list.Capacity, CAPACITY);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Indexator_ShouldThrowArgumentOutOfRange()
        {
            UserList<int> list = new UserList<int> { 0, 1, 2, 3, 4, 5 };

            list[list.Count] = list.Count;
        }

        [TestMethod]
        public void Add_ShouldAddItem()
        {
            const int N = 10;
            
            UserList<int> list = new UserList<int>();
            for (int i = 0; i < N; ++i)
                list.Add(i);

            Assert.AreEqual(list.Count, N);
            for (int i = 0; i < N; ++i)
                Assert.AreEqual(list[i], i);
        }

        [TestMethod]
        public void Clear_ShouldRemoveAllItems()
        {
            UserList<int> list = new UserList<int> { 0, 1, 2, 3, 4, 5 };

            list.Clear();

            Assert.AreEqual(list.Count, 0);
        }

        [TestMethod]
        public void Remove_ByIndex_ShouldRemoveItem()
        {
            UserList<int> list = new UserList<int> { 0, 1, 2, 3, 4, 5 };
            int origin_list_count = list.Count;

            const int N = 4;
            for (int i = 0; i < N; ++i)
                list.RemoveAt(2);

            Assert.AreEqual(list.Count, origin_list_count - N);
            Assert.AreEqual(list[list.Count - 1], 1);
        }

        [TestMethod]
        public void Remove_ByItem_ShouldRemoveItem()
        {
            UserList<int> list = new UserList<int> { 0, 1, 2, 3, 4, 5 };
            int origin_list_count = list.Count;

            var (item_1, item_2) = (3, 5);
            Assert.IsTrue(list.Remove(item_1));
            Assert.IsTrue(list.Remove(item_2));

            Assert.AreEqual(list.Count, origin_list_count - 2);
            Assert.IsFalse(list.Contains(item_1));
            Assert.IsFalse(list.Contains(item_2));
        }

        [TestMethod]
        public void Insert_ShouldInserItem()
        {
            UserList<int> list = new UserList<int> { 0, 1, 2, 3, 4, 5 };
            int origin_list_count = list.Count;

            var (item_1, item_2) = (23, 45);
            list.Insert(2, item_1);
            list.Insert(4, item_2);

            Assert.AreEqual(list.Count, origin_list_count + 2);
            Assert.IsTrue(list.Contains(item_1));
            Assert.IsTrue(list.Contains(item_2));
        }
    }
}
