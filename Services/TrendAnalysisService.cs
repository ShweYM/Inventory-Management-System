using Inventory_Management_System.DB;
using Inventory_Management_System.Models;
using Inventory_Management_System.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace Inventory_Management_System.Services
{
    public class TrendAnalysisService
    {
        private InventoryManagementSystemContext db;


        public TrendAnalysisService(InventoryManagementSystemContext db)
        {
            this.db = db;
        }
        public TrendAnalysisChartViewModel getTrend()
        {
            //TODO: write algorithm to retrieve the historical inventory trend from DB and return in the form of TrendAnalysisChartViewMOdel
            List<int> data1 = new List<int>()
            { 5, 3, 4, 7, 2 };

            //List<string> categroy = new List<string>()
            //    { "Puncher","Paper","Scissor","Clip","Exercise"};

            List<string> categroy = new List<string>();




            List<RequisitionFormsProduct> rfpList = getRequisitionsProducts();
            List<RequisitionForm> rfList = getRequisitions();
            List<Product> plist = getProduct();
            List<ProductCategory> pcList = getProductCat();

            List<TrendAnalysisChartDetails> taList = new List<TrendAnalysisChartDetails>();

            int[][] quantity = new int[13][];
            for (int k = 0; k <= 12; k++)
            {
                quantity[k] = new int[19];
            }

            foreach (RequisitionFormsProduct rfp in rfpList)
            {
                Product _p = db.Products.Find(rfp.Product.Id);
                RequisitionForm _rf = db.RequisitionForms.Find(rfp.RequisitionForm.Id);
                ProductCategory _pc = db.ProductCategories.Find(_p.ProductCategory.Id);


                for (int i = 0; i <= 12; i++)
                {

                    if (_rf.RFDate.Month == i)
                    {
                        for (int j = 0; j <= 18; j++)
                        {
                            if (_p.ProductCategory.Id == j)
                            {
                                quantity[i][j] = quantity[i][j] + rfp.ProductApproved;
                            }
                        }
                    }
                }
            }
            categroy.Add("");

            foreach (ProductCategory pc in pcList)
            {
                categroy.Add(pc.ProductCategoryName);
            }

            TrendAnalysisChartViewModel vm = new TrendAnalysisChartViewModel();
            vm.category = categroy;

            string[] month = { "", "Jan", "Feb", "Mar", "April", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };


            List<TrendAnalysisChartDetails> trendDetails = new List<TrendAnalysisChartDetails>();

            for (int i = 1; i <= 12; i++)
            {
                TrendAnalysisChartDetails newDetail = new TrendAnalysisChartDetails();
                newDetail.name = month[i];
                newDetail.data = quantity[i].ToList();
                trendDetails.Add(newDetail);
            }

            vm.data = trendDetails;

            return vm;
        }
        public List<RequisitionFormsProduct> getRequisitionsProducts()
        {
            List<RequisitionFormsProduct> rfpList = db.RequisitionFormsProducts
                .ToList();
            return rfpList;
        }

        public List<RequisitionForm> getRequisitions()
        {
            List<RequisitionForm> rfList = db.RequisitionForms
                .OrderBy(x => x.RFDate)
                .Where(x => x.RFStatus == Enums.RFStatus.Approved || x.RFStatus == Enums.RFStatus.Completed || x.RFStatus == Enums.RFStatus.Ongoing || x.RFStatus == Enums.RFStatus.NotCompleted)
                .ToList();
            return rfList;
        }
        public List<Product> getProduct()
        {
            List<Product> pList = db.Products
                .OrderBy(x => x.Id)
                .ToList();
            return pList;
        }

        public List<ProductCategory> getProductCat()
        {
            List<ProductCategory> pcList = db.ProductCategories
                .ToList();
            return pcList;
        }


    }
}
