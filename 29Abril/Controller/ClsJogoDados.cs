using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _29Abril.Controller
{

    // criar um evento
    public delegate void OnChangeMontante_Handler(string msg);
    public class ClsJogoDados : UIElement
    {


        // declarar o evento
        public event OnChangeMontante_Handler OnChangeMontante;


        
        private static readonly PropertyMetadata meta = new PropertyMetadata(
            100,
            propertyChangedCallback,  
            coerceValueCallback   
            
            ); 

        private static object coerceValueCallback(DependencyObject d, object baseValue) // se for atingido um limite retorna ao valor "default" // é uma propriedade dependency property 
        {
            if (int.TryParse(baseValue.ToString(), out int valor)) 
            {
                if (valor <= 10) return 2000;
               
            }
            return baseValue;
        }

        private static void propertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
          
            string msg=$"\nold value:{e.OldValue} new value {e.NewValue}\n";
           // MessageBox.Show(msg);
           //disparar o evento
           ((ClsJogoDados)d).OnChangeMontante(msg);
        }

        public int Montante
        {
            get { return (int)GetValue(MontanteProperty); }
            set { SetValue(MontanteProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Montante.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MontanteProperty =
            DependencyProperty.Register(
                "Montante", 
                typeof(int),
                typeof(ClsJogoDados),
                meta,
                validateValueCallback
                );

        private static bool validateValueCallback(object value) // vai validar o meu valor
        {
            if ((int)value < 0) return false;
            return true;
        }

        public int Dado1
        {
            get { return (int)GetValue(Dado1Property); }
            set { SetValue(Dado1Property, value); }
        }

        // Using a DependencyProperty as the backing store for Dado1.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Dado1Property =
            DependencyProperty.Register(
                "Dado1",
                typeof(int),
                typeof(ClsJogoDados),
                new PropertyMetadata(3));



        public int Dado2
        {
            get { return (int)GetValue(Dado2Property); }
            set { SetValue(Dado2Property, value); }
        }

        // Using a DependencyProperty as the backing store for Dado2.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty Dado2Property =
            DependencyProperty.Register("Dado2", typeof(int), typeof(ClsJogoDados), new PropertyMetadata(5));



        public void rolar(int aposta)
        {
            if (Montante >= aposta) Montante -= aposta;
            else if (Montante > 0)
            {
                aposta = Montante;
                Montante = 0;
            }
            else aposta = 0;
            if (aposta > 0)
            {
                Random r = new Random();
                Dado1 = r.Next(1, 7);
                Dado2 = r.Next(1, 7);
                // disparar o evento
                if (Dado1 == Dado2)
                {
                    // disparar o evento
                    RaiseOnPremio(aposta);


                }

            }
        }

        // direct- so é visivel dentro do objeto
        // bubble - pode ser detetado nos conteiners do botao

        private static readonly RoutedEvent onPremioEvent =
            EventManager.RegisterRoutedEvent(
                "OnPremio",
                RoutingStrategy.Direct,
                typeof(RoutedEventHandler),
                typeof(ClsJogoDados)
                );


        // Evento que o cliente vai ver
        public event RoutedEventHandler OnPremio
        {
            add { AddHandler(onPremioEvent, value); }
            remove { RemoveHandler(onPremioEvent, value); }
        }

        public class onPremioRoutedEventArgs: RoutedEventArgs
        {
            public int Aposta;

            public onPremioRoutedEventArgs(int aposta):base(onPremioEvent)
            {
                Aposta = aposta;
            }
        }


        // Disparar
        public void RaiseOnPremio(int aposta)  // instanciar uma lote dos argumentos
                    {
          onPremioRoutedEventArgs args = new onPremioRoutedEventArgs(aposta);
            RaiseEvent(args); // disparar um evento ( passar a classe do argumento)
        }
    }
}
