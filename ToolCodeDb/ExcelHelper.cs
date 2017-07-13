using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System.Reflection;
using NPOI.HSSF.Util;


namespace HolyStone.CaseManager.Domain
{
    public class ExcelHelper
    {
        private IWorkbook workbook;
        private ISheet worksheet;
        
 

        public string FileName { get; private set; }

        public ExcelHelper() { }
        public ExcelHelper(string FilePath)
        {
            this.FileName = FilePath;
            if (File.Exists(FilePath))
            {
                using (FileStream fs = new FileStream(FilePath, FileMode.Open))
                {
                    this.workbook = WorkbookFactory.Create(fs);
                    this.worksheet = this.workbook.GetSheetAt(0);
                }
            }
            else
            {
                CreateExcel(FilePath);
            }

        }

        /// <summary>
        /// 打开Excel文件
        /// </summary>
        /// <param name="FilePath">文件路径</param>
        public void OpenExcel(string FilePath)
        {
            this.FileName = FilePath;
            using (FileStream fs = new FileStream(FilePath, FileMode.Open))
            {
                this.workbook = WorkbookFactory.Create(fs);
                this.worksheet = this.workbook.GetSheetAt(0);
            }
        }

        /// <summary>
        /// 新建一个excel
        /// </summary>
        public void CreateExcel(string filename)
        {
            FileInfo file = new FileInfo(filename);
            string version = file.Extension;

            if (version == ".xls")
                workbook = new HSSFWorkbook();
            else if (version == ".xlsx")
                workbook = new XSSFWorkbook();
            else
                throw new FormatException();
            workbook.CreateSheet("Sheet1");
            workbook.CreateSheet("Sheet2");
            workbook.CreateSheet("Sheet3");
            this.worksheet = this.workbook.GetSheetAt(0);
        }


        /// <summary>
        /// 打开一个Sheet
        /// </summary>
        /// <param name="sheetname">Sheet的名称</param>
        public void OpenSheet(string sheetname)
        {
            this.worksheet = this.workbook.GetSheet(sheetname);
        }

        /// <summary>
        /// 打开一个Sheet
        /// </summary>
        /// <param name="sheetindex">Sheet的索引位置</param>
        public void OpenSheet(int sheetindex)
        {
            this.worksheet = this.workbook.GetSheetAt(sheetindex);
        }


        /// <summary>
        /// 创建一个Sheet,
        /// </summary>
        public void CreateSheet()
        {
            this.workbook.CreateSheet();
        }

        /// <summary>
        /// 创建一个Sheet,
        /// </summary>
        /// <param name="sheetname">Sheet的名称</param>
        public void CreateSheet(string sheetname)
        {
            if (GetSheetsNames().Contains(sheetname))
            {
                workbook.RemoveSheetAt(GetSheetIndex(sheetname));
            }
            this.workbook.CreateSheet(sheetname);
        }

        /// <summary>
        /// 删除一个sheet
        /// </summary>
        /// <param name="sheetindex"></param>
        public void DeleteSheet(int sheetindex)
        {
            if (0 <= sheetindex && sheetindex <= workbook.NumberOfSheets - 1)
            {
                this.workbook.RemoveSheetAt(sheetindex);
            }
            else
            {
                throw new Exception("索引值越界！");
            }
        }

        /// <summary>
        /// 删除一个sheet
        /// </summary>
        /// <param name="sheetname"></param>
        public void DeleteSheet(string sheetname)
        {
            DeleteSheet(GetSheetIndex(sheetname));
        }

        /// <summary>
        /// 获取sheet名
        /// </summary>
        /// <param name="sheetindex">sheet索引值</param>
        /// <returns>sheet索引值</returns>
        public string GetSheetName(int sheetindex)
        {
            if (0 <= sheetindex && sheetindex <= workbook.NumberOfSheets - 1)
            {
                return workbook.GetSheetName(sheetindex);
            }
            else
            {
                throw new Exception("索引值越界！");
            }
        }
        /// <summary>
        /// 获取所有的sheet名
        /// </summary>
        /// <returns></returns>
        public List<string> GetSheetsNames()
        {
            List<string> list = new List<string>();
            foreach (ISheet s in workbook)
            {
                list.Add(s.SheetName);
            }
            return list;
        }

        /// <summary>
        /// 重命名sheet
        /// </summary>
        /// <param name="newname"></param>
        public void RenameWorkSheet(string newname)
        {
            if (GetSheetsNames().Contains(newname))
            {
                throw new Exception("名称为：" + newname + "的sheet已经存在！");
            }
            else
            {
                this.workbook.SetSheetName(this.workbook.GetSheetIndex(this.worksheet), newname);
            }
        }
        /// <summary>
        /// 重命名sheet
        /// </summary>
        /// <param name="index"></param>
        /// <param name="newname"></param>
        public void RenameWorkSheet(int sheetindex, string newname)
        {
            if (GetSheetsNames().Contains(newname))
            {
                throw new Exception("名称为：" + newname + "的sheet已经存在！");
            }
            else
            {
                if (0 <= sheetindex && sheetindex <= workbook.NumberOfSheets - 1)
                {
                    workbook.SetSheetName(sheetindex, newname);
                }
                else
                {
                    throw new Exception("索引值越界！");
                }
            }
        }
        /// <summary>
        /// 重命名sheet
        /// </summary>
        /// <param name="oldname"></param>
        /// <param name="newname"></param>
        public void RenameWorkSheet(string oldname, string newname)
        {
            RenameWorkSheet(workbook.GetSheetIndex(oldname), newname);
        }

        /// <summary>
        /// 获取sheet总数
        /// </summary>
        /// <returns></returns>
        public int GetSheetCounts()
        {
            return this.workbook.NumberOfSheets;
        }

        /// <summary>
        /// 获取sheet索引值
        /// </summary>
        /// <param name="sheetname">sheet名</param>
        /// <returns>sheet索引值</returns>
        public int GetSheetIndex(string sheetname)
        {
            return workbook.GetSheetIndex(sheetname);
        }

        /// <summary>
        /// 获取sheet的总行数
        /// </summary>
        /// <returns>总行数</returns>
        public int GetRowCount()
        {
            return worksheet.LastRowNum + 1;
        }

        /// <summary>
        /// 获取sheet的总行数
        /// </summary>
        /// <param name="sheetname">sheet名</param>
        /// <returns>总行数</returns>
        public int GetRowCount(string sheetname)
        {
            return GetRowCount(workbook.GetSheetIndex(sheetname));
        }

        /// <summary>
        /// 获取sheet的总行数
        /// </summary>
        /// <param name="sheetindex">总行数</param>
        /// <returns>总行数</returns>
        public int GetRowCount(int sheetindex)
        {
            if (0 <= sheetindex && sheetindex <= workbook.NumberOfSheets - 1)
            {
                return workbook.GetSheetAt(sheetindex).LastRowNum + 1;
            }
            else
            {
                throw new Exception("索引值越界！");
            }

        }


        /// <summary>
        /// 获取单元格
        /// </summary>
        /// <returns></returns>
        public ICell Getcell(int rowindex, int colindex)
        {
            ICell cell = null;
            IRow row = worksheet.GetRow(rowindex);
            cell = row.GetCell(colindex);
            return cell;
        
        }
       


        /// <summary>
        /// 设置单元格的颜色
        /// </summary>
        public ICellStyle setCellColor()
        {
            ICellStyle cellStyle = workbook.CreateCellStyle();
            IFont font1 = workbook.CreateFont();
            font1.Color = HSSFColor.OLIVE_GREEN.RED.index;
            cellStyle.SetFont(font1);
            return cellStyle;

        }

        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="sheet">要合并单元格所在的sheet</param>
        /// <param name="rowstart">开始行的索引</param>
        /// <param name="rowend">结束行的索引</param>
        /// <param name="colstart">开始列的索引</param>
        /// <param name="colend">结束列的索引</param>
        public  void SetCellRangeAddress(ISheet sheet, int rowstart, int rowend, int colstart, int colend)
        {
            CellRangeAddress cellRangeAddress = new CellRangeAddress(rowstart, rowend, colstart, colend);
            sheet.AddMergedRegion(cellRangeAddress);
        }

        /// <summary>
        /// 合并单元格
        /// </summary>
        /// <param name="sheet">要合并单元格所在的sheet</param>
        /// <param name="rowstart">开始行的索引</param>
        /// <param name="rowend">结束行的索引</param>
        /// <param name="colstart">开始列的索引</param>
        /// <param name="colend">结束列的索引</param>
        public void SetCellRangeAddress( int rowstart, int rowend, int colstart, int colend)
        {
            CellRangeAddress cellRangeAddress = new CellRangeAddress(rowstart, rowend, colstart, colend);
            worksheet.AddMergedRegion(cellRangeAddress);
        }


        #region 取值
        private object GetValue(ICell cell)
        {
            if (cell.CellType == CellType.BLANK)
            {
                return string.Empty;
            }
            else if (cell.CellType == CellType.BOOLEAN)
            {
                return cell.BooleanCellValue;
            }
            else if (cell.CellType == CellType.NUMERIC)
            {
                return cell.NumericCellValue;
            }
            else
            {
                return cell.StringCellValue;
            }
        }

        /// <summary>
        /// 获取单个单元格的值,index从0开始
        /// </summary>
        /// <param name="rowindex">单元格所在行索引</param>
        /// <param name="columnindex">单元格所在列索引</param>
        /// <returns></returns>
        public object GetValue(int rowindex, int columnindex)
        {
            IRow row = worksheet.GetRow(rowindex);
            if (row != null)
            {
                return GetValue(row.GetCell(columnindex, MissingCellPolicy.CREATE_NULL_AS_BLANK));
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取一行单元格的值,index从0开始
        /// </summary>
        /// <param name="rowindex">行索引</param>
        /// <returns></returns>
        public List<object> GetValues(int rowindex)
        {
            List<object> result = null;
            IRow row = worksheet.GetRow(rowindex);
            if (row != null)
            {
                result = new List<object>();
                int col = row.LastCellNum;
                for (int i = 0; i < col; i++)
                {
                    result.Add(GetValue(row.GetCell(i, MissingCellPolicy.CREATE_NULL_AS_BLANK)));
                }
            }
            return result;
        }

        /// <summary>
        /// 获取一片区域单元格的值,index从0开始
        /// </summary>
        /// <param name="startrow">开始行</param>
        /// <param name="startcol">开始列</param>
        /// <param name="endrow">结束行</param>
        /// <param name="endcol">结束列</param>
        /// <returns></returns>
        public object[,] GetValues(int startrow, int startcol, int endrow, int endcol)
        {
            object[,] result = new object[endrow - startrow + 1, endcol - startcol + 1];
            IRow row = null;
            ICell cell = null;
            for (int i = startrow; i <= endrow; i++)
            {
                row = worksheet.GetRow(i);
                if (row != null)
                {
                    for (int j = startcol; j <= endcol; j++)
                    {
                        cell = row.GetCell(j, MissingCellPolicy.CREATE_NULL_AS_BLANK);
                        result[i - startrow, j - startcol] = GetValue(cell);
                    }
                }
            }
            return result;
        }

        #endregion

        #region 填值

        private void SetValue(ICell cell, object value)
        {
            if (value == null)
            {
                value = string.Empty;
            }
            if (value is bool)
            {
                cell.SetCellValue((bool)value);
                cell.SetCellType(CellType.BOOLEAN);
            }
            else if (value.GetType().IsValueType && !value.GetType().IsEnum)
            {
                if (value is char)
                {
                    cell.SetCellValue(value.ToString());
                    cell.SetCellType(CellType.STRING);
                }
                else
                {
                    if (value is byte)
                    {
                        cell.SetCellValue((double)(byte)value);
                    }
                    else if (value is short)
                    {
                        cell.SetCellValue((double)(short)value);
                    }
                    else if (value is int)
                    {
                        cell.SetCellValue((double)(int)value);
                    }
                    else if (value is long)
                    {
                        cell.SetCellValue((double)(long)value);
                    }
                    else if(value is float)
                    {
                        cell.SetCellValue((double)(float)value);
                    }
                    else
                    {
                        cell.SetCellValue((double)value);
                    }

                    
                    cell.SetCellType(CellType.NUMERIC);
                }

            }
            else
            {
                cell.SetCellValue((string)value);
                cell.SetCellType(CellType.STRING);
            }
        }

        /// <summary>
        /// 设置一个单元格的值
        /// </summary>
        /// <param name="rowindex">单元格行索引</param>
        /// <param name="columnindex">单元格列索引</param>
        /// <param name="value">需要填入的值</param>
        public void SetValue(int rowindex, int columnindex, object value)
        {
            IRow row = worksheet.GetRow(rowindex);
            if (row == null)
            {
                worksheet.CreateRow(rowindex);
                row = worksheet.GetRow(rowindex);
            }

          //  row.GetCell(columnindex, MissingCellPolicy.CREATE_NULL_AS_BLANK)
            
            SetValue(row.GetCell(columnindex, MissingCellPolicy.CREATE_NULL_AS_BLANK), value);
        }

        /// <summary>
        /// 创建人：zenglq
        /// 设置一列的值
        /// </summary>
        /// <param name="rowindex">开始行</param>
        /// <param name="columnindex">开始列</param>
        /// <param name="value">集合值</param>
        public void SetColumsValues(int rowindex, int columnindex, List<string> value)
        {
            IRow row = worksheet.GetRow(rowindex);
            if (row == null)
            {
                worksheet.CreateRow(rowindex);
                row = worksheet.GetRow(rowindex);
            }
            foreach (string s in value)
            {
                SetValue(rowindex++, columnindex, s);
            }
        }


 
        /// <summary>
        /// 创建人：zenglq
        /// 设置一列的值,并给没列值加一个“Tag”
        /// </summary>
        /// <param name="rowindex">开始行</param>
        /// <param name="columnindex">开始列</param>
        /// <param name="value">集合值</param>
        public void SetColumsValuesAddTag(int rowindex, int columnindex, List<string> value)
        {
            IRow row = worksheet.GetRow(rowindex);
            if (row == null)
            {
                worksheet.CreateRow(rowindex);
                row = worksheet.GetRow(rowindex);
            }
            foreach (string s in value)
            {
                SetValue(rowindex++, columnindex, "Tag_"+s);
            }
        }

        /// <summary>
        /// 设置一行单元格的值，从指定列开始填入
        /// </summary>
        /// <param name="rowindex">行索引</param>
        /// <param name="startcol">开始列</param>
        /// <param name="value"></param>
        public void SetValues(int rowindex, int startcol, List<object> value)
        {
            IRow row = worksheet.GetRow(rowindex);
            if (row == null)
            {
                worksheet.CreateRow(rowindex);
                row = worksheet.GetRow(rowindex);
            }

            foreach (object s in value)
            {
                SetValue(rowindex, startcol++, s);
            }
        }

        /// <summary>
        /// 设置一行单元格的值，从第一列开始填入
        /// </summary>
        /// <param name="rowindex">行索引</param>
        /// <param name="value"></param>
        public void SetValues(int rowindex, List<object> value)
        {
            SetValues(rowindex, 0, value);
        }

        /// <summary>
        /// 设置一片区域的单元格的值
        /// </summary>
        /// <param name="startrow">开始行</param>
        /// <param name="startcol">开始列</param>
        /// <param name="value"></param>
        public void SetValues(int startrow, int startcol, object[,] value)
        {
            int rowcount = value.GetLength(0);
            int colcount = value.GetLength(1);
            for (int i = 0; i < rowcount; i++)
            {
                for (int j = 0; j < colcount; j++)
                {
                    SetValue(startrow + i, startcol + j, value[i, j]);
                }
            }
        }

        #endregion
        /// <summary>
        /// 复制
        /// </summary>
        /// <param name="startrow">复制区域开始行</param>
        /// <param name="startcol">复制区域开始列</param>
        /// <param name="endrow">复制区域结束行</param>
        /// <param name="endcol">复制区域结束行</param>
        /// <param name="pstartrow">粘贴区域开始行</param>
        /// <param name="pstartcol">粘贴区域开始列</param>
        public void Copy(int startrow, int startcol, int endrow, int endcol, int pstartrow, int pstartcol)
        {
            SetValues(pstartrow, pstartcol, GetValues(startrow, startcol, endcol, pstartrow));
        }

        /// <summary>
        /// 复制sheet
        /// </summary>
        /// <param name="ssheetname"></param>
        /// <param name="dsheetname"></param>
        public void CopySheetAs(string ssheetname, string dsheetname)
        {
            if (GetSheetsNames().Contains(dsheetname))
            {
                workbook.RemoveSheetAt(GetSheetIndex(dsheetname));
            }
            workbook.SetSheetName(workbook.GetSheetIndex(workbook.CloneSheet(workbook.GetSheetIndex(ssheetname))), dsheetname);
        }

        /// <summary>
        /// 保存
        /// </summary>
        public void Save()
        {
            using (FileStream file = new FileStream(FileName, FileMode.Create))
            {
                this.workbook.Write(file);
            }
        }

        /// <summary>
        /// 另存为
        /// </summary>
        /// <param name="filename"></param>
        public void SaveAs(string filename)
        {
            if (workbook is HSSFWorkbook)
            {
                filename = Path.ChangeExtension(filename, "xls");
            }
            else
            {
                filename = Path.ChangeExtension(filename, "xlsx");
            }
            using (FileStream file = new FileStream(filename, FileMode.Create))
            {
                this.workbook.Write(file);
            }
        }


        #region 导入
        /// <summary>
        /// Excel导入DataSet
        /// </summary>
        /// <param name="excelPath"></param>
        /// <returns></returns>
        public static DataSet ExcelToDataSet(string excelPath)
        {
            return ExcelToDataSet(excelPath, true);
        }
        /// <summary>
        /// Excel导入DataSet
        /// </summary>
        /// <param name="excelPath"></param>
        /// <param name="firstRowAsHeader"></param>
        /// <returns></returns>
        public static DataSet ExcelToDataSet(string excelPath, bool firstRowAsHeader)
        {
            int sheetCount;
            return ExcelToDataSet(excelPath, firstRowAsHeader, out sheetCount);
        }
        /// <summary>
        /// Excel导入DataSet
        /// </summary>
        /// <param name="excelPath"></param>
        /// <param name="firstRowAsHeader"></param>
        /// <param name="sheetCount"></param>
        /// <returns></returns>
        public static DataSet ExcelToDataSet(string excelPath, bool firstRowAsHeader, out int sheetCount)
        {
            using (DataSet ds = new DataSet())
            {
                using (FileStream fileStream = new FileStream(excelPath, FileMode.Open, FileAccess.Read))
                {
                    HSSFWorkbook workbook = new HSSFWorkbook(fileStream);

                    HSSFFormulaEvaluator evaluator = new HSSFFormulaEvaluator(workbook);

                    sheetCount = workbook.NumberOfSheets;

                    for (int i = 0; i < sheetCount; ++i)
                    {
                        ISheet sheet = workbook.GetSheetAt(i);// as ISheet;
                        DataTable dt = ExcelToDataTable(sheet, evaluator, firstRowAsHeader);
                        ds.Tables.Add(dt);
                    }

                    return ds;
                }
            }
        }


        /// <summary>读取excel   
        /// 默认第一行为标头   
        /// </summary>   
        /// <param name="strFileName">excel文档路径</param>   
        /// <returns></returns>   
        public static DataTable Import(string strFileName)
        {
            DataTable dt = new DataTable();

            HSSFWorkbook hssfworkbook;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = new HSSFWorkbook(file);
            }
            ISheet sheet = hssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            IRow headerRow = sheet.GetRow(0);
            int cellCount = headerRow.LastCellNum;

            for (int j = 0; j < cellCount; j++)
            {
                ICell cell = headerRow.GetCell(j);
                dt.Columns.Add(cell.ToString());
            }

            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                DataRow dataRow = dt.NewRow();

                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                        dataRow[j] = row.GetCell(j).ToString();
                }

                dt.Rows.Add(dataRow);
            }
            return dt;
        }

        /// <summary>
        /// Excel导入DataTable
        /// </summary>
        /// <param name="excelPath">Excel路径</param>
        /// <param name="sheetName">工作表名</param>
        /// <returns></returns>
        public static DataTable ExcelToDataTable(string excelPath, string sheetName)
        {
            return ExcelToDataTable(excelPath, sheetName, true);
        }

        /// <summary>
        /// Excel导入DataTable,默认导入第一个工作表
        /// </summary>
        /// <param name="excelPath">Excel路径</param>
        /// <returns></returns>
        public static DataTable ExcelToDataTable(string excelPath)
        {
            return ExcelToDataTable(excelPath, null, true);
        }

        /// <summary>
        /// Excel导入DataTable
        /// </summary>
        /// <param name="excelPath">Excel路径</param>
        /// <param name="sheetName">工作</param>
        /// <param name="firstRowAsHeader">第一行是否为表头</param>
        /// <returns></returns>
        public static DataTable ExcelToDataTable(string excelPath, string sheetName, bool firstRowAsHeader)
        {
            using (FileStream fileStream = new FileStream(excelPath, FileMode.Open, FileAccess.Read))
            {
                HSSFWorkbook workbook = new HSSFWorkbook(fileStream);

                HSSFFormulaEvaluator evaluator = new HSSFFormulaEvaluator(workbook);
                ISheet sheet;
                if (sheetName != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet == null)
                    throw new NullReferenceException("没有找到<" + sheetName + ">工作表");
                return ExcelToDataTable(sheet, evaluator, firstRowAsHeader);
            }
        }

        private static DataTable ExcelToDataTable(ISheet sheet, HSSFFormulaEvaluator evaluator, bool firstRowAsHeader)
        {
            if (firstRowAsHeader)
            {
                return ExcelToDataTableFirstRowAsHeader(sheet, evaluator);
            }
            else
            {
                return ExcelToDataTable(sheet, evaluator);
            }
        }

        //第一行作为标题
        private static DataTable ExcelToDataTableFirstRowAsHeader(ISheet sheet, HSSFFormulaEvaluator evaluator)
        {
            using (DataTable dt = new DataTable())
            {
                // IRow firstRow = sheet.GetRow(0) ;//第一行作为标题
                IRow firstRow;
                try
                {
                    firstRow = sheet.GetRow(3);//第三行做表头
                }
                catch (Exception)
                {

                    throw new NullReferenceException("表头设置错误");
                }

                int cellCount = GetCellCount(sheet);


                for (int i = 0; i < cellCount; i++)//从第一列开始取数据
                {
                    if (firstRow.GetCell(i) != null)
                    {
                        dt.Columns.Add(firstRow.GetCell(i).StringCellValue.Trim() ?? string.Format("F{0}", i + 1), typeof(string));
                    }
                    else
                    {
                        dt.Columns.Add(string.Format("F{0}", i + 1), typeof(string));
                    }
                }

                // for (int i = 1; i <= sheet.LastRowNum; i++)//从第二行开始取数据
                for (int i = 4; i <= sheet.LastRowNum; i++)//从第四行开始
                {
                    IRow row = sheet.GetRow(i);
                    DataRow dr = dt.NewRow();
                    FillDataRowByHSSFRow(row, evaluator, ref dr);
                    dt.Rows.Add(dr);
                }

                dt.TableName = sheet.SheetName;
                return dt;
            }
        }

        //导入DataTable
        private static DataTable ExcelToDataTable(ISheet sheet, HSSFFormulaEvaluator evaluator)
        {
            using (DataTable dt = new DataTable())
            {
                if (sheet.LastRowNum != 0)
                {
                    int cellCount = GetCellCount(sheet);

                    for (int i = 0; i < cellCount; i++)
                    {
                        dt.Columns.Add(string.Format("F{0}", i), typeof(string));
                    }

                    for (int i = 0; i < sheet.FirstRowNum; ++i)
                    {
                        DataRow dr = dt.NewRow();
                        dt.Rows.Add(dr);
                    }

                    for (int i = sheet.FirstRowNum; i <= sheet.LastRowNum; i++)
                    {
                        IRow row = sheet.GetRow(i);
                        DataRow dr = dt.NewRow();
                        FillDataRowByHSSFRow(row, evaluator, ref dr);
                        dt.Rows.Add(dr);
                    }
                }

                dt.TableName = sheet.SheetName;
                return dt;
            }
        }
        /// <summary>
        /// 通过IRow填充DataRow
        /// </summary>
        /// <param name="row"></param>
        /// <param name="evaluator"></param>
        /// <param name="dr"></param>
        private static void FillDataRowByHSSFRow(IRow row, HSSFFormulaEvaluator evaluator, ref DataRow dr)
        {
            if (row != null)
            {
                for (int j = 0; j < dr.Table.Columns.Count; j++)
                {
                    HSSFCell cell = row.GetCell(j) as HSSFCell;

                    if (cell != null)
                    {
                        switch (cell.CellType)
                        {
                            case CellType.BLANK:
                                dr[j] = DBNull.Value;
                                break;
                            case CellType.BOOLEAN:
                                dr[j] = cell.BooleanCellValue;
                                break;
                            case CellType.NUMERIC:
                                if (DateUtil.IsCellDateFormatted(cell))
                                {
                                    dr[j] = cell.DateCellValue;
                                }
                                else
                                {
                                    dr[j] = cell.NumericCellValue;
                                }
                                break;
                            case CellType.STRING:
                                dr[j] = cell.StringCellValue;
                                break;
                            case CellType.ERROR:
                                dr[j] = cell.ErrorCellValue;
                                break;
                            case CellType.FORMULA:
                                cell = evaluator.EvaluateInCell(cell) as HSSFCell;
                                dr[j] = cell.ToString();
                                break;
                            default:
                                throw new NotSupportedException(string.Format("Catched unhandle CellType[{0}]", cell.CellType));
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 得到cell总数
        /// </summary>
        /// <param name="sheet"></param>
        /// <returns></returns>
        private static int GetCellCount(ISheet sheet)
        {
            int firstRowNum = sheet.FirstRowNum;

            int cellCount = 0;

            for (int i = sheet.FirstRowNum; i <= sheet.LastRowNum; ++i)
            {
                IRow row = sheet.GetRow(i);

                if (row != null && row.LastCellNum > cellCount)
                {
                    cellCount = row.LastCellNum;
                }
            }

            return cellCount;
        }
        #endregion

        #region 导出
        /// <summary>   
        /// DataTable导出到Excel文件   
        /// </summary>   
        /// <param name="dtSource">源DataTable</param>   
        /// <param name="strHeaderText">表头文本</param>   
        /// <param name="strFileName">保存位置</param>    
        public static void Export(DataTable dtSource, string strHeaderText, string strFileName)
        {
            using (MemoryStream ms = Export(dtSource, strHeaderText))
            {
                using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
        }

        /// <summary>   
        /// DataTable导出到Excel的MemoryStream   
        /// </summary>   
        /// <param name="dtSource">源DataTable</param>   
        /// <param name="strHeaderText">表头文本</param>   
        public static MemoryStream Export(DataTable dtSource, string strHeaderText)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet();
            sheet.ForceFormulaRecalculation = true;//强制要求Excel在打开时重新计算的属性
            /*
             * CreateFreezePane()
             * 第一个参数表示要冻结的列数；
             * 第二个参数表示要冻结的行数；
             * 第三个参数表示右边区域可见的首列序号，从1开始计算；
             * 第四个参数表示下边区域可见的首行序号，也是从1开始计算
             */
            sheet.CreateFreezePane(0, 2, 0, 3);//冻结表头与列头

            #region 右击文件 属性信息
            {
                DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = "公司";
                workbook.DocumentSummaryInformation = dsi;

                SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                si.Author = "作者"; //填加xls文件作者信息   
                si.ApplicationName = "程序信息"; //填加xls文件创建程序信息   
                si.LastAuthor = "最后保存者"; //填加xls文件最后保存者信息   
                si.Comments = "说明信息"; //填加xls文件作者信息   
                si.Title = "标题信息"; //填加xls文件标题信息   
                si.Subject = "文件主题";//填加文件主题信息   
                si.CreateDateTime = DateTime.Now;
                workbook.SummaryInformation = si;
            }
            #endregion

            ICellStyle dateStyle = workbook.CreateCellStyle();
            IDataFormat format = workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");

            //取得列宽   
            int[] arrColWidth = new int[dtSource.Columns.Count];
            foreach (DataColumn item in dtSource.Columns)
            {
                arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
            }
            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                for (int j = 0; j < dtSource.Columns.Count; j++)
                {
                    int intTemp = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][j].ToString()).Length;
                    if (intTemp > arrColWidth[j])
                    {
                        arrColWidth[j] = intTemp;
                    }
                }
            }



            int rowIndex = 0;

            foreach (DataRow row in dtSource.Rows)
            {
                #region 新建表，填充表头，填充列头，样式
                if (rowIndex == 65535 || rowIndex == 0)
                {
                    if (rowIndex != 0)
                    {
                        sheet = workbook.CreateSheet();
                    }

                    #region 表头及样式
                    {
                        IRow headerRow = sheet.CreateRow(0);
                        headerRow.HeightInPoints = 25;
                        headerRow.CreateCell(0).SetCellValue(strHeaderText);

                        ICellStyle headStyle = workbook.CreateCellStyle();
                        headStyle.Alignment = HorizontalAlignment.CENTER;

                        IFont font = workbook.CreateFont();
                        font.FontHeightInPoints = 20;
                        font.Boldweight = 700;
                        headStyle.SetFont(font);

                        headerRow.GetCell(0).CellStyle = headStyle;
                        CellRangeAddress vra = new CellRangeAddress(0, 0, 0, dtSource.Columns.Count - 1);
                        sheet.AddMergedRegion(vra);
                    }
                    #endregion


                    #region 列头及样式
                    {
                        IRow headerRow = sheet.CreateRow(1);


                        ICellStyle headStyle = workbook.CreateCellStyle();
                        headStyle.Alignment = HorizontalAlignment.CENTER;// CellHorizontalAlignment.CENTER;
                        IFont font = workbook.CreateFont();
                        font.FontHeightInPoints = 10;
                        font.Boldweight = 700;
                        headStyle.SetFont(font);


                        foreach (DataColumn column in dtSource.Columns)
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                            headerRow.GetCell(column.Ordinal).CellStyle = headStyle;

                            //设置列宽   
                            sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 1) * 256);

                        }
                    }
                    #endregion

                    rowIndex = 2;
                }
                #endregion


                #region 填充内容
                IRow dataRow = sheet.CreateRow(rowIndex);
                foreach (DataColumn column in dtSource.Columns)
                {
                    ICell newCell = dataRow.CreateCell(column.Ordinal);

                    string drValue = row[column].ToString();

                    switch (column.DataType.ToString())
                    {
                        case "System.String"://字符串类型   
                            newCell.SetCellValue(drValue);
                            break;
                        case "System.DateTime"://日期类型  
                            if (drValue == "")
                            {
                                newCell.SetCellValue(drValue);
                            }
                            else
                            {
                                DateTime dateV;
                                DateTime.TryParse(drValue, out dateV);
                                newCell.SetCellValue(dateV);
                                newCell.CellStyle = dateStyle;//格式化显示   
                            }
                            break;
                        case "System.Boolean"://布尔型   
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16"://整型   
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            break;
                        case "System.Decimal"://浮点型   
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            break;
                        case "System.DBNull"://空值处理   
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }

                }
                #endregion

                rowIndex++;
            }


            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                return ms;
            }

        }


        /// <summary>   
        /// 用于Web导出   
        /// </summary>   
        /// <param name="dtSource">源DataTable</param>   
        /// <param name="strHeaderText">表头文本</param>   
        /// <param name="strFileName">文件名</param>   
        //public static void ExportByWeb(DataTable dtSource, string strHeaderText, string strFileName)
        //{
        //    HttpContext curContext = HttpContext.Current;

        //    // 设置编码和附件格式   
        //    curContext.Response.ContentType = "application/vnd.ms-excel";
        //    curContext.Response.ContentEncoding = System.Text.Encoding.UTF8;
        //    curContext.Response.Charset = "";
        //    curContext.Response.AppendHeader("Content-Disposition",
        //        "attachment;filename=" + HttpUtility.UrlEncode(strFileName, System.Text.Encoding.UTF8));

        //    curContext.Response.BinaryWrite(Export(dtSource, strHeaderText).GetBuffer());
        //    curContext.Response.End();
        //}
        #endregion

        /// <summary>
        /// 创建人：zenglq
        /// 给一块区域定义名称
        /// </summary>
        /// <param name="reginName">名称</param>
        public void DesignationReginName(string sheetName,string reginName,int startRowIndex,int lastRowIndex,int startColumIndex,int lastColumIndex)
        {
            string rangeArea = "";///Sheet1!$C$2:$K$50
            string startCell = "";//Range开始单元格坐标
            string endCell = "";
             
            startCell = "$" + NumToLetter(startColumIndex+1) + "$" + (startRowIndex+1).ToString();
            endCell = "$" + NumToLetter(lastColumIndex+1) + "$" + (lastRowIndex+1).ToString();
            rangeArea = sheetName + "!" + startCell + ":" + endCell;
            HSSFName Range = (HSSFName)workbook.GetName(reginName);
            if (Range == null)
            {
                Range = (HSSFName)workbook.CreateName();
            }
            Range.NameName = reginName;
            Range.RefersToFormula = rangeArea;
        }
        /// <summary>
        /// 创建人：zenglq
        /// 定义数据的有效性（下拉框选择）
        /// </summary>
        /// <param name="firstRow">开始行</param>
        /// <param name="lastRow">结束行</param>
        /// <param name="firstColum">开始列</param>
        /// <param name="lastColum">结束列</param>
        /// <param name="reginName">区域数据的名称</param>
        /// <param name="sheetName"></param>
        public void DataValidation(int firstRow,int lastRow,int firstColum,int lastColum,string reginName,string sheetName)
        {
            HSSFSheet sheet = (HSSFSheet)workbook.GetSheet(sheetName);
            CellRangeAddressList regions = new CellRangeAddressList(firstRow, lastRow, firstColum, lastColum);
            DVConstraint constraint = DVConstraint.CreateFormulaListConstraint(reginName);
            HSSFDataValidation dataValidate = new HSSFDataValidation(regions, constraint);
            sheet.AddValidationData(dataValidate);
        }

        private string NumToLetter(int num)
        {
            if (num >= 0 && !string.IsNullOrWhiteSpace(num.ToString()))
            {
                string[] test = new string[]{ "","A", "B" ,"C","D","E","F","G",
                                "H","I","J","K","L","M","N",
                                "O","P","Q","R","S","T","U",
                                "V","W","X","Y","Z"};
                return test[num];
            }
            else
            {
                return "";
            }

        }
        /// <summary>
        /// zenglq
        /// 将excel数据导入到datatable
        /// </summary>
        /// <param name="FilePath">文件路径</param>
        /// <param name="sheetName">sheet名称</param>
        /// <returns></returns>
        public static DataTable ExcelInput(string FilePath, string sheetName)
        {
            //第一行一般为标题行。
            DataTable table = new DataTable();

            //根据路径通过已存在的excel来创建HSSFWorkbook，即整个excel文档
            XSSFWorkbook workbook = new XSSFWorkbook(File.Open(FilePath, FileMode.Open));
            XSSFSheet sheet = (XSSFSheet)workbook.GetSheet(sheetName);
            //获取excel的第一个sheet


            IRow headerRow = sheet.GetRow(0);
            //int colsCount = headerRow.LastCellNum-headerRow.FirstCellNum;
            for (int j = headerRow.FirstCellNum; j < headerRow.LastCellNum; j++)
            {
                ICell cell = headerRow.GetCell(j);
                table.Columns.Add(cell.ToString());
            }

            for (int x = sheet.FirstRowNum; x < sheet.LastRowNum; x++)
            {
                DataRow dr = table.NewRow();
                for (int y = headerRow.FirstCellNum; y < headerRow.LastCellNum; y++)
                {
                    dr[y] = sheet.GetRow(x).GetCell(y);
                }
                if (x != headerRow.FirstCellNum)
                {
                    table.Rows.Add(dr);
                }
            }
            sheet = null;
            workbook = null;
            return table;
        }

        /// <summary>
        /// zenglq
        /// 流解析Excel文件输出到DataTable
        /// </summary>
        /// <param name="excelFileStream">文件流</param>
        /// <param name="sheetName">sheet名称</param>
        /// <param name="fileFormart">文件格式（xls（office2003）或者xlsx（office2007））</param>
        /// <returns></returns>
        public static DataTable ExcelRenderFromDataTable(Stream excelFileStream, string sheetName, string fileFormart)
        {
            DataTable table = new DataTable();

            IWorkbook workbook = null;
            ISheet sheet = null;
            if (fileFormart.ToUpper() == "XLS")
            {
                workbook = new HSSFWorkbook(excelFileStream);
                sheet = (HSSFSheet)workbook.GetSheet(sheetName);
            }
            else//xlsx格式
            {
                workbook = new XSSFWorkbook(excelFileStream);
                sheet = (XSSFSheet)workbook.GetSheet(sheetName);
            }


            IRow headerRow = sheet.GetRow(0);
            if (headerRow != null)
            {
                for (int j = headerRow.FirstCellNum; j < headerRow.LastCellNum; j++)
                {
                    ICell cell = headerRow.GetCell(j);
                    table.Columns.Add(cell.ToString());//第一行作为表头
                }
            }

            for (int x = sheet.FirstRowNum; x < sheet.PhysicalNumberOfRows; x++)
            {
                DataRow dr = table.NewRow();
                int rowDataCount = 0;//记录行的每个单元格中是否有数据，如果rowDataCount等于0，则表示该行是空白行
                for (int y = headerRow.FirstCellNum; y < headerRow.LastCellNum; y++)
                {
                    dr[y] = sheet.GetRow(x).GetCell(y);
                    if (dr[y] != null && dr[y].ToString().Replace("\n", "").Trim() != "")
                    {
                        rowDataCount = rowDataCount + 1;
                    }
                }
                if (x != headerRow.FirstCellNum)
                {
                    if (rowDataCount != 0)
                    {
                        table.Rows.Add(dr);
                    }
                    else
                    {
                        break;//如果是空白行，则跳出，不读取下面的行
                    }
                }
            }
            return table;
        }


        /// <summary>
        /// zenglq
        /// 流解析Excel文件输出到DataTable
        /// </summary>
        /// <param name="excelFileStream">文件流</param>
        /// <param name="sheetName">sheet名称</param>
        /// <param name="startCellNum">开始列的索引</param>
        /// <param name="endCellNum">结束列的索引</param>
        /// <param name="fileFormart">文件格式（xls（office2003）或者xlsx（office2007））</param>
        /// <returns></returns>
        public static DataTable ExcelRenderFromDataTable(Stream excelFileStream,string sheetName,int startCellNum,int endCellNum,string fileFormart)
        {
            DataTable table = new DataTable();

            IWorkbook workbook = null; 
            ISheet sheet = null;
            if (fileFormart.ToUpper() == "XLS")
            {
                workbook = new HSSFWorkbook(excelFileStream);
                sheet = (HSSFSheet)workbook.GetSheet(sheetName);
            }
            else//xlsx格式
            {
                workbook = new XSSFWorkbook(excelFileStream);
                sheet = (XSSFSheet)workbook.GetSheet(sheetName);
            }


            IRow headerRow = sheet.GetRow(0);
            if (headerRow != null)
            {
                for (int j = startCellNum; j < endCellNum; j++)
                {
                    ICell cell = headerRow.GetCell(j);
                    table.Columns.Add(cell.ToString());//第一行作为表头
                }
            }

            for (int x = sheet.FirstRowNum; x < sheet.PhysicalNumberOfRows; x++)
            {
                DataRow dr = table.NewRow();
                int rowDataCount = 0;//记录行的每个单元格中是否有数据，如果rowDataCount等于0，则表示该行是空白行
                for (int y = startCellNum; y < endCellNum; y++)
                {
                    dr[y] = sheet.GetRow(x).GetCell(y);
                    if (dr[y] != null && dr[y].ToString().Replace("\n", "").Trim() != "")
                    {
                        rowDataCount = rowDataCount + 1;
                    }
                }
                if (x != headerRow.FirstCellNum)
                {
                    if (rowDataCount != 0)
                    {
                        table.Rows.Add(dr);
                    }
                    else
                    {
                        break;//如果是空白行，则跳出，不读取下面的行
                    }
                }
            }
            return table;
        }
 

        /// <summary>
        /// zenglq
        /// 流解析Excel文件输出到DataTable（默认解析第一个sheet里的数据）
        /// </summary>
        /// <param name="excelFileStream">文件流</param>
        /// <param name="startCellNum">开始列的索引</param>
        /// <param name="endCellNum">结束列的索引</param>
        /// <param name="fileFormart">文件格式（xls（office2003）或者xlsx（office2007））</param>
        /// <returns></returns>
        public static DataTable ExcelRenderFromDataTable(Stream excelFileStream,int startCellNum,int endCellNum, string fileFormart)
        {
            DataTable table = new DataTable();

            IWorkbook workbook = null;
            ISheet sheet = null;
            if (fileFormart.ToUpper() == "XLS")
            {
                workbook = new HSSFWorkbook(excelFileStream);
                sheet = (HSSFSheet)workbook.GetSheetAt(0);
            }
            else//xlsx格式
            {
                workbook = new XSSFWorkbook(excelFileStream);
                sheet = (XSSFSheet)workbook.GetSheetAt(0);
            }

            IRow headerRow = sheet.GetRow(0);
            if (headerRow != null)
            {
                for (int j = startCellNum; j < endCellNum; j++)
                {
                    ICell cell = headerRow.GetCell(j);
                    table.Columns.Add(cell.ToString());
                }
            }

            for (int x = sheet.FirstRowNum; x < sheet.PhysicalNumberOfRows; x++)
            {
                DataRow dr = table.NewRow();
                int rowDataCount = 0;//记录行的每个单元格中是否有数据，如果rowDataCount等于0，则表示该行是空白行
                for (int y = startCellNum; y < endCellNum; y++)
                {
                    dr[y] = sheet.GetRow(x).GetCell(y);
                    if (dr[y]!=null&&dr[y].ToString().Replace("\n","").Trim()!="")
                    {
                        rowDataCount = rowDataCount + 1;
                    }
                }
                if (x != headerRow.FirstCellNum)
                {
                    if (rowDataCount != 0)
                    {
                        table.Rows.Add(dr);
                    }
                    else
                    {
                        break;//如果是空白行，则跳出，不读取下面的行
                    }
                }
            }
            return table;
        }

    
    }
}