using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStore.Lib.Models
{
    public class MovieSearchResponse
    {
        public List<MovieSearchObjectResponse> Search { get; set; }
        public int totalResults { get; set; }
    }

    public class MovieSearchObjectResponse
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string Poster { get; set; }
        public string imdbID { get; set; }
        public string Type { get; set; }
    }
}