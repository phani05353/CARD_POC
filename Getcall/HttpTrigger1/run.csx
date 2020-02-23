#r "Newtonsoft.Json"

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Data.SqlClient;

public static async Task<Data> Run(HttpRequest req, ILogger log)
{
    Data dta = new Data();
    
    var str = Environment.GetEnvironmentVariable("sqldb_connection");

    using (SqlConnection myConnection = new SqlConnection(str))

        {
            string oString = "EXEC testproc @cardType =1";
            SqlCommand oCmd = new SqlCommand(oString, myConnection);           
            myConnection.Open();
            using (SqlDataReader oReader = oCmd.ExecuteReader())
                {
                    while (oReader.Read())
                    {  
                        dta.Country = oReader["Country"].ToString();
                        dta.APRStartDate = oReader["APRStartDate"].ToString();
                        dta.APREndDate = oReader["APREndDate"].ToString();
                        dta.City = oReader["City"].ToString();                  
                    }
                    myConnection.Close();
                }               
        }
    return dta;
}

public class Data
{
    public string APRStartDate { get; set; }
    public string Country { get; set; }
    public string APREndDate { get; set; }
    public string City { get; set; }
}


