using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SereneFlourish_SeleniumTests
{
    public class ImageEndToEndTests
    {
        IWebDriver driver = new FirefoxDriver("C:\\Program Files\\Mozilla Firefox");
        TimeSpan time = TimeSpan.FromSeconds(5);
        private readonly string _baseUrl;
        string projectRoot = Path.GetFullPath(@"..\..\..\");

        public ImageEndToEndTests()
        {
            _baseUrl = "http://localhost:3000";
            driver = new FirefoxDriver("C:\\Program Files\\Mozilla Firefox");
        }

        [Fact]
        public void Image_addImageAtLocationOne_ReturnOk()
        {
            driver.Navigate().GoToUrl(_baseUrl + "/admin");
            //click on image link with href /admin/portfolio/image/1
            driver.FindElement(By.XPath("/html/body/div/div/div/div/div/div/div/div/div/div[1]/a[1]")).Click();
            driver.FindElement(By.Id("imageTitle")).SendKeys("Test Image");
            //click on upload button class btn and btn-primary
            IWebElement chooseFile = driver.FindElement(By.XPath("//*[@id=\"image\"]"));
            chooseFile.SendKeys(projectRoot + @"Images\Calligraphy.jpg");
            //click the upload button
            driver.FindElement(By.XPath("/html/body/div/div/div/div/div[1]/div/p/form/button")).Click();
            driver.Quit();
        }

        [Fact]

        public void Image_addImageAtLocationTwo_ReturnOk()
        {
            driver.Navigate().GoToUrl(_baseUrl + "/admin");
            //click on image link with href /admin/portfolio/image/2
            driver.FindElement(By.XPath("/html/body/div/div/div/div/div/div/div/div/div/div[1]/a[2]/img")).Click();
            driver.FindElement(By.Id("imageTitle")).SendKeys("Test Image");
            //click on upload button class btn and btn-primary
            IWebElement chooseFile = driver.FindElement(By.XPath("//*[@id=\"image\"]"));
            chooseFile.SendKeys(projectRoot + @"Images\Calligraphy.jpg");
            //click the upload button
            driver.FindElement(By.XPath("/html/body/div/div/div/div/div[1]/div/p/form/button")).Click();
            driver.Quit();
        }

        [Fact]
        public void Image_addImageAtLocationThree_ReturnOk()
        {
            driver.Navigate().GoToUrl(_baseUrl + "/admin");
            //click on image link with href /admin/portfolio/image/3
            driver.FindElement(By.XPath("/html/body/div/div/div/div/div/div/div/div/div/div[2]/a[1]")).Click();
            driver.FindElement(By.Id("imageTitle")).SendKeys("Test Image");
            //click on upload button class btn and btn-primary
            IWebElement chooseFile = driver.FindElement(By.XPath("//*[@id=\"image\"]"));
            chooseFile.SendKeys(projectRoot + @"Images\Calligraphy.jpg");
            //click the upload button
            driver.FindElement(By.XPath("/html/body/div/div/div/div/div[1]/div/p/form/button")).Click();
            driver.Quit();
        }

        [Fact]
        public void Image_addImageAtLocationFour_ReturnOk()
        {
            driver.Navigate().GoToUrl(_baseUrl + "/admin");
            //click on image link with href /admin/portfolio/image/4
            driver.FindElement(By.XPath("/html/body/div/div/div/div/div/div/div/div/div/div[2]/a[2]")).Click();
            driver.FindElement(By.Id("imageTitle")).SendKeys("Test Image");
            //click on upload button class btn and btn-primary
            IWebElement chooseFile = driver.FindElement(By.XPath("//*[@id=\"image\"]"));
            chooseFile.SendKeys(projectRoot + @"Images\Calligraphy.jpg");
            //click the upload button
            driver.FindElement(By.XPath("/html/body/div/div/div/div/div[1]/div/p/form/button")).Click();
            driver.Quit();
        }

        [Fact]
        public void Image_addImageAtLocationFive_ReturnOk()
        {
            driver.Navigate().GoToUrl(_baseUrl + "/admin");
            //click on image link with href /admin/portfolio/image/5
            driver.FindElement(By.XPath("/html/body/div/div/div/div/div/div/div/div/div/div[3]/a[1]/img")).Click();
            driver.FindElement(By.Id("imageTitle")).SendKeys("Test Image");
            //click on upload button class btn and btn-primary
            IWebElement chooseFile = driver.FindElement(By.XPath("//*[@id=\"image\"]"));
            chooseFile.SendKeys(projectRoot + @"Images\Calligraphy.jpg");
            //click the upload button
            driver.FindElement(By.XPath("/html/body/div/div/div/div/div[1]/div/p/form/button")).Click();
            driver.Quit();
        }

        [Fact]
        public void Image_addImageAtLocationSix_ReturnOk()
        {
            driver.Navigate().GoToUrl(_baseUrl + "/admin");
            //click on image link with href /admin/portfolio/image/6
            driver.FindElement(By.XPath("/html/body/div/div/div/div/div/div/div/div/div/div[3]/a[2]/img")).Click();
            driver.FindElement(By.Id("imageTitle")).SendKeys("Test Image");
            //click on upload button class btn and btn-primary
            IWebElement chooseFile = driver.FindElement(By.XPath("//*[@id=\"image\"]"));
            chooseFile.SendKeys(projectRoot + @"Images\Calligraphy.jpg");
            //click the upload button
            driver.FindElement(By.XPath("/html/body/div/div/div/div/div[1]/div/p/form/button")).Click();
            driver.Quit();
        }

        //TODO: Add tests for the following:
        //Test above need to be fully implemented
        //update image at location 1, don't need to do all of them since they are the same as the add tests

        //verify that the images are there
        [Fact]
        public void Image_AccessPortfolioPage()
        {
            driver.Navigate().GoToUrl(_baseUrl + "/portfolio");
            driver.Quit();
        }
    }
}
