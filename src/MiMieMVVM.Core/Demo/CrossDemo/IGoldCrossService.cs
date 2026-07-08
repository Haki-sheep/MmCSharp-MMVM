namespace MiMieMVVM.Demo.Cross
{
    /// <summary>
    /// 跨模块金币服务 域接口
    /// </summary>
    public interface IGoldCrossService : ICrossBusinessModuleService
    {
        /// <summary>
        /// 模块间转移金币
        /// </summary>
        bool TryTransfer(string fromModule, string toModule, int amount);

        /// <summary>
        /// 查询模块余额
        /// </summary>
        int GetBalance(string moduleName);
    }
}
