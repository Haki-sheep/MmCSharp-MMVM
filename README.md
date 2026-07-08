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

## 结构

```
unity/                  Unity Package Manager 包
src/MiMieMVVM.Core/     纯 .NET 构建 源码引用 unity/Runtime
```

## 构建

```bash
dotnet build src/MiMieMVVM.Core/MiMieMVVM.Core.csproj -c Release
```
