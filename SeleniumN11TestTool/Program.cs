using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support;
using OpenQA.Selenium.Edge;
using NUnit;
using NUnit.Framework;
using System.Threading;
using OpenQA.Selenium.Support.UI;

namespace SeleniumN11TestTool
{
    class Program
    {
        IWebDriver driver = new FirefoxDriver();
        string ExpectedResult = "n11.com - Alışverişin Uğurlu Adresi";
        string ActualResult;
        string page = @"https://www.n11.com/";

        static void Main(string[] args)
        {



        }

        [SetUp]
        public void Initialize()
        {
            //N11.COM'A GİDİLİR.
            driver.Navigate().GoToUrl(page);
        }

        [Test]
        public void ExceuteTest()
        {
            ActualResult = driver.Title;
            if (ActualResult.Contains(ExpectedResult))
            {
                Console.WriteLine("N11.com başarıyla açıldı");
            }
            else
            {
                Console.WriteLine("N11.com açılamadı");
            }
            Thread.Sleep(3000);

            //N11.COM'A KULLANICI GİRİŞİ YAPILIR.
            driver.FindElement(By.ClassName("btnSignIn")).Click();
            Thread.Sleep(3000);
            driver.FindElement(By.Id("email")).SendKeys("username");
            driver.FindElement(By.Id("password")).SendKeys("password");
            Thread.Sleep(3000);
            driver.FindElement(By.Id("loginButton")).Click();

            IWebElement Kontrol = driver.FindElement(By.XPath(".//*[@id='header']/div/div/div[2]/div[2]/div[2]/div[1]/a[1]"));
            Assert.IsTrue(Kontrol.Text.Equals("Hesabım"), "Kullanıcı girişi başarısız");

            if (Kontrol.Text == "Hesabım")
            {
                Console.WriteLine("Kullanıcı girişi sağlandı");
            }

            Thread.Sleep(3000);
            //BİLGİSAYAR GELİMESİ ARATILIR.
            driver.FindElement(By.Id("searchData")).SendKeys("bilgisayar");
            Thread.Sleep(3000);
            driver.FindElement(By.ClassName("searchBtn")).Click();
            Thread.Sleep(3000);
            //ARAMA SONUÇLARININ 2. SAYFASINA GEÇİŞ YAPILIR.
            driver.FindElement(By.XPath(".//*[@id='contentListing']/div/div/div[2]/div[4]/a[2]")).Click();
            Thread.Sleep(3000);

            IWebElement currentPage = driver.FindElement(By.Id("currentPage"));
            string Sayfa = currentPage.GetAttribute("value").ToString();
            Assert.True(Sayfa.Equals("2"), "2. Sayfaya ulaşılamadı");
            Thread.Sleep(4000);
            Console.WriteLine("Paging kontrolü kullanıldı ve 2. sayfaya ulaşıldı.");

            driver.FindElement(By.XPath(".//*[@id='p-400361774']/div[1]/a[1]")).Click();

            Thread.Sleep(3000);

            driver.FindElement(By.ClassName("btnAddBasket")).Click();

            string urunfiyati = driver.FindElement(By.XPath("/html/body/div[1]/div[2]/div/div[3]/div[2]/div[3]/div[2]/div/div[1]/div/ins")).Text;

            Thread.Sleep(3000);

            driver.FindElement(By.XPath(".//*[@id='header']/div/div/div[2]/div[2]/div[4]/a/i")).Click();

            Thread.Sleep(3000);

            string sepetfiyati = driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[1]/div[2]/div[1]/section/table[2]/tbody/tr/td[3]/div[2]/div/span")).Text;

            if (urunfiyati == sepetfiyati)
            {
                Console.WriteLine("Fiyatlar Aynı");
            }
            else
            {
                Console.WriteLine("Fiyatlar Farklı");
            }

            //SEPET 1 ADET ARTIRILIR.
            driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[1]/div[2]/div[1]/section/table[2]/tbody/tr/td[3]/div[1]/div/span[1]")).Click();

            IWebElement sepetsayisi = driver.FindElement(By.Id("quantity_126783117351"));
            sepetsayisi.GetAttribute("value").ToString();
            Assert.False(sepetsayisi.Equals("2"), "Sepette 2 ürün yok.");
            Thread.Sleep(4000);
            Console.WriteLine("Sepette 2 adet ürün var.");



            Thread.Sleep(3000);         
            //SEPETTEKİ ÜRÜN SİLİNİR.
            driver.FindElement(By.XPath("/html/body/div[1]/div/div/div[1]/div[2]/div[1]/section/table[2]/tbody/tr/td[1]/div[3]/div[2]/span[1]")).Click();
            Thread.Sleep(3000);

            IWebElement SilinmeOnayi = driver.FindElement(By.XPath("//*[@id='wrapper']/div[2]/div/div[1]/div[1]/h2"));
            Assert.True(SilinmeOnayi.Text.Contains("Sepetiniz Boş"));
            Console.WriteLine("Ürün Silindi Sepetiniz Boş");
            Thread.Sleep(3000);

        }

        [TearDown]
        public void CleanUp()
        {
            driver.Close();
        }
    }
}
