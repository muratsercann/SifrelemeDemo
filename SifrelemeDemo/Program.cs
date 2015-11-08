using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SifrelemeDemo
{
    class Program
    {
        static string username = "";
        static string password = "";
        static User newUser = new User();

        static void Main(string[] args)
        {
            Home();
            Console.ReadLine();
        }

        static void Home()
        {
            Console.Clear();
            Console.WriteLine("[1] Sign Up");
            Console.WriteLine("[2] Sign In");
            while (true)
            {
                Console.Write("Your Selection\t: ");
                string key = Console.ReadLine();

                if (key == "1")
                {
                    CreateUser();
                    break;
                }

                else if (key == "2")
                {
                    Login();
                    break;
                }
            }

        }

        static void CreateUser()
        {
            Console.Write("New Username\t\t: ");
            username = Console.ReadLine();
            while (true)
            {
                Console.Write("New Password\t\t: ");
                password = Console.ReadLine();
                Console.Write("Confirm Password\t: ");
                if (password == Console.ReadLine())
                {
                    newUser.UserName = username;
                    newUser.Password = EnCodeWithMD5(username + password);
                    Console.WriteLine("Success sign up :) ");
                    break;
                }
            }
            Home();

        }

        static void Login()
        {
            while (true)
            {
                Console.Write("Username\t: ");
                username = Console.ReadLine();
                Console.Write("Password\t: ");
                password = Console.ReadLine();

                if (!IsEqual(username+password,newUser.Password))
                {
                    Console.WriteLine("Username or Password Incorrect !");
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Welcome " + newUser.UserName);
                    break;
                }
            }

        }

        static string EnCodeWithMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] MD5pass = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(str));
            StringBuilder sb = new StringBuilder();

            foreach (var by in MD5pass)
            {
                //x2 burda string'e çevirirken vermesini istediğimiz format.
                //çıktısında göreceğimiz gibi sayılar ve harflerden oluşucaktır.
                sb.Append(by.ToString("x2").ToLower());
            }
            //oluşturduğumuz string ifadeyi geri döndürdük.
            return sb.ToString();
        }

        public static bool IsEqual(string enteredPassword, string registeredPassword)
        {
            //Sifreli daha önce sifrelemiş olduğumuz parola. Burda veritabanı kullanacak olursanız
            //Sifreli değeri veritabanından çekeceğiniz kullanıcı parolası olacak.
            string enteredMD5 = EnCodeWithMD5(enteredPassword);
            // Kullanıcının giriş yapmak için girdiği parolayı biraz önce yazdığımız method ile
            // Hash haline getirdik.
            StringComparer sc = StringComparer.OrdinalIgnoreCase;
            // StringComparer adından da anlaşıldığı gibi string karşılaştırması yapan bir sınıftır.
            // OrdinalIgnoreCase ile eşitse 0 değilse 1 döndürsün dedik .
            //sc.Compare() methodu ile iki ifadeyi karşılaştırdık. 
            if (0 == sc.Compare(enteredMD5, registeredPassword))
            { //ifadeler uyuşuyorsa burası
                return true;
            }
            else
            {//ifadeler uyuşmuyorsa burası
                return false;
            }
        }
    }


}
