using Syncfusion.Calculate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NamedRangesExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Class derived from ICalcData.
            CalcData calcData = new CalcData();
            CalcEngine engine = new CalcEngine(calcData);

            // To set the data value of a specified row and column.
            calcData.SetValueRowCol(90, 1, 1);
            calcData.SetValueRowCol(50, 1, 2);
            calcData.SetValueRowCol(100, 1, 3);

            // Adding the name to the named range collection,
            engine.AddNamedRange("GROUPCELLS", "A1:C1");

            // Using the name for computing formulas,
            string formula = "SUM(GROUPCELLS)";
            string result = engine.ParseAndComputeFormula(formula);
            Console.WriteLine("The calculated result of name range is : " + result);
            Console.ReadKey();
        }
    }

    public class CalcData : ICalcData
    {
        public event ValueChangedEventHandler ValueChanged;

        Dictionary<string, object> values = new Dictionary<string, object>();
        public object GetValueRowCol(int row, int col)
        {
            object value = null;
            var key = RangeInfo.GetAlphaLabel(col) + row;
            this.values.TryGetValue(key, out value);
            return value;
        }

        public void SetValueRowCol(object value, int row, int col)
        {
            var key = RangeInfo.GetAlphaLabel(col) + row;
            if (!values.ContainsKey(key))
                values.Add(key, value);
            else if (values.ContainsKey(key) && values[key] != value)
                values[key] = value;
        }

        public void WireParentObject()
        {
        }

        private void OnValueChanged(int row, int col, string value)
        {
            if (ValueChanged != null)
                ValueChanged(this, new ValueChangedEventArgs(row, col, value));
        }
    }
}
