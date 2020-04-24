

[HttpPost]
public HttpResponseMessage PostAccount(Account account)
{
HttpResponseMessage response =
Request.CreateResponse<Account>(HttpStatusCode.Created, account);
string newUri = Url.Link("NamedApi", new { accountId = account.AccountId });
response.Headers.Location = new Uri(newUri);
return response;
}

[HttpPut]
public HttpResponseMessage PutAccount(int id, Account account)
{
// perform insert and/or edit here.
}

public class AccountController : ApiController
{
public IEnumerable<Account> GetAccounts()
{
return DataRepository.Accounts;
}
public Account GetAccount(int accountId)
{
Account result = DataRepository.Accounts.SingleOrDefault(acc =>
acc.AccountId == accountId);
if (result == null)
{
throw new HttpResponseException(HttpStatusCode.NotFound);
}
return result;
}
}


public class CustomerController : ApiController
{
public IEnumerable<Customer> GetCustomers(int accountId)
{
return DataRepository.Customers.Where(cust =>
cust.AccountId == accountId);
}
public IEnumerable<Customer> SearchCustomers(string lastName)
{
return DataRepository.Customers.Where(cust =>
cust.LastName.ToLower().Contains(lastName.ToLower()));
}
}

public static class DataRepository
{
public static Account[] Accounts = new Account[]
{
new Account{ AccountId = 1, AccountAlias = "Disney"},
new Account{ AccountId = 2, AccountAlias = "Marvel"},
new Account{ AccountId = 3, AccountAlias = "McDonald's"},
new Account{ AccountId = 4, AccountAlias = "Flintstones"}
};
public static Customer[] Customers = new Customer[]
{
new Customer{ AccountId = 1, CustomerId = 1,
FirstName = "Mickey", LastName = "Mouse"},
new Customer{ AccountId = 1, CustomerId = 2,
FirstName = "Minnie", LastName = "Mouse"},
new Customer{ AccountId = 1, CustomerId = 3,
FirstName = "Donald", LastName = "Duck"},
new Customer{ AccountId = 2, CustomerId = 4,
Objective 4.1: Design a Web API CHAPTER 4 297
FirstName = "Captain", LastName = "America"},
new Customer{ AccountId = 2, CustomerId = 5,
FirstName = "Spider", LastName = "Man"},
new Customer{ AccountId = 2, CustomerId = 6,
FirstName = "Wolverine", LastName = "N/A"},
new Customer{ AccountId = 3, CustomerId = 7,
FirstName = "Ronald", LastName = "McDonald"},
new Customer{ AccountId = 3, CustomerId = 8,
FirstName = "Ham", LastName = "Burgler"},
new Customer{ AccountId = 4, CustomerId = 9,
FirstName = "Fred", LastName = "Flintstone"},
new Customer{ AccountId = 4, CustomerId = 10,
FirstName = "Wilma", LastName = "Flintstone"},
new Customer{ AccountId = 4, CustomerId = 11,
FirstName = "Betty", LastName = "Rubble"},
new Customer{ AccountId = 4, CustomerId = 12,
FirstName = "Barney", LastName = "Rubble"}
};
}


//Use decorators 
[HttpGet] // or [System.Web.Http.AcceptVerbs("GET", "HEAD")]
public Account GetAccount(int accountId)
{
Account result = DataRepo.Accounts.SingleOrDefault(acc =>
acc.AccountId == accountId);
if (result == null)
{
throw new HttpResponseException(HttpStatusCode.NotFound);
}
return result;
}

[HttpGet]
[ActionName("FindCustomers")]
public IEnumerable<Customer> SearchCustomers(string lastName)
{
return DataRepository.Customers.Where(cust =>
cust.LastName.ToLower().Contains(lastName.ToLower()));
}