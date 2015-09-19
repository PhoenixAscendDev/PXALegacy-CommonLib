using System;
using System.Collections.Generic;
using System.Text;

namespace JB2.Common
{
    public class DataTable
    {
        public DataTable()
        {
            Columns = new List<string>();
            Rows = new List<DataRow>();
        }

        public List<string> Columns { get; set; }
        public List<DataRow> Rows { get; set; }

        public DataRow this[int row]
        {
            get
            {
                return Rows[row];
            }
        }

        public void AddRow(object[] values)
        {
            if (values.Length != Columns.Count)
            {
                throw new IndexOutOfRangeException("The number of values in the row must match the number of column");
            }

            var row = new DataRow();
            for (int i = 0; i < values.Length; i++)
            {
                row[Columns[i]] = values[i];
            }

            Rows.Add(row);
        }
    }
}
