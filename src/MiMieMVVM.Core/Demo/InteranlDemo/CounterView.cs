namespace MiMieMVVM.Demo.Internal
{
    /// <summary>
    /// 示例 View 实现 IView 只通过 IViewModel 绑定
    /// </summary>
    public class CounterView : IView
    {
        /// <summary> UI 上显示的计数 </summary>
        public int ShownCount { get; private set; }

        public IViewModel ViewModel { get; private set; } = null!;

        /// <summary>
        /// 绑定 ViewModel
        /// </summary>
        public void Bind(IViewModel viewModel)
        {
            Unbind();
            ViewModel = viewModel;
            ViewModel.Initialize();
            RefreshUI();
        }

        /// <summary>
        /// 解除绑定
        /// </summary>
        public void Unbind()
        {
            if (ViewModel is null)
                return;

            ViewModel.Shutdown();
            ViewModel = null!;
            ShownCount = 0;
        }

        /// <summary>
        /// 模拟按钮点击
        /// </summary>
        public void SimulateAddClick()
        {
            if (ViewModel is CounterViewModel counterViewModel)
            {
                counterViewModel.OnAddClick();
                RefreshUI();
            }
        }

        /// <summary>
        /// 从 VM 展示属性刷新 UI
        /// </summary>
        private void RefreshUI()
        {
            if (ViewModel is CounterViewModel counterViewModel)
                ShownCount = counterViewModel.DisplayCount;
        }
    }
}
