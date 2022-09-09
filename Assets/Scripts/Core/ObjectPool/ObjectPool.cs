using System;
using System.Collections.Generic;

namespace MemezawyDev.Core.ObjectPool
{
    public class ObjectPool<T>
    {
        private Queue<T> _q = new Queue<T>();
        private Func<T> _createAction;
        private Action<T> _getAction;
        private Action<T> _returnAction;
        private int _size;

        public ObjectPool(Func<T> createAction, Action<T> getAction, Action<T> returnAction, int maxSize)
        {
            _size = maxSize;
            _createAction = createAction;
            _getAction = getAction;
            _returnAction = returnAction;
            for (int i = 0; i < _size; i++)
            {
                _q.Enqueue(_createAction());
            }
        }

        public T Get()
        {
            T obj = _q.Dequeue();
            _getAction?.Invoke(obj);
            return obj;
        }

        public void Return(T obj)
        {
            _returnAction.Invoke(obj);
            _q.Enqueue(obj);
        }
    }
}