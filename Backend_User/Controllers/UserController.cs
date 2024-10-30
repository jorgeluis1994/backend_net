using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend_User.Models;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using Npgsql;
using System.Data;

namespace Backend_User.Controllers
{
    #region Controlador
    [ApiController]
    [Route("api/[controller]")]
    public class UserController(IConfiguration configuration) : ControllerBase
    {
        private readonly List<User> listUsers = new List<User>();

        private readonly string _conection = configuration.GetConnectionString("DefaultConnection");

        // Método para obtener todos los usuarios
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            using (IDbConnection db = new NpgsqlConnection(_conection))
            {
                db.Open();
                var users = db.Query<User>("SELECT * FROM users");

                var filterUser = users.Where(usuario => usuario.Name == "Jane Smith").ToList();

                listUsers.Add(filterUser[0]);

                Console.WriteLine(listUsers);
                
                return Ok(listUsers);
            }
        }
    }
    #endregion

}