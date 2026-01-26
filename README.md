# PrismaFramework

Unity URP 2D 游戏开发模板项目。

## 技术栈

- **Unity 2022.3** + **URP 2D**
- **VContainer** - 依赖注入
- **UniTask** - 异步任务
- **ZLogger** - 日志系统
- **Input System** - 输入系统

## 快速开始

```bash
git clone https://github.com/Excurs1ons/PrismaFramework.git
```

用 Unity 打开项目，然后在 `Assets > NuGetForUnity` 中点击 "Restore" 恢复 NuGet 包。

## 项目结构

```
Assets/
├── GameLauncher/    # 启动器和 DI 容器
├── GameMain/        # 游戏主逻辑
└── Settings/        # URP 设置
```

## 文档

- [VContainer 文档](https://vcontainer.hadashikick.jp/)
- [UniTask 文档](https://github.com/Cysharp/UniTask)
- [ZLogger 文档](https://github.com/Cysharp/ZLogger)

MIT License
