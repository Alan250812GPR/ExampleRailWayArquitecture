using MySql.Data.MySqlClient;
using System.Net.Http.Headers;

public class NotificaionMuestra
{
    private readonly HttpClient client;
    private readonly string urlBase;

    public NotificaionMuestra(HttpClient client, string urlBase)
    {
        this.client = client;
        this.urlBase = urlBase;
    }

    public async Task ProcessReaderAsync(MySqlDataReader reader)
    {
        try
        {
            while (await reader.ReadAsync())
            {
                int Example = reader.GetInt32("example");
                int Validate = reader.GetInt32("example");

                await HandleStatusAsync(Example,Validate);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error al procesar el lector: " + ex.Message);
        }
        finally
        {
            await reader.CloseAsync();
        }
    }

    private async Task HandleStatusAsync(int Example, int Validate)
    {
        if (Validate == 0 && Validate >= 15)
        {
            var result = await SendUrl(Example, Validate);
            Console.WriteLine(result);
        }
        else
        {
            Console.WriteLine("Envio no necesario");
        }
    }

    private async Task<string> SendUrl(int Example, int Validate)
    {
        try
        {    
            var data = new Dictionary<string, string>
            {
                { "Example", Example.ToString() },
                { "Validate", Validate.ToString() }
            };

            var content = new FormUrlEncodedContent(data)
            {
                Headers = { ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded") }
            };

            var response = await client.PostAsync(urlBase + "Rest to url", content);
            return await response.Content.ReadAsStringAsync();
        }
        catch (Exception e)
        {
            return "Error: " + e.Message;
        }
    }
}