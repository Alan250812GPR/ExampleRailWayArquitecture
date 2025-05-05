//Bot realizado en RailWay
class Program
{
    protected static readonly string connectionString = "YourConection";
    protected static readonly string sql = "Your Query";
    protected static readonly string urlBase = "Url Base";
    private static readonly HttpClient client = new HttpClient();

    static async Task Main()
    {
        Console.WriteLine("Iniciando proceso");
        
        var databaseManager = new DatabaseManager(connectionString);
        var connectionResult = await databaseManager.OpenConnectionAsync();

        if (connectionResult.IsSuccess)
        {
            var readerResult = await databaseManager.ExecuteReaderAsync(connectionResult.Value, sql);
            if (readerResult.IsSuccess)
            {
                var NotificaionMuestra = new NotificaionMuestra(client, urlBase);
                await NotificaionMuestra.ProcessReaderAsync(readerResult.Value);
            }
            else
            {
                Console.WriteLine(readerResult.Error);
            }
        }
        else
        {
            Console.WriteLine(connectionResult.Error);
        }
    }
}