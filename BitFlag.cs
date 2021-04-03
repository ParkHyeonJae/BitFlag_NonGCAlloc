using System;
using System.Linq.Expressions;

namespace Utilitys.BitFlag
{
    /// <summary>
    /// Reference By https://www.slideshare.net/devcatpublications/enum-boxing-enum-ndc2019
    /// </summary>
    public class ValueCastTo
    {
        protected static class Cache<TFrom, TTo>
        {
            public static readonly Func<TFrom, TTo> Caster = Get();

            static Func<TFrom, TTo> Get()
            {
                var p = Expression.Parameter(typeof(TFrom), "from");
                var c = Expression.ConvertChecked(p, typeof(TTo));
                return Expression.Lambda<Func<TFrom, TTo>>(c, p).Compile();
            }
        }
    }

    public class ValueCastTo<TTo> : ValueCastTo
    {
        public static TTo From<TFrom>(TFrom from)
        {
            return Cache<TFrom, TTo>.Caster(from);
        }
    }

    public class BitFlag<T> where T : Enum
    {
        public int eStateFlag { get; private set; }

        /// <summary>
        /// Flag Turn On
        /// </summary>
        /// <param name="state"></param>
        public void SetOn(T state) => eStateFlag |= ValueCastTo<int>.From<T>(state);

        /// <summary>
        /// Flag Turn Off
        /// </summary>
        /// <param name="state"></param>
        public void SetOff(T state) => eStateFlag &= ~ValueCastTo<int>.From<T>(state);

        /// <summary>
        /// StateFlag Turn On Off
        /// </summary>
        /// <param name="state"></param>
        public void Set(T state) => eStateFlag ^= ValueCastTo<int>.From<T>(state);

        /// <summary>
        /// Reset All State
        /// </summary>
        public void Clear() => eStateFlag = 0;

        /// <summary>
        /// Turn on All State
        /// </summary>
        public void SetAll() => eStateFlag = int.MaxValue;

        /// <summary>
        /// Turn On Exclude parameter States
        /// </summary>
        /// <param name="states"></param>
        public void ExclusiveSetAll(params T[] states)
        {
            eStateFlag = int.MaxValue;
            foreach (T state in states)
            {
                eStateFlag ^= ValueCastTo<int>.From<T>(state);
            }
        }

        /// <summary>
        /// Is Contains Flag
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        public bool HasFlag(T state) => ((eStateFlag & ValueCastTo<int>.From<T>(state)) != 0);
    }
}