using CachacasCanuto.Application.ViewModels.Files;
using CachacasCanuto.Application.ViewModels.Reports;
using ClosedXML.Excel;

namespace CachacasCanuto.Application.Common.Extensions
{
    public static class GenerateFileExtension
    {
        public static FileReportViewModel GenerateCustomerReportFile(List<CustomerSalesReportViewModel> data)
        {
            var worksheetName = $"relatorio_vendas";
            using var workbook = new XLWorkbook();

            var worksheet = workbook.Worksheets.Add(worksheetName);

            var currentRow = 1;
            var currentColumn = 1;

            worksheet.Cell(currentRow, currentColumn++).Value = "Id do cliente";
            worksheet.Cell(currentRow, currentColumn++).Value = "Nome do cliente";
            worksheet.Cell(currentRow, currentColumn++).Value = "Valor total de vendas";

            foreach (var item in data)
            {
                currentRow++;
                currentColumn = 1;
                worksheet.Cell(currentRow, currentColumn++).SetValue(item.CustomerId);
                worksheet.Cell(currentRow, currentColumn++).SetValue(item.CustomerName);
                worksheet.Cell(currentRow, currentColumn).Style.NumberFormat.Format = "[$R$-pt-BR] #,##0.00";
                worksheet.Cell(currentRow, currentColumn++).SetValue(item.TotalSales);

                foreach (var product in item.Reports)
                {
                    worksheet.Cell(1, currentColumn).Value = "Nome do produto";
                    worksheet.Cell(currentRow, currentColumn++).SetValue(product.Product.Name);
                    worksheet.Cell(1, currentColumn).Value = "Quantidade vendida";
                    worksheet.Cell(currentRow, currentColumn).Style.NumberFormat.Format = "#####";
                    worksheet.Cell(currentRow, currentColumn++).SetValue(product.QuantitySold);
                    worksheet.Cell(1, currentColumn).Value = "Valor total de vendas";
                    worksheet.Cell(currentRow, currentColumn).Style.NumberFormat.Format = "[$R$-pt-BR] #,##0.00";
                    worksheet.Cell(currentRow, currentColumn++).SetValue(product.TotalSold);
                };
            }

            using var stream = new MemoryStream();

            workbook.SaveAs(stream);

            var content = stream.ToArray();

            return new(content, worksheetName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }

        public static FileReportViewModel GenerateProductReportFile(List<ProductSalesReportViewModel> data)
        {
            var worksheetName = $"relatorio_vendas";
            using var workbook = new XLWorkbook();

            var worksheet = workbook.Worksheets.Add(worksheetName);

            var currentRow = 1;
            var currentColumn = 1;

            worksheet.Cell(currentRow, currentColumn++).Value = "Id do produto";
            worksheet.Cell(currentRow, currentColumn++).Value = "Nome do produto";
            worksheet.Cell(currentRow, currentColumn++).Value = "Quantidade total de vendas";
            worksheet.Cell(currentRow, currentColumn++).Value = "Valor total de vendas";

            foreach (var item in data)
            {
                currentRow++;
                currentColumn = 1;
                worksheet.Cell(currentRow, currentColumn++).SetValue(item.Id);
                worksheet.Cell(currentRow, currentColumn++).SetValue(item.Name);
                worksheet.Cell(currentRow, currentColumn).Style.NumberFormat.Format = "#####";
                worksheet.Cell(currentRow, currentColumn++).SetValue(item.QuantitySold);
                worksheet.Cell(currentRow, currentColumn).Style.NumberFormat.Format = "[$R$-pt-BR] #,##0.00";
                worksheet.Cell(currentRow, currentColumn++).SetValue(item.TotalAmount);
            }

            using var stream = new MemoryStream();

            workbook.SaveAs(stream);

            var content = stream.ToArray();

            return new(content, worksheetName, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
    }
}
