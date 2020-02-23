#r "Newtonsoft.Json"

using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Data.SqlClient;

public static async Task<string> Run(HttpRequest req, ILogger log)
{
    log.LogInformation("Post Call Triggred");

    int rows =0;

    string APRStartTime = req.Query["APRStartTime"];
    string APREndTime = req.Query["APREndTime"];
    string Country = req.Query["Country"];
    string City = req.Query["City"];
    string CardType = req.Query["CardType"];

    string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
    dynamic data = JsonConvert.DeserializeObject(requestBody);
    APRStartTime = APRStartTime ?? data?.APRStartTime;
    APREndTime = APREndTime ?? data?.APREndTime;
    Country = Country ?? data?.Country;
    City = City ?? data?.City;
    CardType = CardType ?? data?.CardType;

    DateTime myDateAPRStart = DateTime.ParseExact(APRStartTime, "yyyy-MM-dd HH:mm:ss,fff",
                                       System.Globalization.CultureInfo.InvariantCulture);

    DateTime myDateAPREnd = DateTime.ParseExact(APREndTime, "yyyy-MM-dd HH:mm:ss,fff",
                                       System.Globalization.CultureInfo.InvariantCulture);

    var str = Environment.GetEnvironmentVariable("sqldb_connection");
    using (SqlConnection conn = new SqlConnection(str))
    {
        conn.Open();
        var text = "INSERT INTO testTable VALUES (@CardType,@myDateAPRStart,@myDateAPREnd,@Country,@City)";

        using (SqlCommand cmd = new SqlCommand(text, conn))
        {
            cmd.Parameters.AddWithValue("@CardType",CardType);
            cmd.Parameters.AddWithValue("@myDateAPRStart",myDateAPRStart);
            cmd.Parameters.AddWithValue("@myDateAPREnd",myDateAPREnd);
            cmd.Parameters.AddWithValue("@Country",Country);
            cmd.Parameters.AddWithValue("@City",City);
            rows = await cmd.ExecuteNonQueryAsync();
            log.LogInformation($"{rows} rows were updated");
        }
    }

    if (rows > 0)
    {
        return "Insert Successful";
    }
    else 
    {
        return "Error Occured";
    }
}
