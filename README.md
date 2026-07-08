# MiMieMVVM

Unity 功能模块 MVVM 分层接口约束（`netstandard2.1`）

## 依赖方向

```
View → ViewModel → Model
```

## 接口

| 层 | 接口 | 说明 |
|---|---|---|
| View | `IView` | `ViewModel` `Bind` `Unbind` |
| View | `IView<TViewModel>` | 与 ViewModel 类型配对 |
| ViewModel | `IViewModel` | `Initialize` `Shutdown` |
| ViewModel | `IViewModel<TModel>` | 与 Model 类型配对，暴露 `Model` |
| Model | `IModel` | 层标记 |
| Model | `IModelConfig` | 配表 `ConfigId` `Name` |
| Model | `IModelState` | 运行时状态标记 |
| Cross | `ICrossBusinessModuleService` | 跨模块业务服务标记 |
| Cross | `BusinessModuleHub` | Cross 服务 `Register` `Get` `Unregister` |

## 结构

```
src/MiMieMVVM.Core/Feature/
├── View/
├── ViewModel/
├── Model/
└── CrossModule/
```

## 构建

```bash
dotnet build src/MiMieMVVM.Core/MiMieMVVM.Core.csproj -c Release
```
