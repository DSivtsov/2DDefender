using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace Modules.GameManager
{
    public sealed class GameManagerDebugger
    {
        [System.Diagnostics.Conditional("DEBUG_MODULES")]
        public static void DebugInstallGameManagerService() => Debug.Log($"[ModulesInstaller]: Added Service[GameManager]");

        [System.Diagnostics.Conditional("DEBUG_MODULES")]
        public static void DebugInstallServices(ModuleBase module, IEnumerable<object> services)
        {
            string listServices = string.Join(", ", services.Select<object, string>((obj) => obj.GetType().Name));
            Debug.Log($"Module[{module.name}]: Added Services[{listServices}]");
        }

        [System.Diagnostics.Conditional("DEBUG_MODULES")]
        public static void DebugInstallListeners(ModuleBase module, IEnumerable<IGameListener> listeners)
        {
            {
                string existListeners = string.Join(", ", listeners.Select<IGameListener, string>((obj) => obj.GetType().Name));
                Debug.Log($"Module[{module.name}]: Use Listeners in[{existListeners}]");
            }
        }
        
        [System.Diagnostics.Conditional("DEBUG_MODULES")]
        public static void DebugInjection(object target, MethodInfo method, object[] args)
        {
            {
                string listParameters = string.Join(", ", args.Select<object, string>((obj) => obj.GetType().Name));
                UnityEngine.Debug.Log($"Target[{target.GetType().Name}]: Inject [{listParameters}] by ({method.Name}())");
            }
        }
        
        [System.Diagnostics.Conditional("DEBUG_MODULES")]
        public static void DebugResolveDependencies(string name, FieldInfo[] fields)
        {
            {
                string listToOnjections = string.Join(", ", fields.Select<FieldInfo, string>((field) => field.FieldType.Name));
                Debug.Log($"Module[{name}]: Will try make Injection to[{listToOnjections}]");
            }
        }
    }
}