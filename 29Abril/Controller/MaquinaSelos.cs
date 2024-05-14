﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _29Abril.Controller
{
    public class MaquinaSelos
    {
       

        internal static String trocaSelos(int euros)
        {
            String msg = (euros >= 8) ? "\nTudo bem!!" : "\nAtenção quantidade pode ser indevida";
            Console.WriteLine(msg);
            int quoc, r, s5 = 0, s3 = 0;
            if (euros >= 8)
            {
                quoc = euros / 8;
                r = quoc % 8;
                switch (r)
                {
                    case 0:
                        s3 = quoc;
                        s5 = quoc;
                        break;
                    case 1:
                        s3 = quoc + 2;
                        s5 = quoc - 1;
                        break;
                    case 2:
                        s3 = quoc - 1;
                        s5 = quoc + 1;
                        break;
                    case 3:
                        s3 = quoc + 1;
                        s5 = quoc;
                        break;
                    case 4:
                        s3 = quoc + 3;
                        s5 = quoc - 1;
                        break;
                    case 5:
                        s3 = quoc;
                        s5 = quoc + 1;
                        break;
                    case 6:
                        s3 = quoc + 2;
                        s5 = quoc;
                        break;
                    case 7:
                        s3 = quoc - 1;
                        s5 = quoc + 2;
                        break;
                }

            }
            else
            {
                if (euros == 3) { s3 = 1; s5 = 0; }
                else if (euros == 5) { s3 = 0; s5 = 1; }
                else if (euros == 6) { s3 = 2; s5 = 0; }
                else { Console.WriteLine($"\nDevolução da quantia {euros}"); }
            }
            return $"\nEuros={euros} -> Selos de 5E = {s5} 3E={s3}";
        }
    }
}
