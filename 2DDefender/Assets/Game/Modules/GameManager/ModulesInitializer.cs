using UnityEngine;
using System;

namespace Modules.GameManager
{
    public sealed class ModulesInitializer : MonoBehaviour
    {
        [SerializeField] private ModuleBase[] _modules;

        private GameManager _gameManager;
        private GameServiceLocator _serviceLocator;
        private GameInjector _injector;
        private void Awake()
        {
            _serviceLocator = new GameServiceLocator();
            _injector = new GameInjector(_serviceLocator);
            _gameManager = FindObjectOfType<GameManager>();
            if (_gameManager)
            {
                CheckEmptyModulesRecords();
                InstallGameManagerService();
                InstallServices();
                InstallListeners();
                ResolveDependencies();
                _gameManager.ModulesInitFinished();
            }
            else
                throw new NotSupportedException("_gameManager not found. ModulesInstaller stopped.");
        }

        private void CheckEmptyModulesRecords()
        {
            for (int i = 0; i < _modules.Length; i++)
                if (!_modules[i])
                    throw new NotSupportedException($"Module[{i}] is Empty. ModulesInstaller stopped.");
        }

        private void InstallGameManagerService()
        {
            _serviceLocator.AddService(_gameManager);
            GameManagerDebugger.DebugInstallGameManagerService();
        }

        private void InstallServices()
        {
            foreach (var module in _modules)
            {
                var services = module.GetServices(); //C#
                _serviceLocator.AddServices(services);
                GameManagerDebugger.DebugInstallServices(module, services);
            }
        }

        private void InstallListeners()
        {
            foreach (var module in _modules)
            {
                var listeners = module.GetListeners();
                _gameManager.AddListeners(listeners);
                GameManagerDebugger.DebugInstallListeners(module, listeners);
            }
        }

        private void ResolveDependencies()
        {
            foreach (var module in _modules)
            {
                foreach (object field in module.GetInjectedFields())
                {
                    InjectTo(field);
                }
            }
        }

        private void InjectTo(object target)
        {
            _injector.Inject(target);
        }
    }
}