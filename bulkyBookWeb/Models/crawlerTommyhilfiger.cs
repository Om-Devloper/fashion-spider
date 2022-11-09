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
                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(pageSource);
                var xpath = doc.DocumentNode.SelectNodes("//a[@class='nw-productview nwc-anchortag']");
                foreach (var item in xpath)
                {
                    var img = item.SelectNodes("div//img[@class='nwc-lazyimg is-loaded']").LastOrDefault().Attributes["src"].Value;
                    var productDetails = item.SelectNodes("div//img[@class='nwc-lazyimg is-loaded']").LastOrDefault().Attributes["alt"].Value;
                    var price = item.SelectNodes("div//div[@class= 'nw-priceblock-container']").FirstOrDefault().InnerText.Trim();
                }

            }
            catch (Exception e)
            {

            }

        }
    }

}