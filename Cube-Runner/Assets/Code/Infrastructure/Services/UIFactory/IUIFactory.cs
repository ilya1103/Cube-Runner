using UnityEngine;

namespace Code.Infrastructure.Services.UIFactory
{
    public interface IUIFactory
    {
        void CreateUIRoot();
        GameObject CreateGameUI();
    }
}