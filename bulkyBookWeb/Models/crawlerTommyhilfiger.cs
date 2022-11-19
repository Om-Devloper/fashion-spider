using Grpc.Core;
using HtmlAgilityPack;
using Microsoft.Data.SqlClient;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace bulkyBookWeb.Models
{
    public class tommyHilfiger
    {
        public static void tommyhilfiger_Data_Process(int TotalData, int UrlId)
        {
            string connetionString;
            SqlConnection cnn;
            connetionString = @"Data Source=LAPTOP-3RGNJ53I\SQLEXPRESS;Database= Bulky;Trusted_Connection = True;";
            cnn = new SqlConnection(connetionString);
            cnn.Open();
            var newData = new List<AllData_Fields>();
            AllData_Fields fields = new AllData_Fields();
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;
            var chromeOptions = new ChromeOptions();
            IWebDriver m_driver;
            m_driver = new ChromeDriver(service, chromeOptions);
            m_driver.Url = "https://tommyhilfiger.nnnow.com/th-aw22?gender=Men";
            try
            {
                for (int i = 0; i < 5; i++)//25
                {
                    ((IJavaScriptExecutor)m_driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight - 150)");
                    Thread.Sleep(10000);
                }
                var pageSource = m_driver.PageSource;
                HtmlAgilityPack.HtmlDocument Doc = new HtmlAgilityPack.HtmlDocument();
                Doc.LoadHtml(pageSource);
                var xpath = Doc.DocumentNode.SelectNodes("//a[@class='nw-productview nwc-anchortag']");
                foreach (var item in xpath)
                {
                    var Image = item.SelectNodes("div[@class='nwc-hide'][2]").FirstOrDefault().InnerText;
                    if (Image != null)
                    {
                        fields.imageUrl = Image;
                    }
                    try
                    {
                        fields.productDetail = item.SelectNodes("div//div[@class='nw-productview-producttitle']").FirstOrDefault().InnerText;

                    }
                    catch (Exception)
                    {

                    }
                    try
                    {
                        fields.productValue = item.SelectNodes("div//div[@class= 'nw-priceblock-container']").FirstOrDefault().InnerText.Trim();

                    }
                    catch (Exception)
                    {

                    }
                    try
                    {
                        fields.productUrl = $"https://tommyhilfiger.nnnow.com{item.Attributes["href"].Value}";

                    }
                    catch (Exception)
                    {

                    }
                    try
                    {
                        fields.SystemId = item.Attributes["href"].Value.TrimEnd().Split("-").LastOrDefault();

                    }
                    catch (Exception)
                    {

                    }
                    SqlCommand command;
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    String sql = ($"insert into productDataTable (RefId,SystemId,productDetail,productUrl,companyName,productValue,imageUrl) values('{UrlId}','{fields.SystemId}','{fields.productDetail}','{fields.productUrl}','{"Tommy Hilfiger"}','{fields.productValue}','{fields.imageUrl}')");

                    command = new SqlCommand(sql, cnn);

                    adapter.InsertCommand = new SqlCommand(sql, cnn);
                    adapter.InsertCommand.ExecuteNonQuery();

                }
            }
            catch (Exception e)
            {

            }

        }
    }

}