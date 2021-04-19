using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SportsMeeting.Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsMeeting.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public ApplicationDbContext(
             DbContextOptions options,
             IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        private string _connectionStr = "Server =.\\SQLEXPRESS;Database=Sports_meeting;Trusted_Connection=True;";

        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Message> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionStr);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ApplicationUser>()
                .HasMany(m => m.Meetings)
                .WithOne(u => u.ApplicationUser)
                .HasForeignKey(m => m.UserName)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<Meeting>()
                .HasMany(m => m.Participants)
                .WithOne(p => p.Meeting)
                .HasForeignKey(p => p.MeetingId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ApplicationUser>()
                .HasMany(u => u.Participants)
                .WithOne(p => p.User)
                .HasForeignKey(p=>p.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Meeting>()
                .HasOne(m => m.Conversation)
                .WithOne(c => c.Meeting)
                .HasForeignKey<Conversation>(c => c.MeetingId)
                .OnDelete(DeleteBehavior.Restrict);
        } 
    }
}
