using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Net.Http;
//using Microsoft.AspNetCore.Http;

namespace FieldLevel.Models
{
    internal class Posts
    { 
        internal static List<Post> GetCurrentPosts()
        {
            List<Post> _userPosts;
            List<Post> _newUserPosts = null;
            using (var _client = new HttpClient())
            {
                try
                {
                    var httpResponse = _client.GetAsync(new Uri("https://jsonplaceholder.typicode.com/posts"));
                    string jsonText = httpResponse.Result.Content.ReadAsStringAsync().Result;

                    _userPosts = JsonSerializer.Deserialize<List<Post>>(jsonText, new JsonSerializerOptions());

                    //return most recent post for each user
                    _newUserPosts = _userPosts
                        .GroupBy(g => g.userId)
                        .Select(s => s.OrderByDescending(o => o.id).FirstOrDefault()).ToList();
                }
                catch(Exception ex)
                { }
            }

            return _newUserPosts;
        }
    }
}
