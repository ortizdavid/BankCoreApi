using BankCoreApi.Models.Reports;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;

namespace BankCoreApi.Controllers;

public class AccountsFormat
{
    public static byte[] GenerateExcel(IEnumerable<AccountReport> accounts)
    {
        using (var package = new ExcelPackage())
        {
            var worksheet = package.Workbook.Worksheets.Add("Accounts");
            // Add headers
            worksheet.Cells["A1"].Value = "Number";
            worksheet.Cells["B1"].Value = "IBAN";
            worksheet.Cells["C1"].Value = "Type";
            worksheet.Cells["D1"].Value = "Customer Name";
            worksheet.Cells["E1"].Value = "Identification Number";
            worksheet.Cells["F1"].Value = "Balance";
            worksheet.Cells["G1"].Value = "Currency";
            worksheet.Cells["H1"].Value = "Status";
            worksheet.Cells["I1"].Value = "Created At";
            // Add data
            int row = 2;
            foreach (var account in accounts)
            {
                worksheet.Cells[row, 1].Value = account.AccountNumber;
                worksheet.Cells[row, 2].Value = account.Iban;
                worksheet.Cells[row, 3].Value = account.AccountType;
                worksheet.Cells[row, 4].Value = account.CustomerName;
                worksheet.Cells[row, 5].Value = account.IdentificationNumber;
                worksheet.Cells[row, 6].Value = account.Balance;
                worksheet.Cells[row, 7].Value = account.Currency;
                worksheet.Cells[row, 8].Value = account.AccountStatus;
                worksheet.Cells[row, 9].Value = account.CreatedAt.ToString("yyyy-MM-dd");
                row++;
            }
            // Auto fit columns for better readability
            worksheet.Cells.AutoFitColumns();
            // Convert Excel package to bytes
            return package.GetAsByteArray();
        }
    }

    public static byte[] GenerateCSV(IEnumerable<AccountReport> accounts)
    {
        using (MemoryStream memoryStream = new MemoryStream())
        {
            using (StreamWriter streamWriter = new StreamWriter(memoryStream, Encoding.UTF8))
            {
                // Write headers
                streamWriter.WriteLine("AccountNumber,Iban,AccountType,CustomerName,IdentificationNumber,Balance,Currency,AccountStatus,CreatedAt");
                // Write data
                foreach (var account in accounts)
                {
                    streamWriter.WriteLine(string.Format(
                        "{0},{1},{2},{3},{4},{5},{6},{7},{8}",
                        account.AccountNumber,
                        account.Iban,
                        account.AccountType,
                        account.CustomerName,
                        account.IdentificationNumber,
                        account.Currency,
                        account.Balance,
                        account.Currency,
                        account.AccountStatus,
                        account.CreatedAt.ToString("yyyy-MM-dd")
                    ));
                }
                streamWriter.Flush();
            }
            return memoryStream.ToArray();
        }
    }
    

    public static byte[] GeneratePDF(IEnumerable<AccountReport> accounts)
    {
        using (MemoryStream memoryStream = new MemoryStream())
        {
            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            XGraphics graphics = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Arial", 7, XFontStyle.Regular); // Adjusted font size to 7
            // Draw headers
            graphics.DrawString("Number", font, XBrushes.Black, new XRect(30, 50, 100, 20), XStringFormats.TopLeft);
            graphics.DrawString("Iban", font, XBrushes.Black, new XRect(150, 50, 100, 20), XStringFormats.TopLeft);
            graphics.DrawString("Type", font, XBrushes.Black, new XRect(250, 50, 100, 20), XStringFormats.TopLeft);
            graphics.DrawString("Customer Name", font, XBrushes.Black, new XRect(350, 50, 150, 20), XStringFormats.TopLeft);
            graphics.DrawString("Identification Number", font, XBrushes.Black, new XRect(450, 50, 150, 20), XStringFormats.TopLeft);
            graphics.DrawString("Balance", font, XBrushes.Black, new XRect(550, 50, 100, 20), XStringFormats.TopLeft);
            // Draw data
            int yPosition = 70;
            foreach (var account in accounts)
            {
                graphics.DrawString(account.AccountNumber, font, XBrushes.Black, new XRect(30, yPosition, 100, 20), XStringFormats.TopLeft);
                graphics.DrawString(account.Iban, font, XBrushes.Black, new XRect(150, yPosition, 100, 20), XStringFormats.TopLeft);
                graphics.DrawString(account.AccountType, font, XBrushes.Black, new XRect(250, yPosition, 100, 20), XStringFormats.TopLeft);
                graphics.DrawString(account.CustomerName, font, XBrushes.Black, new XRect(350, yPosition, 150, 20), XStringFormats.TopLeft);
                graphics.DrawString(account.IdentificationNumber, font, XBrushes.Black, new XRect(450, yPosition, 150, 20), XStringFormats.TopLeft);
                graphics.DrawString(account.Balance.ToString(), font, XBrushes.Black, new XRect(550, yPosition, 100, 20), XStringFormats.TopLeft);
                yPosition += 20;
            }
            document.Save(memoryStream);
            return memoryStream.ToArray();
        }
    }
}