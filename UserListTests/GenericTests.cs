using System;
using NUnit.Framework;
using UserList;

namespace UserListTests
{
    public class GenericTestCase<T> : IGenericTestCase
    {
        private T[] _array;
        private T[] _insertItems;
        private T[] _removeItems;

        private int _insertIndex;
        private int[] _removeIndexes;

        private const int TEST_CAPACITY = 100;

        public GenericTestCase(T[] array, T[] insertItems, T[] removeItems, int[] removeIndexes )
        {
            if (array == null || array.Length <= 0)
                throw new ArgumentException(nameof(array));

            if (insertItems == null || insertItems.Length <= 0)
                throw new ArgumentException(nameof(insertItems));

            if (removeItems == null || removeItems.Length <= 0)
                throw new ArgumentException(nameof(removeItems));

            _array = array;
            _insertItems = insertItems;
            _removeItems = removeItems;
            _removeIndexes = removeIndexes;

            _insertIndex = array.Length / 2;
        }

        void IGenericTestCase.Construct_WithCollection_ShouldEqualsItems()
        {
            // GIVEN
            UserList<T> list = new UserList<T>(_array);

            // THEN
            Assert.AreEqual(list.Count, _array.Length);
            for (int i = 0; i < list.Count; ++i)
                Assert.AreEqual(list[i], _array[i]);
        }

        void IGenericTestCase.Construct_WithEnumerable_ShouldValid()
        {
            // GIVEN
            TestEnumerable<T> testEnum = new TestEnumerable<T>(_array);

            // WHEN
            UserList<T> list = new UserList<T>(testEnum);

            // THEN
            Assert.AreEqual(list.Count, testEnum.Count);
            for (int i = 0; i < list.Count; ++i)
                Assert.AreEqual(list[i], testEnum[i]);
        }
        
        void IGenericTestCase.Construct_WithCapacity_ShouldValid()
        {
            // GIVEN
            UserList<T> list = new UserList<T>(TEST_CAPACITY);

            // THEN
            Assert.AreEqual(list.Capacity, TEST_CAPACITY);
            Assert.AreEqual(list.Count, 0);
        }

        void IGenericTestCase.SetCapacity_ShouldSaveItems()
        {
            // GIVEN
            UserList<T> list = new UserList<T>(_array)
            {
                Capacity = TEST_CAPACITY
            };

            // THEN
            Assert.AreEqual(list.Count, _array.Length);
            Assert.AreEqual(list.Capacity, TEST_CAPACITY);
        }
        
        void IGenericTestCase.Add_ShouldAddItem()
        {
            // GIVEN
            int N = _array.Length;
            UserList<T> list = new UserList<T>();

            // WHEN
            for (int i = 0; i < N; ++i)
                list.Add(_array[i]);

            // THEN
            Assert.AreEqual(list.Count, N);
            for (int i = 0; i < N; ++i)
                Assert.AreEqual(list[i], _array[i]);
        }

        void IGenericTestCase.Clear_ShouldRemoveAllItems()
        {
            // GIVEN
            UserList<T> list = new UserList<T>(_array);

            // WHEN
            list.Clear();

            // THEN
            Assert.AreEqual(list.Count, 0);
        }
        
        void IGenericTestCase.Remove_ByIndex_ShouldRemoveItem()
        {
            // GIVEN
            UserList<T> list = new UserList<T>(_array);

            // WHEN
            T[] removeItems = new T[_removeIndexes.Length];
            for (int i = 0; i < _removeIndexes.Length; ++i)
            {
                int idx = _removeIndexes[i];

                removeItems[i] = list[idx];
                list.RemoveAt(idx);
            }

            // THEN
            Assert.AreEqual(list.Count, _array.Length - _removeIndexes.Length);
            foreach (T item in removeItems)
                Assert.IsFalse(list.Contains(item));
        }
        
        void IGenericTestCase.Remove_ByItem_ShouldRemoveItem()
        {
            // GIVEN
            UserList<T> list = new UserList<T>(_array);

            // WHEN
            foreach (T item in _removeItems)
                Assert.IsTrue(list.Remove(item));

            // THEN
            Assert.AreEqual(list.Count, _array.Length - _removeItems.Length);
            foreach (T item in _removeItems)
                Assert.IsFalse(list.Contains(item));
        }
        
        void IGenericTestCase.Insert_ShouldInsertItem()
        {
            // GIVEN
            UserList<T> list = new UserList<T>(_array);

            // WHEN
            foreach (T item in _insertItems)
                list.Insert(_insertIndex, item);

            // THEN
            Assert.AreEqual(list.Count, _array.Length + _insertItems.Length);
            foreach (T item in _insertItems)
                Assert.IsTrue(list.Contains(item));
        }
    }

#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    internal class TestObject
#pragma warning restore CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    {
        public TestObject() { }

        public TestObject(int val) => Value = val;

        public int Value { get; set; }

        public override bool Equals(Object obj)
        {
            if (obj is TestObject testObj)
                return Value.Equals(testObj.Value);

            return false;
        }
    }
}
