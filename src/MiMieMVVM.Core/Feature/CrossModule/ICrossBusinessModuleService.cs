using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiMieMVVM
{
    /// <summary>
    /// 跨模块服务应该继承此接口
    /// 在里面写跨模块服务的方法
    /// </summary>
    public interface ICrossBusinessModuleService
    {
    }

    public class BusinessModuleHub
    {
        private static BusinessModuleHub instance = new BusinessModuleHub();
        public static BusinessModuleHub Instance => instance ??= new BusinessModuleHub();
        private readonly Dictionary<Type, ICrossBusinessModuleService> bsDict = new();
        public void RegisterBusinessModule<T>(T service) where T : class, ICrossBusinessModuleService
        {
            bsDict[typeof(T)] = service;
        }
        public T GetBusinessModule<T>() where T : class, ICrossBusinessModuleService
        {
            return (T)bsDict[typeof(T)];
        }

        public void UnregisterBusinessModule<T>() where T : class, ICrossBusinessModuleService
        {
            bsDict.Remove(typeof(T));
        }
    }
}