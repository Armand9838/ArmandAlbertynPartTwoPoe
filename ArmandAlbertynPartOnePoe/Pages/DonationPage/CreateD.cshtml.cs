using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ArmandAlbertynPartOnePoe.Pages.DonationPage
{
    public class CreateDModel : PageModel
    {
        public DisasterInfo disasterInfo = new DisasterInfo();

        public String errorMessage = "";

        public string successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            disasterInfo.DisasterID = Request.Form["DisasterID"];
            disasterInfo.DisasterStart = Request.Form["DisasterStart"];
            disasterInfo.DisasterEnd = Request.Form["DisasterEnd"];
            disasterInfo.DisasterLocation = Request.Form["DisasterLocation"];
            disasterInfo.DisasterDescription = Request.Form["DisasterDescription"];
            disasterInfo.DisasterAid = Request.Form["DisasterAid"];

            if (disasterInfo.DisasterID.Length == 0 || disasterInfo.DisasterDescription.Length == 0)
            {
                errorMessage = "Fields are required!";
                return;
            }

            //save disasters into database

            try
            {
                String connectionString = "Data Source=apprdbarmand.database.windows.net;Initial Catalog=ApprPoe;Persist Security Info=True;User ID=Armand;Password=Adminpassword@01";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO Disaster " +
                                 "(DisasterID, DisasterStart, DisasterEnd, DisasterLocation, DisasterDescription, DisasterAid) VALUES " +
                                 "(@DisasterID, @DisasterStart, @DisasterEnd, @DisasterLocation, @DisasterDescription, @DisasterAid);";

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

            disasterInfo.DisasterID = ""; disasterInfo.DisasterStart = ""; disasterInfo.DisasterEnd = ""; disasterInfo.DisasterLocation = ""; disasterInfo.DisasterDescription = ""; disasterInfo.DisasterAid = "";
            successMessage = "New Disaster added correctly!";

            Response.Redirect("/DonationPage/Disaster");
        }
    }
}
