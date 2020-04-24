/*
Enabling Streaming
At the contract level, you start the process by defining the return type of a method as
a Stream. 

Next, you need to configure the binding, through code or configuration setting,
the TransferMode enumeration value to Streamed. 

Then you typically want to set the max-
ReceivedMessageSize to a value that’s the largest value you calculate to need for items you
stream.

<basicHttpBinding>
<binding name="HttpStreaming" maxReceivedMessageSize="67108864"
transferMode="Streamed"/>
</basicHttpBinding>
<customBinding>
<binding name="487CustomBinding">
<textMessageEncoding messageVersion="Soap12WSAddressing10" />
<httpTransport transferMode="Streamed"
maxReceivedMessageSize="67108864"/>
</binding>
</customBinding>

*/