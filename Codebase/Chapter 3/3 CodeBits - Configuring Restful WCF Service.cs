// SVCUTILS.EXE
/*
svcutil.exe /language:cs /out:TestServiceProxy.cs /config:app.config http://www.williamgryan.mobi/487/TestService 

svcutil.exe /language:c# /out:TestServiceProxy.cs /config:app.config http://www.williamgryan.mobi/487/TestService
*/

// Decorate contracts using [WebGet] and [WebInkove] (POST,PUT,DELETE), also use UriTemplate attribute
// Implement interface 
// Add service behaviour and binding
// Add an endpoint behaviour with the webHttp attribute and binding webHttpBinding
// Add service and point behaviorconfiguration and binding to the previously mentioned attributes
// Use JSON by setting it in the responseformat of the service or adding json/xml endpoints with json/xml behaviors
// Add a rewrite
// https://www.codeproject.com/Articles/571813/A-Beginners-Tutorial-on-Creating-WCF-REST-Services
[ServiceContract]
public interface IBookService
{
    [OperationContract]
    [WebGet]
    List<Book> GetBooksList();

    [OperationContract]
    [WebGet(UriTemplate  = "Book/{id}")]
    Book GetBookById(string id);

    [OperationContract]
    [WebInvoke(UriTemplate = "AddBook/{name}")]
    void AddBook(string name);

    [OperationContract]
    [WebInvoke(UriTemplate = "UpdateBook/{id}/{name}")]
    void UpdateBook(string id, string name);

    [OperationContract]
    [WebInvoke(UriTemplate = "DeleteBook/{id}")]
    void DeleteBook(string id);
}

//Consuming Service using JS
  $.ajax({
    type: "POST",
    accepts: "application/json",
    url: uri,
    contentType: "application/json",
    data: JSON.stringify(item),
    error: function(jqXHR, textStatus, errorThrown) {
      alert("Something went wrong!");
    },
    success: function(result) {
      getData();
      $("#add-name").val("");
    }
  });

  /* Example Requests

  http://localhost:6366/LibraryService.svc/GetBookById?bookid=900
  http://localhost:6366/LibraryService.svc/XML/GetBookById?bookid=900
  http://localhost:6366/LibraryService.svc/JSON/GetBookById?bookid=900

After Rewrite
  http://localhost:6366/LibraryService/GetBookById?bookid=900

  */

//Channel factories
String endpointUri = "http://www.williamgryan.mobi/487/TestService.svc";
String endpointConfigName = "wsHttp_BindingConfig";
WSHttpBinding wSBinding = new WSHttpBinding(EndpointConfigName);
ChannelFactory<ITestService> proxy =
new ChannelFactory<ITestService>(wSBinding, new
EndpointAddress(endpointUri));

ITestService serviceInstance = proxy.CreateChannel();
String[] outlineItems = serviceInstance.GetExamOutline("70-487");
String questionOne = serviceInstance.GetQuestionText(1);
AnswerSet[] answersQuestionOne = serviceInstance.GetQuestionAnswers(1);
AnswerDetails answersDetailsQuestionOne = serviceInstance.GetAnswerDetails(1);
proxy.Close(new TimeSpan(0,0,1,0,0));