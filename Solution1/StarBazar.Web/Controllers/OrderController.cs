using ExcelDataReader;
using LinqToExcel;
using OfficeOpenXml;
using StarBazar.Entities;
using StarBazar.Services;
using StarBazar.Web.ExtensionMethod;
using StarBazar.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace StarBazar.Web.Controllers
{
    public class OrderController : Controller
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["StarBazar"].ConnectionString);

        OleDbConnection Econ;

        public ActionResult Index()
        {
            var list = OrderService.Instance.GetProducts();
            return View(list);
        }

        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)

        {

            string filename = Guid.NewGuid() + Path.GetExtension(file.FileName);

            string filepath = "/excelfolder/" + filename;

            file.SaveAs(Path.Combine(Server.MapPath("/excelfolder"), filename));

            InsertExceldata(filepath, filename);
            var list = OrderService.Instance.GetProducts();
            return View(list);

        }

        private void ExcelConn(string filepath)

        {

            string constr = string.Format(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=""Excel 12.0 Xml;HDR=YES;""", filepath);

            Econ = new OleDbConnection(constr);



        }

        [HttpPost]
        private  void InsertExceldata(string fileepath, string filename)

        {

            string fullpath = Server.MapPath("/excelfolder/") + filename;

            ExcelConn(fullpath);

            string query = string.Format("Select * from [{0}]", "Report$");

            OleDbCommand Ecom = new OleDbCommand(query, Econ);

            Econ.Open();



            DataSet ds = new DataSet();

            OleDbDataAdapter oda = new OleDbDataAdapter(query, Econ);

            Econ.Close();

            oda.Fill(ds);



            DataTable dt = ds.Tables[0];



            SqlBulkCopy objbulk = new SqlBulkCopy(con);

            objbulk.DestinationTableName = "Orders";

            objbulk.ColumnMappings.Add("UserId", "UserId");

            objbulk.ColumnMappings.Add("OrderedAt", "OrderedAt");

            objbulk.ColumnMappings.Add("TotalAmount", "TotalAmount");
            objbulk.ColumnMappings.Add("Status", "Status");
            con.Open();

            objbulk.WriteToServer(dt);

            con.Close();

        }


        public void DownloadToExcel()
        {
            var list = OrderService.Instance.GetProducts();

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            //ws.Cells["A1"].Value = "Communication";
            //ws.Cells["B1"].Value = "Com1";

            //ws.Cells["A2"].Value = "Report";
            //ws.Cells["B2"].Value = "Report1";

            //ws.Cells["A3"].Value = "Date";
            //ws.Cells["B3"].Value = string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", DateTimeOffset.Now);

            ws.Cells["A1"].Value = "UserId";
            ws.Cells["B1"].Value = "OrderedAt";
            ws.Cells["C1"].Value = "TotalAmount";
            ws.Cells["D1"].Value = "Status";
            //ws.Cells["E6"].Value = "OrderItems";

            int rowStart = 2;
            foreach (var item in list)
            {
                ws.Cells[string.Format("A{0}", rowStart)].Value = item.UserId;
                //ws.Cells[string.Format("B{0}", rowStart)].Value = item.OrderedAt;
                ws.Cells[string.Format("B{0}", rowStart)].Value = string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", item.OrderedAt);
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.TotalAmount;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.Status;
                //ws.Cells[string.Format("E{0}", rowStart)].Value = item.OrderItems;
                rowStart++;
            }

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();

        }

        public void DownLoadSampleFile()
        {
            var list = OrderService.Instance.GetProducts();

            ExcelPackage pck = new ExcelPackage();
            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Report");

            ws.Cells["A1"].Value = "UserId";
            ws.Cells["B1"].Value = "OrderedAt";
            ws.Cells["C1"].Value = "TotalAmount";
            ws.Cells["D1"].Value = "Status";

            ws.Cells["A:AZ"].AutoFitColumns();
            Response.Clear();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment: filename=" + "ExcelReport.xlsx");
            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();
        }


      
    }

}
