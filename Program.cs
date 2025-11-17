using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace PMEncrypter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== PMEncrypter - Herramienta de Hash SHA-256 ===");
            Console.WriteLine();
            
            if (args.Length == 0)
            {
                MostrarAyuda();
                return;
            }

            string comando = args[0].ToLower();

            switch (comando)
            {
                case "hash":
                    if (args.Length < 2)
                    {
                        Console.WriteLine("Error: Debes especificar un archivo");
                        Console.WriteLine("Uso: PMEncrypter hash <ruta-del-archivo>");
                        return;
                    }
                    CalcularHash(args[1]);
                    break;
                    
                case "version":
                    MostrarVersion();
                    break;
                    
                case "help":
                case "--help":
                case "-h":
                    MostrarAyuda();
                    break;
                    
                default:
                    Console.WriteLine($"Comando desconocido: {comando}");
                    MostrarAyuda();
                    break;
            }
        }

        static void CalcularHash(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine($"Error: El archivo '{filePath}' no existe");
                    return;
                }

                string hash = CalculateSHA256(filePath);
                FileInfo fileInfo = new FileInfo(filePath);
                
                Console.WriteLine($"Archivo: {Path.GetFileName(filePath)}");
                Console.WriteLine($"Tamaño: {fileInfo.Length:N0} bytes");
                Console.WriteLine($"SHA-256: {hash}");
                Console.WriteLine($"Ruta: {Path.GetFullPath(filePath)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calculando hash: {ex.Message}");
            }
        }

        static string CalculateSHA256(string filePath)
        {
            using SHA256 sha256 = SHA256.Create();
            using FileStream stream = File.OpenRead(filePath);
            
            byte[] hashBytes = sha256.ComputeHash(stream);
            
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashBytes)
            {
                sb.Append(b.ToString("x2"));
            }
            
            return sb.ToString();
        }

        static void MostrarVersion()
        {
            Console.WriteLine("PMEncrypter v1.0.0");
            Console.WriteLine("Herramienta de cálculo de hash SHA-256");
            Console.WriteLine("Copyright 2024 - Proyecto de Empaquetado");
        }

        static void MostrarAyuda()
        {
            Console.WriteLine("Uso: PMEncrypter <comando>");
            Console.WriteLine();
            Console.WriteLine("Comandos disponibles:");
            Console.WriteLine("  hash <archivo>    Calcular hash SHA-256 de un archivo");
            Console.WriteLine("  version           Mostrar información de versión");
            Console.WriteLine("  help              Mostrar esta ayuda");
            Console.WriteLine();
            Console.WriteLine("Ejemplos:");
            Console.WriteLine("  PMEncrypter hash documento.pdf");
            Console.WriteLine("  PMEncrypter version");
            Console.WriteLine("  PMEncrypter help");
        }
    }
}