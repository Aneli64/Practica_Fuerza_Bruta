using System.Security.Cryptography;
using System.Text;

namespace Practica_Fuerza_Bruta;

using System;
using System.IO;

public class Archivo
{
    private string path;

    public Archivo(string path)
    {
        this.path = path;
    }

    private string ObtPass(string passIN)
    {
        //variables necesarias para usar nuestros hilos
        string passEnc = "";
        int numHilos = 4;
        Thread[] threads = new Thread[numHilos];
        AutoResetEvent[] nuevoHilo = new AutoResetEvent[numHilos];
        int numLineasFile = File.ReadLines(path).Count();
        var step = numLineasFile / numHilos;

        //iteramos en el numero de hilos
        for (int i = 0; i < numHilos; i++)
        {
            //lo definimos a false para comenzar de nuevo
            nuevoHilo[i] = new AutoResetEvent(false);

            //lambda que va iterando en nuestros hilos, todo ello en base a nuestro boolean 
            threads[i] = new Thread((object index) =>
            {
                int threadIndex = (int)index;
                ThreadReadFile(passIN, threadIndex * step, (threadIndex + 1) * step, nuevoHilo[threadIndex]);
            });

            //iteramos y vamos iniciando nuestros hilos
            threads[i].Start(i);
        }
        
        return passEnc;
    }

    private void ThreadReadFile(string passIN, int start, int end, AutoResetEvent nuevoHilo)
    {
        using (StreamReader reader = new StreamReader(path))
        {
            for (int i = 0; i < start; i++) reader.ReadLine();

            for (int i = start; i < end; i++)
            {
                if (encriptar(passIN) == encriptar(reader.ReadLine()))
                {
                    Console.WriteLine($"Password {passIN}");
                    break;
                }
            }
        }

        nuevoHilo.Set();
    }

    private string encriptar(string passIN)
    {
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(passIN);
            byte[] hashedBytes = sha256.ComputeHash(passwordBytes);

            StringBuilder stringBuilder = new StringBuilder();

            foreach (var item in hashedBytes) stringBuilder.Append(item.ToString("x2"));

            return stringBuilder.ToString();
        }
    }

    public static void Main(string[] args)
    {
        //Variables File necesarias y contraseña a encontrar en dicho File.
        Archivo file =
            new Archivo("/home/aneli/RiderProjects/Practica_Fuerza_Bruta/Practica_Fuerza_Bruta/2151220-passwords.txt");
        string contrParaAdivinar = "0350292";
        string contrEncrip = "";

        //Leemos el archivo linea a linea con StreamReader,
        //y obtenemos la contraseña
        Console.WriteLine(file.ObtPass(contrParaAdivinar));
    }
}