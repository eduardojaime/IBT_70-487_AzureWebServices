// Transaction Options
//TransactionScope
public class TransactionsHelper
{
    public void example()
    {
        using (TransactionScope mainScope = new TransactionScope())
        {
            // ADO.NET 
            using (SqlConnection firstConnection = new SqlConnection("First"))
            {
                firstConnection.Open();
                using (SqlCommand firstCommand = new SqlCommand("FirstQueryText", firstConnection))
                {
                    Int32 recordsAffected = firstCommand.ExecuteNonQuery();
                }
                
                using (SqlConnection secondConnection = new SqlConnection("Second"))
                {
                    secondConnection.Open();
                    using (SqlCommand secondCommand = new SqlCommand("SecondQueryText", secondConnection))
                    {
                        Int32 secondAffected = secondCommand.ExecuteNonQuery();
                    }
                }
            }
            mainScope.Complete();
        }

        //Entity Connection
        using (TestEntities database = new TestEntities())
        {
            Customer cust = new Customer();
            cust.FirstName = "Ronald";
            cust.LastName = "McDonald";
            cust.AccountId = 3;
            database.Customers.Add(cust);
            database.SaveChanges();
        }

        using (EntityConnection connection = new EntityConnection("TestEntities"))
        {
            using (EntityTransaction trans = connection.BeginTransaction(System.Data.IsolationLevel.Serializable))
            {
                EntityCommand CurrentCommand = new EntityCommand("SOME UPDATE STATEMENT", connection, trans);
                connection.Open();
                Int32 RecordsAffected = CurrentCommand.ExecuteNonQuery();
                trans.Commit();
            }
        }

        SqlConnection myConnection = new SqlConnection("Connection String");

    }
}
