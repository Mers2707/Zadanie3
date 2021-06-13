using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace Zadanie3
{
    class Program
    {
        static string CreateToken(string message, string secret)
        {
            var encoding = new System.Text.UTF8Encoding();
            byte[] keyByte = System.Text.Encoding.UTF8.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(message);
            using (var hmacsha256 = new HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashmessage);
            }
        }
        static string WinOrLose(string[] arr, int comp, int choose)
        {
            string win = "WIN";
            string lose = "LOSE";
            string draw = "DRAW";
            int doArr = (arr.Length - 1) / 2;
            int winLine = comp + doArr;
            if (comp == choose)
            {
                return draw;
            }
            if (arr.Length < winLine)
            {
                int winLine2 = winLine - arr.Length;
                if ((choose <= winLine && choose > comp) || winLine2==choose)
                {
                    return win;
                }
            }
            else
            {
                if (choose <= winLine && choose > comp)
                {
                    return win;
                }
            }
            return lose;
        }

        public static string RandomString(int length)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            using (RNGCryptoServiceProvider rnd = new RNGCryptoServiceProvider())
            {
                while (length-->0)
                {
                    res.Append(valid[GetInt(rnd, valid.Length)]);
                }
            }
            return res.ToString();
        }
        public static int GetInt(RNGCryptoServiceProvider rnd, int max)
        {
            byte[] r = new byte[4];
            int value;
            do
            {
                rnd.GetBytes(r);
                value = BitConverter.ToInt32(r, 0) & Int32.MaxValue;
            } while (value >= max * (Int32.MaxValue / max));
            return value % max;
        }

        static void Main(string[] args)
        {
            int vArr = 0;
            int copyArgs = 0;
            for (int i = 0; i < args.Length; i++)
            {
                vArr = 0;
                for (int j = 0; j < args.Length; j++)
                {
                    if (args[j] == args[i])
                        vArr++;
                    if (vArr>1)
                    {
                        copyArgs++;
                        break;
                    }
                }

            }
            if (args.Length >= 3 && ((args.Length % 2) > 0) && copyArgs <= 1)
                {
                Random rndint = new Random();
                string random1 = RandomString(64);

                while (Console.ReadLine()!="0")
                {
                    int compturn = rndint.Next(1, args.Length);
                    Console.WriteLine("HMAC:" + Program.CreateToken(args[compturn - 1], random1));
                    for (int i = 1; i <= args.Length; i++)
                        {
                            Console.WriteLine(i.ToString() + "-" + args[i-1]);
                        }
                    Console.WriteLine("0 - exit");
                    int choosePlayer = int.Parse(Console.ReadLine());
                    if (choosePlayer == 0)
                    {
                        break;
                    }
                    Console.WriteLine("You move:" + args[choosePlayer-1]);
                    Console.WriteLine("Computer move:" + args[compturn-1]);
                    string winner = WinOrLose(args, compturn, choosePlayer);
                    Console.WriteLine(winner);
                    Console.WriteLine("HMAC key:"+random1);
                }
                }
                else
                {
                    Console.WriteLine("Invalid number of arguments!\r");
                    Console.Write("Press any key to close app...");
                    Console.ReadKey();
                }
                Console.Write("Press any key to close app...");
                Console.ReadKey();
            }
        }
    }
