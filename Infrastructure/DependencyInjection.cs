using Application.IServices;
using Infrastructure.IRepositories;
using Infrastructure.IRepositories.Groups;
using Infrastructure.Repositories;
using Infrastructure.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastrucureService(this IServiceCollection services)
    {
        // Add all services of infrastructure
        //...
        services.AddScoped(typeof(IAccountRepository), typeof(AccountRepository));
        services.AddScoped(typeof(IPostRepository), typeof(PostRepository));
        services.AddScoped(typeof(IGroupRepository), typeof(AccountRepository));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}
