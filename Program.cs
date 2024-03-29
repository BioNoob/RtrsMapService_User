﻿//using CefSharp;
//using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms;

namespace RtrsMapService_User
{
    static class Program
    {

        public delegate void SetStatus(string s);
        public static event SetStatus DoSetStatus;
        public delegate void LoadEnd();
        public static event LoadEnd DoLoadEnd;
        public static bool admin_mode;
        public static bool no_browser_mode;
        //static ImageGetter form;
        public static void ToolStripStatusInvokeAction<TControlType>(this TControlType control, Action<TControlType> del)
    where TControlType : ToolStripStatusLabel
        {
            if (control.GetCurrentParent().InvokeRequired)
                control.GetCurrentParent().Invoke(new Action(() => del(control)));
            else
                del(control);
        }
        public static System.Drawing.Imaging.ImageFormat GetImageFormat(this System.Drawing.Image img)
        {
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Jpeg))
                return System.Drawing.Imaging.ImageFormat.Jpeg;
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Bmp))
                return System.Drawing.Imaging.ImageFormat.Bmp;
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Png))
                return System.Drawing.Imaging.ImageFormat.Png;
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Emf))
                return System.Drawing.Imaging.ImageFormat.Emf;
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Exif))
                return System.Drawing.Imaging.ImageFormat.Exif;
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Gif))
                return System.Drawing.Imaging.ImageFormat.Gif;
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Icon))
                return System.Drawing.Imaging.ImageFormat.Icon;
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.MemoryBmp))
                return System.Drawing.Imaging.ImageFormat.MemoryBmp;
            if (img.RawFormat.Equals(System.Drawing.Imaging.ImageFormat.Tiff))
                return System.Drawing.Imaging.ImageFormat.Tiff;
            else
                return System.Drawing.Imaging.ImageFormat.Wmf;
        }
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Start());
            string[] args = Environment.GetCommandLineArgs();
            var list = new List<string>(args);
            admin_mode = list.Exists(t => t.Contains("admin"));
            no_browser_mode = list.Exists(t => t.Contains("noBrowser"));
            if (no_browser_mode)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                var form = new ImageGetter(!no_browser_mode, admin_mode);
                Application.Run(form);
            }
            else
            {
                AppDomain.CurrentDomain.AssemblyResolve += Resolver;
                LoadApp();
            }
        }
        public static void SetterLoad()
        {
            DoLoadEnd?.Invoke();
        }
        public static void SetterStatus(string st)
        {
            DoSetStatus?.Invoke(st);
        }
        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void LoadApp()
        {
            var settings = new CefSharp.WinForms.CefSettings();

            // Set BrowserSubProcessPath based on app bitness at runtime
            settings.BrowserSubprocessPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                                                   Environment.Is64BitProcess ? "x64" : "x86",
                                                   "CefSharp.BrowserSubprocess.exe");
            settings.WindowlessRenderingEnabled = true;
            settings.SetOffScreenRenderingBestPerformanceArgs();

            // Make sure you set performDependencyCheck false
            CefSharp.Cef.Initialize(settings, performDependencyCheck: false, browserProcessHandler: null);
            CefSharp.Cef.EnableHighDPISupport();
            Application.ApplicationExit += Application_ApplicationExit;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var form = new ImageGetter(!no_browser_mode, admin_mode);
            Application.Run(form);
        }

        private static void Application_ApplicationExit(object sender, EventArgs e)
        {
            CefSharp.Cef.Shutdown();
        }
        private static Assembly Resolver(object sender, ResolveEventArgs args)
        {
            if (args.Name.StartsWith("CefSharp"))
            {
                string assemblyName = args.Name.Split(new[] { ',' }, 2)[0] + ".dll";
                string archSpecificPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                                                       Environment.Is64BitProcess ? "x64" : "x86",
                                                       assemblyName);
                return File.Exists(archSpecificPath)
                           ? Assembly.LoadFile(archSpecificPath)
                           : null;
            }

            return null;

        }
        public static void OpenImage(string path)
        {
            if (File.Exists(path))
            {
                Process.Start(path);
            }
        }
    }
}
