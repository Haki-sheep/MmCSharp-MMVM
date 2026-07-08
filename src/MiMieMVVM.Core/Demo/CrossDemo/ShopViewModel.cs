namespace MiMieMVVM.Demo.Cross
{
    /// <summary>
    /// 商店模块 VM 购买时走跨模块服务
    /// </summary>
    public class ShopViewModel : IViewModel
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialize()
        {
        }

        /// <summary>
        /// 关闭
        /// </summary>
        public void Shutdown()
        {
        }

        /// <summary>
        /// 购买物品 从背包模块扣金币
        /// </summary>
        public bool TryBuyItem(int price)
        {
            var cross = BusinessModuleHub.Instance.GetBusinessModule<IGoldCrossService>();
            return cross.TryTransfer("Bag", "Shop", price);
        }
    }
}
