using Autofac;
using System;
using System.Linq;
using System.Reflection;

namespace ArmaOps.Common
{
    public static class DependencyInjection
    {
        static IContainer _container;
        public static IContainer Container
        {
            get => _container;
            set => _container ??= value;
        }
    }

    public enum InjectionPoint
    {
        ViewStart,
        ViewLayout
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class InjectAttribute : Attribute
    {
        public InjectionPoint InjectionPoint { get; }

        public InjectAttribute(InjectionPoint injectionPoint = InjectionPoint.ViewStart)
        {
            InjectionPoint = injectionPoint;
        }
    }

    public class InjectOnCreateAttribute : InjectAttribute
    {
        public InjectOnCreateAttribute() : base(InjectionPoint.ViewStart)
        {
        }
    }

    public class InjectOnCreateViewAttribute : InjectAttribute
    {
        public InjectOnCreateViewAttribute() : base(InjectionPoint.ViewLayout)
        {
        }
    }

    public static class AutofacExtensions
    {
        public static void Inject(this ILifetimeScope scope, object obj, InjectionPoint injectionPoint = InjectionPoint.ViewStart)
        {
            var type = obj.GetType();

            while (type != null)
            {
                var propertiesToInject = type.GetRuntimeProperties().Where(x => x.GetCustomAttributes().Any(a => a is InjectAttribute && (a as InjectAttribute).InjectionPoint == injectionPoint));//  .CustomAttributes.Any(y => y.AttributeType.Name == nameof(InjectAttribute)));

                foreach (var property in propertiesToInject)
                {
                    var dependency = scope.Resolve(property.PropertyType);
                    property.SetValue(obj, dependency);
                }

                var fieldsToInject = type.GetRuntimeFields().Where(x => x.GetCustomAttributes().Any(a => a is InjectAttribute && (a as InjectAttribute).InjectionPoint == injectionPoint));

                foreach (var field in fieldsToInject)
                {
                    var dependency = scope.Resolve(field.FieldType);
                    field.SetValue(obj, dependency);
                }

                type = type.GetTypeInfo().BaseType;
            }
        }
    }
}
