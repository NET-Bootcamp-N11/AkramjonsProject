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

        [HttpGet]
        public List<experiment> Get(int id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_pgConnector))
            {
                string query = $"select id,name from experiment where id = @id;";
                return connection.Query<experiment>(query, new { id = id}).ToList();
            }
        }
        [HttpDelete]
        public string Delete(int id) 
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_pgConnector))
            {
                string query = $"delete from experiment where id = @id;";
                connection.Execute(query, new { id = id});
                return "already delete ";
            }
        }
        [HttpPatch]
        public string Patch(int id,int new_name) 
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(_pgConnector))
            {
                string query = $"update experiment set name=@name where id =@id;";
                connection.Execute(query, new { name = new_name,id = id });
                return "already delete ";
            }
        }
    }
}
