﻿using System;
using System.Collections;
using System.Collections.Generic;

namespace UserList
{
    // TODO : need unit tests
    public class UserList<T> : IList<T>, ICollection<T>, IEnumerable<T>,
        IEnumerable//, IList, ICollection, IReadOnlyList<T>, IReadOnlyCollection<T>
    {
        private T[] _array;
        private int _count;
        private static readonly T[] _emptyArray = new T[0];

        private const int DEFAULT_CAPACITY = 4;

        // constructors
        public UserList()
        {
            _array = _emptyArray;
        }

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

        public UserList(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            if (capacity == 0)
                _array = _emptyArray;
            else
                _array = new T[capacity];
        }

        // properties
        public int Count => _count;

        public int Capacity
        {
            get => _array.Length;

            set
            {
                int size = _array.Length;

                if (value < size)
                    throw new ArgumentOutOfRangeException(nameof(Capacity));

                if (value == size)
                    return;

                Array.Resize(ref _array, value);
            }
        }

        private int LastIndex => _count - 1;

        // TODO : no impl 
        public bool IsReadOnly => throw new NotImplementedException();

        // operators
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

        // methods
        private void ExtendArray()
        {
            int size = _array.Length;
            int newSize = size <= 0 ? DEFAULT_CAPACITY : size * 2;

            Array.Resize(ref _array, newSize);
        }

        public void Add(T item)
        {
            if (_array.Length <= _count)
                ExtendArray();

            _array[_count] = item;
            _count += 1;
        }

        public void Clear()
        {
            Array.Clear(_array, 0, _count);
            _count = 0;
        }

        public bool Remove(T item)
        {
            if (_count == 0)
                return false;

            int remIndex = Array.IndexOf(_array, item, 0, LastIndex);
            if (remIndex < 0)
                return false;

            RemoveAt(remIndex);

            return true;
        }

        public int IndexOf(T item)
        {
            if (_count == 0)
                return -1;

            return Array.IndexOf(_array, item, 0, LastIndex);
        }

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

        public void RemoveAt(int index)
        {
            if (index >= _count)
                throw new ArgumentOutOfRangeException(nameof(index));

            if (index < LastIndex)
                Array.Copy(_array, index + 1, _array, index, LastIndex - index);

            _array[LastIndex] = default(T);

            _count -= 1;
        }

        public bool Contains(T item)
        {
            if ((Object)item == null)
            {
                for (int i = 0; i < _count; i++)
                    if ((Object)_array[i] == null)
                        return true;
                return false;
            }

            EqualityComparer<T> c = EqualityComparer<T>.Default;
            for (int i = 0; i < _count; i++)
                if (c.Equals(_array[i], item))
                    return true;

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            Array.Copy(_array, 0, array, arrayIndex, _count);
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _count; ++i)
            {
                yield return _array[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}