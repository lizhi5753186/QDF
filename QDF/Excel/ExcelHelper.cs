using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace QDF.Excel
{
    public static  class ExcelHelper
    {
        #region public methods
        /// <summary>
        /// Get the IWorkbook of the dest excel file.
        /// </summary>
        /// <param name="filePath">The dest file path.</param>
        /// <returns></returns>
        public static IWorkbook GetDestWorkBook(string filePath)
        {
            IWorkbook workBook = null;

            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                if (Path.GetExtension(filePath) == ".xls")
                    workBook = new HSSFWorkbook(fs);
                else
                    workBook = new XSSFWorkbook(fs);
            }

            return workBook;
        }

        /// <summary>
        /// Get the ISheet of the dest sheet.
        /// </summary>
        /// <param name="filePath">The dest file path.</param>
        /// <param name="sheetName">The dest sheet name.</param>
        /// <returns></returns>
        public static ISheet GetDestWorkSheet(string filePath, string sheetName)
        {
            var workBook = GetDestWorkBook(filePath);

            ISheet workSheet = null;
            workSheet = GetDestWorkSheet(workBook, sheetName);

            return workSheet;
        }
        /// <summary>
        /// Get the ISheet of the dest sheet.
        /// </summary>
        /// <param name="workBook">The IWorkbook dest file.</param>
        /// <param name="sheetName">The dest sheet name.</param>
        /// <returns></returns>
        public static ISheet GetDestWorkSheet(IWorkbook workBook, string sheetName)
        {
            ISheet workSheet = null;
            workSheet = workBook.GetSheet(sheetName);

            if (workSheet == null)
                throw new Exception(string.Format("There is no sheet named <{0}> in the excel.", sheetName));

            return workSheet;
        }
        /// <summary>
        /// Read dest sheet as a DataTable.
        /// The column is named with 'F1 F2 ...'
        /// </summary>
        /// <param name="filePath">The dest file path.</param>
        /// <param name="sheetName">The dest sheet name.</param>
        /// <param name="startRow">Start line number.</param>
        /// <returns></returns>
        public static DataTable ReadSheetAsDataTable(string filePath, string sheetName, int startRow = 0)
        {
            var sheet = GetDestWorkSheet(filePath, sheetName);
            return GetDataTableFromISheet(sheet, startRow);
        }
        /// <summary>
        /// Read dest excel as a DataTable.
        /// The tables are named with sheet name.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static DataSet ReadExcelAsDataSet(string filePath)
        {
            var fileName = Path.GetFileNameWithoutExtension(filePath);
            var ds = new DataSet(fileName);

            var workBook = GetDestWorkBook(filePath);
            for (var i = 0; i < workBook.NumberOfSheets; i++)
                ds.Tables.Add(GetDataTableFromISheet(workBook.GetSheetAt(i)));
            return ds;
        }
        /// <summary>
        /// Get the string value of the ICell.
        /// </summary>
        /// <param name="cell">Dest ICell.</param>
        /// <returns></returns>
        public static string GetDestCellStringValue(ICell cell)
        {
            if (cell == null)
            {
                return string.Empty;
            }

            string result;
            switch (cell.CellType)
            {
                case CellType.String:
                    result = cell.StringCellValue;
                    break;
                case CellType.Numeric:
                    result = DateUtil.IsCellDateFormatted(cell)
                        ? cell.DateCellValue.ToShortDateString() 
                        : cell.NumericCellValue.ToString();
                    break;
                default:
                    result = string.Empty;
                    break;
            }
            return result;
        }
        /// <summary>
        /// Set dest cell a string value.
        /// </summary>
        /// <typeparam name="TValue">string ,int or any Type overrided ToString().</typeparam>
        /// <param name="row"></param>
        /// <param name="cellPosition"></param>
        /// <param name="value"></param>
        public static void SetDestCellStringValue<TValue>(IRow row, int cellPosition, TValue value)
        {
            if (row == null)
                throw new Exception("The row is a NULL row.");

            if (row.GetCell(cellPosition) == null)
                row.CreateCell(cellPosition).SetCellValue(value.ToString());
            else
                row.GetCell(cellPosition).SetCellValue(value.ToString());
        }
        /// <summary>
        /// Set dest cell a string value and dest style.
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="row"></param>
        /// <param name="cellPosition"></param>
        /// <param name="style"></param>
        /// <param name="value"></param>
        public static void SetDestCellStringValueAndStyle<TValue>(IRow row, int cellPosition, ICellStyle style, TValue value)
        {
            SetDestCellStringValue<TValue>(row, cellPosition, value);
            var cell = row.GetCell(cellPosition);
            cell.CellStyle = style;
        }
        /// <summary>
        /// Get all sheet names as a list.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static List<string> GetAllSheetNames(string filePath)
        {
            return GetAllSheetNames(GetDestWorkBook(filePath));
        }
        /// <summary>
        /// Get all sheet names as a list.
        /// </summary>
        /// <param name="workBook"></param>
        /// <returns></returns>
        public static List<string> GetAllSheetNames(IWorkbook workBook)
        {
            var result = new List<string>();

            for (var i = 0; i < workBook.NumberOfSheets; i++)
                result.Add(workBook.GetSheetName(i));

            return result;
        }

        /// <summary>
        /// Save workBook to dest excel file.
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="workBook"></param>
        public static void SaveWorkBookToExcelFile(IWorkbook workBook, string filePath)
        {
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite))
                workBook.Write(fs);
        }

        /// <summary>
        /// Convert a DataSet to a excel file.
        /// </summary>
        /// <param name="dataSet"></param>
        /// <param name="filePath"></param>
        public static void DataSetToExcel(DataSet dataSet, string filePath)
        {
            var ext = Path.GetExtension(filePath);
            var workbook = DataSetToWorkBook(dataSet, ext);
            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                workbook.Write(fs);
            }
        }

        /// <summary>
        /// Read dest ISheet as a DataTable.
        /// The column is named with 'F1 F2 ...'
        /// </summary>
        /// <param name="workSheet"></param>
        /// <param name="startRow"></param>
        /// <returns></returns>
        public static DataTable GetDataTableFromISheet(ISheet workSheet, int startRow = 0)
        {
            if (workSheet == null)
                throw new Exception("Dest work sheet does not exists.");

            var result = new DataTable(workSheet.SheetName);

            var columnNumber = GuessSheetColumnNumber(workSheet);

            for (var i = 0; i < columnNumber; i++)
                result.Columns.Add(string.Format("F{0}", i + 1), typeof(string));

            for (var i = startRow; i <= workSheet.LastRowNum; i++)
            {
                var row = workSheet.GetRow(i);
                if (row == null)
                    continue;
                result.Rows.Add(ConvertIRowToDataRow(row, result));
            }

            return result;
        }
        #endregion

        #region private methods

        private static int GuessSheetColumnNumber(ISheet workSheet)
        {
            var headerRow = workSheet.GetRow(workSheet.FirstRowNum);
            var middleRow = workSheet.GetRow(workSheet.LastRowNum / 2);
            var lastRow = workSheet.GetRow(workSheet.LastRowNum);

            int result = Math.Max(headerRow.LastCellNum, middleRow.LastCellNum);
            result = Math.Max(result, lastRow.LastCellNum);
            return result;
        }

        private static DataRow ConvertIRowToDataRow(IRow row, DataTable dataTable)
        {
            var dr = dataTable.NewRow();

            for (var i = 0; i < dataTable.Columns.Count; i++)
                dr[i] = GetDestCellStringValue(row.GetCell(i));

            return dr;
        }

        private static void DataTableToWorkSheet(DataTable dataTable, ISheet sheet)
        {
            var headerRow = sheet.CreateRow(0);
            //Set the header
            foreach (DataColumn dc in dataTable.Columns)
                headerRow.CreateCell(dc.Ordinal).SetCellValue(dc.ColumnName);

            //Set the content
            for (var i = 0; i < dataTable.Rows.Count; i++)
            {
                var row = sheet.CreateRow(i + 1);
                for (var j = 0; j < dataTable.Columns.Count; j++)
                    row.CreateCell(j).SetCellValue(dataTable.Rows[i][j].ToString());
            }
        }

        private static IWorkbook DataSetToWorkBook(DataSet dataSet, string extension)
        {
            IWorkbook workBook = null;
            if (extension == ".xls")
                workBook = new HSSFWorkbook();
            else
                workBook = new XSSFWorkbook();

            try
            {
                foreach (DataTable dt in dataSet.Tables)
                {
                    ISheet sheet = null;
                    sheet = workBook.CreateSheet(dt.TableName);
                    DataTableToWorkSheet(dt, sheet);
                }
            }
            catch (Exception)
            {
                throw new Exception(
                    string.Format("Error while converting DataSet <{0}> to WorkBook.", dataSet.DataSetName));
            }

            return workBook;
        }
        #endregion
    }
}
