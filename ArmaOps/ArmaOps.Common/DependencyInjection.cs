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
        ViewLifetime,
        ViewCreate,
        ViewLayout
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class InjectAttribute : Attribute
    {
        public InjectionPoint InjectionPoint { get; }

        public InjectAttribute(InjectionPoint injectionPoint = InjectionPoint.ViewLifetime)
        {
            InjectionPoint = injectionPoint;
        }
    }

    public class InjectViewLifetimeAttribute : InjectAttribute
    {
        public InjectViewLifetimeAttribute() : base(InjectionPoint.ViewLifetime)
        {
        }
    }

    public class InjectOnCreateAttribute : InjectAttribute
    {
        public InjectOnCreateAttribute() : base(InjectionPoint.ViewCreate)
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
        public static void Inject(this ILifetimeScope scope, object obj, InjectionPoint injectionPoint = InjectionPoint.ViewLifetime)
        {
            var type = obj.GetType();

            while (type != null)
            {
                var propertiesToInject = type.GetRuntimeProperties().Where(x => x.GetCustomAttributes().Any(a => a is InjectAttribute && (a as InjectAttribute).InjectionPoint == injectionPoint));

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
