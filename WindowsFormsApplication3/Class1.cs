using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;

namespace VoltageDropCalculatorApplication
{
    public class ProjDataset : System.Data.DataSet
    {

        List<int> mfNodeList = new List<int>();

        public ProjDataset(List<int> mfList)
        {
            this.mfNodeList = mfList;
        }

        private void writeXMlList(string projectName, List<int> mflist)
        {
            XDocument doc = XDocument.Load(projectName);
            XElement list = doc.Element("List");
            var xml = new XElement("List", mflist.Select(x => new XElement("itemValue", x)));            
            doc.Add(xml);
            doc.Save(projectName);
            

        }
      
    }
}
