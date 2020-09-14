using Microsoft.AspNetCore.Hosting;

namespace SysPlan.Application.Service
{
    public class HostingEnvironmentService
    {
        public static IHostingEnvironment Environment;
        public static void Load(IHostingEnvironment env)
        {
            Environment = env;
        }
    }
}
