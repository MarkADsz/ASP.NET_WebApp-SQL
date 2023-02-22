using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebApplication1.Pages.Movies
{
    public class IndexModel : PageModel
    {
        public List<MovieInfo> listMovies = new List<MovieInfo>();
        public void OnGet()
        {

            try
            {
                   String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mymovies;Integrated Security=True";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM movies";
                    using (SqlCommand command = new SqlCommand(sql, connection)) {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while(reader.Read())
                            {
                                MovieInfo movieInfo = new MovieInfo();
                                movieInfo.id = "" + reader.GetInt32(0);
                                movieInfo.movie_name = reader.GetString(1);
                                movieInfo.movie_year= reader.GetString(2);
                                movieInfo.movie_genre= reader.GetString(3);
                                movieInfo.addedAt = reader.GetDateTime(4).ToString();
                                
                                listMovies.Add(movieInfo);
                            }
                        }
                    }
                }
            
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: "+ ex.ToString());
            }
    
        }
    }

    public class MovieInfo
    {
        public String id;
        public String movie_name;
        public String movie_year;
        public String movie_genre;
        public String addedAt;
    }
}
