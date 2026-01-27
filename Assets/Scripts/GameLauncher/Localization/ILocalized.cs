using UnityEngine;

namespace PrismaFramework.GameLauncher.Localization
{
    public abstract class Localized : MonoBehaviour
    {
        // === 状态缓存 ===
        protected string currentKey;

        protected object[] currentArgs;

        //脏标记
        protected bool isDirty = false;
    }
}