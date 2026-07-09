# MiMieMVVM

Unity 功能模块 MVVM 分层接口约束（`netstandard2.1`）

## 依赖方向

```
View → ViewModel → Model
```

View 持有并绑定 ViewModel；ViewModel **不持有** View。这是 MVVM / Presentation Model 的核心，不是 MVP（Presenter 反向驱动 View）。

## 为何没有属性通知 / 命令接口

本库面向 Unity，**刻意不提供** `INotifyPropertyChanged`、`ICommand` 一类 WPF 式契约。

| WPF / XAML 惯例 | Unity 落地 |
|---|---|
| `INotifyPropertyChanged` 通用绑定 | ViewModel 用 `event` / 回调推送状态变化 |
| `ICommand` 绑定按钮 | `Button.onClick.AddListener(vm.方法)` |
| 声明式 XAML Binding | `IView.Bind` 订阅、`Unbind` 退订（手动绑定） |

原因：

1. Unity 没有 XAML 绑定引擎，`ICommand` / `INPC` 解决不了平台缺口，只会多一层空壳。
2. MVVM 的内核是「View 观察面向界面的状态对象」，不是「必须叫 Command」。方法监听与 Command 语义等价：输入进 ViewModel，业务不进 View。
3. 绑定胶水写在 `Bind` 里属于 Unity 常见的手动绑定，不改变依赖方向，也不等于 MVP Supervising Controller。

推荐写法：ViewModel 属性变更发事件；View 在 `Bind` 订阅、在 `Unbind` 退订；按钮直接监听 ViewModel 公开方法。

## 接口

| 层 | 接口 | 说明 |
|---|---|---|
| View | `IView` | `ViewModel` `Bind` `Unbind` |
| ViewModel | `IViewModel` | `Initialize` `Shutdown` |
| Model | `IModel` | 层标记 |
| Model | `IModelConfig` | 配表 `ConfigId` `Name` |
| Model | `IModelState` | 运行时状态标记 |
| Cross | `ICrossBusinessModuleService` | 跨模块业务服务标记 |
| Cross | `BusinessModuleHub` | Cross 服务 `Register` `Get` `Unregister` |

## Unity 安装（UPM）

在 `Packages/manifest.json` 添加：

```json
"com.hakisheep.mm-mvvm": "git@github.com:Haki-sheep/MmCSharp-MMVM.git?path=unity"
```

HTTPS：

```json
"com.hakisheep.mm-mvvm": "https://github.com/Haki-sheep/MmCSharp-MMVM.git?path=unity"
```

## Demo

`samples/` **不进 UPM**。UPM 只装 `unity/`；Demo 随完整仓库 git 克隆一起拿到，作学习参考。

| Demo | 路径 | 说明 |
|---|---|---|
| 1 内部调用 | `samples/MiMieMVVM.Demos/InternalFlow` | View → VM → Model 单模块流程 |
| 2 跨模块 | `samples/MiMieMVVM.Demos/CrossModule` | `BusinessModuleHub` 解耦调用 |
| 3 Unity | `samples/MiMieMVVM.Demos/Unity` | UGUI 手动绑定（`MIMIE_UNITY` 宏） |

运行 Demo1 / Demo2：

```bash
dotnet run --project samples/MiMieMVVM.Demos/MiMieMVVM.Demos.csproj
```

Demo3 说明见 `samples/MiMieMVVM.Demos/Unity/README.md`。

## 结构

```
unity/                    UPM 包（仅此目录被 ?path=unity 导入）
src/MiMieMVVM.Core/       纯 .NET 构建 源码引用 unity/Runtime
samples/MiMieMVVM.Demos/  随 git 仓库提供 不进 UPM
```

## 构建

```bash
dotnet build MieMieMVVM.sln -c Release
```
