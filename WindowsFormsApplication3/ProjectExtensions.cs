using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;

namespace VoltageDropCalculatorApplication
{
    public static class Extensions
    {
        public static void Switch<T>(this IList<T> array, int index1, int index2)
        {
            var aux = array[index1];
            array[index1] = array[index2];
            array[index2] = aux;
        }

        public static DataTable ToDataTable<T>(this IList<T> listToConvert)
        {
            DataTable table = new DataTable();
            PropertyInfo[] props = typeof(T).GetProperties();
            props.ToList().ForEach(prop => table.Columns.Add(prop.Name, prop.PropertyType));

            listToConvert.ToList().ForEach(item =>
            {
                var row = table.NewRow();
                foreach (var prop in props)
                {
                    row[prop.Name] = item.GetType().GetProperty(prop.Name).GetValue(item);
                }
                table.Rows.Add(row);
            });
            return table;
        }

    }

}
