namespace MiMieMVVM.Demo.Cross
{
    /// <summary>
    /// 跨模块调用链演示 Shop VM → BusinessModuleHub → GoldCrossService
    /// </summary>
    public static class CrossDemoRunner
    {
        /// <summary>
        /// 运行示例 返回购买后背包剩余金币
        /// </summary>
        public static int Run()
        {
            var goldService = new GoldCrossService();
            goldService.SetBalance("Bag", 100);

            BusinessModuleHub.Instance.RegisterBusinessModule<IGoldCrossService>(goldService);

            var shopViewModel = new ShopViewModel();
            var bagViewModel = new BagViewModel();

            shopViewModel.Initialize();
            bagViewModel.Initialize();

            bool bought = shopViewModel.TryBuyItem(30);
            int bagGold = bagViewModel.GetGold();

            shopViewModel.Shutdown();
            bagViewModel.Shutdown();
            BusinessModuleHub.Instance.UnregisterBusinessModule<IGoldCrossService>();

            return bought ? bagGold : -1;
        }
    }
}
