namespace MiMieMVVM
{
    /// <summary>
    /// ViewModel 层契约
    /// 模块用例入口与生命周期
    /// </summary>
    public interface IViewModel
    {
        /// <summary>
        /// 初始化
        /// </summary>
        void Initialize();

        /// <summary>
        /// 关闭
        /// </summary>
        void Shutdown();
    }
}
