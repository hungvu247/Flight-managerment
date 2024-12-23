using DataAccess.BussinessObjects;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class BookingPlatformDAO
    {
        
       private static FlightManagementDbContext storeContext = new FlightManagementDbContext();
        public static List<BookingPlatform> getBookingPlatforms()
        {
            return storeContext.BookingPlatforms.Where(status => status.Status.Equals(true)).OrderBy(a  => a.Id).ToList();
            //return platforms;
        }

        public static List<BookingPlatform> getBookingPlatformsDeactive()
        {
            return storeContext.BookingPlatforms.Where(status => status.Status.Equals(false)).OrderBy(a => a.Id).ToList();
            //return platforms;
        }


        public static BookingPlatform? GetBookingPlatformsById(string id)
        {
            BookingPlatform? bookingPlatform = new BookingPlatform();
            int platformId = Int32.Parse(id);
           
            bookingPlatform = storeContext.BookingPlatforms.Find(platformId);
            
            return bookingPlatform;
        }

        public static void insertBookingPlatforms(BookingPlatform bookingPlatform)
        {

            
            storeContext.Add(bookingPlatform);
            storeContext.SaveChanges();
        }

        public static void updateBookingPlatforms(BookingPlatform bookingPlatform)
        {

            
            storeContext.Update(bookingPlatform);
            storeContext.SaveChanges();
        }

        public static void DeleteBookingPlatform(String id)
        {
            string sql = "UPDATE BookingPlatform SET status = 0 WHERE id = @id ";
            string connectionString = GetConnectionString("DefaultConnectionStrings");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    throw;
                }
            }
        }

        public static void Active(String id)
        {
            string sql = "UPDATE BookingPlatform SET status = 1 WHERE id = @id ";
            string connectionString = GetConnectionString("DefaultConnectionStrings");
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    throw;
                }
            }
        }

        public static bool IsDuplicateBookingPlatformName(string name)
        {
            
            return storeContext.BookingPlatforms.Any(aa => aa.Name == name);
        }

        public static bool IsDuplicateBookingPlatformUrl(string url)
        {
            
            return storeContext.BookingPlatforms.Any(aa => aa.Url == url);
        }

        public static bool CheckExistName(string id, string name)
        {
            //FlightManagementDbContext flightManagementDbContext = new FlightManagementDbContext();
            
            string currentName = null;
           
            string sql = "SELECT name FROM BookingPlatform WHERE id = @id AND status = 1";
            string connectionString = GetConnectionString("DefaultConnectionStrings");
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@name", id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                currentName = reader.GetString(reader.GetOrdinal("name"));
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(currentName) && !name.Equals(currentName, StringComparison.OrdinalIgnoreCase))
                    {
                        string checkQuery = "SELECT COUNT(*) as number FROM BookingPlatform WHERE name = @name AND id != @id";

                        using (SqlCommand cmd = new SqlCommand(checkQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@name", name);
                            cmd.Parameters.AddWithValue("@id", id);

                            int count = 0;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    //currentName = reader.GetString(reader.GetOrdinal("url"));
                                    count = reader.GetInt32(reader.GetOrdinal("number"));
                                }
                            }
                            if (count > 0)
                            {
                                // Email đã tồn tại cho nhân viên khác
                                return true;
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                // Xử lý ngoại lệ, ví dụ: throw e; để ngoại lệ được lan truyền lên để xử lý ở mức cao hơn.
            }

            return false;
        }


        public static bool CheckExistUrl(string id, string url)
        {
            //FlightManagementDbContext flightManagementDbContext = new FlightManagementDbContext();

            string currentName = null;

            string sql = "SELECT url FROM BookingPlatform WHERE id = @id AND status = 1";
            string connectionString = GetConnectionString("DefaultConnectionStrings");
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                currentName = reader.GetString(reader.GetOrdinal("url"));
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(currentName) && !url.Equals(currentName, StringComparison.OrdinalIgnoreCase))
                    {
                        string checkQuery = "SELECT COUNT(*) as number FROM BookingPlatform WHERE url = @url AND id != @id";

                        using (SqlCommand cmd = new SqlCommand(checkQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@url", url);
                            cmd.Parameters.AddWithValue("@id", id);
                            int count = 0;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    //currentName = reader.GetString(reader.GetOrdinal("url"));
                                    count = reader.GetInt32(reader.GetOrdinal("number"));
                                }
                            }
                            if (count > 0)
                            {
                                // Email đã tồn tại cho nhân viên khác
                                return true;
                            }
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                // Xử lý ngoại lệ, ví dụ: throw e; để ngoại lệ được lan truyền lên để xử lý ở mức cao hơn.
            }

            return false;
        }

       

        public static int GetCurrentID()
        {
            int currentId = 0;
            string sql = "SELECT MAX(id) as currentId FROM bookingPlatform";
            string connectionString = GetConnectionString("DefaultConnectionStrings");
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                       // cmd.Parameters.AddWithValue("@id", id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                currentId = reader.GetInt32(reader.GetOrdinal("currentId"));
                            }
                        }
                    }
                }
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.Message);
                // Xử lý ngoại lệ, ví dụ: throw e; để ngoại lệ được lan truyền lên để xử lý ở mức cao hơn.
            }

            return currentId;
        }

        public static string GetConnectionString(string connectionStringName = "DefaultConnectionStrings")
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfiguration configuration = builder.Build();

            return configuration.GetConnectionString(connectionStringName);
        }

        
        
    }


}


