//creator: Dylan Shim
//RSA Algorithm 
//Outputs to console and a txt file with ASCII codes

using System;
using System.IO;
using System.Text;
namespace RSA_Algorithm__csharp_
{
    class Program
    {

        static int gcd(int x, int y) 
        {
            int temp;
            while (true)
            {
                temp = x % y;
                if (temp == 0)
                {
                    return y;
                }
                x = y;
                y = temp;
            }
        }

        static double encrypt(double e, double phi)
        {
            double temp;
            while (e < phi)
            {
                temp = gcd((int)e, (int)phi);
                if (temp == 1)
                    break;
                else
                    e++;
            }
            return e;
        }

        static double decrypt(double e, double phi)
        {
            double e1 = 1 / e;
            double d = e1 % phi;
            return d;
        }

        static byte[] convertToASCII(string message)
        {
            byte[] asciiBytes = Encoding.ASCII.GetBytes(message);
            return asciiBytes;
        }

        static string convertASCIItoString(double[] encrypted)
        {
            string result = "";
            foreach (var item in encrypted)
            {
                char chr = (char)item;
                result = result + chr;
            }
            return result;
        }

        static void printtoText(string path, double[] ascii)         //currently there is no implementation of deleting text, must be done manually for now
        {
            string message = "";
            foreach (var item in ascii)
            {
                message = message + item.ToString() + " ";
            }

            if (!File.Exists(path))
            {
                string start = "Encrypted ASCII Codes" + Environment.NewLine;
                File.WriteAllText(path, start);
            }
            string appendText = message + Environment.NewLine + "--------------" + Environment.NewLine;
            File.AppendAllText(path, appendText);
        }
        static void Main()
        {
            double p = 79;      // prime #1
            double q = 41;      // prime #2
            double n = p * q;
            double phi = (p - 1) * (q - 1); //calculation for phi
            double e = 2;  //encryption for public key

            e = encrypt(e, phi); // calculates coprime integers so that e and phi are coprime
            double d = decrypt(e, phi); //calculates private key


            //***User Input***//
            string message = "null";
            Console.WriteLine("Type a message you'd like to encode");
            message = Console.ReadLine();

            //***Convert message to ASCII***//
            byte[] ascii = new byte[convertToASCII(message).Length];
            ascii = convertToASCII(message);

            //***Encrypt Message***//
            double[] encryptedmsg = new double[ascii.Length];
            for (int i = 0; i < ascii.Length; i++)
            {
                encryptedmsg[i] = Math.Pow(ascii[i], e);
            }

            //***Decrypt Message***//
            double[] decryptedmsg = new double[encryptedmsg.Length];
            for (int l = 0; l < decryptedmsg.Length; l++)
            {
                decryptedmsg[l] = Math.Pow(encryptedmsg[l], d);
            }

            for (int j = 0; j < encryptedmsg.Length; j++)
            {
                encryptedmsg[j] = encryptedmsg[j] % n;
            }
            for (int k = 0; k < decryptedmsg.Length; k++)
            {
                decryptedmsg[k] = Math.Ceiling(decryptedmsg[k] % n);        //ceiling function in order to detect rounding errors
            }

            //***Convert Decrypted Message back to string***//
            string result = convertASCIItoString(decryptedmsg);

            //***Debug Info***//
            Console.WriteLine("n: {0}", n);
            Console.WriteLine("phi: {0}", phi);
            Console.WriteLine("e: {0}", e);
            Console.WriteLine("d: {0}", d);
            Console.WriteLine("Message: {0}\n", message);
            Console.Write("ASCII Message: ");
            foreach (var item in ascii)
                Console.Write("{0} ", item);
            Console.WriteLine();
            Console.Write("Encrypted Message: ");
            foreach (var item in encryptedmsg)
                Console.Write("{0} ", item);
            Console.WriteLine();
            Console.WriteLine();
            Console.Write("Decrypted Message: ");
            foreach (var item in decryptedmsg)
                Console.Write("{0} ", item);
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("Resulting Message: {0}", result);

            string path = @"D:\Visual Studio\RSA Algorithm (csharp)\txt files for encrypted ascii\EncryptedASCII.txt";      //path of txt file, change to your unique path
            printtoText(path, encryptedmsg);





        }

    }
}
