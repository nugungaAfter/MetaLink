using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataStruct
{
    public interface IDeque<T>
    {
        public void Push(T Value);
        public T Pop();
        public void Enqueue(T Value);
        public void FrontEnqueue(T Value);
        public void RearEnqueue(T Value);
        public T Dequeue();
        public T FrontDequeue();
        public T RearDequeue();
        public T this[int Index] { get; set; }
        public void Clear();
        public void RemoveAt(int Index);
        public bool Contains(T Value);
        public bool IsEmpty();
        public void Insert(int Index, T Value);
        public T[] ToArray();
        public List<T> ToList();
        public Queue<T> ToQueue();
        public Stack<T> ToStack();
        public void Add(T Value);
    }

    [System.Serializable]
    public class Deque<T> : IDeque<T>, IEnumerable<T>, IEnumerable, IReadOnlyCollection<T>, ICloneable
    {
        public List<T> m_Values;

        public T this[int Index] { 
            get => m_Values[Index];
            set => m_Values[Index] = value;
        }

        public Deque()
        {
            m_Values = new List<T>();
        }
        public Deque(List<T> p_Value)
        {
            m_Values = new List<T>(p_Value);
        }
        public Deque(Stack<T> p_Value)
        {
            m_Values = new List<T>(p_Value.ToArray());
        }
        public Deque(Queue<T> p_Value)
        {
            m_Values = new List<T>(p_Value.ToArray());
        }
        public int Count => m_Values.Count;
        public IEnumerator<T> GetEnumerator() => m_Values.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => m_Values.GetEnumerator();
        public bool IsEmpty() => Count == 0;
        public void Clear() => m_Values.Clear();
        public bool Contains(T Value) => m_Values.Contains(Value);
        public void FrontEnqueue(T Value) => m_Values.Insert(0, Value);
        public void RearEnqueue(T Value) => m_Values.Add(Value);
        public T FrontDequeue()
        {
            var ret = this[0];
            m_Values.RemoveAt(0);
            return ret;
        }
        public T RearDequeue()
        {
            var ret = this[Count == 0 ? 0 : Count - 1];
            m_Values.RemoveAt(Count == 0 ? 0 : Count - 1);
            return ret;
        }
        public void Push(T Value) => m_Values.Insert(0, Value);
        public T Pop()
        {
            var ret = this[0];
            m_Values.RemoveAt(0);
            return ret;
        }
        public void RemoveAt(int Index) => m_Values.RemoveAt(Index);
        public void Insert(int Index, T Value) => m_Values.Insert(Index, Value);
        public T[] ToArray() => m_Values.ToArray();
        public List<T> ToList() => m_Values;
        public Queue<T> ToQueue() => new Queue<T>(m_Values);
        public Stack<T> ToStack() => new Stack<T>(m_Values);
        public void Add(T Value) => m_Values.Add(Value);
        public void Enqueue(T Value) => FrontEnqueue(Value);
        public T Dequeue() => FrontDequeue();
        public object Clone() => new Deque<T>(m_Values);
    }
}