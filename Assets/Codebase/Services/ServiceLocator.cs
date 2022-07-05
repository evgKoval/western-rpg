using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Codebase.Services
{
  public class ServiceLocator
  {
    public static ServiceLocator Container => _instance ??= new ServiceLocator();

    private static ServiceLocator _instance;

    private readonly Dictionary<Type, IService> _services = new();

    public void RegisterSingle<TService>(TService service) where TService : IService =>
      _services.Add(typeof(TService), service);

    public void RegisterSingle<TService, TImplementation>()
      where TService : IService
      where TImplementation : class, IService =>
      _services.Add(typeof(TService), Single<TImplementation>());

    public TService Single<TService>() where TService : class, IService =>
      (TService)GetService(typeof(TService));

    public void DisposeAll() =>
      _services.Clear();

    private object GetService(Type type)
    {
      if (_services.ContainsKey(type))
        return _services[type];

      object service = CreateService(type);
      _services.Add(type, (IService)service);

      return service;
    }

    private object CreateService(Type type)
    {
      ConstructorInfo constructorInfo = type.GetConstructors().First();

      IEnumerable<Type> parametersTypes = constructorInfo.GetParameters().Select(p => p.ParameterType);

      List<IService> parametersCollection = parametersTypes.Select(parameterType =>
        _services.TryGetValue(parameterType, out IService obj) ? obj : null).ToList();

      return constructorInfo.Invoke(parametersCollection.ToArray());
    }
  }
}