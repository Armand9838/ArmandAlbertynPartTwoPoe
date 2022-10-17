using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace ArmandAlbertynPartOnePoe.Pages.DonationPage
{
    public class DisasterModel : PageModel
    {
        public List<DisasterInfo> ListDisaster = new List<DisasterInfo>();

        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=apprdbarmand.database.windows.net;Initial Catalog=ApprPoe;Persist Security Info=True;User ID=Armand;Password=Adminpassword@01";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    String sql = "SELECT * FROM Disaster";
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                DisasterInfo disasterInfo = new DisasterInfo();
                                disasterInfo.DisasterID = "" + reader.GetInt32(0);
                                disasterInfo.DisasterStart = reader.GetDateTime(1).ToString();
                                disasterInfo.DisasterEnd = reader.GetDateTime(2).ToString();
                                disasterInfo.DisasterLocation = reader.GetString(3);
                                disasterInfo.DisasterDescription = reader.GetString(4);
                                disasterInfo.DisasterAid = reader.GetString(5);


                                ListDisaster.Add(disasterInfo);
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

    public class DisasterInfo
    {
        public string DisasterID;
        public string DisasterStart;
        public string DisasterEnd;
        public string DisasterLocation;
        public string DisasterDescription;
        public string DisasterAid;
    }
}
