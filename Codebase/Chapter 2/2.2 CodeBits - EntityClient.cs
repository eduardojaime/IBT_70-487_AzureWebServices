using System.Data.EntityClient;
using System.Data.Transactions;

public class EntityDataProviderHelper
{
    public void ImplementEntityClient()
    {

        try
        {
            String provider = "System.Data.SqlClient";
            String serverName = "localhost";
            String databaseName = "dbname";

            SqlConnectionStringBuilder donnBuilder = new SqlConnectionStringBuilder();
            ConnBuilder.DataSource = serverName;
            ConnBuilder.InitialCatalog = databaseName;

            EntityConnectionStringBuilder efBuilder = new EntityConnectionStringBuilder();
            efBuilder.Provider = Provider;
            efBuilder.ProviderConnectionString = ConnBuilder.ToString();
            efBuilder.Metadata = @"*csdl, *ssdl, *msl";

            EntityConnection Connection = new EntityConnection(EfBuilder.ToString());
            // EntityConnection connection = new EntityConnection(ConnectionStringHere);

            // Transactions
            EntityFrameworkSamplesEntities Context = new EntityFrameworkSamplesEntities();

            // context does not provide database so use Context.Database object
            IDbTransaction TransactionInstance = Context.Database.Connection.BeginTransaction();
            try
            {
                // Do Something
                Context.SaveChanges();
                TransactionInstance.Commit();
            }
            catch (EntityCommandExecutionException)
            {
                TransactionInstance.Rollback();
            }
            
            EntityFrameworkSamplesEntities Context = new EntityFrameworkSamplesEntities();
            using (TransactionScope CurrentScope = new TransactionScope())
            {
                try
                {
                    Context.SaveChanges();
                    CurrentScope.Complete();
                }
                catch (EntityCommandExecutionException)
                {
                    //Handle the exception as you normally would
                    //It won't be committed so transaction wise you're done
                }
            }


        }
        catch (Exception ex)
        {

        }
        finally
        {

        }

    }
}