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
        string passEnc = "";
        int contLinea = 1;
        StreamReader reader = new StreamReader(path);
        while (!reader.EndOfStream) {
            if (passIN == reader.ReadLine())
            {
                passEnc = $"Password {passIN} encontrada en línea -> {contLinea}";
                break;
            }
            passEnc = $"Password {passIN} no encontrada";
            contLinea++;
        }
        return passEnc;
    }

    public void encriptar(string passIN)
    {
        
    }
    
    public static void Main(string[] args)
    {
        //Variables File necesarias y contraseña a encontrar en dicho File.
        Archivo file = new Archivo("/home/aneli/RiderProjects/Practica_Fuerza_Bruta/Practica_Fuerza_Bruta/2151220-passwords.txt");
        string contrParaAdivinar = "0350292";
        string contrEncrip = "";
        
        //Leemos el archivo linea a linea con StreamReader,
        //y obtenemos la contraseña y la linea donde se encuentra.
        Console.WriteLine(file.ObtPass(contrParaAdivinar));
        
    }
    
}