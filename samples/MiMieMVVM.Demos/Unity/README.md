# Demo3 Unity 引擎调用

本 Demo 展示 Unity 下 `View → ViewModel → Model` 的手动绑定写法。

源码：`Demo3UnityBagView.cs`（`#if MIMIE_UNITY` 包裹，纯 .NET 工程不编译）。

**不进 UPM**：`samples/` 只随完整仓库 git 下载；游戏工程用 UPM 装的是 `unity/` 包。需要跑本 Demo 时，从本仓库拷脚本到自己的 `Assets`。

## 为何用宏

本仓库 Core 的 `asmdef` 设置了 `noEngineReferences: true`，不能引用 `UnityEngine`。  
Demo 用 `MIMIE_UNITY` 宏隔离引擎 API，拷入游戏工程后定义该宏即可编译。

## 使用步骤

1. 克隆本仓库，从 `samples/MiMieMVVM.Demos/Unity/` 拷 `Demo3UnityBagView.cs` 到游戏工程 `Assets`。
2. 游戏工程已通过 UPM 引用本包（见根目录 README）。
3. 在该脚本所在程序集的 asmdef 中增加 Version Defines，或 Player Settings → Scripting Define Symbols 加入：

```
MIMIE_UNITY
```

asmdef 示例：

```json
{
  "name": "Game.UI",
  "references": ["MiMieMVVM"],
  "versionDefines": [
    {
      "name": "Unity",
      "expression": "2021.3",
      "define": "MIMIE_UNITY"
    }
  ]
}
```

4. 场景中建 UI：`Text` + `Button`，挂上 `BagView`，拖引用。
5. 运行后点按钮，金币每次 -10。

## 调用链

```
玩家点 Button
  → onClick → bagVm.Buy()
  → Model 改 Gold → OnGoldChanged
  → BagView.RefreshGold → Text.text
```

与 Demo1 相同分层，只是 View 从控制台换成 `MonoBehaviour` + UGUI。
