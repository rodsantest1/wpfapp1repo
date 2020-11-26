using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace UnitTestProject1
{
    [TestClass]
    public class AppiumTests : WpfAppSession
    {
        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            Setup();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            TearDown();
        }

        [TestMethod]
        public void Add2Numbers_test()
        {
            //arrange
            string expected = "4";
            string actual = "";
            //act
            //Make sure API is running somewhere
            var input1 = FindById("Input1");
            input1.SendKeys("3");
            var input2 = FindById("Input2");
            input2.SendKeys("1");

            Stopwatch sw = new Stopwatch();
            sw.Start();
            do
            {
                actual = FindById("Sum").Text;

                if (actual != "4" && sw.Elapsed.TotalSeconds < 10) continue; else break;

            } while (true);

            //assert
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        /// This demonstrates testing nested objects like finding something in
        /// a listbox
        /// </summary>
        [TestMethod]
        public void FindInListBox_test()
        {
            //arrange
            bool expected = true;
            bool actual = false;
            //act
            //Make sure API is running somewhere
            var listBox1 = FindById("ListBox1");
            var listBoxItem1 = FindAllByClassName("ListBoxItem", listBox1).FirstOrDefault();
            var x = FindAllByClassName("TextBlock", listBoxItem1).ToList();
            actual = x.Exists(x => x.Text.Contains("Hello World"));
            //assert
            Assert.AreEqual(expected, actual);
        }
    }
}