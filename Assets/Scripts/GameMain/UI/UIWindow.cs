using PrismaFramework.GameLauncher.UI;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace PrismaFramework.GameMain.UI
{
    public abstract class UIWindow : MonoBehaviour,IUIWindow
    {
        public AsyncOperationHandle AssetHandle { get; set; }
    }
}