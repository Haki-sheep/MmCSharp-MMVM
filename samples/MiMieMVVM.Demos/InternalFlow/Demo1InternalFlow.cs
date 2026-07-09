using System;

namespace MiMieMVVM.Demos.InternalFlow
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
    /// 持有 Model 对外暴露用例与状态变更事件
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
    /// 控制台伪 View
    /// 模拟 Unity 下 Bind 订阅与按钮调用
    /// </summary>
    public class ConsoleBagView : IView
    {
        /// <summary> 具体 VM </summary>
        private BagViewModel bagVm;

        public IViewModel ViewModel { get; private set; }

        /// <summary>
        /// 绑定 ViewModel
        /// </summary>
        public void Bind(IViewModel viewModel)
        {
            bagVm = (BagViewModel)viewModel;
            ViewModel = viewModel;
            bagVm.OnGoldChanged += RefreshGold;
            RefreshGold(bagVm.Gold);
        }

        /// <summary>
        /// 解绑
        /// </summary>
        public void Unbind()
        {
            if (bagVm != null)
                bagVm.OnGoldChanged -= RefreshGold;
            bagVm = null;
            ViewModel = null;
        }

        /// <summary>
        /// 模拟按钮点击
        /// </summary>
        public void ClickBuy()
        {
            bagVm.Buy();
        }

        private void RefreshGold(int gold)
        {
            Console.WriteLine($"[BagView] Gold = {gold}");
        }
    }

    /// <summary>
    /// Demo1 入口
    /// View → ViewModel → Model 单模块内部流程
    /// </summary>
    public static class Demo1InternalFlow
    {
        /// <summary>
        /// 运行
        /// </summary>
        public static void Run()
        {
            Console.WriteLine("=== Demo1 C# 内部调用流程 ===");

            var state = new BagState();
            var vm = new BagViewModel(state);
            var view = new ConsoleBagView();

            vm.Initialize();
            view.Bind(vm);

            // 模拟玩家点购买
            view.ClickBuy();
            view.ClickBuy();

            view.Unbind();
            vm.Shutdown();

            Console.WriteLine();
        }
    }
}
