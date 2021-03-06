﻿using MovieStore.Lib.Models;
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
        private const string API_URI = "http://www.omdbapi.com/?apikey={API_KEY}&";
        private readonly HttpClient _client;

        public OMDBDataAccess()
        {
            _client = new HttpClient();
        }

        public async Task<MovieResponse> GetMovieByTitle(string title)
        {
            var result = await _client.GetAsync($"{API_URI}t={title}&plot=full");
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

        public async Task<MovieResponse> GetMovieById(string id)
        {
            var result = await _client.GetAsync($"{API_URI}i={id}&plot=full");
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

        public async Task<List<MovieIdsResponse>> GetTop100MovieIds()
        {
            string URL = "https://imdb8.p.rapidapi.com/title/get-top-rated-movies/";

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "imdb8.p.rapidapi.com");
            client.DefaultRequestHeaders.Add("X-RapidAPI-Key", "4d61e7bdb2msh8a6a096b8f281cdp122f8djsn35ddf37bbbfc");

            var result = await client.GetAsync(URL);

            if (result.IsSuccessStatusCode)
            {
                var r = await result.Content.ReadAsStringAsync();

                var data = JsonConvert.DeserializeObject<List<MovieIdsResponse>>(r).Take(100).ToList();

                return data;
            }
            else throw new Exception("Error fetching data");
        }

        public async Task<List<Movie>> GetTop100Movies()
        {
            var rnd = new Random();
            var movieIdList = await GetTop100MovieIds();

            List<Movie> output = new List<Movie>();
            foreach (var movieId in movieIdList)
            {
                var m = await GetMovieById(movieId.id.Replace("/title/", "").Replace("/", "").Trim());
                var movie = new Movie
                {
                    Title = m.Title,
                    Director = m.Director,
                    Price = rnd.Next(29, 199),
                    ReleaseYear = m.Year,
                    Genre = m.Genre,
                    ImdbID = m.imdbID,
                    ImdbRating = m.imdbRating,
                    Plot = m.Plot,
                    Poster = FixPosterURL(m.Poster),
                    Rated = m.Rated
                };

                output.Add(movie);
            }

            return output;
        }

        public async Task<MovieSearchResponse> SearchMovies(string query)
        {
            var result = await _client.GetAsync($"{API_URI}s={query}");
            if (result.IsSuccessStatusCode)
            {
                var r = await result.Content.ReadAsStringAsync();
                MovieSearchResponse movieSearchResponse = JsonConvert.DeserializeObject<MovieSearchResponse>(r);
                return movieSearchResponse;
            }
            else throw new Exception("Error fetching data");
        }

        public string FixPosterURL(string url)
        {
            string output = url.Replace("_SX300.jpg", "_.jpg");

            return output;
        }
    }
}