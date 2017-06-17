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
    public class UsersController : ApiController
    {
        private WebMazeGameContext db = new WebMazeGameContext();

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
            user.Wins = 2; // <------------------ CHANGE!!!!!
            user.Losses = 0;
            user.DateRegister = DateTime.Now;

            // add the user to the table
            db.Users.Add(user);
            await db.SaveChangesAsync();

            return Ok(user);
        }

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

        [HttpPost]
        [Route("api/Users/UpdateWinner")]
        public void UpdateWinner(User winner)
        {
             User user = db.Users.Where(e => (e.Name == winner.Name)).ToArray()[0];
            user.Wins++;
            db.SaveChanges();
        }

        [HttpPost]
        [Route("api/Users/UpdateLooser")]
        public void UpdateLooser(User looser)
        {
            User user = db.Users.Where(e => (e.Name == looser.Name)).ToArray()[0];
            user.Losses++;
            db.SaveChanges();
        }

        [HttpPost]
        [Route("api/Users/CreateUserRankingTable")]
        public IEnumerable<User> CreateUserRankingTable()
        {
            // take all users with win / losses > 0
            return db.Users.Where(e => (e.Wins >0 || e.Losses > 0)).ToArray();
        }

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

        // GET: api/Users
        public IQueryable<User> GetUsers()
        {
            return db.Users;
        }

        // GET: api/Users/5
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

     /*
        // DELETE: api/Users/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> DeleteUser(int id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            await db.SaveChangesAsync();

            return Ok(user);
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        */
        private bool UserExists(string name)
        {
            return db.Users.Count(e => e.Name == name) > 0;
        }
    }
}