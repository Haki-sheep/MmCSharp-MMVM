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

    /// <summary>
    /// ViewModel 层泛型契约
    /// 约束 ViewModel 与 Model 配对
    /// </summary>
    public interface IViewModel<TModel> : IViewModel
        where TModel : IModel
    {
        /// <summary>
        /// Model 引用
        /// </summary>
        TModel Model { get; }
    }
}
