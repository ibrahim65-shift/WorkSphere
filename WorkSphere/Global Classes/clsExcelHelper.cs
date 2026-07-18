using ClosedXML.Excel;
using System;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace WorkSphere.Global_Classes
{
    public static class clsExcelHelper
    {
        public static void Export(Form owner, DataTable dataTable, string sheetName)
        {
            if (dataTable == null || dataTable.Rows.Count == 0)
            {
                MessageBox.Show("لا توجد بيانات للتصدير.", "تنبيه", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.RestoreDirectory = true;
                saveFileDialog.AddExtension = true;
                saveFileDialog.DefaultExt = "xlsx";
                saveFileDialog.Filter = "Excel File (.xlsx)|.xlsx";
                saveFileDialog.Title = "تصدير ملف Excel";
                saveFileDialog.FileName = $"{sheetName}_{DateTime.Now:yyyyMMdd_HHmmss}.xlsx";

                // ✅ تمرير الفورم كـ Owner لتجنب مشكلة Minimize
                if (saveFileDialog.ShowDialog(owner) == DialogResult.OK)
                {
                    try
                    {
                        // ✅ إنشاء ملف Excel مباشرة من DataTable
                        using (XLWorkbook workbook = new XLWorkbook())
                        {
                            workbook.Worksheets.Add(dataTable, sheetName);
                            workbook.SaveAs(saveFileDialog.FileName);
                        }

                        // ✅ رسالة نجاح
                        MessageBox.Show("✅ تم تصدير الملف بنجاح!", "نجاح", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (IOException)
                    {
                        MessageBox.Show("الملف مفتوح حالياً، الرجاء إغلاقه ثم إعادة المحاولة.", "خطأ في الملف", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"حدث خطأ أثناء التصدير:\n{ex.Message}", "خطأ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}