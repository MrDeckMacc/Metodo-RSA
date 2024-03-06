using System;
using System.Numerics;
using System.Text;

namespace Examen.Models
{
    public class CifradoRSA
    {

        public static string CifrarContraseña(string contaseña, BigInteger p, BigInteger q)
        {
            StringBuilder contraseñaEncryptada = new StringBuilder();
            BigInteger n = CalcularN(p,q);
            BigInteger e = 11;

            foreach(char character in contaseña)
            {
                int posicion = PosicionABC(character);

                BigInteger cifrado = BigInteger.ModPow(posicion, e, n);

                contraseñaEncryptada.Append(cifrado.ToString());
                contraseñaEncryptada.Append(' ');
            }

            return contraseñaEncryptada.ToString().Trim();
        }

        public static string DescifrarContraseña(string contraseñaCifrada,  BigInteger p, BigInteger q) 
        {
            StringBuilder contraseñaDesincriptada = new StringBuilder();

            BigInteger e = 11;
            BigInteger n = CalcularN(p, q);
            BigInteger phi = CalcularPhi(p, q);
            BigInteger d = CalcularD(e, phi);
            
            string[] cifras = contraseñaCifrada.Trim().Split(' ');
            foreach(string cifra in cifras) 
            {
                BigInteger numeroCifrado = BigInteger.Parse(cifra);
                BigInteger posicionDescifrada = BigInteger.ModPow(numeroCifrado, d, n);
                char letraDescifrada = ConvertirPosicion((int)posicionDescifrada);
                contraseñaDesincriptada.Append(letraDescifrada);
            }
            return contraseñaDesincriptada.ToString();
        }

        static BigInteger CalcularN(BigInteger p, BigInteger q) 
        {
            return p * q;
        }

        static BigInteger CalcularPhi(BigInteger p, BigInteger q)
        {
            return (p - 1) * (q - 1);
        }

        static BigInteger CalcularD(BigInteger e, BigInteger phi) 
        {
            BigInteger k = 3;
            return (1+k*phi)/e;
        }

        static int PosicionABC(char letra)
        {
            return char.ToLower(letra) - 'a' + 1;
        }

        static char ConvertirPosicion(int posicion) 
        {
            return (char)('a' + posicion - 1);
        }
    }
}
