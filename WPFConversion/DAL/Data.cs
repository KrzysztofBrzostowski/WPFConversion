using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.IO;


namespace WPFConversion.DAL
{
    public class Data
    {
        public Data()
        {
        }

        public List<DataUnit> GetDataList(string path, CultureInfo cultureInfo)
        {
            var retList = new List<DataUnit>();

            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] tab_str = line.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        DataUnit du = new DataUnit() { Type = tab_str[0], Unit = tab_str[1], Factor = float.Parse(tab_str[2], cultureInfo) };
                        retList.Add(du);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Can not access the file or data error. Details:");
                Console.WriteLine(e.Message);
                throw new ArgumentException();
            }

            return retList;
        }

    }
}
