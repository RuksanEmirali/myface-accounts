using System.Collections.Generic;
using System.Linq;
using MyFace.Models.Database;
using MyFace.Models.Request;
using System;
using MyFace.Data;
using System.Threading.Tasks;

namespace MyFace.Repositories
{
    public interface IUsersRepo
    {
        IEnumerable<User> Search(UserSearchRequest search);
        int Count(UserSearchRequest search);
        User GetById(int id);
        User GetByUsername(string username);
        User Create(CreateUserRequest newUser);
        User Update(int id, UpdateUserRequest update);
        void Delete(int id);
        public async Task<User> Authenticate(string username, string password);
    }
    
    public class UsersRepo : IUsersRepo
    {
        private readonly MyFaceDbContext _context;

        public UsersRepo(MyFaceDbContext context)
        {
            _context = context;
        }
        
        public IEnumerable<User> Search(UserSearchRequest search)
        {
            return _context.Users
                .Where(p => search.Search == null || 
                            (
                                p.FirstName.ToLower().Contains(search.Search) ||
                                p.LastName.ToLower().Contains(search.Search) ||
                                p.Email.ToLower().Contains(search.Search) ||
                                p.Username.ToLower().Contains(search.Search)
                            ))
                .OrderBy(u => u.Username)
                .Skip((search.Page - 1) * search.PageSize)
                .Take(search.PageSize);
        }

        public int Count(UserSearchRequest search)
        {
            return _context.Users
                .Count(p => search.Search == null || 
                            (
                                p.FirstName.ToLower().Contains(search.Search) ||
                                p.LastName.ToLower().Contains(search.Search) ||
                                p.Email.ToLower().Contains(search.Search) ||
                                p.Username.ToLower().Contains(search.Search)
                            ));
        }

        public User GetById(int id)
        {
            return _context.Users
                .Single(user => user.Id == id);
        }

        
        public User GetByUsername(string username)
        {
            return _context.Users
                .Single(user => user.Username == username);
        }

        public User Create(CreateUserRequest newUser)
        {
            List<string> saltHash = SaltAndHashGenerator.getSaltAndHash(newUser.Password);
            var insertResponse = _context.Users.Add(new User
            {
                FirstName = newUser.FirstName,
                LastName = newUser.LastName,
                Email = newUser.Email,
                Username = newUser.Username,
                Salt = saltHash[0],
                HashedPassword = saltHash[1],
                ProfileImageUrl = newUser.ProfileImageUrl,
                CoverImageUrl = newUser.CoverImageUrl,
            });
            _context.SaveChanges();

            return insertResponse.Entity;
        }

        


        public User Update(int id, UpdateUserRequest update)
        {
            var user = GetById(id);

            user.FirstName = update.FirstName;
            user.LastName = update.LastName;
            user.Username = update.Username;
            user.Email = update.Email;
            user.ProfileImageUrl = update.ProfileImageUrl;
            user.CoverImageUrl = update.CoverImageUrl;

            _context.Users.Update(user);
            _context.SaveChanges();

            return user;
        }

        public void Delete(int id)
        {
            var user = GetById(id);
            _context.Users.Remove(user);
            _context.SaveChanges();
        }

        public async Task<User> Authenticate(string username, string password)
        {
            User user = GetByUsername(username);
            
            // get the salt from user
            // convert password into hash password (use salt to do it)

            //  _hash = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            // password: password,
            // salt: salt,
            // prf: KeyDerivationPrf.HMACSHA256,
            // iterationCount: 100000,
            // numBytesRequested: 256 / 8));

            //if this hash password match with user.HashedPassword 
            // then Authenticate ok and return user
            // otherwise return null
            
          

            return user;
        }
    }
}