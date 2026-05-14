using NotificationApp.DALLibrary.Context;
using NotificationApp.ModelLibrary.Models;

namespace NotificationApp.DALLibrary.Repositories{
    public class UserRepository: AbstractRepository<int, User>{
        public UserRepository(NotificationAppContext context) : base(context)
        {
        }

        public override User? Create(User user){
            try{
                _context.users.Add(user);
                _context.SaveChanges();
                return user;
            }catch(Exception ex){
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public override User? Get(int key)
        {
            try
            {
                return _context.users.FirstOrDefault(u => u.UserId == key);
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public override List<User>? GetAll()
        {
            try
            {
                return _context.users.ToList();
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public override User? Update(int key, User item)
        {
            try
            {
                var existingUser = _context.users.FirstOrDefault(u => u.UserId == key);
                if(existingUser == null)
                {
                    return null;
                }
                
                existingUser.UserName = item.UserName;
                existingUser.MobileNumber = item.MobileNumber;
                existingUser.EmailId = item.EmailId;
                
                _context.users.Update(existingUser);
                _context.SaveChanges();
                return existingUser;
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public override User? Delete(int key)
        {
            try
            {
                var user = _context.users.FirstOrDefault(u => u.UserId == key);
                if(user != null)
                {
                    _context.users.Remove(user);
                    _context.SaveChanges();
                    return user;
                }
                return null;
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public User? GetByEmail(string email)
        {
            try
            {
                return _context.users.FirstOrDefault(u => u.EmailId == email);
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public User? GetByMobileNumber(string mobileNumber)
        {
            try
            {
                return _context.users.FirstOrDefault(u => u.MobileNumber == mobileNumber);
            }catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }
    }
}