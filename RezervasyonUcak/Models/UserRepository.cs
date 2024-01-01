using RezervasyonUcak.Areas.Employees.Models;

namespace RezervasyonUcak.Models
{
    public class UserRepository
    {


        readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }
        public User getEmployeeByUsernameAndPassword(string mail, string password)
        {


            User user = (from _user in _context.Users
                         where _user.Email == mail &&
                         _user.Password == password
						 && _user.Deleted == false
                         select _user).FirstOrDefault();
            return user;
        }

        public void deleteUserByUsername(string username)
        {

            User user=_context.Users.Where(user=>user.Name==username && user.Deleted==false).FirstOrDefault();
            user.Deleted= true;
            _context.SaveChanges();
        }


        public bool exisByUsernameAndDeletedFalse(string username)
        {

            return   _context.Users.Any(user=>user.Name==username &&user.Deleted==false);

        }


        public bool exisByMailAndDeletedFalse(string mail) {       
      return  _context.Users.Any(_user=>_user.Email==mail&& _user.Deleted==false );

        
        }


       



    }
}
