# MiMieMVVM

Unity 功能模块 MVVM 分层接口约束（`netstandard2.1`）

纯契约层：只定义 View / ViewModel / Model / 跨模块 Hub 的接口与最小示例，不含 Unity 依赖。

## 结构

```
src/MiMieMVVM.Core/
├── Feature/
│   ├── View/           IView  IView<TViewModel>
│   ├── ViewModel/      IViewModel  IViewModel<TModel>
│   ├── Model/          IModel  IModelConfig  IModelState
│   └── CrossModule/    ICrossBusinessModuleService  BusinessModuleHub
└── Demo/
    ├── InteranlDemo/   模块内 V → VM → M 示例
    └── CrossDemo/      跨模块 BusinessModuleHub 示例
```

## 依赖方向

```
View → ViewModel → Model
         ↓
BusinessModuleHub → ICrossBusinessModuleService（跨模块，不经过 View）
```

| 层 | 接口 | 职责 |
|---|---|---|
| View | `IView` / `IView<TViewModel>` | 表现与输入，`Bind` / `Unbind` ViewModel |
| ViewModel | `IViewModel` / `IViewModel<TModel>` | 用例编排、生命周期，`Initialize` / `Shutdown` |
| Model | `IModel` | 层标记 |
| Model | `IModelConfig` | 只读配表条目 `ConfigId` `Name` |
| Model | `IModelState` | 运行时状态标记 |
| Cross | `ICrossBusinessModuleService` | 跨模块业务服务标记 |
| Cross | `BusinessModuleHub` | 业务 Cross 服务注册与查找 |

## 泛型约定

**View 与 ViewModel 配对**

```csharp
public interface IView<TViewModel> : IView
    where TViewModel : class, IViewModel
{
    new TViewModel ViewModel { get; }
    void Bind(TViewModel viewModel);
}
```

**ViewModel 与 Model 配对**

```csharp
public interface IViewModel<TModel> : IViewModel
    where TModel : IModel
{
    TModel Model { get; }
}
```

- 基接口 `IViewModel` **不暴露** `Model`，避免 View 通过基接口绕过 VM 直接碰 M
- 基接口 `IView` **暴露** `ViewModel`，View 本来就需要持有 VM

## 跨模块通信

框架基础设施（对象池、音频、UI、存档）由 [HakiSheep-Framework](https://github.com/Haki-sheep) 的 `ModuleHub` 管理。

**业务跨模块**（背包↔商店、快捷互转等）走 `BusinessModuleHub`，与框架层分离：

```csharp
// 启动时登记
BusinessModuleHub.Instance.RegisterBusinessModule<IGoldCrossService>(new GoldCrossService());

// 模块 VM 内调用
var cross = BusinessModuleHub.Instance.GetBusinessModule<IGoldCrossService>();
cross.TryTransfer("Bag", "Shop", price);
```

域接口继承 `ICrossBusinessModuleService`，具体方法在域接口里扩展。

## 存档

持久化不放在 Model 契约内，统一使用 [MiMieSaver](https://github.com/Haki-sheep/MmCSharp-Saver) 的 `IArchiveModule` / `IArchiveMgr`。

## Demo

### 模块内：`Demo/InteranlDemo`

```
点击 → CounterView → CounterViewModel.OnAddClick() → CounterState.Increment()
                ↑______________ RefreshUI ______________|
```

```csharp
int result = MiMieMVVM.Demo.Internal.InternalDemoRunner.Run(); // 返回 2
```

### 跨模块：`Demo/CrossDemo`

```
ShopViewModel.TryBuyItem → BusinessModuleHub → IGoldCrossService.TryTransfer
BagViewModel.GetGold     → BusinessModuleHub → IGoldCrossService.GetBalance
```

```csharp
int bagGold = MiMieMVVM.Demo.Cross.CrossDemoRunner.Run(); // Bag 100 买 30，返回 70
```

## 构建

```bash
dotnet build src/MiMieMVVM.Core/MiMieMVVM.Core.csproj -c Release
```

输出：`src/MiMieMVVM.Core/bin/Release/netstandard2.1/MiMieMVVM.dll`

## Unity 接入

1. 将 `MiMieMVVM.dll` 放入 Unity 项目 `Plugins` 或程序集引用
2. View 实现 `IView<TViewModel>`（MonoBehaviour）
3. ViewModel 实现 `IViewModel<TModel>`，内部持有 State
4. Config 实现 `IModelConfig`（通常 `[Serializable]` + SO 列表）
5. 跨模块服务实现 `ICrossBusinessModuleService` 子接口，启动时 `RegisterBusinessModule`

复杂 UI（如网格拖拽）可继续用操作结果（OpReport）驱动 View，简单 UI（菜单、HUD）可逐步接入完整 Bind 流程。

## 命名空间

- `MiMieMVVM` — 核心接口
- `MiMieMVVM.Demo.Internal` — 模块内示例
- `MiMieMVVM.Demo.Cross` — 跨模块示例
