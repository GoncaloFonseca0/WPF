using _29Abril.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace _29Abril.Controller
{
    public class Controller : UIElement
    {

        MainWindow main = (MainWindow)App.Current.MainWindow;
        DispatcherTimer timer = new DispatcherTimer(); // quando acontecer vitória inicia um delegate e cria uma função recursiva 
        public int poscor = 0;

        public Cmd cmdRolar { get; set; }  
        public ClsJogoDados myjogo { get; set; }
        public Cmd cmdNavega { get; set; }
        public Cmd cmdSair { get; set; }

        public Cmd cmdSelos { get; set; }
        public Controller()
        {

            myjogo=new ClsJogoDados();
            myjogo.OnChangeMontante += Myjogo_OnChangeMontante; 
            myjogo.OnPremio += Myjogo_OnPremio; // registar o evento
            cmdSair = new Cmd(canSair, Sair);
            cmdNavega = new Cmd(canNavega, Navega);
            cmdSelos = new Cmd(canTrocaSelos, TrocaSelos);
            cmdRolar = new Cmd(canRolar, Rolar);
        }

        private void Myjogo_OnChangeMontante(string msg)
        {
            Dados pag = (Dados)main.frame.Content;
            pag.visor.Text += msg; // colocar na caixa de texto o valor antigo e o recente
        }

        private void Myjogo_OnPremio(object sender, RoutedEventArgs e) // e-> tem aposta
        {
            // ir buscar a pagina
            Dados pagina = (Dados)main.frame.Content;

            // resposta ao evento
            ClsJogoDados myjogo = (ClsJogoDados)sender; // foi o que disparou -> sender objecto generico

            int premio = myjogo.Dado1 * 2 * ((ClsJogoDados.onPremioRoutedEventArgs)e).Aposta;  // calculo

            // criar mensagem
            pagina.visor.Text = $"Parabéns ganhou {premio} euros";
            myjogo.Montante += premio;

           // timer cores 
            timer.Interval = TimeSpan.FromMilliseconds(1000);
            timer.Tick += Timer_Tick;
            timer.Start();


            string path = AppDomain.CurrentDomain.BaseDirectory;
            int idx = path.IndexOf("bin");
            path = path.Substring(0, idx) + @"dados\gongo.wav";
            SoundPlayer soundPlayer = new SoundPlayer(path); // instanciar com o ficheiro onde esta a musica
            soundPlayer.Load();
            soundPlayer.Play();


        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            System.Windows.Media.Brush[] cores = new System.Windows.Media.Brush[] { Brushes.Yellow, Brushes.Red, Brushes.Blue, Brushes.Green }; // array de cores
            Dados pag = (Dados)main.frame.Content;
            pag.visor.Background = cores[(poscor +1)%cores.Length];
            poscor++;
        }

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

            timer.Stop(); // quando carregamos no "Rolar" ele para o timer

            // Quando voltar a rolar os dados apaga a mensagem
             Dados pagina = (Dados)main.frame.Content;
            pagina.visor.Text = "";
           


            if (parameter == null) return;
            if (int.TryParse(parameter.ToString(), out int aposta))
            {
                myjogo.rolar(aposta);
                Dados pag = (Dados)main.frame.Content;   // bloquear o botao de jogar se o montante for 0
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
                case "JogoSpace":
                    main.frame.Source = new Uri(@"/View/JogoSpace.xaml", UriKind.Relative);
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
