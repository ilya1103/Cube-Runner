using UnityEngine;

namespace Project.Code.Infrastructure.Services.UIFactory
{
    public interface IUIFactory
    {
        void CreateUIRoot();
        GameObject CreateGameUI();
    }
}