using System;
using System.IO;
using System.Threading;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace WinApp.Tests;

public class WindowsAppTest
{
    
    private WindowsDriver<WindowsElement> WindowsDriver;
    [SetUp]
    public void Setup()
    {
        var appiumOptions = new AppiumOptions(){};
        // appiumOptions.AddAdditionalCapability("");
        appiumOptions.AddAdditionalCapability(
            "app", 
            @"C:\Users\USER\RiderProjects\WinApp.Tests\WindowsApp\bin\Debug\WindowsApp.exe");
        WindowsDriver = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), appiumOptions);
        
        WindowsDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
        // WindowsDriver.Manage().Window.Maximize();
    }

    [Test]
    public void LoginTest()
    {
        // KeyIn Account
        var accountElement = WindowsDriver.FindElementByAccessibilityId("txtAccount");
        accountElement.Click();
        accountElement.SendKeys("Patrick");
        var account = accountElement.Text;

        // KeyIn Password
        var passwordElement = WindowsDriver.FindElementByAccessibilityId("txtPassword");
        passwordElement.Click();
        passwordElement.SendKeys("P@ssw0rd");
        var password = passwordElement.Text;

        // Click Login Button
        var loginButton = WindowsDriver.FindElementByAccessibilityId("btnLogin");
        loginButton.Click();
        
        // Click Dialog OK
        var dialog = WindowsDriver.FindElementByClassName("#32770");
        var dialogText = dialog.FindElementByAccessibilityId("65535").Text;
        dialog.FindElementByAccessibilityId("2")
            .Click();

        // Assert
        account.Should().Be("Patrick");
        password.Should().Be("P@ssw0rd");
        dialogText.Should().Be("Patrick, P@ssw0rd");
    }

    [TearDown]
    public void TearDown()
    {
        WindowsDriver.Quit();
        WindowsDriver.Dispose();
    }
}