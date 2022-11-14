using HtmlAgilityPack;
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
                for (int i = 0; i < 30; i++)
                {
                    ((IJavaScriptExecutor)m_driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight - 150)");
                    Thread.Sleep(10000);
                    Console.WriteLine(i);
                }
                var pageSource = m_driver.PageSource;
                HtmlAgilityPack.HtmlDocument Doc = new HtmlAgilityPack.HtmlDocument();
                Doc.LoadHtml(pageSource);
                var xpath = Doc.DocumentNode.SelectNodes("//a[@class='nw-productview nwc-anchortag']");
                foreach (var item in xpath)
                {
                    fields.imageUrl = item.SelectNodes("div//img[@class='nwc-lazyimg is-loaded']").LastOrDefault().Attributes["src"].Value;
                    fields.productDetail = item.SelectNodes("div//img[@class='nwc-lazyimg is-loaded']").LastOrDefault().Attributes["alt"].Value;
                    fields.productValue = item.SelectNodes("div//div[@class= 'nw-priceblock-container']").FirstOrDefault().InnerText.Trim();
                    fields.productUrl = $"https://tommyhilfiger.nnnow.com{item.Attributes["href"].Value}";
                    fields.SystemId = item.Attributes["href"].Value.TrimEnd().Split(" ").LastOrDefault();
                    newData.Add(new AllData_Fields { RefId = UrlId, imageUrl = fields.imageUrl, productDetail = fields.productDetail, productValue = fields.productValue, productUrl = fields.productUrl, SystemId = fields.SystemId, companyName= "Tommy Hilfiger" });


                }
            }
            catch (Exception e)
            {

            }

        }
    }

}