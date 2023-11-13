namespace Practica_Fuerza_Bruta;

using System;
using System.IO;

public class Archivo
{
    private static string path = "/home/aneli/RiderProjects/Practica_Fuerza_Bruta/Practica_Fuerza_Bruta/2151220-passwords.txt";
    private static string contenido = File.ReadAllText(path);
    public String ReadCont() => contenido;

    public String h()
    {
        for (int i = 0; i < contenido.Length; i++)
        {
            
        }
        return "";
    }
    
}