using Energy.Contracts.Application.Contracts.Update;
using Energy.Contracts.Application.Repositories;
using Energy.Contracts.Infrastructure.Repositories;
using Energy.Contracts.Infrastructure.Validators;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Energy.Contracts.Infrastructure;

public static class WebApplicationBuilderExtension
{
    public static WebApplicationBuilder AddContractModule(this WebApplicationBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.AddPersistence();
        builder.AddServices();
        builder.AddControllers();

        return builder;
    }

    private static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
    {

        builder.Services.AddScoped(sp =>
        {
            var config = sp.GetRequiredService<IConfiguration>();
            var connectionString = config.GetConnectionString("DefaultConnection");
            var connection = new NpgsqlConnection(connectionString);
            return connection;
        });

        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<IContractRepository, ContractRepository>();
        builder.Services.AddScoped<IRateRepository, RateRepository>();

        return builder;

    }

    private static WebApplicationBuilder AddServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddMediatR(options =>
        {
            options.RegisterServicesFromAssemblies(typeof(WebApplicationBuilderExtension).Assembly);
        });

        builder.Services.AddValidatorsFromAssemblyContaining<UpdateContractValidator>();

        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return builder;
    }

    private static WebApplicationBuilder AddControllers(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();
        return builder;
    }

}