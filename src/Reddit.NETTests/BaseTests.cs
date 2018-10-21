using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Reddit.NETTests
{
    public abstract class BaseTests
    {
        private TestContext TestContextInstance;

        public TestContext TestContext
        {
            get
            {
                return TestContextInstance;
            }
            set
            {
                TestContextInstance = value;
            }
        }

        public Dictionary<string, string> GetData()
        {
            // Begin .NET Core workaround.  --Kris
            string xmlData;
            using (System.IO.Stream stream = System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("Reddit.NETTests.Reddit.NETTestsData.xml"))
            {
                using (System.IO.StreamReader streamReader = new System.IO.StreamReader(stream))
                {
                    xmlData = streamReader.ReadToEnd();
                }
            }

            System.Xml.XmlDocument xmlDocument = new System.Xml.XmlDocument();
            xmlDocument.LoadXml(xmlData);

            return new Dictionary<string, string>
            {
                { "AppId", xmlDocument.GetElementsByTagName("AppId")[0].InnerText },
                { "RefreshToken", xmlDocument.GetElementsByTagName("RefreshToken")[0].InnerText },
                { "Subreddit", xmlDocument.GetElementsByTagName("Subreddit")[0].InnerText }
            };
            // End .NET Core workaround.  --Kris

            // TODO - Replace above workaround with commented code below for all test classes after .NET Core adds support for DataSourceAttribute.  --Kris
            // https://github.com/Microsoft/testfx/issues/233
            /*return new Dictionary<string, string>
            {
                { "AppId", (string) TestContext.DataRow["AppId"] },
                { "RefreshToken", (string) TestContext.DataRow["RefreshToken"] },
                { "Subreddit", (string) TestContext.DataRow["Subreddit"] }
            };*/
        }
    }
}
