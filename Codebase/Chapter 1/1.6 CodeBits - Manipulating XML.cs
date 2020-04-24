// Use this class as an example of how to represent these fields in XML
public class Customer
{
    public Customer() { }
    public Customer(String firstName, String middleInitial, String lastName)
    {
        FirstName = firstName;
        MiddleInitial = middleInitial;
        LastName = lastName;
    }
    public String FirstName { get; set; }
    public String MiddleInitial { get; set; }
    public String LastName { get; set; }
}

public static class XmlManipulationHelper
{
    // CREATE AN XML DOCUMENT THAT REPRESENTS THE CLASS
    public static void WriteCustomers()
    {
        String fileName = "Customers.xml";
        List<Customer> customerList = new List<Customer>();
        Customer johnPublic = new Customer("John", "Q", "Public");
        Customer billRyan = new Customer("Bill", "G", "Ryan");
        Customer billGates = new Customer("William", "G", "Gates");
        customerList.Add(johnPublic);
        customerList.Add(billRyan);
        customerList.Add(billGates);
        using (XmlWriter writerInstance = XmlWriter.Create(fileName))
        {
            writerInstance.WriteStartDocument();
            writerInstance.WriteStartElement("Customers");
            foreach (Customer customerInstance in customerList)
            {
                writerInstance.WriteStartElement("Customer");
                writerInstance.WriteElementString("FirstName", customerInstance.FirstName);
                writerInstance.WriteElementString("MiddleInitial", customerInstance.MiddleInitial);
                writerInstance.WriteElementString("LastName", customerInstance.LastName);
                writerInstance.WriteEndElement();
            }
            writerInstance.WriteEndElement();
            writerInstance.WriteEndDocument();
        }
    }

    // XMLReader example > Iterating through previously created file
    public static void ReadCustomers()
    {
        String fileName = "Customers.xml";
        XmlTextReader reader = new XmlTextReader(fileName);
        while (reader.Read())
        {
            switch (reader.NodeType)
            {
                case XmlNodeType.Element: // The node is an element.
                    Console.Write("<" + reader.Name);
                    Console.WriteLine(">");
                    break;
                case XmlNodeType.Text: //Display the text in each element.
                    Console.WriteLine(reader.Value);
                    break;
                case XmlNodeType.EndElement: //Display the end of the element.
                    Console.Write("</" + reader.Name);
                    Console.WriteLine(">");
                    break;
            }
        }
    }

    public static void XmlDocumentExample()
    {
        //Reading
        String fileName = "Customers.xml";
        XmlDocument documentInstance = new XmlDocument();
        documentInstance.Load(fileName);
        XmlNodeList currentNodes = documentInstance.DocumentElement.ChildNodes;
        foreach (XmlNode myNode in currentNodes)
        {
            Console.WriteLine(myNode.InnerText);
        }
        //Writing 
        XmlDocument documentInstance = new XmlDocument();
        XmlElement customers = documentInstance.CreateElement("Customers");
        XmlElement customer = documentInstance.CreateElement("Customer");
        XmlElement firstNameJohn = documentInstance.CreateElement("FirstName");
        XmlElement middleInitialQ = documentInstance.CreateElement("MiddleInitial");
        XmlElement lastNamePublic = documentInstance.CreateElement("LastName");
        firstNameJohn.InnerText = "John";
        middleInitialQ.InnerText = "Q";
        lastNamePublic.InnerText = "Public";
        customer.AppendChild(firstNameJohn);
        customer.AppendChild(middleInitialQ);
        customer.AppendChild(lastNamePublic);
        customers.AppendChild(customer);
        documentInstance.AppendChild(customers);
    }

    public void XPathQueryExample()
    {
        //language, see http://www.w3.org/TR/xpath
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(xml);
        XPathNavigator nav = doc.CreateNavigator();
        string query = "//People/Person[@firstName='Jane']";
        XPathNodeIterator iterator = nav.Select(query);
        Console.WriteLine(iterator.Count); // Displays 1
        while (iterator.MoveNext())
        {
            string firstName = iterator.Current.GetAttribute("firstName", "");
            string lastName = iterator.Current.GetAttribute("lastName", "");
            Console.WriteLine("Name: {0} {1}", firstName, lastName);
        }
    }

    public void LINQtoXMLExample()
    {

        XElement customers = new XElement("Customers", new XElement("Customer",
    new XElement("FirstName", "John"), new XElement("MiddleInitial", "Q"),
    new XElement("LastName", "Public")));
        String fullName = customers.Element("Customer").Element("FirstName").ToString() +
        customers.Element("Customer").Element("MiddleInitial").ToString() +
        customers.Element("Customer").Element("LastName").ToString();

        XDocument sampleDoc = new XDocument(new XComment("This is a comment sample"),
        new XElement("Customers",
        new XElement("Customer",
        new XElement("FirstName", "John"))));
        sampleDoc.Save("CommentFirst.xml");
    }

    public void AdvancedManipulation()
    {
        String encodedFirstName = XmlConvert.EncodeName("First Name");
        Console.WriteLine("Encoded FirstName: {0}", encodedFirstName);
        String decodedFirstName = XmlConvert.DecodeName(encodedFirstName);
        Console.WriteLine("Encoded FirstName: {0}", decodedFirstName);
        String encodedFirstNameWithColon = XmlConvert.EncodeLocalName("First:Name");
        Console.WriteLine("Encoded FirstName with Colon: {0}", encodedFirstNameWithColon);
        decodedFirstName = XmlConvert.DecodeName(encodedFirstNameWithColon);
        Console.WriteLine("Encoded FirstName with Colon: {0}", decodedFirstName);
    }
}