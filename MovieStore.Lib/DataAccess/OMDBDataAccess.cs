using MovieStore.Lib.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Lib.DataAccess
{
    public class OMDBDataAccess
    {
        private const string API_URI = "http://www.omdbapi.com/?apikey=56e90cf2&";
        private readonly HttpClient _client;

        public OMDBDataAccess()
        {
            _client = new HttpClient();
        }

        public async Task<MovieResponse> GetMovieByTitle(string title)
        {
            var result = await _client.GetAsync($"{API_URI}t={title}");
            if (result.IsSuccessStatusCode)
            {
                var r = await result.Content.ReadAsStringAsync();
                MovieResponse movieData = JsonConvert.DeserializeObject<MovieResponse>(r);
                return movieData;
            }
            else
            {
                throw new Exception("Error");
            }
        }
    }
}

