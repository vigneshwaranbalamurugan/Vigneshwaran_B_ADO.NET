using Microsoft.EntityFrameworkCore;
using NotificationApp.ModelLibrary.Models;

namespace NotificationApp.DALLibrary.Context
{
    public class NotificationAppContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=notificationdb;Username=postgres;Password=978681");
        }

        public DbSet<Notification> notifications {get;set;}
        public DbSet<EmailNotification> emailNotifications {get;set;}
        public DbSet<SMSNotification> smsNotifications {get;set;}
        public DbSet<User> users{get;set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(
                user =>
                {
                    user.HasKey(user=>user.UserId);
                    user.HasIndex(user=>user.EmailId).IsUnique();
                    user.Property(user=>user.EmailId).IsRequired();
                    user.Property(user=>user.MobileNumber).IsRequired();
                }
            );

            modelBuilder.Entity<Notification>()
                .HasDiscriminator<NotiType>("NotificationType")
                .HasValue<EmailNotification>(NotiType.EmailNotification)
                .HasValue<SMSNotification>(NotiType.SMSNotification);

            modelBuilder.Entity<Notification>(
                notification =>
                {
                    notification.HasKey(notification=>notification.Id);
                    notification.Property(notification=>notification.Message).IsRequired().HasMaxLength(160);
                    notification.Property(notification=>notification.SentDate).HasColumnType("timestamp without time zone");

                    // Relationship
                    notification.HasOne(notification=>notification.User).WithMany(user=>user.Notifications).HasForeignKey(notification=>notification.UsertoNotify).HasConstraintName("FK_Notification_User").OnDelete(DeleteBehavior.Restrict);
                }
            );
        }
    }

}