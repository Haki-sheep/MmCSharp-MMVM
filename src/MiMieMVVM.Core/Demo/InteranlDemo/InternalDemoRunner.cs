namespace MiMieMVVM.Demo.Internal
{
    /// <summary>
    /// 模块内调用链演示 View → VM → State
    /// </summary>
    public static class InternalDemoRunner
    {
        /// <summary>
        /// 运行示例 返回最终 UI 显示值
        /// </summary>
        public static int Run()
        {
            var viewModel = new CounterViewModel();
            var view = new CounterView();

            view.Bind(viewModel);
            view.SimulateAddClick();
            view.SimulateAddClick();

            int result = view.ShownCount;
            view.Unbind();
            return result;
        }
    }
}
