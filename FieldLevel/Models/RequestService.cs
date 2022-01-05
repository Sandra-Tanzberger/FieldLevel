using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace FieldLevel.Models
{
    public class RequestService
    {
        //private static ConcurrentQueue<HttpResponse> requestQueue = new ConcurrentQueue<HttpResponse>(); //not implemented
        //private static ConcurrentQueue<HttpResponse> ResponseQueue = new ConcurrentQueue<HttpResponse>(); //not implemented

        private static ConcurrentQueue<string> requestQueue = new ConcurrentQueue<string>();
        private static ConcurrentQueue<List<Post>> responseQueue = new ConcurrentQueue<List<Post>>();

        private static RequestService requestService = new RequestService(); // single object instance, used internally
        private static bool runTask = true;

        private static HttpClient client = new HttpClient();

        private RequestService()
        {
            //rturn Task for future logging if task is stopped
            // _ = HandleRequests();//start task queue
            var task = HandleRequests();
            task.Wait();
        }

        public bool RunTask { get; set; }
        public static void QueueRequest(string postBackUrl)
        {
            requestQueue.Enqueue(postBackUrl);
        }

        internal static async Task HandleRequests()
        {
            while (runTask)
            {
                //if request in queue, get response and post data
                //HttpResponse reponse; not implemented
                string postBackUrl;
                if (requestQueue.TryDequeue(out postBackUrl))
                {
                    try
                    {   //ideal, but web api mvc auto send ok accepted response from void endpoint method - web sockets?
                        //await HttpResponseWritingExtensions.WriteAsync(reponse, (JsonSerializer.Serialize(Posts.GetCurrentPosts())));

                        //simplest implementation - get callbackurl from request, post data to endpoint
                        await client.PostAsync(postBackUrl, new ByteArrayContent(System.Text.Encoding.UTF8.GetBytes(JsonSerializer.Serialize(Posts.GetCurrentPosts()))));

                    }
                    catch (Exception ex)
                    {
                        //log error
                    }
                }

                await Task.Delay(60000);
            }
        }
    }
}
