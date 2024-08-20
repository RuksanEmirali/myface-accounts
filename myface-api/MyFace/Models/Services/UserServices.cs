// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Extensions.Configuration;
// using System;
// using System.Collections.Generic;
// using System.Data;
// using System.Linq;
// using System.Security.Cryptography;
// using System.Text;
// using System.Threading.Tasks;
// using MyFace.Models.Database;
// using MyFace.Repositories;
// using Microsoft.Data.Sqlite;

// namespace MyFace.Models.Services
// {
//     public interface IUserService
//     {
//        Task<User> Authenticate(string username, string password);
//        Task<IEnumerable<User>> GetAll();
//     }

//     public class UserService : IUserService
//     {

//         private readonly IConfiguration _configuration;
//         public UserService(IConfiguration configuration)
//         {
//             _configuration = configuration;
//         }

//         List<User> _users = new List<User>();

//         //private List<User> _users = new List<User>

//         //{

//         //    new User { UserId = "1", FirstName = "Test", LastName = "User", Username = "test1", Password = "test1" }

//         //    //var a = new Getproducts();

//         // };

//         public async Task<User> Authenticate(string username, string password)
//         {
//             string lineIndatabase = GetByUsername(username);
            


//            GetUserTable();
//            var genpassword = GenerateHashFromSalt(password);

//             var user = await Task.Run(() => _users.SingleOrDefault(x => x.Username == username && x.HashedPassword == genpassword));

//             // return null if user not found

//             if (user == null)

//                 return null;

//             // authentication successful so return user details without password
//             user.HashedPassword = null;

//             return user;
//         }
//         public async Task<IEnumerable<User>> GetAll()

//         {
//             GetUserTable();

//             //return users without passwords
//             return await Task.Run(() => _users.Select(x =>
//             {
//                 x.HashedPassword = null;

//                 return x;
//             }));
//         }

//         // Getting data from user tabel from database

//         private List<User> GetUserTable()
//         {

//             string query = @"select * from myface.Users;";
//             DataTable table = new DataTable();
//             //string sqlDataSource = _configuration.GetConnectionString("Data Source=myface.db");
//             // sqlite = new SQLiteConnection("Data Source=/path/to/file.db");
//             using (var mycon = new SqliteConnection("Data Source=myface.db"))

//             {
//                 mycon.Open();

//                 SQLiteCommand cmd;
//                     cmd = mycon.CreateCommand();
//                     cmd.CommandText = query;

//                 using (MySqlCommand mycommond = new MySqlCommand(query, mycon))
//                 {
//                     SQLiteCommand cmd;
//                     cmd = mycon.CreateCommand();
//                     cmd.CommandText = query;  //set the passed query

//                     myReader = mycommond.ExecuteReader();
//                     table.Load(myReader);
//                     myReader.Close();
//                     mycon.Close();
//                 }
//             }
//             for (int i = 0; i < table.Rows.Count; i++)

//             {
//                 User usr = new User();
//                 usr.UserId = Convert.ToInt32(table.Rows[i]["userid"]);

//                 //usr.UserId = table.Rows[i]["userid"].ToString();
//                 usr.Username = table.Rows[i]["userName"].ToString();
//                 usr.Password = table.Rows[i]["password"].ToString();
//                 usr.FirstName = table.Rows[i]["firstName"].ToString();
//                 usr.LastName = table.Rows[i]["lastName"].ToString();
//                 usr.EmailId = table.Rows[i]["emailid"].ToString();
//                 _users.Add(usr);
//             }

//             return (_users);
//         }

//         // password converstion to Hash key

//         public string GenerateMySQLHash(string key)
//         {

//             // byte[] keyArray = Encoding.UTF8.GetBytes(key);
//             // SHA1Managed enc = new SHA1Managed();
//             // byte[] encodedKey = enc.ComputeHash(enc.ComputeHash(keyArray));
//             // StringBuilder myBuilder = new StringBuilder(encodedKey.Length);

//             // foreach (byte b in encodedKey)
//             //     myBuilder.Append(b.ToString("X2"));

//              return "" ;

//         }
//     }
// }


