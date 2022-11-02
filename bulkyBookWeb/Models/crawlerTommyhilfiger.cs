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
                for (int i = 0; i < 50; i++)
                {
                    ((IJavaScriptExecutor)m_driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight - 150)");
                    Thread.Sleep(1000);
                }
                var Doc = m_driver.PageSource;
                HtmlDocument.LoadHtml(Doc);
            }
            catch (Exception e)
            {

            }

        }
    }

}