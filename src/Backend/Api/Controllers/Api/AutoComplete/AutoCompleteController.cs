using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using EvrenDev.Application.DTOS.AutoComplete;

namespace EvrenDev.Controllers.Api
{
    [ApiController]
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AutoCompleteController : Controller
    {
        private readonly IConfiguration _configuration;

        public AutoCompleteController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Index([FromQuery] string table, 
            string column, 
            string query)
        {
            var connectionString = _configuration.GetValue<string>("ConnectionStrings:ApplicationConnection");
            await using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();


            await using var sqlTransaction = connection.BeginTransaction();
            var commandText = $@"SELECT Id, {column} FROM {table}
                                        WHERE {column} LIKE N'%{HttpUtility.UrlDecode(query)}%';";

            await using var command = new SqlCommand(commandText, connection, sqlTransaction);

            try
            {
                var reader = await command.ExecuteReaderAsync();
                var response = new List<AutoCompleteResponse>();

                while (reader.Read())
                {
                    var item = new AutoCompleteResponse()
                    {
                        Id = Guid.Parse(reader["Id"].ToString()),
                        Selector = column,
                        Value = reader[column].ToString()
                    };

                    response.Add(item);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                sqlTransaction.Rollback();
                return BadRequest(new { error = true, selector = column, message = msg });
            }
        }
    }
}