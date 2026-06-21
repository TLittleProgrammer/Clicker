using Cysharp.Threading.Tasks;

namespace Infrastructure.ResourcesProvider
{
    public interface IResourcesProvider
    {
        UniTask<TTarget> LoadAsset<TTarget>(string path);
    }
}