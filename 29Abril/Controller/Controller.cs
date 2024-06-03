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
        public Cmd cmdRolar { get; set; }  
        public ClsJogoDados myJogo { get; set; }
        public Cmd cmdNavega { get; set; }
        public Cmd cmdSair { get; set; }

        public Cmd cmdSelos { get; set; }
        public Controller()
        {

            myJogo=new ClsJogoDados();
            cmdSair = new Cmd(canSair, Sair);
            cmdNavega = new Cmd(canNavega, Navega);
            cmdSelos = new Cmd(canTrocaSelos, TrocaSelos);
            cmdRolar = new Cmd(canRolar, Rolar);
        }

        MainWindow main = (MainWindow)App.Current.MainWindow;

        public bool canRolar(object parameter)
        {



            if (parameter == null) return false;
            if ( int.TryParse(parameter.ToString(), out int dado))
            {
                if (dado > 0) return true;


            }
           
            return false;
        }

        public void Rolar(Object parameter)
        {


            if (parameter == null) return;
            if (int.TryParse(parameter.ToString(), out int aposta))
            {
                myJogo.rolar(aposta);
                Dados pag = (Dados)main.frame.Content;
                if (pag.txtmontante.Text == "0") pag.slider.Value = 0;

            }
           
        }
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
                    main.frame.Source = new Uri(@"/View/Dados.xaml", UriKind.Relative);
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
