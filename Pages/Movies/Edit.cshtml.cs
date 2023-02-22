using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebApplication1.Pages.Movies
{
    public class EditModel : PageModel
    {
        public MovieInfo movieInfo = new MovieInfo();
        public String errorMessage = "";
        public String successMessage = "";
        public void OnGet()
        {
            String id = Request.Query["id"];
            try {

                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mymovies;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM movies WHERE id=@id";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                 
                                movieInfo.id = "" + reader.GetInt32(0);
                                movieInfo.movie_name = reader.GetString(1);
                                movieInfo.movie_year = reader.GetString(2);
                                movieInfo.movie_genre = reader.GetString(3);
                                 

                               
                            }
                        }


                    }
                }

            }
            catch(Exception ex)
            {
                errorMessage = ex.Message;
            }
        }
        public void OnPost() {
            movieInfo.id = Request.Query["id"];
            movieInfo.movie_name = Request.Form["name"];
            movieInfo.movie_year = Request.Form["year"];
            movieInfo.movie_genre = Request.Form["genre"];
            if (movieInfo.id.Length == 0 || movieInfo.movie_name.Length == 0 || movieInfo.movie_year.Length == 0 ||
                movieInfo.movie_genre.Length == 0 )
            {
                errorMessage = "All fields are required";
                return;
            }
            //save
            try
            {
                String connectionString = "Data Source=.\\sqlexpress;Initial Catalog=mymovies;Integrated Security=True";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE movies SET movie_name=@movie_name, movie_year=@movie_year, main_genre=@main_genre WHERE id=@id;";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@movie_name", movieInfo.movie_name);
                        command.Parameters.AddWithValue("@movie_year", movieInfo.movie_year);
                        command.Parameters.AddWithValue("@main_genre", movieInfo.movie_genre);
                        command.Parameters.AddWithValue("@id", movieInfo.id);

                        command.ExecuteNonQuery();


                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }
            Response.Redirect("/Movies/Index");
        }
    }
}
