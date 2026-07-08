namespace MiMieMVVM
{
    /// <summary>
    /// Model 层标记
    /// </summary>
    public interface IModel
    {
    }

    /// <summary>
    /// 配表条目
    /// </summary>
    public interface IModelConfig : IModel
    {
        int ConfigId { get; }
        string Name { get; }
    }

    /// <summary>
    /// 运行时状态标记
    /// </summary>
    public interface IModelState : IModel
    {
        
    }
}
