// CONFIGURING WCF SERVICE THROUGH API
[ServiceContract(Namespace="http://www.williamgryan.mobi/Books/70-487",
Name="RandomText")]
public interface ITestService{}

[ServiceBehavior(Name="Test", Namespace="http://www.williamgryan.mobi/Books/70-487/Services")]
public class TestService : ITestService
{}

// Default BasicHttpBinding
BasicHttpBinding BasicHttp = new BasicHttpBinding();
// SecurityMode Specified
// SecurityMode values include None, Transport, Message, TransportCredentialOnly
// and TransportWithMessageCredential
BasicHttpBinding BasicHttpSecurity = new BasicHttpBinding(BasicHttpSecurityMode.None);
// Using Binding configured in .config file
BasicHttpBinding BasicHttpConfig = new BasicHttpBinding("BasicHttpBindingConfigSample");

// Default wsHttpBinding
WSHttpBinding WsHttp = new WSHttpBinding();
// SecurityMode Specified
// SecurityMode values include None, Transport, Message
// and TransportWithMessageCredential
WSHttpBinding WsHttpSecurity = new WSHttpBinding(SecurityMode.None);
// Uses Binding configured in .config file
WSHttpBinding WsHttpConfig = new WSHttpBinding("wsHttpBindingConfigSample");
// Sets the Security mode and indicates whether or not
// ReliableSessionEnabled should be Enabled or Not
WSHttpBinding WsHttpReliable = new WSHttpBinding(SecurityMode.None, true);