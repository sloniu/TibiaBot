using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TibiaBot.Models;
using TibiaBot.Properties;

namespace TibiaBot
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        void Application_Exit(object sender, EventArgs e)
        {
            Settings.Default.Save();
        }
    }
}
