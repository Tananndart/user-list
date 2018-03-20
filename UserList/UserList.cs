using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UserList
{
    /// <summary>
    /// Generic class UserList
    /// Abbreviated version of the standard List.
    /// Implement all interfaces base List, but not supported ICollection.SyncRoot.
    /// </summary>
    /// <typeparam name="T"> any type impl Object.Equals(obj) </typeparam>
    public class UserList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IReadOnlyList<T>, IReadOnlyCollection<T>,
        IEnumerable, IList, ICollection
    {
        private T[] _array;
        private int _count;
        private static readonly T[] _emptyArray = new T[0];

        private const int DEFAULT_CAPACITY = 4;

        /// <summary>
        /// Standart constructor - create empty UserList.
        /// </summary>
        public UserList() => _array = _emptyArray;

        /// <summary>
        /// Cunstructor by IEnumerable.
        /// Create UserList with all elements in enumerable param.
        /// </summary>
        /// <param name="enumerable"> any type impl IEnumerable </param>
        public UserList(IEnumerable<T> enumerable)
        {
            if (enumerable == null)
                throw new ArgumentNullException(nameof(enumerable));

            if (enumerable is ICollection<T> collection)
            {
                int elementCount = collection.Count;
                if (elementCount <= 0)
                {
                    _array = _emptyArray;
                    return;
                }

                _array = new T[elementCount];
                collection.CopyTo(_array, 0);

                _count = elementCount;
                return;
            }

            _array = _emptyArray;
            using (IEnumerator<T> en = enumerable.GetEnumerator())
                while (en.MoveNext())
                    Add(en.Current);
        }

        /// <summary>
        /// Constructor with initial capacity.
        /// </summary>
        /// <param name="capacity">number of reserved items</param>
        public UserList(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentException(nameof(capacity));

            _array = (capacity != 0) ? new T[capacity] : _emptyArray;
        }

        /// <summary>
        /// Return count elements
        /// </summary>
        public int Count => _count;

        /// <summary>
        /// Return or set capacity.
        /// </summary>
        public int Capacity
        {
            get => _array.Length;

            set
            {
                int size = _array.Length;

                if (value < size)
                    throw new ArgumentOutOfRangeException(nameof(Capacity));

                if (value > size)
                    Array.Resize(ref _array, value);
            }
        }

        private int LastIndex => _count - 1;

        bool ICollection<T>.IsReadOnly => false;

        bool IList.IsReadOnly => false;

        bool IList.IsFixedSize => false;

        /// <summary>
        /// While not supported!
        /// </summary>
        object ICollection.SyncRoot => throw new NotSupportedException();

        bool ICollection.IsSynchronized => false;

        public T this[int index]
        {
            get
            {
                if (index >= _count)
                    throw new ArgumentOutOfRangeException(nameof(index));

                return _array[index];
            }

            set
            {
                if (index >= _count)
                    throw new ArgumentOutOfRangeException(nameof(index));

                _array[index] = value;
            }
        }

        object IList.this[int index]
        {
            get => this[index];

            set
            {
                ThrowIfNullOrDefault(value);

                try
                {
                    this[index] = (T)value;
                }
                catch (InvalidCastException ex)
                {
                    throw new ArgumentException($"{nameof(value)} HAS a wrong type", ex);
                }
            }
        }

        private void ExtendArray()
        {
            int size = _array.Length;
            int newSize = size <= 0 ? DEFAULT_CAPACITY : size * 2;

            Array.Resize(ref _array, newSize);
        }

        private void ThrowIfNullOrDefault(object value)
        {
            if (this.IsNullAndDefaultNotNullable(value))
                throw new ArgumentException($"{nameof(value)} IS null OR incorrect type");
        }

        /// <summary>
        /// Add new element to the end
        /// </summary>
        /// <param name="item">new element</param>
        public void Add(T item)
        {
            if (_array.Length <= _count)
                ExtendArray();

            _array[_count] = item;
            _count += 1;
        }

        /// <summary>
        /// Add new element to the end
        /// </summary>
        /// <param name="value">new element</param>
        /// <returns>index from new element</returns>
        int IList.Add(object value)
        {
            ThrowIfNullOrDefault(value);

            try
            {
                Add((T)value);
            }
            catch (InvalidCastException ex)
            {
                throw new ArgumentException($"{nameof(value)} HAS a wrong type", ex);
            }

            return LastIndex;
        }

        /// <summary>
        /// Clear all elements
        /// </summary>
        public void Clear()
        {
            Array.Clear(_array, 0, _count);
            _count = 0;
        }

        /// <summary>
        /// Remove element by index
        /// </summary>
        /// <param name="index">index from remove</param>
        public void RemoveAt(int index)
        {
            if (index >= _count)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (index < LastIndex)
                Array.Copy(_array, index + 1, _array, index, LastIndex - index);

            _array[LastIndex] = default(T);

            _count -= 1;
        }

        /// <summary>
        /// Remove element
        /// </summary>
        /// <param name="item">element</param>
        /// <returns>true if the element was removed</returns>
        public bool Remove(T item)
        {
            if (_count == 0)
                return false;

            int remIndex = Array.IndexOf(_array, item, 0, _count);
            if (remIndex < 0)
                return false;

            RemoveAt(remIndex);

            return true;
        }

        /// <summary>
        /// Remove element
        /// </summary>
        /// <param name="value">element from remove</param>
        void IList.Remove(object value)
        {
            ThrowIfNullOrDefault(value);

            try
            {
                Remove((T)value);
            }
            catch (InvalidCastException ex)
            {
                throw new ArgumentException($"{nameof(value)} HAS a wrong type", ex);
            }
        }

        /// <summary>
        /// Searches for the element and returns the index of its first occurrence in array
        /// </summary>
        /// <param name="item">element from search</param>
        /// <returns>index of the found element</returns>
        public int IndexOf(T item)
        {
            if (_count == 0)
                return -1;

            return Array.IndexOf(_array, item, 0, _count);
        }

        /// <summary>
        /// Searches for the element and returns the index of its first occurrence in array
        /// </summary>
        /// <param name="value">element from search</param>
        /// <returns>index of the found element</returns>
        int IList.IndexOf(object value)
        {
            ThrowIfNullOrDefault(value);

            try
            {
                return IndexOf((T)value);
            }
            catch (InvalidCastException ex)
            {
                throw new ArgumentException($"{nameof(value)} HAS a wrong type", ex);
            }
        }

        /// <summary>
        /// Insert a new element in the specified position
        /// </summary>
        /// <param name="index">pos from insert</param>
        /// <param name="item">new element</param>
        public void Insert(int index, T item)
        {
            if (index >= _count)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (_array.Length <= _count)
                ExtendArray();

            Array.Copy(_array, index, _array, index + 1, _count - index);

            _array[index] = item;
            _count += 1;
        }

        /// <summary>
        /// Insert a new element in the specified position
        /// </summary>
        /// <param name="index">pos from insert</param>
        /// <param name="value">new element</param>
        void IList.Insert(int index, object value)
        {
            ThrowIfNullOrDefault(value);

            try
            {
                Insert(index, (T)value);
            }
            catch (InvalidCastException ex)
            {
                throw new ArgumentException($"{nameof(value)} HAS a wrong type", ex);
            }
        }

        /// <summary>
        /// Specifies whether the collection contains the specified element
        /// </summary>
        /// <param name="item">element from checking</param>
        /// <returns>true if list contains item</returns>
        public bool Contains(T item)
        {
            return _array.Contains(item);
        }

        /// <summary>
        /// Specifies whether the collection contains the specified element
        /// </summary>
        /// <param name="value">element from checking</param>
        /// <returns>true if list contains value</returns>
        bool IList.Contains(object value)
        {
            ThrowIfNullOrDefault(value);

            try
            {
                return Contains((T)value);
            }
            catch (InvalidCastException ex)
            {
                throw new ArgumentException($"{nameof(value)} HAS a wrong type", ex);
            }
        }

        /// <summary>
        /// Copy elements in specified array
        /// </summary>
        /// <param name="array">array to copy</param>
        /// <param name="arrayIndex">array index from start insertion</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            Array.Copy(_array, 0, array, arrayIndex, _count);
        }

        /// <summary>
        /// Copy elements in specified array
        /// </summary>
        /// <param name="array">array to copy</param>
        /// <param name="index">array index from start insertion</param>
        void ICollection.CopyTo(Array array, int index)
        {
            if ((array != null) && (array.Rank != 1))
                throw new ArgumentException("Multy rank array not supported");

            try
            {
                Array.Copy(_array, 0, array, index, _count);
            }
            catch (ArrayTypeMismatchException ex)
            {
                throw new ArgumentException("Invalid array type", ex);
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _count; ++i)
                yield return _array[i];
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    /// <summary>
    /// Extension from UserList.
    /// Contains additional methods for validating values.
    /// </summary>
    public static class UserListExtension
    {
        /// <summary>
        /// Check value from null and check default value equals null.
        /// </summary>
        /// <typeparam name="T">type list items</typeparam>
        /// <param name="list">UsertList object</param>
        /// <param name="value">value from checking</param>
        /// <returns></returns>
        public static bool IsNullAndDefaultNotNullable<T>(this UserList<T> list, object value)
        {
            return (value == null && !(default(T) == null)) ;
        }
    }
}
