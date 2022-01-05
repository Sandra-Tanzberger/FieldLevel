using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;

namespace FieldLevel.Models
{
    internal class Posts
    {
        private static string path = Path.GetFullPath((Environment.GetCommandLineArgs())[1].ToString());
        private static string newpath = Path.GetFullPath((Environment.GetCommandLineArgs())[2].ToString());
        internal static List<Post> GetCurrentPosts()
        {
            string jsonText = File.ReadAllText(path);
            List<Post> userPosts = JsonSerializer.Deserialize<List<Post>>(jsonText, new JsonSerializerOptions());

            //return most recent post for each user

            List<Post> newUserPosts = (List<Post>)userPosts
                .GroupBy(g =>  g.userId )
                .Select(s => s.OrderByDescending(o => o.id).FirstOrDefault()).ToList();

            return newUserPosts;
        }

        internal static void WritePostsToFile(List<Post> posts)
        {
            var json = JsonSerializer.Serialize<List<Post>>(posts);

            // This text is added only once to the file.
            if (!File.Exists(newpath))
            {
                File.WriteAllText(newpath, json);
            }
        }
    }
}
