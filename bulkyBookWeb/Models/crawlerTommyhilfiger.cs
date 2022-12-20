using ConnectionLibrary;
using Microsoft.Data.SqlClient;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Data;
namespace bulkyBookWeb.Models
{
    public class tommyHilfiger
    {
        public static ConnectionClass Con = new ConnectionClass();
        public static void tommyhilfiger_Data_Process(int TotalData, int UrlId)
        {

            //SqlConnection con = null;


            //string connetionString;
            //connetionString = @"Data Source=LAPTOP-3RGNJ53I\SQLEXPRESS;Database= Bulky;Trusted_Connection = True;";
            //con = new SqlConnection(connetionString);


            //con.Open();

            //productValue
            /*SqlCommand value = new SqlCommand("SELECT productValue FROM [productDataTable]", cnn);
            SqlDataAdapter daValue = new SqlDataAdapter(value);
            daValue.Fill(dt);
            var productValue = dt.Rows;*/

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
                for (int i = 0; i < 2; i++)//25
                {
                    ((IJavaScriptExecutor)m_driver).ExecuteScript("window.scrollTo(0, document.body.scrollHeight - 150)");
                    Thread.Sleep(3000);
                }
                var pageSource = m_driver.PageSource;
                HtmlAgilityPack.HtmlDocument Doc = new HtmlAgilityPack.HtmlDocument();
                Doc.LoadHtml(pageSource);
                var xpath = Doc.DocumentNode.SelectNodes("//a[@class='nw-productview nwc-anchortag']");
                foreach (var item in xpath)
                {
                    fields.SystemId = item.Attributes["href"].Value.TrimEnd().Split("-").LastOrDefault();

                    var DuplicateCheck = Con.Select("select Count(1) from productDataTable  where SystemId= '" + fields.SystemId + "' and RefId='" + UrlId + "'");
                    if (DuplicateCheck != null)
                    {
                        if (DuplicateCheck.Rows[0][0].ToString() == "0")
                        {
                            var Image = item.SelectNodes("div[@class='nwc-hide'][2]").FirstOrDefault().InnerText;
                            if (Image != null)
                            {
                                fields.imageUrl = Image;
                            }

                            fields.productDetail = item.SelectNodes("div//div[@class='nw-productview-producttitle']").FirstOrDefault().InnerText;

                            fields.productValue = item.SelectNodes("div//div[@class= 'nw-priceblock-container']").FirstOrDefault().InnerText.Trim();

                            fields.productUrl = $"https://tommyhilfiger.nnnow.com{item.Attributes["href"].Value}";


                            var AddTODataTable = Con.InsertNew($"insert into productDataTable (RefId,SystemId,productDetail,productUrl,companyName,productValue,imageUrl) values('{UrlId}','{fields.SystemId}','{fields.productDetail}','{fields.productUrl}','{"Tommy Hilfiger"}','{fields.productValue}','{fields.imageUrl}')");
                        }
                        else
                        {

                        }
                        
                        //SqlCommand command;
                        //SqlDataAdapter adapter = new SqlDataAdapter();
                        //String sql = ($"insert into productDataTable (RefId,SystemId,productDetail,productUrl,companyName,productValue,imageUrl) values('{UrlId}','{fields.SystemId}','{fields.productDetail}','{fields.productUrl}','{"Tommy Hilfiger"}','{fields.productValue}','{fields.imageUrl}')");

                        //command = new SqlCommand(sql, con);

                        //adapter.InsertCommand = new SqlCommand(sql, con);
                        //adapter.InsertCommand.ExecuteNonQuery();

                    }
                    else
                    {

                    }
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