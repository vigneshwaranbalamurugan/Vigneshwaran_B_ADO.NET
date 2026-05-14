using System;
using NotificationApp.ModelLibrary.Models;
using NotificationApp.DALLibrary.Context;
using Microsoft.EntityFrameworkCore;

namespace NotificationApp.DALLibrary.Repositories
{
    public class NotificationRepository : AbstractRepository<int, Notification>
    {
        public NotificationRepository(NotificationAppContext context) : base(context)
        {
        }

        public override Notification? Create(Notification item)
        {
            try
            {
                if (item.SentDate == default)
                {
                    item.SentDate = DateTime.UtcNow;
                }
                _context.notifications.Add(item);
                _context.SaveChanges();
                return item;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public override List<Notification>? GetAll()
        {
            try
            {
                return _context.notifications
                    .Include(n => n.User)
                    .ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public override Notification? Get(int key)
        {
            try
            {
                return _context.notifications
                    .Include(n => n.User)
                    .FirstOrDefault(n => n.Id == key);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public override Notification? Update(int key, Notification item)
        {
            try
            {
                var existingNotification = _context.notifications.FirstOrDefault(n => n.Id == key);
                if (existingNotification == null)
                {
                    return null;
                }

                existingNotification.UsertoNotify = item.UsertoNotify;
                existingNotification.Message = item.Message;
                existingNotification.SentDate = item.SentDate == default ? DateTime.UtcNow : item.SentDate;
                existingNotification.NotificationType = item.NotificationType;

                _context.notifications.Update(existingNotification);
                _context.SaveChanges();
                return existingNotification;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public override Notification? Delete(int key)
        {
            try
            {
                var notification = _context.notifications.FirstOrDefault(n => n.Id == key);
                if (notification != null)
                {
                    _context.notifications.Remove(notification);
                    _context.SaveChanges();
                    return notification;
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}