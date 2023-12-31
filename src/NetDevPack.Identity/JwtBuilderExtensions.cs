﻿using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NetDevPack.Identity.Data;
using NetDevPack.Identity.Interfaces;
using NetDevPack.Identity.Jwt;
using NetDevPack.Security.Jwt.Core;
using NetDevPack.Security.Jwt.Core.Interfaces;

namespace Microsoft.Extensions.DependencyInjection;

public static class JwtBuilderExtensions
{
    public static IJwksBuilder AddNetDevPackIdentity<TIdentityUser, TKey>(this IServiceCollection services, Action<JwtOptions> options = null)
        where TIdentityUser : IdentityUser<TKey>
        where TKey : IEquatable<TKey>
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IJwtBuilder, JwtBuilderInject<TIdentityUser, TKey>>();
        return services.AddJwksManager(options);
    }

    public static IJwksBuilder AddNetDevPackIdentity<TIdentityUser>(this IServiceCollection services, Action<JwtOptions> options = null)
        where TIdentityUser : IdentityUser
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IJwtBuilder, JwtBuilderInject<TIdentityUser, string>>();
        return services.AddJwksManager(options);
    }

    public static IJwksBuilder AddNetDevPackIdentity(this IServiceCollection services, Action<JwtOptions> options = null)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<IJwtBuilder, JwtBuilderInject<IdentityUser, string>>();
        return services.AddJwksManager(options);
    }

    public static IJwksBuilder AddNetDevPackIdentity<TIdentityUser, TKey>(this IJwksBuilder services)
        where TIdentityUser : IdentityUser<TKey>
        where TKey : IEquatable<TKey>
    {
        services.Services.AddHttpContextAccessor();
        services.Services.AddScoped<IJwtBuilder, JwtBuilderInject<TIdentityUser, TKey>>();
        return services;
    }

    public static IJwksBuilder AddNetDevPackIdentity<TIdentityUser>(this IJwksBuilder services)
        where TIdentityUser : IdentityUser
    {
        services.Services.AddHttpContextAccessor();
        services.Services.AddScoped<IJwtBuilder, JwtBuilderInject<TIdentityUser, string>>();
        return services;
    }

    public static IJwksBuilder AddNetDevPackIdentity(this IJwksBuilder services)
    {
        services.Services.AddHttpContextAccessor();
        services.Services.AddScoped<IJwtBuilder, JwtBuilderInject<IdentityUser, string>>();
        return services;
    }

    public static IdentityBuilder AddIdentityConfiguration(this IServiceCollection services)
    {
        if (services == null) throw new ArgumentException(nameof(services));

        return services
                .AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<NetDevPackAppDbContext>()
                .AddDefaultTokenProviders();
    }

    public static IdentityBuilder AddDefaultIdentity(this IServiceCollection services, Action<IdentityOptions> options = null)
    {
        if (services == null) throw new ArgumentException(nameof(services));
        return services
            .AddIdentity<IdentityUser, IdentityRole>()
            .AddDefaultTokenProviders();
    }

    public static IdentityBuilder AddCustomIdentity<TIdentityUser>(this IServiceCollection services, Action<IdentityOptions> options = null)
        where TIdentityUser : IdentityUser
    {
        if (services == null) throw new ArgumentException(nameof(services));

        return services.AddIdentity<TIdentityUser, IdentityRole>(options)
            .AddDefaultTokenProviders();
    }

    public static IdentityBuilder AddCustomIdentity<TIdentityUser, TKey>(this IServiceCollection services, Action<IdentityOptions> options = null)
        where TIdentityUser : IdentityUser<TKey>
        where TKey : IEquatable<TKey>
    {
        if (services == null) throw new ArgumentException(nameof(services));
        return services.AddIdentity<TIdentityUser, IdentityRole<TKey>>(options)
            .AddDefaultTokenProviders();
    }

    public static IdentityBuilder AddDefaultRoles(this IdentityBuilder builder)
    {
        return builder.AddRoles<IdentityRole>();
    }

    public static IdentityBuilder AddCustomRoles<TRole>(this IdentityBuilder builder)
        where TRole : IdentityRole
    {
        return builder.AddRoles<TRole>();
    }

    public static IdentityBuilder AddCustomRoles<TRole, TKey>(this IdentityBuilder builder)
        where TRole : IdentityRole<TKey>
        where TKey : IEquatable<TKey>
    {
        return builder.AddRoles<TRole>();
    }

    public static IdentityBuilder AddDefaultEntityFrameworkStores(this IdentityBuilder builder)
    {
        return builder.AddEntityFrameworkStores<NetDevPackAppDbContext>();
    }

    public static IdentityBuilder AddCustomEntityFrameworkStores<TContext>(this IdentityBuilder builder) where TContext : DbContext
    {
        return builder.AddEntityFrameworkStores<TContext>();
    }

    public static IServiceCollection AddIdentityEntityFrameworkContextConfiguration(
        this IServiceCollection services, Action<DbContextOptionsBuilder> options)
    {
        if (services == null) throw new ArgumentException(nameof(services));
        if (options == null) throw new ArgumentException(nameof(options));
        return services.AddDbContext<NetDevPackAppDbContext>(options);
    }

    public static IApplicationBuilder UseAuthConfiguration(this IApplicationBuilder app)
    {
        if (app == null) throw new ArgumentException(nameof(app));

        return app.UseAuthentication()
            .UseAuthorization();
    }
}
