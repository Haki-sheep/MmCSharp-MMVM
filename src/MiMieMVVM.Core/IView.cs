namespace MiMieMVVM
{
    /// <summary>
    /// View 层契约
    /// 表现与输入绑定 ViewModel
    /// </summary>
    public interface IView
    {
        /// <summary>
        /// 绑定 ViewModel
        /// </summary>
        void Bind(IViewModel viewModel);

        /// <summary>
        /// 解除绑定
        /// </summary>
        void Unbind();
    }
}
