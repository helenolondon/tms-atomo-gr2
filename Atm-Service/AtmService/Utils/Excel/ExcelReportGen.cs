using SpreadsheetLight;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace AtmService.Utils.Excel
{
    public class ExcelReportGen
    {
        public string StartColumn { get; set; } = "A";
        public int StartRow { get; set; } = 1;
        public bool AutoFitAllColumns { get; set; } = true;

        private SLDocument slDoc = null;
        int currentRow = 0;
        string currentCol = null;
        string lastCol = null;

        public Stream GenListExport<T>(List<T> source, string[] headers = null)
        {
            InitDocument();

            if(headers != null)
            {
                foreach (var header in headers)
                {
                    PrintCol(header);
                };

                NewLine();
            }

            foreach(var lItem in source)
            {   
                foreach(var prop in lItem.GetType().GetProperties())
                {
                    PrintCol(prop.GetValue(lItem));
                };

                NewLine();
            };

            ApplyFormattings();

            return this.SaveToStream();
        }

        private void InitDocument()
        {
            this.slDoc = new SLDocument();
            currentRow = this.StartRow;
            currentCol = this.StartColumn;
            lastCol = this.StartColumn;
        }
        public Stream SaveToStream()
        {
            var st = new MemoryStream();

            this.slDoc.SaveAs(st);
            st.Position = 0;

            return st;
        }

        private void ApplyFormattings()
        {
            if (this.AutoFitAllColumns)
            {
                slDoc.AutoFitColumn(this.StartColumn, lastCol);
            }
        }

        private string IncColumn(string column)
        {
            if(column.Length == 0)
            {
                return column;
            };

            var c = column[column.Length - 1];

            if(c == 'Z')
            {
                if (column.Length > 1)
                {
                    return IncColumn(column.Substring(0, column.Length - 1)) + 'A';
                }
                else
                {
                    return "AA";
                }
            }
            else
            {
                c = (char)(int)(c + 1);
            };

            if(column.Length > 1)
            {
                column = column.Substring(0, column.Length - 1) + c;
            }
            else
            {
                column = c.ToString();
            }
            
            return column;
        }

        private void NewLine()
        {
            this.currentRow++;
            this.currentCol = this.StartColumn;
        }

        private void NewColumn()
        {
            this.currentCol = this.IncColumn(this.currentCol);
            
            if (this.currentRow == this.StartRow)
            {
                lastCol = this.currentCol;
            }
        }

        private void PrintCol(Object value)
        {
            if(value != null)
            {
                if (value.GetType() == typeof(string))
                {
                    slDoc.SetCellValue(currentCol.ToString() + currentRow.ToString(), (string)value);
                }
                else if (value.GetType() == typeof(DateTime))
                {
                    slDoc.SetCellValue(currentCol.ToString() + currentRow.ToString(), DateTime.Parse(value.ToString()));
                    slDoc.SetCellStyle(currentCol.ToString() + currentRow.ToString(), new SLStyle() { FormatCode = "dd/MM/yyyy" });
                } 
                else if (value.GetType() == typeof(int))
                {
                    slDoc.SetCellValue(currentCol.ToString() + currentRow.ToString(), (int)value);
                }
                else if (value.GetType() == typeof(decimal))
                {
                    slDoc.SetCellValue(currentCol.ToString() + currentRow.ToString(), (decimal)value);
                } 
                else if (value.GetType() == typeof(double))
                {
                    slDoc.SetCellValue(currentCol.ToString() + currentRow.ToString(), (double)value);
                } 
                else if (value.GetType() == typeof(bool))
                {
                    slDoc.SetCellValue(currentCol.ToString() + currentRow.ToString(), (bool)value);
                };

                NewColumn();
            }
        }
    }
}
