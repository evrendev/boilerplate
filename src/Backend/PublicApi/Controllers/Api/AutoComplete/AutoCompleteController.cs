using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EvrenDev.Application.DTOS.AutoComplete;

namespace EvrenDev.PublicApi.Controllers.Api
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
        public async Task<IActionResult> Index(AutoCompleteRequest request)
        {
            var connectionString = _configuration.GetValue<string>("ConnectionStrings:ApplicationConnection");
            await using var connection = new SqlConnection(connectionString);
            await connection.OpenAsync();


            await using var sqlTransaction = connection.BeginTransaction();
            var commandText = @$"
                SELECT 
                    Id, 
                    {request.Column} 
                FROM {request.Table} 
                WHERE {request.Column} 
                LIKE 
                    N'%{request.DecodedQuery}%' 
                AND 
                    LanguageId={request.LanguageId}
                AND
                    Deleted=0;";

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
                        Selector = request.Column,
                        Value = reader[request.Column].ToString()
                    };

                    response.Add(item);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                var msg = ex.Message.ToString();
                sqlTransaction.Rollback();
                return BadRequest(new { error = true, selector = request.Column, message = msg });
            }
        }
    }
}