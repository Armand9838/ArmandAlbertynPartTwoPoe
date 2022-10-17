using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ArmandAlbertynPartOnePoe.Pages.DonationPage
{
    public class EditModel : PageModel
    {

        public DonateInfo donateInfo = new DonateInfo();
        public String errorMessage = "";
        public String successMessage = "";

        public void OnGet()
        {
            String DonationID = Request.Query["DonationID"];

            try
            {
                string connectionString = "Data Source=apprdbarmand.database.windows.net;Initial Catalog=ApprPoe;Persist Security Info=True;User ID=Armand;Password=Adminpassword@01";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Donation WHERE id=@DonationID";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@id", DonationID);
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                DonateInfo donateInfo = new DonateInfo();
                                donateInfo.DonationID = "" + reader.GetInt32(0);
                                donateInfo.DonateDate = reader.GetDateTime(1).ToString();
                                donateInfo.DonateAmount = reader.GetString(2);
                                donateInfo.DonateCategory = reader.GetString(3);
                                donateInfo.DonateDescription = reader.GetString(4);
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
            donateInfo.DonationID = Request.Form["DonationID"];
            donateInfo.DonateDate = Request.Form["DonateDate"];
            donateInfo.DonateAmount = Request.Form["DonateAmount"];
            donateInfo.DonateCategory = Request.Form["DonateCategory"];
            donateInfo.DonateDescription = Request.Form["DonateDescription"];

            if (donateInfo.DonationID.Length == 0 || donateInfo.DonateDate.Length == 0 || donateInfo.DonateAmount.Length == 0)
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
                    String sql = "UPDATE Donation " +
                                 "( SET DonationID=@DonationID, DonateDate=@DonateDate, DonateAmount=@DonateAmount, DonateCategory=@DonateCategory, DonateDescription=@DonateDescription) VALUES " +
                                 "WHERE DonationID=@DonationID";

                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@DonationID", donateInfo.DonationID);
                        command.Parameters.AddWithValue("@DonateDate", donateInfo.DonateDate);
                        command.Parameters.AddWithValue("@DonateAmount", donateInfo.DonateAmount);
                        command.Parameters.AddWithValue("@DonateCategory", donateInfo.DonateCategory);
                        command.Parameters.AddWithValue("@DonateDescription", donateInfo.DonateDescription);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return;
            }

            Response.Redirect("/DonationPage/Donate");

        }
     }
 }
            
