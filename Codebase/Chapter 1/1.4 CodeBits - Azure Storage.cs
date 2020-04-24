public class AzureStorageHelper
{
    // Azure Storage
    public void UploadBlob(string fileContents, string filename, string containerName, string accountName, string accountKey)
    {
        // ARRANGE
        StorageCredentials creds = new StorageCredentials(accountName, accountKey);
        CloudStorageAccount acct = new CloudStorageAccount(creds, true);

        CloudBlobClient client = acct.CreateCloudBlobClient();
        CloudBlobContainer container = client.GetContainerReference(containerName);
        // ACT
        container.CreateIfNotExists();

        ICloudBlob blob = container.GetBlockBlobReference(filename);
        using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(fileContents)))
        {
            blob.UploadFromStream(stream);
        }
        // ASSERT
        Assert.That(blob.Properties.Length, Is.EqualTo(fileContents.Length));
    }

    // USING WEB CONFIG
    // <add name = "StorageConnection" connectionString="DefaultEndpointsProtocol=https;AccountName=ACCOUNT_NAME_GOES_HERE;AccountKey=ACCOUNT_KEY_GOES_HERE" />

    // and then
    public void UploadBlobFromConfig(string fileContents, string filename, string containerName)
    {
        // ARRANGE
        CloudStorageAccount acct = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnection"].ConnectionString);
        CloudBlobClient client = acct.CreateCloudBlobClient();
        CloudBlobContainer container = client.GetContainerReference(containerName);
        // ACT
        container.CreateIfNotExists();
        ICloudBlob blob = container.GetBlockBlobReference(filename);
        using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(fileContents)))
        {
            blob.UploadFromStream(stream);
        }
        // ASSERT
        Assert.That(blob.Properties.Length, Is.EqualTo(fileContents.Length));
    }

    public void DeleteBlobFromConfig(string filename, string containerName)
    {
        // ARRANGE
        CloudStorageAccount acct = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnection"].ConnectionString);
        CloudBlobClient client = acct.CreateCloudBlobClient();
        CloudBlobContainer container = client.GetContainerReference(containerName);
        // ACT
        container.CreateIfNotExists();
        ICloudBlob blob = container.GetBlockBlobReference(filename);
        bool wasDeleted = blob.DeleteIfExists();
        // ASSERT
        Assert.That(wasDeleted, Is.EqualTo(true));
    }

    // Tables and Queues
    public class Record : TableEntity
    {
        public Record() : this(DateTime.UtcNow.ToShortDateString(), Guid.NewGuid().ToString())
        { }
        public Record(string partitionKey, string rowKey)
        {
            this.PartitionKey = partitionKey;
            this.RowKey = rowKey;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
    [TestCase("file")]
    public void UploadTableFromConfig(string tableName)
    {
        // ARRANGE
        CloudStorageAccount acct = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["StorageConnection"].ConnectionString);
        CloudTableClient client = acct.CreateCloudTableClient();

        var table = client.GetTableReference(tableName);

        Record entity = new Record("1", "asdf") { FirstName = "Fred", LastName = "Flintstone" };
        // ACT
        table.CreateIfNotExists(); // create table
        TableOperation insert = TableOperation.Insert(entity);
        table.Execute(insert); // insert record

        TableOperation fetch = TableOperation.Retrieve<Record>("1", "asdf");
        TableResult result = table.Execute(fetch); // fetch record
        
        TableOperation del = TableOperation.Delete(result.Result as Record);
        table.Execute(del); // delete record
                            // ASSERT
        Assert.That(((Record)result.Result).FirstName, Is.EqualTo("Fred"));
    }
    // SQL Databases - rETRIES

    public class MyRetryStrategy : ITransientErrorDetectionStrategy
    {
        public bool IsTransient(Exception ex)
        {
            if (ex != null && ex is SqlException)
            {
                foreach (SqlError error in (ex as SqlException).Errors)
                {

                    switch (error.Number)
                    {
                        case 1205:
                            System.Diagnostics.Debug.WriteLine("SQL Error: Deadlock condition. Retrying...");
                            return true;
                        case -2:
                            System.Diagnostics.Debug.WriteLine("SQL Error: Timeout expired. Retrying...");
                            return true;
                    }
                }
            }
            // For all others, do not retry.
            return false;
        }
    }
    public void sqlConnectionRetry()
    {
        //You can use the retry strategy when executing a query from ADO.NET:
        // RetryPolicy retry = new RetryPolicy<MyRetryStrategy>(5, new TimeSpan(0, 0, 5));
        using (SqlConnection connection = new SqlConnection(< connectionstring >))
        {
            connection.OpenWithRetry(retry);
            SqlCommand command = new SqlCommand("<sql query>");
            command.Connection = connection;
            command.CommandTimeout = CommandTimeout;
            SqlDataReader reader = command..ExecuteReaderWithRetry(retry);
            while (reader.Read())
            {
                // process data
            }
        }
    }
}