using TMPro;
using VContainer;
using R3;

namespace PrismaFramework.GameLauncher.Localization
{
    public class LocalizedTMPText : Localized
    {
        // === 依赖 ===
        private TMP_Text _targetText;

        private ILocalizationService _locService;

        // [Inject] 注入服务
        [Inject]
        public void Construct(ILocalizationService locService)
        {
            _locService = locService;

            // 关键机制：监听全局 Revision
            // 当语言切换时，无需广播，只要把自己标记为脏，下一帧 LateUpdate 自动刷新
            _locService.Revision
                .Subscribe(_ => MarkDirty())
                .AddTo(this);
        }

        private void Awake()
        {
            _targetText = GetComponent<TMP_Text>();
        }

        private void OnEnable()
        {
            MarkDirty();
        }
        // === 外部调用接口 ===

        // 设置 Key (支持 string 或 Enum.ToString)
        public void SetKey(string key)
        {
            if (currentKey == key) return; // 没变就不脏
            currentKey = key;
            MarkDirty();
        }

        // 设置参数 (params 会产生数组分配，要注意 GC，频繁调用建议用重载)
        public void SetArgs(params object[] args)
        {
            // 这里简单比较引用，严格比较内容太费，通常这就够了
            if (currentArgs == args) return;
            currentArgs = args;
            MarkDirty();
        }

        // 快捷设置 Key + Args
        public void SetData(string key, params object[] args)
        {
            currentKey = key;
            currentArgs = args;
            MarkDirty();
        }

        // === 核心刷新机制 ===

        private void MarkDirty()
        {
            isDirty = true;
        }

        // 使用 LateUpdate 确保在一帧内无论 SetKey/SetArgs 被调用多少次
        // String.Format 和 GetText 只会执行一次 (Batching)
        private void LateUpdate()
        {
            if (!isDirty) return;
            if (_locService == null) return; // 防御未注入
            if (string.IsNullOrEmpty(currentKey)) return;

            RefreshText();
            isDirty = false; // 清除脏标记
        }

        private void RefreshText()
        {
            // 这里调用Service (带缓存的)
            string finalContainer = _locService.GetText(currentKey, currentArgs);
            _targetText.text = finalContainer;
        }
    }
}