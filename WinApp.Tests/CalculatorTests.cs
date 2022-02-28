using System;
using System.Threading;
using FluentAssertions;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;

namespace WinApp.Tests;

public class CalculatorTests
{
    private WindowsDriver<WindowsElement> WindowsDriver;
    [SetUp]
    public void Setup()
    {
        var appiumOptions = new AppiumOptions(){};
        // appiumOptions.AddAdditionalCapability("");
        appiumOptions.AddAdditionalCapability("app", "Microsoft.WindowsCalculator_8wekyb3d8bbwe!App");
        WindowsDriver = new WindowsDriver<WindowsElement>(new Uri("http://127.0.0.1:4723"), appiumOptions);
        
        WindowsDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
        WindowsDriver.Manage().Window.Maximize();
    }

    [Test]
    public void CalculatorTest()
    {
        WindowsDriver.Keyboard.SendKeys("2");
        WindowsDriver.Keyboard.SendKeys("+");
        WindowsDriver.Keyboard.SendKeys("3");
        WindowsDriver.Keyboard.SendKeys("=");
        WindowsDriver.FindElementByAccessibilityId("CalculatorResults")
            .Text
            .Should()
            .Be("Display is 5");
    }

    [TearDown]
    public void TearDown()
    {
        WindowsDriver.Quit();
        WindowsDriver.Dispose();
    }
}