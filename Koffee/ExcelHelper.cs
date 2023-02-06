using System;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace Koffee;

class ExcelHelper : IDisposable
{
    private Excel.Application _excel;
    private Excel.Workbook _workbook;
    private string _filePath;

    public ExcelHelper()
    {
        _excel = new Excel.Application();
    }

    public void Dispose()
    {
        try
        {
            _workbook.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    internal bool Open(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                _workbook = _excel.Workbooks.Open(filePath);
            }
            else
            {
                _workbook = _excel.Workbooks.Add();
                _filePath = filePath;
            }

            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    internal bool Set(string column, int row, object data)
    {
        try
        {
            ((Excel.Worksheet) _excel.ActiveSheet).Cells[row, column] = data;
            
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public void Save()
    {
        if (!string.IsNullOrEmpty( _filePath))
        {
            _workbook.SaveAs(_filePath);
            _filePath = null;
        }
        else
        {
            _workbook.Save();
        }
    }
}