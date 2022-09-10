using System;
using System.Collections.Generic;

namespace MemezawyDev.Core.ObjectPool
{
    public class ObjectPool<T>
    {
        private readonly Stack<T> _stack = new Stack<T>();
        private readonly Func<T> _createAction;
        private readonly Action<T> _getAction;
        private readonly Action<T> _returnAction;
        private int Size { get; }

        public ObjectPool(Func<T> createAction, Action<T> getAction, Action<T> returnAction, int maxSize)
        {
            Size = maxSize;
            _createAction = createAction;
            _getAction = getAction;
            _returnAction = returnAction;
            for (int i = 0; i < Size; i++)
            {
                Return(_createAction());
            }
        }

        public T Get()
        {
            if (_stack.Count == 0) return default;
            T obj = _stack.Pop();
            _getAction?.Invoke(obj);
            return obj;
        }

        public void Return(T obj)
        {
            _returnAction.Invoke(obj);
            _stack.Push(obj);
        }
    }
}