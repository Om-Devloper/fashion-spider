using ConnectionLibrary;
using Microsoft.Data.SqlClient;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Data;
namespace bulkyBookWeb.Models
{
    public class levisCrawler
    {
        public static ConnectionClass Con = new ConnectionClass();
        public static void levis_Data_Process(int TotalData, int UrlId)
        {
            var newData = new List<AllData_Fields>();
            AllData_Fields fields = new AllData_Fields();
            ChromeDriverService service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;
            var chromeOptions = new ChromeOptions();
            IWebDriver m_driver;
            m_driver = new ChromeDriver(service, chromeOptions);
            //m_driver.Url = "https://www.netflix.com/in/login";
            //m_driver.Url = "https://www.levi.in/end-of-season-sale?prefn1=gender&prefv1=Men&srule=Popularity%20-New ";
            Thread.Sleep(5000);

            try
            {
                for (int i = 1; i < 2; i++)//25
                {
                    var pageSource = m_driver.PageSource;
                    HtmlAgilityPack.HtmlDocument Doc = new HtmlAgilityPack.HtmlDocument();
                    Doc.LoadHtml(pageSource);
                    var xpath = Doc.DocumentNode.SelectNodes("//div[@class='product-tile']");
                    foreach (var item in xpath)
                    {
                        fields.SystemId = item.SelectSingleNode("div/a[@class='thumb-link']").Attributes["href"].Value.Split("/").LastOrDefault().Split(".").FirstOrDefault();
                        var DuplicateCheck = Con.Select("select Count(1) from productDataTable  where SystemId= '" + fields.SystemId + "' and RefId='" + UrlId + "'");
                        if (DuplicateCheck != null)
                        {
                            if (DuplicateCheck.Rows[0][0].ToString() == "0")
                            {
                                var Image = item.SelectSingleNode("div[@class='product-image']/a[@class='thumb-link']/img").Attributes["src"].Value;
                                if (Image != null)
                                {
                                    fields.imageUrl = Image;
                                }
                                else
                                {

                                }

                                fields.productDetail = item.SelectSingleNode("div[@class='product-image']/a[@class='thumb-link']/img").Attributes["alt"].Value.Replace("'","");

                                fields.productValue = item.SelectNodes("div/span[@class='product-sales-price']/span[@class= 'pricevalue']").FirstOrDefault().InnerText.Trim();

                                fields.productUrl = $"https://levi.in{item.SelectSingleNode("div/a[@class='thumb-link']").Attributes["href"].Value}";

                                var AddTODataTable = Con.InsertNew($"insert into productDataTable (RefId,SystemId,productDetail,productUrl,companyName,productValue,imageUrl) values('{UrlId}','{fields.SystemId}','{fields.productDetail}','{fields.productUrl}','{"Tommy Hilfiger"}','{fields.productValue}','{fields.imageUrl}')");
                            }
                            else
                            {

                            }
                        }
                        else
                        {
                        }
                    }
                    var link = m_driver.FindElement(By.XPath($"//div[@class = 'pagenumber']//a[@class= 'page-{i + 1} storepage jsorderpage']")).GetAttribute("href");
                    m_driver.Url = link;
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = System.ConsoleColor.Red;
                Console.WriteLine($"{ex.Message}{DateTime.Now} {Environment.NewLine}");
            }
        }
    }
}