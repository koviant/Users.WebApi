using Ninject;

using Users.BLL;
using Users.BLL.Base;
using Users.DAL.Base;
using Users.DAL.EF;

namespace DependencyResolver
{
    public static class DependencyResolver
    {
        public static void ResolveDependencies(this IKernel kernel)
        {
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IUserRepository>().To<EFUserRepository>();
        }
    }
}
