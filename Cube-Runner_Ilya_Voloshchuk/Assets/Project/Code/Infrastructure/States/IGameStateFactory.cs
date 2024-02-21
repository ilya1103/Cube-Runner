using System;
using Project.Code.Infrastructure.States.Interfaces;

namespace Project.Code.Infrastructure.States
{
    public interface IGameStateFactory
    {
        IExitableState Create(Type type);

        T GetState<T>() where T : class, IExitableState;
    }
}