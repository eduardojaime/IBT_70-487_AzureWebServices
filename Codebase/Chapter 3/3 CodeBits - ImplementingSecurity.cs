public void ShowMessageSecurity () {
    // Set Security Mode to Message in Constructor
    WSHttpBinding WsHttpSecurity = new WSHttpBinding (SecurityMode.Message);
    // Use default constructor
    WSHttpBinding wsHttpSecurity2 = new WSHttpBinding ();
    // Set the Security property manually
    wsHttpSecurity2.Security.Mode = SecurityMode.Message;
}

// Config > Editor > Bindings > Security Mode = Message
/*
<wsHttpBinding>
<binding name="wsHttpBindingConfigSample" >
<security mode="Message"/>
</binding>
</wsHttpBinding>
*/

// Transport level security
public void ShowMessageSecurity () {
    // Set Security Mode to Message in Constructor
    WSHttpBinding WsHttpSecurity = new WSHttpBinding (SecurityMode.Transport);
    // Use default constructor
    WSHttpBinding WsHttpSecurity2 = new WSHttpBinding ();
    // Set the Security property manually
    WsHttpSecurity2.Security.Mode = SecurityMode.Transport;
}
// Config > Editor > Bindings > Security Mode = Transport
/*
<wsHttpBinding>
<binding name="wsHttpBindingConfigSample">
<security mode="Transport" />
</binding>
</wsHttpBinding>
*/