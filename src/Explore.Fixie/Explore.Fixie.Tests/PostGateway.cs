using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Explore.Fixie.Tests
{
    public class PostGateway
    {
        private readonly HttpClient _client = new HttpClient();
        private readonly ILogger _logger;
        public const string PostDoesNotExistFormat = "Post with id '{0}' does not exist.";


        public PostGateway(string baseUri, ILogger logger)
        {
            _client = new HttpClient {BaseAddress = new Uri(baseUri)};
            _logger = logger;
        }

        public async Task<List<Post>> GetPosts()
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _client.GetAsync("posts");

            return await response.Content.ReadAsAsync<List<Post>>();
        }

        public async Task<Post> GetPostBy(int id)
        {
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _client.GetAsync($"posts/{id}");

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                var message = string.Format(PostDoesNotExistFormat, id);
                _logger.Error(message);
                return null;
            }

            return await response.Content.ReadAsAsync<Post>();
        }
    }
}