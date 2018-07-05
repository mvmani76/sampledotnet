// <snippet1>
using System;
using System.Security.Cryptography;
using System.Security.Cryptography.Xml;
using System.Xml;

public class VerifyXML
{

    public static void Main(String[] args)
    {
            // Create a new CspParameters object to specify
            // a key container.
            // <snippet2>
            CspParameters cspParams = new CspParameters();
            cspParams.KeyContainerName = "XML_DSIG_RSA_KEY";
            // </snippet2>

            // Create a new RSA signing key and save it in the container. 
            // <snippet3>
            RSACryptoServiceProvider rsaKey = new RSACryptoServiceProvider(cspParams);
            // </snippet3>

            // Create a new XML document.
            // <snippet4>
            XmlDocument xmlDoc = new XmlDocument();

            // Load an XML file into the XmlDocument object.
            xmlDoc.PreserveWhitespace = true;
            xmlDoc.Load("test.xml");
            // </snippet4>

            // Verify the signature of the signed XML.
            Console.WriteLine("Verifying signature...");
            bool result = VerifyXml(xmlDoc, rsaKey);

            // Display the results of the signature verification to 
            // the console.
            if (result)
            {
                Console.WriteLine("The XML signature is valid.");
            }
            else
            {
                Console.WriteLine("The XML signature is not valid.");
            }

        }
    }


    

    // Verify the signature of an XML file against an asymmetric 
    // algorithm and return the result.
    public static Boolean VerifyXml(XmlDocument Doc, RSA Key)
    {
        // Check arguments.
        if (Doc == null)
            throw new ArgumentException("Doc");
        if (Key == null)
            throw new ArgumentException("Key");

        // Create a new SignedXml object and pass it
        // the XML document class.
        // <snippet5>
        SignedXml signedXml = new SignedXml(Doc);
        // </snippet5>

        // Find the "Signature" node and create a new
        // XmlNodeList object.
        // <snippet6>
        XmlNodeList nodeList = Doc.GetElementsByTagName("Signature");
        // </snippet6>

        // Throw an exception if no signature was found.
        if (nodeList.Count <= 0)
        {
            throw new CryptographicException("Verification failed: No Signature was found in the document.");
        }

        // This example only supports one signature for
        // the entire XML document.  Throw an exception 
        // if more than one signature was found.
        if (nodeList.Count >= 2)
        {
            throw new CryptographicException("Verification failed: More that one signature was found for the document.");
        }

        // Load the first <signature> node.  
        // <snippet7>
        signedXml.LoadXml((XmlElement)nodeList[0]);
        // </snippet7>

        // Check the signature and return the result.
        // <snippet8>
        return signedXml.CheckSignature(Key);
        // </snippet8>
    }
}
// </snippet1>
