namespace MiMieMVVM.Demo.Internal
{
    /// <summary>
    /// 示例 View 只认识 VM 不直接改 State
    /// </summary>
    public class CounterView : IView<CounterViewModel>
    {
        /// <summary> UI 上显示的计数 </summary>
        public int ShownCount { get; private set; }

        public CounterViewModel ViewModel { get; private set; } = null!;

        IViewModel IView.ViewModel => ViewModel;

        /// <summary>
        /// 绑定 ViewModel
        /// </summary>
        void IView.Bind(IViewModel viewModel)
        {
            Bind((CounterViewModel)viewModel);
        }

        /// <summary>
        /// 绑定 ViewModel
        /// </summary>
        public void Bind(CounterViewModel viewModel)
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
            ViewModel.OnAddClick();
            RefreshUI();
        }

        /// <summary>
        /// 从 VM 读 State 刷新 UI
        /// </summary>
        private void RefreshUI()
        {
            ShownCount = ViewModel.Model.Count;
        }
    }
}
