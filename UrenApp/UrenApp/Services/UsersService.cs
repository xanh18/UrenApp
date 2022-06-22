using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using UrenApp.Models;

namespace UrenApp.Services
{
    public enum LoginResults
    {
        Success,
        UserNotFound,
    }

    public static class UsersService
    {
        static string baseUrl = "http://10.0.2.2:7143";

        static HttpClient client;

        static UsersService()
        {

            client = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };

        }

        public static async Task<LoginResults> Login(string username, string password)
        {

            var user = new Users
            {
                UserName = username,
                Password = password,
                Role = "werknemer"
            };


            var url = $"/api/users?{nameof(Users.UserName)}={username}&{nameof(Users.Password)}={password}";
            var response = await client.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return LoginResults.UserNotFound;
            }
            else
            {
                var result = await response.Content.ReadFromJsonAsync<Users>();
                return LoginResults.Success;
            }
        }

        public static async Task Register(string username, string password)
        {

            var user = new Users
            {
                UserName = username,
                Password = password,
                Role = "werknemer"

            };

            var response = await client.PostAsJsonAsync("/api/users/register", user);

            if (!response.IsSuccessStatusCode)
            {

            }


        }




    }
}
