﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Remote;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Appium;
using System.Diagnostics;
using OpenQA.Selenium.Support.UI;

namespace UnitTestProject1
{
    public abstract class WpfAppSession
    {
        protected const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723";
        //protected const string WindowsApplicationDriverUrl = "http://127.0.0.1:4723/wd/hub";
        protected static WindowsDriver<WindowsElement> session;
        protected static WebDriverWait wait;

        public static void Setup()
        {
            StartWinAppDriver();

            string configName = "";
#if DEBUG
            configName = "Debug";
#elif RELEASE
            configName = "Release";
#endif

            var app = Path.Combine(
                Directory.GetCurrentDirectory(),
                $@"..\..\..\..\WpfApp1\bin\{configName}", "WpfApp1.exe");

            if (session == null)
            {
                AppiumOptions caps = new AppiumOptions();
                caps.AddAdditionalCapability("app", app);
                caps.AddAdditionalCapability("deviceName", "WindowsPC");
                caps.AddAdditionalCapability("newCommandTimeout", 120);
                session = new WindowsDriver<WindowsElement>(new Uri(WindowsApplicationDriverUrl), caps);
                wait = new WebDriverWait(session, TimeSpan.FromSeconds(20));
                Assert.IsNotNull(session);
                Assert.IsNotNull(session.SessionId);
            }
        }

        public static void TearDown()
        {
            // Close the application and delete the session
            if (session != null)
            {
                session.Close();
                session.Quit();
                session = null;
            }

            StopWinAppDriver();

        }

        private static void StartWinAppDriver()
        {
            var processName = "WinAppDriver";

            if (!Process.GetProcessesByName(processName).Any())
            {
                // NOTE: Setup PATH environment variable to install directory for WinAppDriver
                // Default path is `C:\Program Files (x86)\Windows Application Driver`
                ProcessStartInfo processSettings = new ProcessStartInfo(processName)
                {
                    UseShellExecute = true,
                    //Verb = "runas",
                    WorkingDirectory = @"C:\Program Files (x86)\Windows Application Driver"
                };

                Process.Start(processSettings);
            }
        }

        /// <summary>
        /// Closes the running instance of WinAppDriver.exe
        /// </summary>
        private static void StopWinAppDriver()
        {
            var runningProcess = Process.GetProcessesByName("WinAppDriver").FirstOrDefault();
            runningProcess?.Kill();
        }

    }
}