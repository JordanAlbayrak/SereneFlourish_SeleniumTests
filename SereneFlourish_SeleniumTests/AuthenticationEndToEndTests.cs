﻿using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Xunit;

namespace SereneFlourish_SeleniumTests
{
    public class AuthenticationEndToEndTests
    {
        private readonly EdgeDriver _driver = new();
        private readonly TimeSpan _time = TimeSpan.FromSeconds(5);

        [Fact]
        //TC9-TSE01
        public void Login_Should_Be_Successful()
        {
            var wait = new WebDriverWait(_driver, _time);

            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            _driver.Manage().Window.Maximize();

            _driver.Url = "http://localhost:3000/admin/login";

            // Enter username
            _driver.FindElement(By.Id("username")).SendKeys("admin");
            _driver.FindElement(By.Id("password")).SendKeys("admin");

            // click login button
            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();
            
            //check if we are at localhost:3000
            wait.Until(ExpectedConditions.UrlContains("localhost:3000"));
            
            _driver.Quit();
            
        }

        [Fact]
        //TC9-TSE02
        public void Login_Should_Fail()
        {
            var wait = new WebDriverWait(_driver, _time);

            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            _driver.Manage().Window.Maximize();

            _driver.Url = "http://localhost:3000/admin/login";

            // Enter username
            _driver.FindElement(By.Id("username")).SendKeys("admin");
            _driver.FindElement(By.Id("password")).SendKeys("wrong");

            // click login button
            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            //check if an alert is shown
            wait.Until(ExpectedConditions.AlertIsPresent());
            
            _driver.Quit();
        }
        
        [Fact]
        //TC9-TSE03
        public void Login_Should_Fail_With_Empty_Fields()
        {
            var wait = new WebDriverWait(_driver, _time);

            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            _driver.Manage().Window.Maximize();

            _driver.Url = "http://localhost:3000/admin/login";

            // Enter username
            _driver.FindElement(By.Id("username")).SendKeys("");
            _driver.FindElement(By.Id("password")).SendKeys("");

            // click login button
            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            //check if an alert is shown
            wait.Until(ExpectedConditions.AlertIsPresent());
            
            _driver.Quit();
        }
        
        [Fact]
        //TC9-TSE04
        public void Login_Should_Fail_With_Username()
        {
            var wait = new WebDriverWait(_driver, _time);

            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            _driver.Manage().Window.Maximize();

            _driver.Url = "http://localhost:3000/admin/login";

            // Enter username
            _driver.FindElement(By.Id("username")).SendKeys("");
            _driver.FindElement(By.Id("password")).SendKeys("admin");

            // click login button
            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            //check if an alert is shown
            wait.Until(ExpectedConditions.AlertIsPresent());
            
            _driver.Quit();
        }
        
        //TC9-TSE05
        [Fact]
        public void Login_Should_Fail_With_Password()
        {
            var wait = new WebDriverWait(_driver, _time);

            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

            _driver.Manage().Window.Maximize();

            _driver.Url = "http://localhost:3000/admin/login";

            // Enter username
            _driver.FindElement(By.Id("username")).SendKeys("admin");
            _driver.FindElement(By.Id("password")).SendKeys("");

            // click login button
            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            //check if an alert is shown
            wait.Until(ExpectedConditions.AlertIsPresent());
            
            _driver.Quit();
        }
        
    }
}