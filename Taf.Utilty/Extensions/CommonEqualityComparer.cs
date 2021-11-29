using System;
using System.Collections.Generic;

namespace Taf.Utility{
    public class CommonEqualityComparer<T, V> : IEqualityComparer<T>{
        private readonly IEqualityComparer<V> comparer;
        private readonly Func<T, V>           keySelector;

        public CommonEqualityComparer(Func<T, V> keySelector, IEqualityComparer<V> comparer){
            this.keySelector = keySelector;
            this.comparer    = comparer;
        }

        public CommonEqualityComparer(Func<T, V> keySelector)
            : this(keySelector, EqualityComparer<V>.Default){ }

        public bool Equals(T x, T y) => comparer.Equals(keySelector(x), keySelector(y));

        public int GetHashCode(T obj) => comparer.GetHashCode(keySelector(obj));
    }
}
