using System.Web.Http.Dependencies;

namespace Tomaszkiewicz.BotFramework.WebApi.Extensions
{
    public static class DependencyScopeExtensions
    {
        public static T Resolve<T>(this IDependencyScope scope)
        {
            return (T) scope.GetService(typeof (T));
        }
    }
}