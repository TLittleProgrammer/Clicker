using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

namespace Infrastructure.ResourcesProvider
{
    public sealed class AddressablessProvider : IResourcesProvider
    {
        public async UniTask<TTarget> LoadAsset<TTarget>(string path)
        {
            var resource = Addressables.LoadAssetAsync<TTarget>(path);
            
            await resource.Task;

            return resource.Result;
        }
    }
}