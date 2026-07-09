// 本文件在纯 .NET 控制台工程中被 csproj 排除编译
// 拷入 Unity 工程后由 asmdef 定义 MIMIE_UNITY 即可编译
// 详见同目录 README.md

#if MIMIE_UNITY
using System;
using UnityEngine;
using UnityEngine.UI;

namespace MiMieMVVM.Demos.UnityFlow
{
    /// <summary>
    /// 背包运行时状态
    /// </summary>
    public class BagState : IModelState
    {
        /// <summary> 金币 </summary>
        public int Gold;
    }

    /// <summary>
    /// 背包 ViewModel
    /// </summary>
    public class BagViewModel : IViewModel
    {
        /// <summary> 运行时状态 </summary>
        private readonly BagState state;

        public int Gold => state.Gold;

        public event Action<int> OnGoldChanged;

        public BagViewModel(BagState state)
        {
            this.state = state;
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            state.Gold = 100;
            OnGoldChanged?.Invoke(state.Gold);
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public void Shutdown()
        {
            OnGoldChanged = null;
        }

        /// <summary>
        /// 购买
        /// </summary>
        public void Buy()
        {
            state.Gold -= 10;
            OnGoldChanged?.Invoke(state.Gold);
        }
    }

    /// <summary>
    /// Unity View
    /// Bind 订阅事件 按钮直接监听 VM 方法
    /// </summary>
    public class BagView : MonoBehaviour, IView
    {
        /// <summary> 金币文本 </summary>
        [SerializeField] private Text goldText;
        /// <summary> 购买按钮 </summary>
        [SerializeField] private Button buyButton;
        /// <summary> 具体 VM </summary>
        private BagViewModel bagVm;

        public IViewModel ViewModel { get; private set; }

        private void Start()
        {
            var state = new BagState();
            var vm = new BagViewModel(state);
            vm.Initialize();
            Bind(vm);
        }

        private void OnDestroy()
        {
            Unbind();
            if (ViewModel != null)
                ViewModel.Shutdown();
        }

        /// <summary>
        /// 绑定 ViewModel
        /// </summary>
        public void Bind(IViewModel viewModel)
        {
            bagVm = (BagViewModel)viewModel;
            ViewModel = viewModel;
            bagVm.OnGoldChanged += RefreshGold;
            buyButton.onClick.AddListener(bagVm.Buy);
            RefreshGold(bagVm.Gold);
        }

        /// <summary>
        /// 解绑
        /// </summary>
        public void Unbind()
        {
            if (bagVm != null)
            {
                bagVm.OnGoldChanged -= RefreshGold;
                buyButton.onClick.RemoveListener(bagVm.Buy);
            }
            bagVm = null;
            ViewModel = null;
        }

        private void RefreshGold(int gold)
        {
            goldText.text = gold.ToString();
        }
    }
}
#endif
