using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable AssignNullToNotNullAttribute

namespace NinjaTrader.Core.Custom
{
    public class EnumMetadataCache<TEnum> where TEnum : Enum
    {
        private static readonly List<EnumItem> cachedMetadata;

        static EnumMetadataCache()
        {
            var enumType = typeof(TEnum);

            cachedMetadata = (
                from value in enumType.GetEnumValues().OfType<IComparable>()
                let memberInfo = enumType.GetMember(value.ToString()).First()
                let attributes = memberInfo.GetCustomAttributes(true)
                let name = memberInfo.Name
                select new EnumItem(name, value, attributes)
            ).ToList();
        }

        public static TAttribute GetAttribute<TAttribute>(TEnum value)
            where TAttribute : Attribute
        {
            var enumItem = GetCachedValue(value);
            var attribute = enumItem.Attributes.OfType<TAttribute>().SingleOrDefault();
            return attribute;
        }

        public static IEnumerable<TEnum> GetAllMembers()
        {
            return cachedMetadata.Select(_ => _.Instance).Cast<TEnum>();
        }

        private static EnumItem GetCachedValue(object value)
        {
            var item = cachedMetadata.Single(_ => _.Instance.CompareTo(value) == 0);
            return item;
        }

        private class EnumItem
        {
            public EnumItem(string name, IComparable instance, object[] attributes)
            {
                Instance = instance;
                Attributes = attributes;
                Name = name;
            }

            public object[] Attributes { get; }

            public IComparable Instance { get; }

            public string Name { get; }
        }

        public static TEnum GetMember(string memberName)
        {
            int index = -1;

            for (int i = 0; i < cachedMetadata.Count; i++)
            {
                if (string.Compare(cachedMetadata[i].Name, memberName, StringComparison.Ordinal) == 0)
                    return (TEnum)cachedMetadata[i].Instance;

                if (string.Compare(cachedMetadata[i].Name, memberName, StringComparison.OrdinalIgnoreCase) == 0)
                    index = i;
            }

            if (index == -1)
            {
                throw new InvalidOperationException(
                    $"Type {typeof(TEnum).Name} does not contain member with name {memberName}");
            }

            var value = (TEnum)cachedMetadata[index].Instance;

            return value;
        }
    }
}