using Code.StaticData;

namespace Code.Infrastructure.Services.StaticData
{
    public interface IStaticDataService
    {
        void LoadAll();
        ScenesStaticData ScenesStaticData { get; }
        LevelStaticData LevelStaticData{ get; }
    }
}