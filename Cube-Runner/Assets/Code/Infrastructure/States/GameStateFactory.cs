using System;
using System.Collections.Generic;
using Code.Infrastructure.States.Api;
using Zenject;

namespace Code.Infrastructure.States
{
    public class GameStateFactory : IGameStateFactory
    {
        private readonly Dictionary<Type, Func<IExitableState>> _statesResolvers;
        
        public GameStateFactory(DiContainer container)
        {
            _statesResolvers = new Dictionary<Type, Func<IExitableState>>
            {
                [typeof(BootstrapState)] = container.Resolve<BootstrapState>,
                [typeof(LoadLevelState)] = container.Resolve<LoadLevelState>,
            };
        }

        public IExitableState Create(Type type)
        {
            if (!_statesResolvers.TryGetValue(type, out Func<IExitableState> resolver))
                throw new Exception(type.Name);

            return resolver();
        }

        public T GetState<T>() where T : class, IExitableState =>
            Create(typeof(T)) as T;
    }
}