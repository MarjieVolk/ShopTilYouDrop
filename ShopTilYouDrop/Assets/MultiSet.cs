using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class MultiSet<T> : ICollection<T>
{
    private readonly Dictionary<T, int> _data;

    public MultiSet()
    {
        _data = new Dictionary<T, int>();
    }

    public MultiSet(ICollection<T> other)
    {
        _data = new Dictionary<T, int>();
        foreach (T item in other)
        {
            Add(item);
        }
    }

    private MultiSet(Dictionary<T, int> data)
    {
        _data = data;
    }

    public override string ToString()
    {
        StringBuilder ret = new StringBuilder();
        ret.Append("(");
        foreach(T item in this){
            if (ret.Length > 1) ret.Append(",");
            ret.Append(item);
        }
        ret.Append(")");
        return ret.ToString();
    }

    public MultiSet<T> Except(MultiSet<T> other)
    {
        Dictionary<T, int> differenceData = new Dictionary<T, int>(_data);
        foreach (KeyValuePair<T, int> pair in _data)
        {
            if (other._data.ContainsKey(pair.Key))
            {
                int otherValue = other._data[pair.Key];
                if (otherValue >= pair.Value)
                {
                    differenceData.Remove(pair.Key);
                }
                else
                {
                    differenceData[pair.Key] -= otherValue;
                }
            }
        }

        return new MultiSet<T>(differenceData);
    }

    public void Add(T item)
    {
        if (!_data.ContainsKey(item)) _data[item] = 0;
        _data[item]++;
    }

    public void Clear()
    {
        _data.Clear();
    }

    public bool Contains(T item)
    {
        return _data.ContainsKey(item);
    }

    public void CopyTo(T[] array, int arrayIndex)
    {
        foreach (KeyValuePair<T, int> entry in _data)
        {
            for (int i = 0; i < entry.Value; i++)
            {
                array[arrayIndex] = entry.Key;
                arrayIndex++;
            }
        }
    }

    public int Count
    {
        get { return _data.Values.Sum(); }
    }

    public bool IsReadOnly
    {
        get { return false; }
    }

    public bool Remove(T item)
    {
        if (!Contains(item)) return false;

        if (_data[item] == 0) _data.Remove(item);
        _data[item]--;
        return true;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return new MultisetEnumerator<T>(this);
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return new MultisetEnumerator<T>(this);
    }

    private class MultisetEnumerator<U> : IEnumerator<U>
    {
        private readonly MultiSet<U> _set;
        private readonly IEnumerator<KeyValuePair<U, int>> _pairIterator;
        private int _index;

        public MultisetEnumerator(MultiSet<U> multiSet)
        {
            _set = multiSet;
            _pairIterator = _set._data.GetEnumerator();
            _index = 0;
        }

        public U Current
        {
            get { return _pairIterator.Current.Key; }
        }

        public void Dispose()
        {
            _pairIterator.Dispose();
        }

        object System.Collections.IEnumerator.Current
        {
            get { return Current; }
        }

        public bool MoveNext()
        {
            if (_index < _pairIterator.Current.Value - 1)
            {
                _index++;
                return true;
            }

            _index = 0;
            return _pairIterator.MoveNext();
        }

        public void Reset()
        {
            _index = 0;
            _pairIterator.Reset();
        }
    }
}
