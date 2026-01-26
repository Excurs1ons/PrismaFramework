# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.
!!!禁止在任何地方提及ClaudeCode/Co-Authored/Copilots!!!
## 项目架构

这是一个 Unity URP 2D 游戏项目，采用 **VContainer** 依赖注入框架作为核心架构。

### 启动流程

```
RootLifetimeScope (MonoBehaviour on Bootstrap Scene)
    ↓ 注册所有服务到 DI 容器
GameBootstrapper (IAsyncStartable)
    ↓ 初始化各子系统
MainMenu Scene
```

### 程序集结构

| 程序集 | 用途 |
|--------|------|
| `GameLauncher` | 启动器和根容器，包含 `RootLifetimeScope` 和 `GameBootstrapper` |
| `GameMain` | 游戏主逻辑代码 |

**重要**: `GameLauncher.asmdef` 使用 `overrideReferences: true` 并直接引用 NuGet DLL：
- `Microsoft.Extensions.Logging.Abstractions.dll`
- `Microsoft.Extensions.Logging.dll`
- `ZLogger.dll`

这是为了确保日志系统在启动时可用。其他需要日志的模块必须引用 `GameLauncher` 程序集。

### 依赖注入模式

使用 **VContainer** 进行依赖注入：

```csharp
// 在 LifetimeScope.Configure() 中注册服务
builder.Register<TService>(Lifetime.Singleton);
builder.Register<TInterface, TImplementation>(Lifetime.Singleton);

// 通过构造函数注入
public class SomeClass
{
    private readonly ILogger _logger;
    public SomeClass(ILogger<SomeClass> logger)
    {
        _logger = logger;
    }
}
```

### 日志系统

使用 **ZLogger** (Microsoft.Extensions.Logging)：

```csharp
// 在 LifetimeScope 中创建 ILoggerFactory
private static ILoggerFactory CreateLogFactory()
{
    return LoggerFactory.Create(logging =>
    {
        logging.SetMinimumLevel(LogLevel.Trace);
        logging.AddZLoggerUnityDebug();
    });
}

// 注册为单例
builder.RegisterInstance(CreateLogFactory());

// 使用 - 注入 ILoggerFactory 并创建 logger
public class GameBootstrapper
{
    private readonly ILogger _logger;
    public GameBootstrapper(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<GameBootstrapper>();
        _logger.LogInformation("Game Framework Starting...");
    }
}
```

### 待集成的系统

以下系统已在 `GameBootstrapper` 中预留接口，但尚未实现：

1. **YooAsset** - 资源管理 (需要实现 `IAssetProvider`)
2. **Luban** - 配置表系统 (需要实现 `IConfigService`)
3. **HybridCLR** - 热更新 (需要下载并加载 HotUpdate.dll)
4. **MessagePipe** - 事件总线 (已安装，待集成)
5. **网络服务** - 需要实现 `INetworkService`

## NuGet 包管理

使用 **NuGetForUnity** 管理包：

- 配置文件: `Assets/NuGet.config`
- 包列表: `Assets/packages.config`
- 包安装位置: `Assets/Packages/` (自定义位置)
- 程序集引用: 在 `.asmdef` 中配置 `precompiledReferences`

**首次克隆项目后**，需要在 Unity 编辑器中打开 NuGetForUnity 窗口并点击 "Restore" 来下载 NuGet 包的 DLL 文件（DLL 文件被 .gitignore 忽略，不在版本控制中）。

## 代码约定

### 异步操作
使用 **UniTask** 而非 Unity 的 coroutine：

```csharp
using Cysharp.Threading.Tasks;

public async UniTask StartAsync(CancellationToken cancellationToken)
{
    await SceneManager.LoadSceneAsync("MainMenu").ToUniTask(cancellationToken: cancellationToken);
}
```

### 命名空间
- `GameLauncher.Boot` - 启动相关类
- 其他模块应按功能组织命名空间
