using Cysharp.Threading.Tasks;
using Project.Code.Infrastructure.UI;
using Zenject;

namespace Project.Code.Infrastructure.Services.LoadingCurtainService
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