﻿using System.Data.SqlClient;
using ST10254164_CLDV6212_GR2_part1.Models;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace ST10254164_CLDV6212_GR2_part1.Services
{
    public class CustomerService
    {
            private readonly IConfiguration _configuration;

            public CustomerService(IConfiguration configuration)
            {
                _configuration = configuration;
            }

            public async Task InsertCustomerAsync(CustomerProfiles profile)
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                var query = @"INSERT INTO CustomerTable (FirstName, SecondName, Email, PhoneNumber)
                          VALUES (@FirstName, @SecondName, @Email, @PhoneNumber)";

            using (SqlConnection connection = new SqlConnection(connectionString))
                {
                SqlCommand command = new SqlCommand(query, connection);
                    command.Parameters.AddWithValue("@FirstName", profile.FirstName);
                    command.Parameters.AddWithValue("@SecondName", profile.LastName);
                    command.Parameters.AddWithValue("@Email", profile.Email);
                    command.Parameters.AddWithValue("@PhoneNumber", profile.PhoneNumber);

                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
}
