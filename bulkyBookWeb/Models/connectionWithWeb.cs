using ConnectionLibrary;
using System.Data;

namespace bulkyBookWeb.Models.connectWithWeb
{

    public class connectionWithWeb
    {
        public static ConnectionClass Con = new ConnectionClass();
        public connectionWithWeb()
        {
        }
        public int Count()
        {
             var totalCount = Con.Select("SELECT * FROM [productDataTable]").Rows.Count;
            return totalCount;
        }
        public string productDetail(int i)
        {
            var printProduct = Con.Select("SELECT productDetail FROM [productDataTable]").Rows[i].ItemArray.FirstOrDefault().ToString();
            return printProduct;
        }
        public string productValue(int i)
        {
            var productValue = Con.Select("SELECT productValue FROM [productDataTable]").Rows[i].ItemArray.FirstOrDefault().ToString();
            return productValue;
        }
        public string imageUrl(int i)
        {
            var imageUrl = Con.Select("SELECT imageUrl FROM [productDataTable]").Rows[i].ItemArray.FirstOrDefault().ToString();
            return imageUrl;
        }
        public string productUrl(int i)
        {
            var productUrl = Con.Select("SELECT productUrl FROM [productDataTable]").Rows[i].ItemArray.FirstOrDefault().ToString();
            return productUrl;
        }

    }
}
