using Code.StaticData;
using UnityEngine;

namespace Code.Infrastructure.Services.StaticData
{
    public sealed class StaticDataService : IStaticDataService
    {
        private const string LevelsPath = "StaticData/Level Data";
        private const string ScenesRoutingPath = "StaticData/Scenes Data";

        public ScenesStaticData ScenesStaticData { get; private set; }
        public LevelStaticData LevelStaticData { get; private set; }

        public void LoadAll()
        {
            LoadScenesData();
            LoadLevelsData();
        }

        private void LoadScenesData() =>
            ScenesStaticData = Resources.Load<ScenesStaticData>(ScenesRoutingPath);

        private void LoadLevelsData() =>
            LevelStaticData = Resources.Load<LevelStaticData>(LevelsPath);
    }
}