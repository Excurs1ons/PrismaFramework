# PrismaFramework

基于 Unity URP 2D 的游戏开发框架，提供现代化的依赖注入、日志系统和模块化架构。

## 特性

- **Unity 2022.3** + **Universal Render Pipeline (URP) 2D**
- **VContainer** - 强大的依赖注入框架
- **ZLogger** - 高性能结构化日志系统
- **UniTask** - 异步/await 任务管理
- **Input System** - 新版输入系统
- **NuGetForUnity** - 在 Unity 中使用 NuGet 包

## 技术栈

| 技术 | 版本 | 用途 |
|------|------|------|
| VContainer | 1.14.0 | 依赖注入 |
| UniTask | 2.5.10 | 异步任务 |
| ZLogger | 2.5.10 | 日志系统 |
| MessagePipe | 1.8.1 | 事件总线 |
| Luban | 1.1.1 | 配置表（待集成） |

## 项目结构

```
Assets/
├── GameLauncher/          # 启动器和根容器
│   └── Scripts/Boot/
│       ├── RootLifetimeScope.cs    # DI 容器根
│       ├── GameBootstrapper.cs     # 游戏启动入口
│       └── GameConfig.cs           # 配置类
├── GameMain/              # 游戏主逻辑
└── Settings/              # URP 渲染设置
```

## 快速开始

### 环境要求

- Unity 2022.3 LTS 或更高版本
- Git

### 克隆项目

```bash
git clone https://github.com/Excurs1ons/PrismaFramework.git
cd PrismaFramework
```

### 首次设置

1. 用 Unity 打开项目
2. 打开 `Assets > NuGetForUnity > Manage NuGet Packages`
3. 点击 "Restore" 恢复 NuGet 包
4. 等待 Unity 完成导入

### 日志使用

```csharp
using Microsoft.Extensions.Logging;

public class YourClass
{
    private readonly ILogger _logger;

    public YourClass(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<YourClass>();
    }

    public void DoSomething()
    {
        _logger.LogInformation("执行操作...");
    }
}
```

### 依赖注入

```csharp
// 在 LifetimeScope 中注册
builder.Register<IYourService, YourService>(Lifetime.Singleton);

// 通过构造函数注入
public class YourClass
{
    private readonly IYourService _service;
    public YourClass(IYourService service)
    {
        _service = service;
    }
}
```

## 待集成功能

- [ ] Addressables 资源管理
- [ ] Luban 配置表系统
- [ ] HybridCLR 热更新
- [ ] MessagePipe 事件总线
- [ ] 网络服务

## 许可证

MIT License
