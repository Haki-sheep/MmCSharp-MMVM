namespace MiMieMVVM.Demo.Cross
{
    /// <summary>
    /// 背包模块 VM 只读本模块逻辑 金币余额走 Cross 查询
    /// </summary>
    public class BagViewModel : IViewModel
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
        /// 查询背包金币
        /// </summary>
        public int GetGold()
        {
            var cross = BusinessModuleHub.Instance.GetBusinessModule<IGoldCrossService>();
            return cross.GetBalance("Bag");
        }
    }
}
