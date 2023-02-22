using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebApplication1.Pages.Movies
{
    public class Index1Model : PageModel
    {
        public MovieInfo movieInfo = new MovieInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet() {

        }
        public void OnPost() {
            movieInfo.movie_name = Request.Form["name"];
            movieInfo.movie_year = Request.Form["year"];
            movieInfo.movie_genre = Request.Form["genre"];
            if (movieInfo.movie_name.Length == 0 || movieInfo.movie_year.Length == 0 ||
                movieInfo.movie_genre.Length == 0 )
            {
                errorMessage = "All fields are required";
                return;
            }
            //save
            try {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mymovies;Integrated Security=True";
                using(SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO movies(movie_name,movie_year,main_genre) VALUES (@movie_name,@movie_year,@main_genre);";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@movie_name", movieInfo.movie_name);
                        command.Parameters.AddWithValue("@movie_year", movieInfo.movie_year);
                        command.Parameters.AddWithValue("@main_genre", movieInfo.movie_genre);
           

                        command.ExecuteNonQuery();


                    }
                }
            }
            catch(Exception ex) {
                errorMessage = ex.Message;
                return;
            }
            /*movieInfo.name = "";
            movieInfo.email = "";
            movieInfo.phone = "";
            movieInfo.address = "";*/
            successMessage = "New Movie Added";
            Response.Redirect("/Movies/Index");
        }
    }
}
