public class AzureStorageHelper
{
    // This method is called only once to initialize service-wide policies.
    public static void InitializeService(DataServiceConfiguration config)
    {
// TODO: set rules to indicate which entity sets and service operations are visible, updatable, etc.
// Examples:
// config.SetEntitySetAccessRule("MyEntityset", EntitySetRights.AllRead);
// config.SetServiceOperationAccessRule("MyServiceOperation", ServiceOperationRights.All);
        config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V2;
    }

    // SetEntitySetAcessRule
    //config.SetEntitySetAccessRule("Courses", EntitySetRights.AllRead | EntitySetRights.WriteMerge);

    // SetServiceOperationsAcessRule
    //config.SetServiceOperationAccessRule("OperationName", ServiceOperationRights.All);


    // QUERIES - oData
    // use https://services.odata.org/V4/TripPinServiceRW/
    public Example()
    {
        String ServiceUri = "http://servicehost/ExamPrepService.svc";
        ExamServiceContext ExamContext = new ExamServiceContext(new Uri(ServiceUri);
        DataServiceQuery < Questions > = ExamContext.Question
        .AddQueryOptions("$filter", "id gt 5")
        .AddQueryOptions("$expand", "Answers");
    }
}