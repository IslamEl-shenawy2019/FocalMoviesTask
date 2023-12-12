using Focal.Core.Interfaces.Repository;
using Focal.Core.Interfaces.Services;
using Focal.Core.Profiles;
using Focal.Core.Services;
using Focal.Infrastructure.Repository;

namespace FocalMovies.Extiensions
{
    public static class ServiceExtensions
    {
        public static void AddProfileMapper(this IServiceCollection services) 
        {
            services.AddAutoMapper(typeof(MetaDataProfile));
        }

        public static void AddRepos(this IServiceCollection services) 
        {
            services.AddScoped<IMetaDataRepository, MetaDataRepository>();
        }

        public static void AddCoreServices(this IServiceCollection services) 
        {
            services.AddScoped<IMetaDataService, MetaDataService>();
        }
    }
}
