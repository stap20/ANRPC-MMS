using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.IO;

namespace ANRPC_Inventory
{
    public partial class FReports : Form
    {
        public FReports()
        {
            InitializeComponent();
        }

        private void FReports_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
            reportViewer1.Reset();
            reportViewer1.SetDisplayMode(DisplayMode.PrintLayout);
            reportViewer1.ZoomMode = ZoomMode.Percent;
            reportViewer1.ZoomPercent = 100;
            this.reportViewer1.LocalReport.EnableExternalImages = true;

            // TODO: This line of code loads data into the 'ANRPC_SMSDataSet12.EmpTrans_View_print' table. You can move, or remove it, as needed.
            //    this.empTrans_View_printTableAdapter1.Fill(this.ANRPC_SMSDataSet12.EmpTrans_View_print);
            if (Constants.FormNo == 1)
            {

             
                    reportViewer1.Reset();
                    ReportParameter rp1 = new ReportParameter("p1", Constants.Unit);
                    ReportParameter rp2 = new ReportParameter("p2", Constants.TasnifNo);
                    ReportParameter rp3 = new ReportParameter("p3", Constants.TasnifName);
                    ReportParameter rp4 = new ReportParameter("p4", Constants.Desc);
                    ReportParameter rp5 = new ReportParameter("p5", Constants.Quan);
                    ReportParameter rp6 = new ReportParameter("p6", Constants.RakmEdafa);
                   ReportParameter rp7 = new ReportParameter("p7", Constants.DateEdafa);
                

                    Microsoft.Reporting.WinForms.ReportDataSource RDC = new Microsoft.Reporting.WinForms.ReportDataSource();
                    RDC.Name = "DataSet1";
                 //  RDC.Value = this.EmpTrans_View_printBindingSource;
                    this.reportViewer1.LocalReport.DataSources.Add(RDC);
                    this.reportViewer1.LocalReport.ReportEmbeddedResource = "ANRPC_Inventory.R_TasnifCard.rdlc";
              //      this.EmpTrans_View_printTableAdapter.Fill(this.ANRPC_SMSDataSet37.EmpTrans_View_print);
                    this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3,rp4,rp5,rp6,rp7 });
                    this.reportViewer1.RefreshReport();
                
             //   else
              //  {
             //       reportViewer1.Reset();

             //       Microsoft.Reporting.WinForms.ReportDataSource RDC = new Microsoft.Reporting.WinForms.ReportDataSource();
              ///      RDC.Name = "DataSet1";
             //       RDC.Value = this.EmpTrans_View_printBindingSource;
              ///      this.reportViewer1.LocalReport.DataSources.Add(RDC);
              //      this.reportViewer1.LocalReport.ReportEmbeddedResource = "ANRPC_SMS.Report2.rdlc";
              //      this.EmpTrans_View_printTableAdapter.Fill(this.ANRPC_SMSDataSet37.EmpTrans_View_print);

              ///      this.reportViewer1.RefreshReport();
             //   }

                //    }


            
                /*
            else if (Constants.FormNo == 2)
            {



                if (Constants.searchbtn == true)
                {
                    reportViewer1.Reset();
                    ReportParameter rp1 = new ReportParameter("p1", Constants.date1);
                    ReportParameter rp2 = new ReportParameter("p2", Constants.date2);
                    ReportParameter rp3 = new ReportParameter("p3", Constants.searchbtn.ToString());
                    Microsoft.Reporting.WinForms.ReportDataSource RDC = new Microsoft.Reporting.WinForms.ReportDataSource();
                    RDC.Name = "DataSet1";
                    RDC.Value = this.Mangers_print_viewBindingSource;
                    this.reportViewer1.LocalReport.DataSources.Add(RDC);
                    this.reportViewer1.LocalReport.ReportEmbeddedResource = "ANRPC_SMS.Report1.rdlc";
                    this.Mangers_print_viewTableAdapter.Fill(this.ANRPC_SMSDataSet20.Mangers_print_view);
                    this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3 });
                    this.reportViewer1.RefreshReport();
                }
                else
                {
                    reportViewer1.Reset();

                    Microsoft.Reporting.WinForms.ReportDataSource RDC = new Microsoft.Reporting.WinForms.ReportDataSource();

                    RDC.Name = "DataSet1";
                    RDC.Value = this.Mangers_print_viewBindingSource;
                    this.reportViewer1.LocalReport.DataSources.Add(RDC);
                    this.reportViewer1.LocalReport.ReportEmbeddedResource = "ANRPC_SMS.Report1.rdlc";
                    this.Mangers_print_viewTableAdapter.Fill(this.ANRPC_SMSDataSet20.Mangers_print_view);
                    this.reportViewer1.RefreshReport();
                }
            */

            }
            else if (Constants.FormNo == 2)
            {
                reportViewer1.Reset();
                ReportParameter rp1 = new ReportParameter("p1", Constants.Date_E);
                ReportParameter rp2 = new ReportParameter("p2", Constants.AmrNo);
                ReportParameter rp3 = new ReportParameter("p3", Constants.AmrSanaMalya);
                ReportParameter rp4 = new ReportParameter("p4", Constants.MwardName);
                ReportParameter rp5 = new ReportParameter("p5", Constants.No_Tard);
                ReportParameter rp6 = new ReportParameter("p6", Constants.No_Bnod);
                ReportParameter rp7 = new ReportParameter("p7", Constants.Sanf);
                ReportParameter rp8 = new ReportParameter("p8", Constants.Date_Amr);

                ReportParameter rp9 = new ReportParameter("p9",Constants.Sign1);
                ReportParameter rp10 = new ReportParameter("p10", Constants.Sign2);
                ReportParameter rp11 = new ReportParameter("p11", Constants.Sign3);
                ReportParameter rp12 = new ReportParameter("p12", Constants.Sign4);
                this.reportViewer1.LocalReport.EnableExternalImages = true;


                //Microsoft.Reporting.WinForms.ReportDataSource RDC = new Microsoft.Reporting.WinForms.ReportDataSource();
               // RDC.Name = "DataSet1";
                //  RDC.Value = this.EmpTrans_View_printBindingSource;
               // this.reportViewer1.LocalReport.DataSources.Add(RDC);
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "ANRPC_Inventory.R_Estlam.rdlc";
                //      this.EmpTrans_View_printTableAdapter.Fill(this.ANRPC_SMSDataSet37.EmpTrans_View_print);
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7 ,rp8,rp9,rp10,rp11,rp12});
               
                
                this.reportViewer1.RefreshReport();
            }
            else if (Constants.FormNo == 3)
            {
                reportViewer1.Reset();
                this.reportViewer1.LocalReport.EnableExternalImages = true;


                Microsoft.Reporting.WinForms.ReportDataSource RDC = new Microsoft.Reporting.WinForms.ReportDataSource();
                RDC.Name = "DataSet1";
                RDC.Value = this.SP_ReportMotabkaBindingSource;
                this.reportViewer1.LocalReport.DataSources.Add(RDC);
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "ANRPC_Inventory.R_Motabka.rdlc";
                this.sP_ReportMotabkaTableAdapter1.Fill(this.ANRPC_Inventory_v2DataSet3.SP_ReportMotabka, Constants.EdafaNo, Constants.EdafaFY);
                 //   this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { });
                this.reportViewer1.RefreshReport();
            }
            else if (Constants.FormNo ==4)
            {
                ReportParameter rp1 = new ReportParameter("p1", Constants.MangerName);
                reportViewer1.Reset();

                this.reportViewer1.LocalReport.EnableExternalImages = true;

                Microsoft.Reporting.WinForms.ReportDataSource RDC = new Microsoft.Reporting.WinForms.ReportDataSource();
                RDC.Name = "DataSet1";
                RDC.Value = this.SP_ReportEstagelBindingSource;
                this.reportViewer1.LocalReport.DataSources.Add(RDC);
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "ANRPC_Inventory.R_Estagal.rdlc";
                this.SP_ReportEstagelTableAdapter.Fill(this.ANRPC_Inventory_v2DataSet4.SP_ReportEstagel, Constants.EdafaNo, Constants.EdafaFY);
                //   this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { });
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1});

                this.reportViewer1.RefreshReport();
            }
            else if (Constants.FormNo == 5)
            {
                reportViewer1.Reset();


                Microsoft.Reporting.WinForms.ReportDataSource RDC = new Microsoft.Reporting.WinForms.ReportDataSource();
                RDC.Name = "DataSet1";
                RDC.Value = this.SP_EdafaR1BindingSource;
                this.reportViewer1.LocalReport.DataSources.Add(RDC);
                this.reportViewer1.LocalReport.EnableExternalImages = true;

                
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "ANRPC_Inventory.Edafa_R1.rdlc";
                this.SP_EdafaR1TableAdapter.Fill(this.ANRPC_InventoryDataSet5.SP_EdafaR1, Constants.EdafaNo, Constants.EdafaFY);
                //   this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { });
                this.reportViewer1.RefreshReport();//SP_AmrsheraaR1TableAdapter
            }
            else if (Constants.FormNo == 5)
            {
                reportViewer1.Reset();


                Microsoft.Reporting.WinForms.ReportDataSource RDC = new Microsoft.Reporting.WinForms.ReportDataSource();
                RDC.Name = "DataSet1";
                RDC.Value = this.SP_EdafaR1BindingSource;
                this.reportViewer1.LocalReport.DataSources.Add(RDC);
                this.reportViewer1.LocalReport.EnableExternalImages = true;

                
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "ANRPC_Inventory.Edafa_R1.rdlc";
                this.SP_EdafaR1TableAdapter.Fill(this.ANRPC_InventoryDataSet5.SP_EdafaR1, Constants.EdafaNo, Constants.EdafaFY);
                //   this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { });
                this.reportViewer1.RefreshReport();//SP_AmrsheraaR1TableAdapter
            }
            else if (Constants.FormNo == 6)
            {
                reportViewer1.Reset();


                Microsoft.Reporting.WinForms.ReportDataSource RDC = new Microsoft.Reporting.WinForms.ReportDataSource();
                RDC.Name = "DataSet1";
                RDC.Value = this.SP_AmrsheraaR1BindingSource;
                this.reportViewer1.LocalReport.DataSources.Add(RDC);
                this.reportViewer1.LocalReport.EnableExternalImages = true;

                // TODO
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "ANRPC_Inventory.Amrsheraa_R1.rdlc";
                this.sP_AmrsheraaR1TableAdapter1.Fill(this.ANRPC_Inventory_v2DataSet2.SP_AmrsheraaR1, Convert.ToInt32(Constants.AmrNo), Constants.AmrSanaMalya);
                //   this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { });
                this.reportViewer1.RefreshReport();//SP_AmrsheraaR1TableAdapter
            }
            else if (Constants.FormNo == 7)
            {
                //SP_TalbTawreedR1TableAdapter
                //SP_EznsarfR1TableAdapter
                reportViewer1.Reset();


                Microsoft.Reporting.WinForms.ReportDataSource RDC = new Microsoft.Reporting.WinForms.ReportDataSource();
                RDC.Name = "DataSet1";
                RDC.Value = this.SP_EznsarfR1BindingSource;
                this.reportViewer1.LocalReport.DataSources.Add(RDC);
                this.reportViewer1.LocalReport.EnableExternalImages = true;

                // TODO
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "ANRPC_Inventory.EznSarf_R1.rdlc";
                this.sP_EznsarfR1TableAdapter1.Fill(this.ANRPC_Inventory_v2DataSet5.SP_EznsarfR1, Convert.ToInt32(Constants.EznNo), Constants.EznFY);
                //   this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { });
                this.reportViewer1.RefreshReport();//SP_AmrsheraaR1TableAdapter
            }
            else if (Constants.FormNo == 8)
            {
                reportViewer1.Reset();


                Microsoft.Reporting.WinForms.ReportDataSource RDC = new Microsoft.Reporting.WinForms.ReportDataSource();
                RDC.Name = "DataSet1";
                RDC.Value = this.SP_TalbTawreedR1BindingSource;
                this.reportViewer1.LocalReport.DataSources.Add(RDC);
                this.reportViewer1.LocalReport.EnableExternalImages = true;

                // TODO
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "ANRPC_Inventory.TalbTawreed_R1.rdlc";
                this.sP_TalbTawreedR1TableAdapter1.Fill(this.ANRPC_Inventory_v2DataSet.SP_TalbTawreedR1, Convert.ToInt32(Constants.TalbNo), Constants.TalbFY);
                //   this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { });
                this.reportViewer1.RefreshReport();//SP_AmrsheraaR1TableAdapter
            }
            else if (Constants.FormNo == 88)
            {
                reportViewer1.Reset();


                Microsoft.Reporting.WinForms.ReportDataSource RDC = new Microsoft.Reporting.WinForms.ReportDataSource();
                RDC.Name = "DataSet1";
                RDC.Value = this.SP_TalbTawreedR1BindingSource;
                this.reportViewer1.LocalReport.DataSources.Add(RDC);
                this.reportViewer1.LocalReport.EnableExternalImages = true;
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "ANRPC_Inventory.TalbTawreed_R1.rdlc";
                Byte[] mybytes = reportViewer1.LocalReport.Render("PDF");
                //Byte[] mybytes = report.Render("PDF"); for exporting to PDF
                using (FileStream fs = File.Create(@"D:\SalSlip.pdf"))
                {
                    fs.Write(mybytes, 0, mybytes.Length);
                }
            }
            else if (Constants.FormNo == 9)
            {
                reportViewer1.Reset();
                ReportParameter rp1 = new ReportParameter("p1", Constants.STockBian);
                ReportParameter rp2 = new ReportParameter("p2", Constants.STockno);
                ReportParameter rp3 = new ReportParameter("p3", Constants.STockmin);
                ReportParameter rp4 = new ReportParameter("p4", Constants.stockmax);
                ReportParameter rp5 = new ReportParameter("p5", Constants.Stocklocation);
                ReportParameter rp6 = new ReportParameter("p6", Constants.Stockunit);
                ReportParameter rp7 = new ReportParameter("p7", Constants.STockNoALL);

                Microsoft.Reporting.WinForms.ReportDataSource RDC = new Microsoft.Reporting.WinForms.ReportDataSource();
                RDC.Name = "DataSet1";
                RDC.Value = this.SP_SearchTasnifTransBindingSource;
                this.reportViewer1.LocalReport.DataSources.Add(RDC);
                this.reportViewer1.LocalReport.EnableExternalImages = true;

                // TODO
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "ANRPC_Inventory.SanfID_R1.rdlc";
                this.sP_SearchTasnifTransTableAdapter1.Fill(this.ANRPC_Inventory_v2DataSet5.SP_SearchTasnifTrans, Constants.STockNoALL);
                this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { rp1, rp2, rp3, rp4, rp5, rp6, rp7 });
               
                //   this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { });
                this.reportViewer1.RefreshReport();//SP_AmrsheraaR1TableAdapter
            }
            else if (Constants.FormNo == 10)
            {
                reportViewer1.Reset();


                Microsoft.Reporting.WinForms.ReportDataSource RDC = new Microsoft.Reporting.WinForms.ReportDataSource();
                RDC.Name = "DataSet1";
                RDC.Value = this.SP_ReportSafeAmountBindingSource;
                this.reportViewer1.LocalReport.DataSources.Add(RDC);
                this.reportViewer1.LocalReport.EnableExternalImages = true;

                // TODO
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "ANRPC_Inventory.R_SafeAmount.rdlc";
                this.SP_ReportSafeAmountTableAdapter.Fill(this.ANRPC_Inventory_v2DataSet1.SP_ReportSafeAmount);
                //   this.reportViewer1.LocalReport.SetParameters(new ReportParameter[] { });
                this.reportViewer1.RefreshReport();//SP_AmrsheraaR1TableAdapter
            }
            else if (Constants.FormNo == 11)
            {
                reportViewer1.Reset();


                Microsoft.Reporting.WinForms.ReportDataSource RDC = new Microsoft.Reporting.WinForms.ReportDataSource();
                RDC.Name = "DataSet1";
                RDC.Value = this.T_MaxQuanBindingSource;
                this.reportViewer1.LocalReport.DataSources.Add(RDC);
                this.reportViewer1.LocalReport.EnableExternalImages = true;

                // TODO
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "ANRPC_Inventory.R_MaxQuan.rdlc";
                this.T_MaxQuanTableAdapter.Fill(this.ANRPC_Inventory_v2DataSet1.T_MaxQuan);
                this.reportViewer1.RefreshReport();//SP_AmrsheraaR1TableAdapter
            }
            else if (Constants.FormNo == 12)
            {
                reportViewer1.Reset();


                Microsoft.Reporting.WinForms.ReportDataSource RDC = new Microsoft.Reporting.WinForms.ReportDataSource();
                RDC.Name = "DataSet1";
                RDC.Value = this.T_MinQuanBindingSource;
                this.reportViewer1.LocalReport.DataSources.Add(RDC);
                this.reportViewer1.LocalReport.EnableExternalImages = true;

                // TODO
                this.reportViewer1.LocalReport.ReportEmbeddedResource = "ANRPC_Inventory.R_MinQuan.rdlc";
                this.T_MinQuanTableAdapter.Fill(this.ANRPC_Inventory_v2DataSet5.T_MinQuan);
                this.reportViewer1.RefreshReport();//SP_AmrsheraaR1TableAdapter
            }
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
