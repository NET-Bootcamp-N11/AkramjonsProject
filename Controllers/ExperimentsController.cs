using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using Npgsql;
using Dapper;



namespace WebApplication3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExperimentsController : ControllerBase
    {
        private string _pgConnector = "Host=localhost;Port=5432;Database=TestDB;username=postgres;Password=Akramjon_09;";

        #region Post
        [HttpPost]
        public void Post(string name, int age)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_pgConnector))
            {
                string query = "INSERT INTO experiment (name, age) VALUES (@name, @age);";
                connection.Execute(query, new { name = name, age= age });
            }
        }
        #endregion

        #region Read
        [HttpGet]
        [Route("GetById")]
        public List<experiment> GetById(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_pgConnector))
            {
                string query = "select  * from experiment where id = @id;";
                return connection.Query<experiment>(query, new { id = id }).ToList();
            }
        }

        [HttpGet]
        [Route("GetByName")]
        public List<experiment> GetByName(string name)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_pgConnector))
            {
                string query = "select * from experiment where name = @name";
                return connection.Query<experiment>(query, new { name = name }).ToList();
            }
        }

        [HttpGet]
        [Route("GetByAge")]
        public List<experiment> GetByAge(int age)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_pgConnector))
            {
                string query = "select * from experiment where age = @age;";
                return connection.Query<experiment>(query, new { age = age }).ToList();
            }
        }

        [HttpGet]
        public List<experiment> GetAll()
        {
            using (NpgsqlConnection connector = new NpgsqlConnection(_pgConnector))
            {
                string query = "select * from experiment";
                return connector.Query<experiment>(query).ToList();
            }
        }
        #endregion


        #region Delete
        [HttpDelete]
        [Route("DeleteById")]
        public string DeleteById(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_pgConnector))
            {
                string query = $"delete from experiment where id = @id;";
                connection.Execute(query, new { id = id });
                return "already delete ";
            }
        }

        [HttpDelete]
        [Route("DeleteaByName")]
        public string DeleteByName(string Name) 
        {
            using (NpgsqlConnection connector = new NpgsqlConnection(_pgConnector)) 
            {
                string query = "delete from experiment where name = @name limit 1";
                connector.Execute(query, new { Name = Name });
                return "already delete";
            }
        }

        [HttpDelete]
        [Route("DeleteByAge")]
        public string DeleteByAge(int age)
        {
            using (NpgsqlConnection connector = new NpgsqlConnection(_pgConnector))
            {
                string query = "delete from experiment where age = @age limit 1";
                connector.Execute(query, new { age = age });
                return "already delete";
            }
        }
        #endregion

        #region update patch
        [HttpPatch]
        [Route("Update/NameById")]
        public string UpdateNameById(int id,string name) 
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_pgConnector))
            {
                string query = $"update experiment set name=@name where id =@id;";
                connection.Execute(query, new { name = name,id = id });
                return "already delete ";
            }
        }
        [HttpPatch]
        [Route("Update/AgeById")]
        public string UpdateAge(int id, int age)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_pgConnector))
            {
                string query = $"update experiment set age=@age where id =@id;";
                connection.Execute(query, new { age = age, id = id });
                return "Update";
            }
        }
        #endregion

        #region 
        [HttpPut]
        public string UpdateAll(int old_id, string name, int age) 
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_pgConnector)) 
            {
                string query = "Update experiment set name = @name, age = @age where id = @id";
                connection.Execute(query, new { old_id = old_id, name = name, age = age });
                return "Update";
            }
        }
        #endregion
    }
}
