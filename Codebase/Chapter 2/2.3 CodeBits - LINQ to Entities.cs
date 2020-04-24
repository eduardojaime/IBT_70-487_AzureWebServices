

public class LINQtoEntitiesHelper
{
    public void ImplementLINQtoEntities()
    {
        // Select all
        IQueryable<Account> query = from acct in Context.Accounts
                                    where acct.AccountAlias == "Primary"
                                    select acct;
        // Select fields
        IQueryable<Account> query = from acct in Context.Accounts
                                    where acct.AccountAlias == "Primary"
                                    select acct.CreatedDate;
        // Populating custom object
        IQueryable<Account> query = from acct in Context.Accounts
                                    where acct.AccountAlias == "Primary"
                                    select new
                                    {
                                        AccountAlias = acct.AccountAlias,
                                        CreatedDate = acct.CreatedDate
                                    };

        // Log a query > Examine the sql statements being generated to determine if they are the problem
        IQueryable<Account> Query = from Acct in Context.Accounts
                                    where Acct.AccountAlias == "Primary"
                                    select Acct;
        // You can cast this query explicitly to an ObjectQuery; upon doing so, you can trace the
        //query information using the following addition:
        String Tracer = (Query as ObjectQuery).ToTraceString();
        //The same can be accomplished using the DbContext in a similar manner:
        String Output = (from Acct in ContextName.ContextSet
                         select Acct).ToString();

    }
}