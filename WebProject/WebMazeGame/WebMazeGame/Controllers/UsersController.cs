using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WebMazeGame.Models;

namespace WebMazeGame.Controllers
{
    /// <summary>
    /// UsersController class
    /// </summary>
    public class UsersController : ApiController
    {
        private WebMazeGameContext db = new WebMazeGameContext();

        /// <summary>
        /// Add user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Users/AddUser")]
        public async Task<IHttpActionResult> AddUser(User user)
        {
            // if the user exists, return that it exists
            if (UserExists(user.Name))
            {
                return Ok("exist");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // hash password
            user.Password = Hash(user.Password);

            // initialize attributes
            user.Wins = 0;
            user.Losses = 0;
            user.DateRegister = DateTime.Now;

            // add the user to the table
            db.Users.Add(user);
            await db.SaveChangesAsync();

            return Ok(user);
        }

        /// <summary>
        /// Hash function
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static string Hash(string input)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    // can be "x2" if you want lowercase
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }
        }

        /// <summary>
        /// Update the winner 
        /// </summary>
        /// <param name="winner"></param>
        [HttpPost]
        [Route("api/Users/UpdateWinner")]
        public void UpdateWinner(User winner)
        {
             User user = db.Users.Where(e => (e.Name == winner.Name)).ToArray()[0];
            user.Wins++;
            db.SaveChanges();
        }

        /// <summary>
        /// Update the looser
        /// </summary>
        /// <param name="looser"></param>
        [HttpPost]
        [Route("api/Users/UpdateLooser")]
        public void UpdateLooser(User looser)
        {
            User user = db.Users.Where(e => (e.Name == looser.Name)).ToArray()[0];
            user.Losses++;
            db.SaveChanges();
        }

        /// <summary>
        /// Create user ranking table
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Users/CreateUserRankingTable")]
        public IEnumerable<User> CreateUserRankingTable()
        {
            // take all users with win / losses > 0
            return db.Users.Where(e => (e.Wins >0 || e.Losses > 0)).ToArray();
        }

        /// <summary>
        /// Check user for log in
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/Users/CheckUserForLogIn")]
        public IHttpActionResult CheckUserForLogIn(User user)
        {
            // if the user exists, return that it exists
            if (!UserExists(user.Name))
            {
                return Ok("not exist");
            }

            // check password
            User[] users = db.Users.Where(e => e.Name == user.Name).ToArray();
            User userFromTable = users[0];
            if(userFromTable.Password != Hash(user.Password))
            {
                return Ok("not exist");
            } 
            return Ok(userFromTable);
        }

        /// <summary>
        /// get users.
        /// </summary>
        /// <returns></returns>
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }

        /// <summary>
        /// get a user.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> GetUser(int id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        /// <summary>
        /// Chechs if a user exists
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool UserExists(string name)
        {
            return db.Users.Count(e => e.Name == name) > 0;
        }
    }
}