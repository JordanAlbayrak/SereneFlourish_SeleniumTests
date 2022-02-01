using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium.Edge;
using Xunit;

namespace SereneFlourish_SeleniumTests
{
    public class ImageEndToEndTests
    {
        private readonly EdgeDriver _driver;
        private readonly string _baseUrl;
        private readonly string _projectRoot;

        public ImageEndToEndTests()
        {
            _baseUrl = "http://localhost:3000";
            _driver = new EdgeDriver();
            _projectRoot = Path.GetFullPath(@"..\..\..\");
        }

        //TC5-TSE01
        [Fact]
        public void Image_addImageAtLocationOne_ReturnOk()
        {
            Login();
            _driver.Navigate().GoToUrl(_baseUrl + "/admin/dashboard/portfolio");
            //click on image link with href /admin/portfolio/image/1
            _driver.FindElement(By.XPath("/html/body/div/div/div/div/div/div/div/div/div/div[1]/a[1]")).Click();
            _driver.FindElement(By.Id("imageTitle")).SendKeys("Test Image");
            //click on upload button class btn and btn-primary
            IWebElement chooseFile = _driver.FindElement(By.XPath("//*[@id=\"image\"]"));
            chooseFile.SendKeys(_projectRoot + @"Images\Calligraphy.jpg");
            //click the upload button
            _driver.FindElement(By.XPath("/html/body/div/div/div/div/div[1]/div/p/form/button")).Click();
            _driver.Quit();
        }

        //TC5-TSE02
        [Fact]
        public void Image_addImageAtLocationTwo_ReturnOk()
        {
            Login();
            _driver.Navigate().GoToUrl(_baseUrl + "/admin/dashboard/portfolio");
            //click on image link with href /admin/portfolio/image/2
            _driver.FindElement(By.XPath("/html/body/div/div/div/div/div/div/div/div/div/div[1]/a[2]/img")).Click();
            _driver.FindElement(By.Id("imageTitle")).SendKeys("Test Image");
            //click on upload button class btn and btn-primary
            IWebElement chooseFile = _driver.FindElement(By.XPath("//*[@id=\"image\"]"));
            chooseFile.SendKeys(_projectRoot + @"Images\Calligraphy.jpg");
            //click the upload button
            _driver.FindElement(By.XPath("/html/body/div/div/div/div/div[1]/div/p/form/button")).Click();
            _driver.Quit();
        }

        //TC5-TSE03
        [Fact]
        public void Image_addImageAtLocationThree_ReturnOk()
        {
            Login();
            _driver.Navigate().GoToUrl(_baseUrl + "/admin/dashboard/portfolio");
            //click on image link with href /admin/portfolio/image/3
            _driver.FindElement(By.XPath("/html/body/div/div/div/div/div/div/div/div/div/div[2]/a[1]")).Click();
            _driver.FindElement(By.Id("imageTitle")).SendKeys("Test Image");
            //click on upload button class btn and btn-primary
            IWebElement chooseFile = _driver.FindElement(By.XPath("//*[@id=\"image\"]"));
            chooseFile.SendKeys(_projectRoot + @"Images\Calligraphy.jpg");
            //click the upload button
            _driver.FindElement(By.XPath("/html/body/div/div/div/div/div[1]/div/p/form/button")).Click();
            _driver.Quit();
        }

        //TC5-TSE04
        [Fact]
        public void Image_addImageAtLocationFour_ReturnOk()
        {
            Login();
            _driver.Navigate().GoToUrl(_baseUrl + "/admin/dashboard/portfolio");
            //click on image link with href /admin/portfolio/image/4
            _driver.FindElement(By.XPath("/html/body/div/div/div/div/div/div/div/div/div/div[2]/a[2]")).Click();
            _driver.FindElement(By.Id("imageTitle")).SendKeys("Test Image");
            //click on upload button class btn and btn-primary
            IWebElement chooseFile = _driver.FindElement(By.XPath("//*[@id=\"image\"]"));
            chooseFile.SendKeys(_projectRoot + @"Images\Calligraphy.jpg");
            //click the upload button
            _driver.FindElement(By.XPath("/html/body/div/div/div/div/div[1]/div/p/form/button")).Click();
            _driver.Quit();
        }

        [Fact]
        public void Image_addImageAtLocationFive_ReturnOk()
        {
            Login();
            _driver.Navigate().GoToUrl(_baseUrl + "/admin/dashboard/portfolio");
            //click on image link with href /admin/portfolio/image/5
            _driver.FindElement(By.XPath("/html/body/div/div/div/div/div/div/div/div/div/div[3]/a[1]/img")).Click();
            _driver.FindElement(By.Id("imageTitle")).SendKeys("Test Image");
            //click on upload button class btn and btn-primary
            IWebElement chooseFile = _driver.FindElement(By.XPath("//*[@id=\"image\"]"));
            chooseFile.SendKeys(_projectRoot + @"Images\Calligraphy.jpg");
            //click the upload button
            _driver.FindElement(By.XPath("/html/body/div/div/div/div/div[1]/div/p/form/button")).Click();
            _driver.Quit();
        }

        //TC5-TSE05
        [Fact]
        public void Image_addImageAtLocationSix_ReturnOk()
        {
            Login();
            _driver.Navigate().GoToUrl(_baseUrl + "/admin/dashboard/portfolio");
            //click on image link with href /admin/portfolio/image/6
            _driver.FindElement(By.XPath("/html/body/div/div/div/div/div/div/div/div/div/div[3]/a[2]/img")).Click();
            _driver.FindElement(By.Id("imageTitle")).SendKeys("Test Image");
            //click on upload button class btn and btn-primary
            IWebElement chooseFile = _driver.FindElement(By.XPath("//*[@id=\"image\"]"));
            chooseFile.SendKeys(_projectRoot + @"Images\Calligraphy.jpg");
            //click the upload button
            _driver.FindElement(By.XPath("/html/body/div/div/div/div/div[1]/div/p/form/button")).Click();
            _driver.Quit();
        }
        
        //TC5-TSE06
        [Fact]
        public void Image_AccessPortfolioPage()
        {
            Login();
            _driver.Navigate().GoToUrl(_baseUrl + "/admin/dashboard/portfolio");
            _driver.Quit();
        }
        
        private void Login()
        {
            _driver.Manage().Window.Maximize();

            _driver.Url = "http://localhost:3000/admin/login";

            // Enter username
            _driver.FindElement(By.Id("username")).SendKeys("admin");
            _driver.FindElement(By.Id("password")).SendKeys("admin");

            // click login button
            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            Thread.Sleep(3000);
        }
    }
}
