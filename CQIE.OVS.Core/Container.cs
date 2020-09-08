using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.Windsor;
using Castle.MicroKernel;
using Castle.Windsor.Configuration.Interpreters;

namespace CQIE.OVS.Core
{
    public sealed class Container
    {
        /// <summary>
        /// 拦截器 
        /// </summary>
        private XmlInterpreter interpreter;

        /// <summary>
        /// IOC容器变量
        /// </summary>
        private WindsorContainer windsor;

        /// <summary>
        /// IOC窗口实例
        ///     静态私有变量
        ///     与Instance一起形成单例模式 
        /// </summary>
        private static readonly Container instance = new Container();

        /// <summary>
        /// 全局IOC窗口实例
        ///     单例模式
        /// </summary>
        public static Container Instance
        {
            get { return instance; }
        }
        public IKernel Kernel { get; private set; }

        private Container()
        {
            try
            {
                interpreter = new Castle.Windsor.Configuration.Interpreters.XmlInterpreter("Content/Config/ServiceConfig.xml");
                windsor = new WindsorContainer(interpreter);
                Kernel = windsor.Kernel;
            }
            catch (Exception E)
            {
                throw E;
            }
        }
        public T Resolve<T>()
        {
            return (T)Kernel.Resolve<T>();
        }
        public T Resolve<T>(string key)
        {
            return (T)Kernel.Resolve<T>(key);
        }
        public void Dispose()
        {
            Kernel.Dispose();
        }
    }
}
