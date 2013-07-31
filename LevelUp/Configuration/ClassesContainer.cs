using System.Reflection;
using Autofac;

namespace Present.WebMvc.Configuration
{
    public interface IClassesContainer
    {
        IContainer Container { get; }

        T Resolve<T>();
    }

    public class ClassesContainer : IClassesContainer
    {
        public IContainer Container { get; set; }

        private readonly ContainerBuilder _builder = new ContainerBuilder();

        public ClassesContainer()
        {
            var builder = new ContainerBuilder();

            var assemblies = new[]
                {
                    Assembly.Load("Present.WebMvc"),
                    Assembly.Load("Present.Infrastructure.Services"),
                    Assembly.Load("Present.Domain")
                };

            _builder.RegisterAssemblyModules(assemblies);

            Container = builder.Build();
        }

        public T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
    }
}
