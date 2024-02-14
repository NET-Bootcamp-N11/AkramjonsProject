using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using Npgsql;



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
                connection.Open();
                string query = $"select id,name from experiment where id = {id};";
                using NpgsqlCommand cmd = new NpgsqlCommand(query, connection);

                var result = cmd.ExecuteReader();
                List<experiment> list = new List<experiment>();

                while (result.Read())
                {
                    list.Add(new experiment
                    {
                        id = (int)result[0],
                        name = (string)result[1]
                    }) ;
                }


                return list;
            }
        }

        
    }
}
