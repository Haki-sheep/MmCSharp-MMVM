# MiMieMVVM

Unity 功能模块 MVVM 分层接口约束

## 结构

```
src/MiMieMVVM.Core/     netstandard2.1 核心接口
```

## 三层约定

| 层 | 接口 | 职责 |
|---|---|---|
| Model | `IModel` | Config State Save 数据与规则 |
| ViewModel | `IViewModel` | 用例编排 对外 API 生命周期 |
| View | `IView` | 表现与输入 Bind Unbind ViewModel |

## 依赖方向

```
View → ViewModel → Model
```

## 构建

```bash
dotnet build src/MiMieMVVM.Core/MiMieMVVM.Core.csproj -c Release
```
