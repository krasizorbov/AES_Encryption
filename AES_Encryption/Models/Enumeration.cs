using System;
namespace AES_Encryption.Models
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public abstract class Enumeration : IComparable
    {
        private static readonly ConcurrentDictionary<Type, IEnumerable<object>> EnumCache
            = new ConcurrentDictionary<Type, IEnumerable<object>>();

        public int Value { get; }

        public string Name { get; }

        public override string ToString() => this.Name;

        protected Enumeration(int value, string name)
        {
            this.Value = value;
            this.Name = name;
        }

        public static IEnumerable<T> GetAll<T>()
            where T : Enumeration
        {
            var type = typeof(T);

            var values = EnumCache.GetOrAdd(type, _ => type
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Select(f => f.GetValue(null)!));

            return values.Cast<T>();
        }

        public static IEnumerable<string> GetAllNames<T>()
            where T : Enumeration
        {
            var names = GetAll<T>().Select(x => x.Name);

            return names;
        }

        public static T FromValue<T>(int value)
            where T : Enumeration
        {
            return Parse<T, int>(value, "value", item => item.Value == value);
        }

        public static T FromName<T>(string name)
            where T : Enumeration
        {
            return Parse<T, string>(name, "name", item => item.Name == name);
        }

        public static string NameFromValue<T>(int value)
            where T : Enumeration
        {
            return FromValue<T>(value).Name;
        }

        public static bool HasValue<T>(int value)
            where T : Enumeration
        {
            try
            {
                FromValue<T>(value);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool HasName<T>(string name)
            where T : Enumeration
        {
            try
            {
                FromName<T>(name);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Enumeration otherValue))
            {
                return false;
            }

            var typeMatches = this.GetType() == obj.GetType();
            var valueMatches = this.Value.Equals(otherValue.Value);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode()
        {
            return (this.GetType().ToString() + this.Value).GetHashCode();
        }

        public int CompareTo(object other)
        {
            return this.Value.CompareTo(((Enumeration)other!).Value);
        }

        private static T Parse<T, TValue>(TValue value, string description, Func<T, bool> predicate)
            where T : Enumeration
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);

            if (matchingItem == null)
            {
                throw new InvalidOperationException($"'{value}' is not a valid {description} in {typeof(T)}");
            }

            return matchingItem;
        }
    }
}

