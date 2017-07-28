using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFConversion.DAL;

namespace WPFConversion.BLL
{
    public class ViewModel : ObjectNotifyBase
    {
        private readonly string m_path = @"..\..\Units.txt";

        public Model Model { get; private set; }

        public ViewModel()
        {
            var data = new Data();
            this.Model = new BLL.Model(data.GetDataList(m_path, System.Threading.Thread.CurrentThread.CurrentCulture));

            //System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("pl-PL");
            System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("pl-PL");
        }

        //private float Calculate(float inputValue, string unitFrom, string unitTo)
        //{
        //    if (unitFrom == null || unitTo == null) return 0.0F;

        //    decimal rateFrom = (decimal)m_DataList.Where(data => data.Unit.Equals(unitFrom))
        //        .Select(data => data.Factor).FirstOrDefault();

        //    decimal rateTo = (decimal)m_DataList.Where(data => data.Unit.Equals(unitTo))
        //        .Select(data => data.Factor).FirstOrDefault();

        //    if ((rateFrom == 0.0M) && (rateTo == 0.0M)) return 0.0F;

        //    decimal calculation = ((decimal)inputValue) * ((1.0M / rateFrom) * rateTo);

        //    return (float)calculation;
        //}


    }
}
