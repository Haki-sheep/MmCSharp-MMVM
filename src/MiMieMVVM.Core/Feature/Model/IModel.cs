namespace MiMieMVVM
{
    /// <summary>
    /// Model 层标记
    /// Config State Save 等数据与规则
    /// </summary>
    public interface IModel
    {

    }

    /// <summary>
    /// 配置相关应该继承此接口
    /// </summary>
    public interface IModelConfig : IModel
    {
        int ConfigId { get; }
        string Name { get; }

        string GetName() => Name;
        int GetConfigId() => ConfigId;
    }


    public interface IModelSave : IModel
    {
        
    }
}
