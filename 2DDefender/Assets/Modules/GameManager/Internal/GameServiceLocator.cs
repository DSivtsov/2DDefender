using System;
using System.Collections.Generic;

namespace Modules.GameManager
{
    internal sealed class GameServiceLocator
    {
        private readonly List<object> services = new List<object>();

        internal List<T> GetServices<T>()
        {
            var result = new List<T>();
            foreach (var service in services)
            {
                if (service is T tService)
                {
                    result.Add(tService);
                }
            }

            return result;
        }

        internal object GetService(Type serviceType)
        {
            foreach (var service in services)
            {
                if (serviceType.IsInstanceOfType(service))
                {
                    return service;
                }
            }
            throw new NotFoundServiceException($"Service of type [{serviceType.Name}] is not found!");
        }

        internal T GetService<T>()
        {
            foreach (var service in services)
            {
                if (service is T result)
                {
                    return result;
                }
            }

            throw new NotFoundServiceException($"Service of type [{typeof(T).Name}] is not found!");
        }

        internal void AddService(object service)
        {
            services.Add(service);
        }

        public void AddServices(IEnumerable<object> services)
        {
            this.services.AddRange(services);
        }
    }

    internal sealed class NotFoundServiceException : SystemException
    {
        public NotFoundServiceException() : base() { }

        public NotFoundServiceException(string message) : base(message) { }
    }
}