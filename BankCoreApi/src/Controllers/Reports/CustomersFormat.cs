using BankCoreApi.Models.Reports;
using OfficeOpenXml;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;

namespace BankCoreApi.Controllers
{
    public class CustomersFormat
    {
        public static byte[] GenerateExcel(IEnumerable<CustomerReport> customers)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Customers");
                // Add headers
                worksheet.Cells["A1"].Value = "Customer Name";
                worksheet.Cells["B1"].Value = "Identification Number";
                worksheet.Cells["C1"].Value = "Birth Date";
                worksheet.Cells["D1"].Value = "Gender";
                worksheet.Cells["E1"].Value = "Type";
                worksheet.Cells["F1"].Value = "Phone";
                worksheet.Cells["G1"].Value = "Email";
                worksheet.Cells["H1"].Value = "Addres";
                worksheet.Cells["I1"].Value = "Status";
                worksheet.Cells["J1"].Value = "Created At";
                // Add data
                int row = 2;
                foreach (var customer in customers)
                {
                    worksheet.Cells[row, 1].Value = customer.CustomerName;
                    worksheet.Cells[row, 2].Value = customer.IdentificationNumber;
                    worksheet.Cells[row, 3].Value = customer.BirthDate;
                    worksheet.Cells[row, 4].Value = customer.Gender;
                    worksheet.Cells[row, 5].Value = customer.CustomerType;
                    worksheet.Cells[row, 6].Value = customer.Phone;
                    worksheet.Cells[row, 7].Value = customer.Email;
                    worksheet.Cells[row, 8].Value = customer.Address;
                    worksheet.Cells[row, 9].Value = customer.CustomerStatus;
                    worksheet.Cells[row, 10].Value = customer.CreatedAt.ToString("yyyy-MM-dd");
                    row++;
                }
                // Auto fit columns for better readability
                worksheet.Cells.AutoFitColumns();
                // Convert Excel package to bytes
                return package.GetAsByteArray();
            }
        }

        public static byte[] GenerateCSV(IEnumerable<CustomerReport> customers)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (StreamWriter streamWriter = new StreamWriter(memoryStream, Encoding.UTF8))
                {
                    // Write headers
                    streamWriter.WriteLine("CustomerName,IdentificationNumber,BirthDate,Gender,CustomerType,Phone,Email,Address,CustomerStatus,CreatedAt");
                    // Write data
                    foreach (var customer in customers)
                    {
                        streamWriter.WriteLine(string.Format(
                            "{0},{1},{2},{3},{4},{5},{6},{7},{8},{9}",
                            customer.CustomerName,
                            customer.IdentificationNumber,
                            customer.BirthDate,
                            customer.Gender,
                            customer.CustomerType,
                            customer.Phone,
                            customer.Email,
                            customer.Address,
                            customer.CustomerStatus,
                            customer.CreatedAt.ToString("yyyy-MM-dd")
                        ));
                    }
                    streamWriter.Flush();
                }
                return memoryStream.ToArray();
            }
        }
        

        public static byte[] GeneratePDF(IEnumerable<CustomerReport> customers)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                PdfDocument document = new PdfDocument();
                PdfPage page = document.AddPage();
                XGraphics graphics = XGraphics.FromPdfPage(page);
                XFont font = new XFont("Arial", 7, XFontStyle.Regular); // Adjusted font size to 7
                // Draw headers
                graphics.DrawString("Customer Name", font, XBrushes.Black, new XRect(30, 50, 100, 20), XStringFormats.TopLeft);
                graphics.DrawString("IdentificationNumber", font, XBrushes.Black, new XRect(150, 50, 100, 20), XStringFormats.TopLeft);
                graphics.DrawString("Birth Date", font, XBrushes.Black, new XRect(250, 50, 100, 20), XStringFormats.TopLeft);
                graphics.DrawString("Gender", font, XBrushes.Black, new XRect(350, 50, 150, 20), XStringFormats.TopLeft);
                graphics.DrawString("Type", font, XBrushes.Black, new XRect(450, 50, 150, 20), XStringFormats.TopLeft);
                graphics.DrawString("Status", font, XBrushes.Black, new XRect(550, 50, 100, 20), XStringFormats.TopLeft);
                // Draw data
                int yPosition = 70;
                foreach (var customer in customers)
                {
                    graphics.DrawString(customer.CustomerName, font, XBrushes.Black, new XRect(30, yPosition, 100, 20), XStringFormats.TopLeft);
                    graphics.DrawString(customer.IdentificationNumber, font, XBrushes.Black, new XRect(150, yPosition, 100, 20), XStringFormats.TopLeft);
                    graphics.DrawString(customer.BirthDate.ToString("yyyy-MM-dd"), font, XBrushes.Black, new XRect(250, yPosition, 100, 20), XStringFormats.TopLeft);
                    graphics.DrawString(customer.Gender, font, XBrushes.Black, new XRect(350, yPosition, 150, 20), XStringFormats.TopLeft);
                    graphics.DrawString(customer.CustomerType, font, XBrushes.Black, new XRect(450, yPosition, 150, 20), XStringFormats.TopLeft);
                    graphics.DrawString(customer.CustomerStatus, font, XBrushes.Black, new XRect(550, yPosition, 100, 20), XStringFormats.TopLeft);
                    yPosition += 20;
                }
                document.Save(memoryStream);
                return memoryStream.ToArray();
            }
        }
    }
}