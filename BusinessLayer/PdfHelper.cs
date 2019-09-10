using DataLayer;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.IO;

namespace BusinessLayer
{
    public class PdfHelper
    {
        public byte[] CreatePdf(Request request, string approver)
        {
            byte[] result;
            using (MemoryStream ms = new MemoryStream())
            {
                Document doc = new Document(PageSize.A4, 0, 0, 0, 0);
                PdfWriter writer = PdfWriter.GetInstance(doc, ms);
                doc.Open();

                doc.NewPage();
                doc.Add(new Paragraph("Vacation approval file"));
                doc.Add(new Paragraph("Approved by: " + approver));
                doc.Add(new Paragraph("Start date: " + request.RequestStartDate.ToShortDateString()));
                doc.Add(new Paragraph("End date: " + request.RequestEndDate.ToShortDateString()));
                doc.Add(new Paragraph("Number of days: " + request.RequestNumberOfDays));
                doc.Add(new Paragraph("Employee comment: " + request.RequestComment));
                doc.Add(new Paragraph("HR Comment: " + request.RequestDenialComment));

                doc.Close();
                result = ms.ToArray();

                ms.Close();
            }

            return result;
        }
    }
}
