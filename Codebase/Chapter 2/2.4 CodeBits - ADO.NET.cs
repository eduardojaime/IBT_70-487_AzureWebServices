public class ADOHelper
{
    public void ImplementAsyncMethods()
    {
        using (SqlConnection sqlConnection = new SqlConnection("ConnectionStringName"))
        {
            sqlConnection.Open();
            using (SqlCommand sqlCommand = new SqlCommand("SELECT * FROM Transactions WHERE id = @ID", Connection))
            {
                sqlCommand.Parameters.AddWithValue("@ID", "IDValue");
                IAsyncResult sqlResult = sqlCommand.BeginExecuteReader(CommandBehavior.CloseConnection);
                while (!sqlResult.IsCompleted)
                {
                    //Wait or do something else
                }
                using (SqlDataReader sqlReader = sqlCommand.EndExecuteReader(Result))
                {
                    // You have the reader, use it!
                }
            }
        }

    }
}