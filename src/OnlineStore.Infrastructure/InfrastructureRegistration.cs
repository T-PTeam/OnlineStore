using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OnlineStore.Core.Common;
using OnlineStore.Core.Domain.Categories.Common;
using OnlineStore.Core.Domain.Products.Common;
using OnlineStore.Infrastructure.Core.Common;
using OnlineStore.Infrastructure.Core.Domain.Categories.Common;
using OnlineStore.Infrastructure.Core.Domain.Products.Common;

namespace OnlineStore.Infrastructure;

public static class InfrastructureRegistration
{
    public static void AddInfrastructureRegistration(this IServiceCollection service)
    {
        service.AddMediatR(typeof(InfrastructureRegistration));

        //UnitOfWork
        service.AddScoped<IUnitOfWork, UnitOfWork>();

        //Repository
        service.AddScoped<IProductRepository, ProductRepository>();
        service.AddScoped<ICategoryRepository, CategoryRepository>();

        //Checkers
        service.AddScoped<IProductPriceMustBePositiveChecker, ProductPriceMustBePositiveChecker>();
    }
}