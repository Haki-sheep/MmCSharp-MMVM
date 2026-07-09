using System;

namespace MiMieMVVM.Demos.CrossModule
{
    /// <summary>
    /// 钱包跨模块服务
    /// 供其他模块通过 Hub 查找
    /// </summary>
    public interface IWalletService : ICrossBusinessModuleService
    {
        int Gold { get; }
        bool TrySpend(int cost);
        event Action<int> OnGoldChanged;
    }

    /// <summary>
    /// 钱包服务实现
    /// </summary>
    public class WalletService : IWalletService
    {
        /// <summary> 金币 </summary>
        private int gold = 50;

        public int Gold => gold;

        public event Action<int> OnGoldChanged;

        /// <summary>
        /// 尝试扣费
        /// </summary>
        public bool TrySpend(int cost)
        {
            if (gold < cost)
                return false;
            gold -= cost;
            OnGoldChanged?.Invoke(gold);
            return true;
        }
    }

    /// <summary>
    /// 商店 ViewModel
    /// 不直接依赖钱包模块实现 只依赖 Cross 接口
    /// </summary>
    public class ShopViewModel : IViewModel
    {
        /// <summary> 钱包服务 </summary>
        private IWalletService wallet;

        public event Action<string> OnMessage;

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
            wallet = BusinessModuleHub.Instance.GetBusinessModule<IWalletService>();
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public void Shutdown()
        {
            wallet = null;
            OnMessage = null;
        }

        /// <summary>
        /// 购买商品
        /// </summary>
        public void Purchase()
        {
            bool ok = wallet.TrySpend(20);
            OnMessage?.Invoke(ok
                ? $"购买成功 剩余金币 {wallet.Gold}"
                : "金币不足");
        }
    }

    /// <summary>
    /// 控制台伪 View
    /// </summary>
    public class ConsoleShopView : IView
    {
        /// <summary> 具体 VM </summary>
        private ShopViewModel shopVm;

        public IViewModel ViewModel { get; private set; }

        /// <summary>
        /// 绑定 ViewModel
        /// </summary>
        public void Bind(IViewModel viewModel)
        {
            shopVm = (ShopViewModel)viewModel;
            ViewModel = viewModel;
            shopVm.OnMessage += Print;
        }

        /// <summary>
        /// 解绑
        /// </summary>
        public void Unbind()
        {
            if (shopVm != null)
                shopVm.OnMessage -= Print;
            shopVm = null;
            ViewModel = null;
        }

        /// <summary>
        /// 模拟购买点击
        /// </summary>
        public void ClickPurchase()
        {
            shopVm.Purchase();
        }

        private void Print(string msg)
        {
            Console.WriteLine($"[ShopView] {msg}");
        }
    }

    /// <summary>
    /// Demo2 入口
    /// 跨模块通过 BusinessModuleHub 解耦调用
    /// </summary>
    public static class Demo2CrossModule
    {
        /// <summary>
        /// 运行
        /// </summary>
        public static void Run()
        {
            Console.WriteLine("=== Demo2 跨模块调用 ===");

            var wallet = new WalletService();
            BusinessModuleHub.Instance.RegisterBusinessModule<IWalletService>(wallet);

            var vm = new ShopViewModel();
            var view = new ConsoleShopView();

            vm.Initialize();
            view.Bind(vm);

            view.ClickPurchase();
            view.ClickPurchase();
            view.ClickPurchase();

            view.Unbind();
            vm.Shutdown();
            BusinessModuleHub.Instance.UnregisterBusinessModule<IWalletService>();

            Console.WriteLine();
        }
    }
}
