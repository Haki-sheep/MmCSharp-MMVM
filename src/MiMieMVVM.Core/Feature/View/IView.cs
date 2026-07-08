namespace MiMieMVVM
{
    /// <summary>
    /// View 层契约
    /// 表现与输入绑定 ViewModel
    /// </summary>
    public interface IView
    {
        IViewModel ViewModel { get; }
        void Bind(IViewModel viewModel);
        void Unbind();
    }
}
