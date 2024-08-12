using Microsoft.Azure.Cosmos;

namespace CosmosDb;

class Program
{
    private static readonly string EndpointUri = "https://az204-vku-cosmos.documents.azure.com:443/";
    private static readonly string PrimaryKey = "...TODO...";

    private CosmosClient _cosmosClient = null!;

    
    // The database we will create
    private Database _database = null!;

    // The container we will create.
    private Container _container = null!;

    private string databaseId = "az204Database";
    private string containerId = "az204Container";

    public static async Task Main(string[] args)
    {
        try
        {
            Console.WriteLine("Beginning operations...\n");
            Program p = new Program();
            await p.CosmosAsync();

        }
        catch (CosmosException de)
        {
            Exception baseException = de.GetBaseException();
            Console.WriteLine("{0} error occurred: {1}", de.StatusCode, de);
        }
        catch (Exception e)
        {
            Console.WriteLine("Error: {0}", e);
        }
        finally
        {
            Console.WriteLine("End of program, press any key to exit.");
            Console.ReadKey();
        }
    }

    public async Task CosmosAsync()
    {
        // Create a new instance of the Cosmos Client
        _cosmosClient = new CosmosClient(EndpointUri, PrimaryKey);

        // Runs the CreateDatabaseAsync method
        await CreateDatabaseAsync();

        // Run the CreateContainerAsync method
        await CreateContainerAsync();
    }

    private async Task CreateDatabaseAsync()
    {
        // Create a new database using the cosmosClient
        _database = await _cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
        Console.WriteLine("Created Database: {0}\n", _database.Id);
    }

    private async Task CreateContainerAsync()
    {
        // Create a new container
        _container = await _database.CreateContainerIfNotExistsAsync(containerId, "/LastName");
        Console.WriteLine("Created Container: {0}\n", _container.Id);
    }
}
 
