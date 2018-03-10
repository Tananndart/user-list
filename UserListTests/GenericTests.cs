using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UserList;

namespace UserListTests
{
    internal static class GenericTests
    {
        internal static void Construct_WithCollection_ShouldEqualsItems<T>(T[] array)
        {
            if (array == null || array.Length <= 0)
                throw new ArgumentException(nameof(array));

            UserList<T> list = new UserList<T>(array);

            Assert.AreEqual(list.Count, array.Length);
            for (int i = 0; i < list.Count; ++i)
                Assert.AreEqual(list[i], array[i]);
        }

        internal static void Construct_WithEnumerable_ShouldValid<T>(T[] array)
        {
            if (array == null || array.Length <= 0)
                throw new ArgumentException(nameof(array));

            TestEnumerable<T> testEnum = new TestEnumerable<T>(array);

            UserList<T> list = new UserList<T>(testEnum);

            Assert.AreEqual(list.Count, testEnum.Count);
            for (int i = 0; i < list.Count; ++i)
                Assert.AreEqual(list[i], testEnum[i]);
        }
        
        internal static void Construct_WithCapacity_ShouldValid<T>(int capacity)
        {
            if (capacity <= 0)
                throw new ArgumentException(nameof(capacity));

            UserList<T> list = new UserList<T>(capacity);

            Assert.AreEqual(list.Capacity, capacity);
            Assert.AreEqual(list.Count, 0);
        }

        internal static void SetCapacity_ShouldSaveItems<T>(T[] array, int capacity)
        {
            if (array == null || array.Length <= 0)
                throw new ArgumentException(nameof(array));

            if (capacity <= 0)
                throw new ArgumentException(nameof(capacity));

            UserList<T> list = new UserList<T>(array)
            {
                Capacity = capacity
            };

            Assert.AreEqual(list.Count, array.Length);
            Assert.AreEqual(list.Capacity, capacity);
        }
        
        internal static void Indexator_ShouldThrowArgumentOutOfRange<T>(T[] array)
        {
            if (array == null || array.Length <= 0)
                throw new ArgumentException(nameof(array));

            UserList<T> list = new UserList<T>(array);

            list[list.Count] = default(T);
        }
        
        internal static void Add_ShouldAddItem<T>(T[] array)
        {
            if (array == null || array.Length <= 0)
                throw new ArgumentException(nameof(array));

            int N = array.Length;

            UserList<T> list = new UserList<T>();
            for (int i = 0; i < N; ++i)
                list.Add(array[i]);

            Assert.AreEqual(list.Count, N);
            for (int i = 0; i < N; ++i)
                Assert.AreEqual(list[i], array[i]);
        }

        internal static void Clear_ShouldRemoveAllItems<T>(T[] array)
        {
            if (array == null || array.Length <= 0)
                throw new ArgumentException(nameof(array));

            UserList<T> list = new UserList<T>(array);

            list.Clear();

            Assert.AreEqual(list.Count, 0);
        }
        
        internal static void Remove_ByIndex_ShouldRemoveItem<T>(T[] array, int[] removeIndexes)
        {
            if (array == null || array.Length <= 0)
                throw new ArgumentException(nameof(array));

            if (removeIndexes == null || removeIndexes.Length <= 0)
                throw new ArgumentException(nameof(removeIndexes));

            UserList<T> list = new UserList<T>(array);

            T[] removeItems = new T[removeIndexes.Length];
            for (int i = 0; i < removeIndexes.Length; ++i)
            {
                int idx = removeIndexes[i];

                removeItems[i] = list[idx];
                list.RemoveAt(idx);
            }

            Assert.AreEqual(list.Count, array.Length - removeIndexes.Length);
            foreach (T item in removeItems)
                Assert.IsFalse(list.Contains(item));
        }
        
        internal static void Remove_ByItem_ShouldRemoveItem<T>(T[] array, T[] removeItems)
        {
            if (array == null || array.Length <= 0)
                throw new ArgumentException(nameof(array));

            if (removeItems == null || removeItems.Length <= 0)
                throw new ArgumentException(nameof(removeItems));

            UserList<T> list = new UserList<T>(array);

            foreach (T item in removeItems)
                Assert.IsTrue(list.Remove(item));

            Assert.AreEqual(list.Count, array.Length - removeItems.Length);
            foreach (T item in removeItems)
                Assert.IsFalse(list.Contains(item));
        }
        
        internal static void Insert_ShouldInsertItem<T>(T[] array, T[] insertItems, int idxFromInsert)
        {
            if (array == null || array.Length <= 0)
                throw new ArgumentException(nameof(array));

            if (insertItems == null || insertItems.Length <= 0)
                throw new ArgumentException(nameof(insertItems));

            UserList<T> list = new UserList<T>(array);

            foreach (T item in insertItems)
                list.Insert(idxFromInsert, item);

            Assert.AreEqual(list.Count, array.Length + insertItems.Length);
            foreach (T item in insertItems)
                Assert.IsTrue(list.Contains(item));
        }
    }

    internal class TestObject
    {
        public TestObject() { }

        public TestObject(int val) => Value = val;

        public int Value { get; set; }
    }
}
