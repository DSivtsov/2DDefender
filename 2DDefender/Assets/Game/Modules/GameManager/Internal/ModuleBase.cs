using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Modules.GameManager
{
    public abstract class ModuleBase : MonoBehaviour
    {
        public virtual IEnumerable<object> GetServices()
        {
            Type type = this.GetType();
            FieldInfo[] fields = type.GetFields(
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.DeclaredOnly
            );

            foreach (var field in fields)
            {
                if (field.IsDefined(typeof(ServiceAttribute)))
                {
                    yield return field.GetValue(this);
                }
            }
        }

        public virtual IEnumerable<IGameListener> GetListeners()
        {
            Type type = this.GetType();
            FieldInfo[] fields = type.GetFields(
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.DeclaredOnly
            );

            foreach (var field in fields)
            {
                if (field.IsDefined(typeof(ListenerAttribute)))
                {
                    var value = field.GetValue(this);
                    if (value is IGameListener gameListener)
                    {
                        yield return gameListener;
                    }
                }
            }
        }

        public virtual IEnumerable<object> GetInjectedFields()
        {
            Type type = this.GetType();
            FieldInfo[] fields = type.GetFields(
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.DeclaredOnly
            );
            GameManagerDebugger.DebugResolveDependencies(name, fields);
            foreach (var field in fields)
            {
                object target = field.GetValue(this);

                if (target == null)
                {
                    Debug.LogError($"Module[{type.Name}]: Can't make injection to not initialized field[{field.Name}]");
                    continue;
                }
                
                yield return target;
            }
        }
    }
}