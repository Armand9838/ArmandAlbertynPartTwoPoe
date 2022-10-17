using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ArmandAlbertynPartOnePoe.Pages.DonationPage
{
    public class CreateModel : PageModel
    {
        public DonateInfo donateInfo = new DonateInfo();

        public String errorMessage = "";

        public string successMessage = "";

        public void OnGet()
        {
        }

        public void OnPost()
        {
            donateInfo.DonationID = Request.Form["DonationID"];
            donateInfo.DonateDate = Request.Form["DonateDate"];
            donateInfo.DonateAmount = Request.Form["DonateAmount"];
            donateInfo.DonateCategory = Request.Form["DonateCategory"];
            donateInfo.DonateDescription = Request.Form["DonateDescription"];

            if(donateInfo.DonateDate.Length == 0 || donateInfo.DonateAmount.Length == 0)
            {
                errorMessage = "Fields are required!";
                return;
            }

            //save donations into database

            try
            {
                String connectionString = "Data Source=apprdbarmand.database.windows.net;Initial Catalog=ApprPoe;Persist Security Info=True;User ID=Armand;Password=Adminpassword@01";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "INSERT INTO Donation " +
                                 "(ID, date, amount, category, description) VALUES " +
                                 "(@DonationID, @DonateDate, @DonateAmount, @DonateCategory, @DonateDescription);";

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

            donateInfo.DonationID = ""; donateInfo.DonateDate = ""; donateInfo.DonateAmount = ""; donateInfo.DonateCategory = ""; donateInfo.DonateDescription = "";
            successMessage = "New donation added correctly!";

            Response.Redirect("/DonationPage/Donate");
        }
    }
}
