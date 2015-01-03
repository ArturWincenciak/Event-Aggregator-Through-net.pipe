using System;
using System.Collections.Generic;
using System.Reflection;
using TeoVincent.EA.Common;

namespace TeoVincent.EA.Client.UnitTests
{
    public class FieldReflector
    {
        internal static object GetInstanceField(Type type, object instance, string fieldName)
        {
            const BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic| BindingFlags.Static;
            FieldInfo field = type.GetField(fieldName, bindFlags);
            return field.GetValue(instance);
        }

        internal static Dictionary<Type, List<IListener>> GetListeners(InternalEventAggregator ea)
        {
            return GetInstanceField(typeof(InternalEventAggregator), ea, "listeners") as Dictionary<Type, List<IListener>>;
        }
    }
}