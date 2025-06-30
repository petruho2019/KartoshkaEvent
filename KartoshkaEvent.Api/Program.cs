using Auction.JwtProvider;
using KartoshkaEvent.Api;
using KartoshkaEvent.Api.AuthHandler;
using KartoshkaEvent.Api.TestRequests;
using KartoshkaEvent.Application;
using KartoshkaEvent.CacheService;
using KartoshkaEvent.DataAccess;
using KartoshkaEvent.MailService;
using KartoshkaEvent.YooKassaPayment;
using Microsoft.AspNetCore.Authentication;
using System.Diagnostics;
using System.Reflection;

internal class Program
{
    private async static Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var services = builder.Services;
        var configuration = builder.Configuration;

        services.AddHttpClient();
        services.AddHttpContextAccessor();

        services
            .AddApplicationLayer()
            .AddDataAccess(configuration)
            .AddCache(configuration)
            .AddJwtProvider()
            .AddMailService()
            .AddYooKassaService();

        services.AddControllers();

        services.AddAuthentication(opt =>
        {
            opt.DefaultScheme = "AccessRefresh";
            opt.DefaultChallengeScheme = "AccessRefresh";
        }).AddScheme<AuthenticationSchemeOptions, AccessRefreshAuthenticationHandler>("AccessRefresh", opt => { });

        services.AddAuthorization();

        builder.Services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen();

        builder.Services.AddCors(conf =>
        {
            conf.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyHeader();
                policy.AllowAnyMethod();
                policy.AllowAnyOrigin();
            });
        });

        services
            .AddScoped<ITestRequestValidationService, TestRequestValidationService>();

        var app = builder.Build();

        app.UseAuthentication();

        app.UseRouting();

        app.UseCors("AllowAll");

        app.UseAuthorization();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(opt =>
            {
                opt.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
                opt.RoutePrefix = string.Empty;
            });
        } 

        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<KartoshkaEventContext>();
            await DbInitializer.Initialize(context);
        }

        app.UseStaticFiles();

        app.MapControllers();

        app.Run();


    }
}