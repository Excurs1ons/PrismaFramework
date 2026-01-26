# 这是一个Unity项目模板，提供一些工业级第三方开源库的预引用

# 其他可选引用/工具
YooAsset
SmartFormat

## 路径结构
```text
Assets/
├── Editor/                  // 编辑器扩展代码
├── GameMain/                // [热更域] 所有的游戏逻辑（UI, 玩法, 配置读取）
│   ├── Scripts/
│   │   ├── Domain/          // 领域逻辑 (非MonoBehaviour)
│   │   ├── View/            // UI, 场景物体 (MonoBehaviour)
│   │   └── Infrastructure/  // 网络, 存档实现
│   └── _AssemblyDefinition  // [重要] 这里定义 "GameMain.dll" (HybridCLR热更集)
├── GameLauncher/            // [AOT域] 启动器，仅包含最基础的代码
│   ├── Scripts/
│   │   ├── Boot/            // 游戏入口
│   │   └── Patch/           // 更新检测，下载热更DLL
│   └── _AssemblyDefinition  // 这里定义 "GameLauncher.dll" (不热更)
├── Res/                     // 资源目录 (配合 YooAsset 收集)
│   ├── Configs/             // Luban 生成的二进制/JSON数据
│   ├── UI/
│   └── Audios/
├── Plugins/                 // Native 库 (LZ4, so/dll 文件)
└── ThirdParty/              // 购买的插件 (Odin, DOTween, SuperScrollView)
```