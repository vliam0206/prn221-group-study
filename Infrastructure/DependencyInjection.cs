using Application.IServices;
using Infrastructure.IRepositories;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Comments;
using Infrastructure.Repositories.Groups;
using Infrastructure.Repositories.Posts;
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
        services.AddScoped(typeof(ICommentRepository), typeof(CommentRepository));
        services.AddScoped(typeof(PostRepository));
        services.AddScoped(typeof(GroupRepository));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        return services;
    }
}
