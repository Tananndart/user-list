using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace UserList
{
    // TODO : need unit tests
    public class UserList<T> : IList<T>
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

            // TODO : rewrite without enumerable.Count()!
            int elementCount = enumerable.Count();
            if (elementCount <= 0)
            {
                _array = _emptyArray;
                return;
            }

            _array = new T[elementCount];

            ICollection<T> collection = enumerable as ICollection<T>;
            if (collection != null)
            {
                collection.CopyTo(_array, 0);
            }
            else
            {
                int i = 0;
                foreach (T val in enumerable)
                {
                    _array[i] = val;
                    ++i;
                }
            }

            _count = elementCount;
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
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        // TODO: use yield ?
        public struct Enumerator : IEnumerator<T>, System.Collections.IEnumerator
        {
            private UserList<T> _list;
            private int _index;
            private T _current;

            internal Enumerator(UserList<T> list)
            {
                _list = list;
                _index = 0;
                _current = default(T);
            }

            public T Current => _current;

            object IEnumerator.Current
            {
                get
                {
                    if (_index == 0 || _index >= _list._count)
                        throw new InvalidOperationException(nameof(Current));

                    return Current;
                }
            }

            public void Dispose() {}

            public bool MoveNext()
            {
                if (_index < _list._count)
                {
                    _current = _list._array[_index];
                    _index += 1;
                    return true;
                }

                _index = _list._count;
                _current = default(T);
                return false;
            }

            public void Reset()
            {
                _index = 0;
                _current = default(T);
            }
        }
    }
}
