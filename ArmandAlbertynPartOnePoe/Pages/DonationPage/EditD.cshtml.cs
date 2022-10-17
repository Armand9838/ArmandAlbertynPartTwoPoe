using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ArmandAlbertynPartOnePoe.Pages.DonationPage
{
    public class EditDModel : PageModel
    {

        public DisasterInfo disasterInfo = new DisasterInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String DisasterID = Request.Query["DisasterID"];

            try
            {
                string connectionString = "Data Source=apprdbarmand.database.windows.net;Initial Catalog=ApprPoe;Persist Security Info=True;User ID=Armand;Password=Adminpassword@01";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Donation WHERE DisasterID=@DisasterID";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", DisasterID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                DisasterInfo disasterInfo = new DisasterInfo();
                                disasterInfo.DisasterID = "" + reader.GetInt32(0);
                                disasterInfo.DisasterStart = reader.GetDateTime(1).ToString();
                                disasterInfo.DisasterEnd = reader.GetDateTime(2).ToString();
                                disasterInfo.DisasterLocation = reader.GetString(3);
                                disasterInfo.DisasterDescription = reader.GetString(4);
                                disasterInfo.DisasterAid = reader.GetString(5);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
            }
        }

        public void OnPost()
        {
            disasterInfo.DisasterID = Request.Form["DisasterID"];
            disasterInfo.DisasterStart = Request.Form["DisasterStart"];
            disasterInfo.DisasterEnd = Request.Form["DisasterEnd"];
            disasterInfo.DisasterLocation = Request.Form["DisasterLocation"];
            disasterInfo.DisasterDescription = Request.Form["DisasterDescription"];
            disasterInfo.DisasterAid = Request.Form["DisasterAid"];

            if (disasterInfo.DisasterID.Length == 0 || disasterInfo.DisasterStart.Length == 0 || disasterInfo.DisasterDescription.Length == 0)
            {
                errorMessage = "These fields are required!";
                return;
            }

            try
            {
                String connectionString = "Data Source=apprdbarmand.database.windows.net;Initial Catalog=ApprPoe;Persist Security Info=True;User ID=Armand;Password=Adminpassword@01";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "UPDATE Disaster " +
                                 "( SET DisasterID=@DisasterID, DisasterStart=@DisasterStart, DisasterEnd=@DisasterEnd, DisasterLocation=@DisasterLocation, DisasterDescription=@DisasterDescription, DisasterAid=@DisasterAid) VALUES " +
                                 "WHERE DisasterID=@DisasterID";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@DisasterID", disasterInfo.DisasterID);
                        command.Parameters.AddWithValue("@DisasterStart", disasterInfo.DisasterStart);
                        command.Parameters.AddWithValue("@DisasterEnd", disasterInfo.DisasterEnd);
                        command.Parameters.AddWithValue("@DisasterLocation", disasterInfo.DisasterLocation);
                        command.Parameters.AddWithValue("@DisasterDescription", disasterInfo.DisasterDescription);
                        command.Parameters.AddWithValue("@DisasterAid", disasterInfo.DisasterAid);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/DonationPage/Disaster");
        }
    }
}
