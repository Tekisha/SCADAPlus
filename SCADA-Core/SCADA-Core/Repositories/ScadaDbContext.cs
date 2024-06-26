﻿using System.Data.Entity;
using MySql.Data.EntityFramework;
using SCADA_Core.Models;

namespace SCADA_Core.Repositories;

[DbConfigurationType(typeof(MySqlEFConfiguration))]
public class ScadaDbContext() : DbContext("name=ScadaDbContext")
{
    public DbSet<User> Users { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<TagValue> TagValues { get; set; }
    public DbSet<Alarm> Alarms { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Tag>()
            .Map<DigitalOutputTag>(m => m.Requires("TagType").HasValue("DigitalOutput"))
            .Map<DigitalInputTag>(m => m.Requires("TagType").HasValue("DigitalInput"))
            .Map<AnalogOutputTag>(m => m.Requires("TagType").HasValue("AnalogOutput"))
            .Map<AnalogInputTag>(m => m.Requires("TagType").HasValue("AnalogInput"));

        modelBuilder.Entity<User>().HasKey(u => u.Username);
        modelBuilder.Entity<Alarm>().HasKey(a => a.Id);

        base.OnModelCreating(modelBuilder);
    }
}