# Perform Calculation by defining the name for cell or range of cells

## About the sample

This example will explain how to perform the calculation by defining the name for a cell or range of cells. Also, you can set and retrive the values in row and column index using ICalcData.

The below code explain how to create a class for set and retrive the values in row and column index using ICalcData,

```c#
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
```
The below code will explain how to set the data value of a specified cell range,

```c#
CalcData calcData = new CalcData();
CalcEngine engine = new CalcEngine(calcData);

 // To set the data value of a specified row and column.
calcData.SetValueRowCol(90, 1, 1);
calcData.SetValueRowCol(50, 1, 2);
calcData.SetValueRowCol(100, 1, 3);
```

The below code will explain how to define the name for range of cells and perform the calculation,

```c#
// Adding the name to the named range collection,
engine.AddNamedRange("GROUPCELLS", "A1:C1");

// Using the name for computing formulas,
string formula = "SUM(GROUPCELLS)";
string result = engine.ParseAndComputeFormula(formula);
```

The output of calculation with values based on cell ranges,

![Perform calculation by defining name for cell or range of cells](https://blog.syncfusion.com/wp-content/uploads/2018/11/Perform-calculation-defining-name-cell-range-cells.png)