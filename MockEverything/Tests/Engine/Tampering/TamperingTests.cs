﻿namespace MockEverythingTests.Engine
{
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Net;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MockEverything.Engine.Browsers;
    using MockEverything.Engine.Tampering;
    using MockEverything.Inspection;
    using MockEverything.Inspection.MonoCecil;
    using NetProxies;

    [TestClass]
    public class TamperingTests
    {
        [TestMethod]
        [TestCategory("System tests")]
        public void TestTampering()
        {
            var testsPath = Path.GetDirectoryName(Path.GetDirectoryName(Path.GetDirectoryName(this.CurrentPath)));
            var readFileDirectoryPath = Path.Combine(testsPath, @"DownloadString\bin\Debug");
            var readFileExePath = Path.Combine(readFileDirectoryPath, "MockEverythingTests.DownloadString.exe");
            var tampering = new Tampering
            {
                Pair = new Pair<IAssembly>(
                    proxy: new Assembly(this.FindAssemblyPathOf(typeof(WebClientProxy))),
                    target: new Assembly(this.FindAssemblyPathOf<WebClient>())),
                ResultVersion = new Version("2.0.14.2075"),
            };

            tampering.Tamper().Save(Path.Combine(readFileDirectoryPath, "System.dll"));

            var info = new ProcessStartInfo(readFileExePath)
            {
                WindowStyle = ProcessWindowStyle.Hidden
            };

            using (var process = Process.Start(info))
            {
                process.WaitForExit();
                var actual = process.StandardOutput.ReadToEnd();
                var expected = "Hello, World!";
                Assert.AreEqual(expected, actual);
            }
        }

        private string CurrentPath
        {
            get
            {
                var path = this.FindAssemblyPathOf<TamperingTests>();
                return Path.GetDirectoryName(path);
            }
        }

        private string FindAssemblyPathOf<T>()
        {
            return this.FindAssemblyPathOf(typeof(T));
        }

        private string FindAssemblyPathOf(System.Type type)
        {
            var codeBase = type.Assembly.CodeBase;
            return Uri.UnescapeDataString(new UriBuilder(codeBase).Path);
        }
    }
}
