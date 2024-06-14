using BankCoreApi.Models.Reports;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;

namespace BankCoreApi.Controllers
{
    public class TransactionsFormat
    {
        public static byte[] GenerateExcel(IEnumerable<TransactionReport> transactions)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Transactions");
                // Add headers
                worksheet.Cells["A1"].Value = "Transaction Code";
                worksheet.Cells["B1"].Value = "Source Account";
                worksheet.Cells["C1"].Value = "Destination Account";
                worksheet.Cells["D1"].Value = "Transaction Date";
                worksheet.Cells["E1"].Value = "Transaction Type";
                worksheet.Cells["F1"].Value = "Amount";
                worksheet.Cells["G1"].Value = "Currency";
                worksheet.Cells["H1"].Value = "Balance Before";
                worksheet.Cells["I1"].Value = "Balance After";
                worksheet.Cells["J1"].Value = "Status";
                worksheet.Cells["K1"].Value = "Description";
                // Add data
                int row = 2;
                foreach (var transaction in transactions)
                {
                    worksheet.Cells[row, 1].Value = transaction.Code;
                    worksheet.Cells[row, 2].Value = transaction.SourceAccountNumber;
                    worksheet.Cells[row, 3].Value = transaction.DestinationAccountNumber;
                    worksheet.Cells[row, 4].Value = transaction.TransactionDate.ToString("yyyy-MM-dd");
                    worksheet.Cells[row, 5].Value = transaction.TransactionType;
                    worksheet.Cells[row, 6].Value = transaction.Amount;
                    worksheet.Cells[row, 7].Value = transaction.Currency;
                    worksheet.Cells[row, 8].Value = transaction.BalanceBefore;
                    worksheet.Cells[row, 9].Value = transaction.BalanceAfter;
                    worksheet.Cells[row, 10].Value = transaction.TransactionStatus;
                    worksheet.Cells[row, 11].Value = transaction.Description;
                    row++;
                }
                // Auto fit columns for better readability
                worksheet.Cells.AutoFitColumns();
                // Convert Excel package to bytes
                return package.GetAsByteArray();
            }
        }

        public static byte[] GenerateCSV(IEnumerable<TransactionReport> transactions)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (StreamWriter streamWriter = new StreamWriter(memoryStream, Encoding.UTF8))
                {
                    // Write headers
                    streamWriter.WriteLine("Code,SourceAccountNumber,DestinationAccountNumber,TransactionDate,TransactionType,Amount,Currency,BalanceBefore,BalanceAfter,TransactionStatus,Description");
                    // Write data
                    foreach (var transaction in transactions)
                    {
                        streamWriter.WriteLine(string.Format(
                            "{0},{1},{2},{3},{4},{5},{6},{7},{8},{9},{10}",
                            transaction.Code,
                            transaction.SourceAccountNumber,
                            transaction.DestinationAccountNumber,
                            transaction.TransactionDate.ToString("yyyy-MM-dd"),
                            transaction.TransactionType,
                            transaction.Amount,
                            transaction.Currency,
                            transaction.BalanceBefore,
                            transaction.BalanceAfter,
                            transaction.TransactionStatus,
                            transaction.Description
                        ));
                    }
                    streamWriter.Flush();
                }
                return memoryStream.ToArray();
            }
        }
        

        public static byte[] GeneratePDF(IEnumerable<TransactionReport> transactions)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                PdfDocument document = new PdfDocument();
                PdfPage page = document.AddPage();
                XGraphics graphics = XGraphics.FromPdfPage(page);
                XFont font = new XFont("Arial", 7, XFontStyle.Regular); // Adjusted font size to 7
                // Draw headers
                graphics.DrawString("Transaction Code", font, XBrushes.Black, new XRect(30, 50, 100, 20), XStringFormats.TopLeft);
                graphics.DrawString("Source", font, XBrushes.Black, new XRect(150, 50, 100, 20), XStringFormats.TopLeft);
                graphics.DrawString("Destination", font, XBrushes.Black, new XRect(250, 50, 100, 20), XStringFormats.TopLeft);
                graphics.DrawString("Transaction Date", font, XBrushes.Black, new XRect(350, 50, 150, 20), XStringFormats.TopLeft);
                graphics.DrawString("Transaction Type", font, XBrushes.Black, new XRect(450, 50, 150, 20), XStringFormats.TopLeft);
                graphics.DrawString("Amount", font, XBrushes.Black, new XRect(550, 50, 100, 20), XStringFormats.TopLeft);
                // Draw data
                int yPosition = 70;
                foreach (var transaction in transactions)
                {
                    graphics.DrawString(transaction.Code?.ToString(), font, XBrushes.Black, new XRect(30, yPosition, 100, 20), XStringFormats.TopLeft);
                    graphics.DrawString(transaction.SourceAccountNumber, font, XBrushes.Black, new XRect(150, yPosition, 100, 20), XStringFormats.TopLeft);
                    graphics.DrawString(transaction.DestinationAccountNumber, font, XBrushes.Black, new XRect(250, yPosition, 100, 20), XStringFormats.TopLeft);
                    graphics.DrawString(transaction.TransactionDate.ToString("yyyy-MM-dd"), font, XBrushes.Black, new XRect(350, yPosition, 150, 20), XStringFormats.TopLeft);
                    graphics.DrawString(transaction.TransactionType, font, XBrushes.Black, new XRect(450, yPosition, 150, 20), XStringFormats.TopLeft);
                    graphics.DrawString(transaction.Amount.ToString(), font, XBrushes.Black, new XRect(550, yPosition, 100, 20), XStringFormats.TopLeft);
                    yPosition += 20;
                }
                document.Save(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}