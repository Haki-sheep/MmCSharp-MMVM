namespace MiMieMVVM
{
    /// <summary>
    /// View 层契约
    /// 表现与输入绑定 ViewModel
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// 当前绑定的 ViewModel
        /// </summary>
        IViewModel ViewModel { get; }

        /// <summary>
        /// 绑定 ViewModel
        /// </summary>
        void Bind(IViewModel viewModel);

        /// <summary>
        /// 解除绑定
        /// </summary>
        void Unbind();
    }

    /// <summary>
    /// View 层泛型契约
    /// 约束 View 与 ViewModel 配对
    /// </summary>
    public interface IView<TViewModel> : IView
        where TViewModel : class, IViewModel
    {
        /// <summary>
        /// 当前绑定的 ViewModel
        /// </summary>
        new TViewModel ViewModel { get; }

        /// <summary>
        /// 绑定 ViewModel
        /// </summary>
        void Bind(TViewModel viewModel);
    }
}
