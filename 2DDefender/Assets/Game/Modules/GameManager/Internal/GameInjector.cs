using System.Linq;
using System;
using System.Reflection;

namespace Modules.GameManager
{
    internal sealed class GameInjector
    {
        private readonly GameServiceLocator serviceLocator;

        public GameInjector(GameServiceLocator serviceLocator)
        {
            this.serviceLocator = serviceLocator;
        }

        internal void Inject(object target)
        {
            Type type = target.GetType();
            MethodInfo[] methods = type.GetMethods(
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic |
                BindingFlags.FlattenHierarchy
            );

            foreach (var method in methods)
            {
                if (method.IsDefined(typeof(InjectAttribute)))
                {
                    InvokeMethod(method, target);
                }
            }
        }

        private void InvokeMethod(MethodInfo method, object target)
        {
            ParameterInfo[] parameters = method.GetParameters();
            object[] args = new object[parameters.Length];
            try
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    ParameterInfo parameter = parameters[i];
                    Type type = parameter.ParameterType;
                    object arg = this.serviceLocator.GetService(type);
                    args[i] = arg;
                }
            }
            catch (NotFoundServiceException)
            {
                //To show which the list of parameters was planned to Inject
                string listParameters = string.Join(", ", parameters.Select<ParameterInfo, string>((param) => param.ParameterType.Name));
                UnityEngine.Debug.LogError($"Target[{target.GetType().Name}]: CAN'T Inject [{listParameters}] by ({method.Name}())");
                throw;
            }
            GameManagerDebugger.DebugInjection(target, method, args);
            method.Invoke(target, args);
        }
    }
}