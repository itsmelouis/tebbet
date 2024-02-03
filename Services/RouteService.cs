using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tebbet.Services
{
    public class RouteService
    {
        // Singleton
        private RouteService() { }
        private static RouteService _instance;
        public static RouteService GetInstance()
        {
            if (_instance == null)
            {
                _instance = new RouteService();
            }
            
            return _instance;
        }

        private Window PrecedentWindow { get; set; }
        private Window Window { get; set; }
        
        public void ChangePage(string className)
        {
            if (Window != null)
            {
                PrecedentWindow = Window;
            }
            if (PrecedentWindow != null)
            {
                if (PrecedentWindow.Title == className)
                {
                    return;
                }
            }
            if (!String.IsNullOrEmpty(className)) 
            {
                Type type = Type.GetType("Tebbet.Views."+className);
                if (type != null)
                {
                    object instance = Activator.CreateInstance(type);
                    if (instance is Window)
                    {
                        Window = (Window)instance;
                        Window.Show();
                    }
                }
            }
            PrecedentWindow?.Close();

            return;
        }
    }
}
