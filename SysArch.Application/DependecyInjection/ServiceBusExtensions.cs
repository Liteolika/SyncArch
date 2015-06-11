using MassTransit;
using StructureMap;
using SysArch.Application.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SysArch.Application.DependecyInjection
{
    public static class ServiceBusExtensions
    {

        public static void LoadHandlersFromContainer(this IServiceBus bus, IContainer container, Type genericType)
        {
            var handlerInterfaces = GetHandlerInterfaces(container, genericType);

            foreach (var handlerInterface in handlerInterfaces)
            {
                var msgType = handlerInterface.GetGenericArguments()[0];
                var handlerInstance =
                    container.ForGenericType(genericType)
                              .WithParameters(msgType)
                              .GetInstanceAs<object>();

                var action = GetHandleAction(handlerInstance, msgType);

                Subscribe(bus, msgType, action);
            }

        }

        private static IEnumerable<Type> GetHandlerInterfaces(IContainer container, Type genericType)
        {
            return container.Model.PluginTypes.
                              Where(
                                  p =>
                                  p.PluginType.IsGenericType &&
                                  p.PluginType.GetGenericTypeDefinition() == genericType).
                              Select(p => p.PluginType).ToArray();
        }

        private static Delegate GetHandleAction(object handlerInstance, Type msgType)
        {

            var handlerMethod = handlerInstance.GetType().GetMethod("Handle");
            var action = Delegate.CreateDelegate(typeof(Action<>).MakeGenericType(msgType),
                                                 handlerInstance, handlerMethod);
            return action;
        }

        private static void Subscribe(IServiceBus bus, Type msgType, object action)
        {
            var subMethod = typeof(HandlerSubscriptionExtensions)
                .GetMethods()
                .Single(m => 
                    m.Name == "SubscribeHandler" && 
                    m.GetParameters().Length == 2)
                .MakeGenericMethod(msgType);

            subMethod.Invoke(bus, new object[] { bus, action });
        }

    }
}
