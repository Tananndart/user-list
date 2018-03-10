using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace UserListTests
{
    internal class TestEnumerable<T> : IEnumerable<T>
    {
        private T[] _array = new T[0];

        public TestEnumerable(ICollection<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            int elementCount = collection.Count;
            if (elementCount > 0)
            {
                _array = new T[elementCount];
                collection.CopyTo(_array, 0);
            }
        }

        public int Count => _array.Length;

        public T this[int index]
        {
            get
            {
                if (index >= Count)
                    throw new ArgumentOutOfRangeException(nameof(index));

                return _array[index];
            }

            set
            {
                if (index >= Count)
                    throw new ArgumentOutOfRangeException(nameof(index));

                _array[index] = value;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _array.AsEnumerable<T>().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
