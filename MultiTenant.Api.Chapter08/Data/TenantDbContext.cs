﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections.Generic;

namespace MultiTenant.Api.Chapter08.Data;

public class TenantDbContext : DbContext
{
    public readonly string TenantId;

    public TenantDbContext(DbContextOptions<TenantDbContext> options, string tenantId)
        : base(options)
    {
        TenantId = tenantId;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var tenantSchema = $"tenant_{TenantId}";
        optionsBuilder.ReplaceService<IModelCacheKeyFactory, TenantModelCacheKeyFactory>();
        optionsBuilder.UseSqlServer($"Server=CrmServerAddress;Database=CrmDataBase;User Id=DbUsername;Password=DbPassword;SearchPath={tenantSchema}");
    }

    public DbSet<Customer> Customers { get; set; }
}

public class TenantModelCacheKeyFactory : IModelCacheKeyFactory
{
    public object Create(DbContext context, bool designTime)
    {
        var tenantContext = context as TenantDbContext;
        return (context.GetType(), tenantContext?.TenantId);
    }
}