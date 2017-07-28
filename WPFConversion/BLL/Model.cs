using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using WPFConversion.DAL;


namespace WPFConversion.BLL
{
    public class Model : ObjectNotifyBase
    {
        public Model(List<DataUnit> dataList)
        {
            this.m_DataList = dataList;
            m_operationTypes = new ObservableCollection<string>();


            dataList.Select(operation => operation.Type).Distinct()
                .ToList()
                .ForEach(data => m_operationTypes.Add(data));

            this.m_UnitsFrom = new ObservableCollection<string>();
            this.UnitsTo = new ObservableCollection<string>();
        }

        private List<DataUnit> m_DataList;

        private string m_Operation;
        public string Operation
        {
            get
            {
                return m_Operation;
            }

            set
            {
                m_Operation = value;

                if (m_operationTypes != null)
                {
                    UnitsFrom.Clear();
                    m_DataList.Where(data => data.Type == Operation)
                        .Select(data => data.Unit)
                        .Distinct()
                        .ToList()
                        .ForEach(data => m_UnitsFrom.Add(data));
                }

                SendNotification("UnitsFrom");
                SendNotification("Result");


            }
        }

        private ObservableCollection<string> m_operationTypes;
        public ObservableCollection<string> OperationTypes
        {
            get
            {
                return m_operationTypes;
            }
        }


        private ObservableCollection<string> m_UnitsFrom;
        public ObservableCollection<string> UnitsFrom
        {
            get
            {
                return m_UnitsFrom;
            }
        }



        public ICollection<string> UnitsTo { get; private set; }

        private string m_UnitFrom;
        public string UnitFrom
        {
            get { return m_UnitFrom; }
            set
            {
                m_UnitFrom = value;

                UnitsTo.Clear();

                m_DataList.Where(data => data.Type == Operation)
                    .Select(data => data.Unit)
                    .Distinct()
                    .Where(unit => !unit.Equals(m_UnitFrom))
                    .ToList()
                    .ForEach(unit => UnitsTo.Add(unit));

                SendNotification("Result");
            }
        }

        private string m_UnitTo;

        public string UnitTo
        {
            get { return m_UnitTo; }
            set
            {
                m_UnitTo = value;
                SendNotification("Result");
            }
        }


        private float m_Result;
        public float Result
        {
            get
            {
                m_Result = Calculate(InputValue, UnitFrom, UnitTo);
                return m_Result;
            }
        }


        public float InputValue { get; set; }


        private float Calculate(float inputValue, string unitFrom, string unitTo)
        {
            if (unitFrom == null || unitTo == null) return 0.0F;

            decimal rateFrom = (decimal)m_DataList.Where(data => data.Unit.Equals(unitFrom))
                .Select(data => data.Factor).FirstOrDefault();

            decimal rateTo = (decimal)m_DataList.Where(data => data.Unit.Equals(unitTo))
                .Select(data => data.Factor).FirstOrDefault();

            if ((rateFrom == 0.0M) && (rateTo == 0.0M)) return 0.0F;

            decimal calculation = ((decimal)inputValue) * ((1.0M / rateFrom) * rateTo);

            return (float)calculation;
        }

    }
}
