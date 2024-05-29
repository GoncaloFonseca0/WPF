using _29Abril.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace _29Abril.Controller
{
    public class Controller : UIElement
    {
        public Cmd cmdNavega { get; set; }
        public Cmd cmdSair { get; set; }

        public Cmd cmdSelos { get; set; }
        public Controller()
        {
            cmdSair = new Cmd(canSair, Sair);
            cmdNavega = new Cmd(canNavega, Navega);
            cmdSelos = new Cmd(canTrocaSelos, TrocaSelos);
        }

        MainWindow main = (MainWindow)App.Current.MainWindow;
        public bool canNavega(Object parameter)
        {
            string destino = parameter.ToString();
            if (main.frame.Source.ToString().Contains(destino)) return false;
            return true;
        }
        public void Navega(Object parameter)
        {
            String destino = parameter.ToString();
            switch (destino)
            {
                case "Inicio":
                    main.frame.Source = new Uri(@"/View/Inicio.xaml", UriKind.Relative);
                    break;
                case "JogoDados":
                    main.frame.Source = new Uri(@"/View/JogoDados.xaml", UriKind.Relative);
                    break;
                case "Selos":
                    main.frame.Source = new Uri(@"/View/Selos.xaml", UriKind.Relative);
                    break;
                default:
                    main.frame.Source = new Uri(@"/View/Inicio.xaml", UriKind.Relative);
                    break;
            }
        }

        public bool canSair(Object parameter)
        {
            return true;
        }
        public void Sair(Object parameter)
        {
            App.Current.Shutdown();
        }

        public bool canTrocaSelos(Object parameter)
        {

            if (parameter == null || String.IsNullOrEmpty(parameter.ToString()) || int.Parse(parameter.ToString()) == 0) return false;
            return true;
        }
        public void TrocaSelos(Object parameter)
        {
            int euros;
            if (!int.TryParse(parameter.ToString(), out euros)) euros = 0;
            Selos pgselos = (Selos)main.frame.Content;
            pgselos.txtresultado.Text = Biblioteca.trocaSelos(euros);
        }

    }
}
