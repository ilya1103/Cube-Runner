using Code.Infrastructure.UI;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Code.Infrastructure.Services.LoadingCurtainService
{
    public class LoadingCurtainService : ILoadingCurtainService
    {
        private LoadingCurtain _loadingCurtain;

        [Inject]
        public void Construct(LoadingCurtain loadingCurtain) =>
            _loadingCurtain = loadingCurtain;

        public void Show() =>
            _loadingCurtain.Show();

        public void Hide() =>
            _loadingCurtain.Hide().Forget();
    }
}