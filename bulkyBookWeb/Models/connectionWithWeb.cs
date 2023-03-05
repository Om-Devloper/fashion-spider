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
        public int Count(string refId)
        {
            var totalCount = Con.Select($"SELECT * FROM [productDataTable] where refId={refId}").Rows.Count;
            return totalCount;
        }
        public string productDetail(int i,string refId)
        {
            var printProduct = Con.Select($"SELECT productDetail FROM [productDataTable] where refId={refId}").Rows[i].ItemArray.FirstOrDefault().ToString();
            return printProduct;
        }
        public string productValue(int i, string refId)
        {
            var productValue = Con.Select($"SELECT productValue FROM [productDataTable] where refId={refId}").Rows[i].ItemArray.FirstOrDefault().ToString();
            return productValue;
        }
        public string imageUrl(int i, string refId)
        {
            var imageUrl = Con.Select($"SELECT imageUrl FROM [productDataTable] where refId={refId}").Rows[i].ItemArray.FirstOrDefault().ToString();
            return imageUrl;
        }
        public string productUrl(int i, string refId)
        {
            var productUrl = Con.Select($"SELECT productUrl FROM [productDataTable] where refId={refId}").Rows[i].ItemArray.FirstOrDefault().ToString();
            return productUrl;
        }

    }
}
