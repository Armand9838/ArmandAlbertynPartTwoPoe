using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ArmandAlbertynPartOnePoe.Pages.DonationPage
{
    public class DonateModel : PageModel
    {
        public List<DonateInfo> ListDonate = new List<DonateInfo>();

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=apprdbarmand.database.windows.net;Initial Catalog=ApprPoe;Persist Security Info=True;User ID=Armand;Password=Adminpassword@01";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Donation";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DonateInfo donateInfo = new DonateInfo();
                                donateInfo.DonationID = "" + reader.GetInt32(0);
                                donateInfo.DonateDate = reader.GetDateTime(1).ToString();
                                donateInfo.DonateAmount = reader.GetString(2);
                                donateInfo.DonateCategory = reader.GetString(3);
                                donateInfo.DonateDescription = reader.GetString(4);

                                ListDonate.Add(donateInfo);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exeption: " + ex.ToString());
            }
        }
    }

    public class DonateInfo
    {
        public string DonationID;
        public string DonateDate;
        public string DonateAmount;
        public string DonateCategory;
        public string DonateDescription;
    }
}
