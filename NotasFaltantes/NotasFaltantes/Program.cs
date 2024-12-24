using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main()
    {
        string caminhoDoDisco = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        string caminhoRelativo = "\\NotasFaltantes\\NotasFaltantes\\arquivos.txt";
        string caminhoDeEntrada = Path.Combine(caminhoDoDisco, caminhoRelativo);

        if (File.Exists(caminhoDeEntrada))
        {
            try
            {
                string[] linhas = File.ReadAllLines(caminhoDeEntrada);
                List<int> numeros = new List<int>();

                foreach (string linha in linhas)
                {
                    string[] partes = linha.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

                    if (partes.Length >= 2 &&
                        int.TryParse(partes[0], out int numero1) &&
                        int.TryParse(partes[1], out int numero2))
                    {
                        if (numero1 <= numero2)
                        {
                            for (int i = numero1; i <= numero2; i++)
                            {
                                numeros.Add(i);
                            }
                        }
                        else
                        {
                            numeros.Add(numero1);
                            numeros.Add(numero2);
                        }
                    }
                    else
                    {
                        Console.WriteLine($"Linha ignorada (formato inválido): {linha}");
                    }
                }
               
                string numerosFormatados = string.Join(",", numeros);

                File.WriteAllText(caminhoDeEntrada, numerosFormatados + Environment.NewLine);

                Console.WriteLine($"Os números foram formatados e salvos no arquivo {caminhoDeEntrada}:");
                Console.WriteLine(numerosFormatados);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao processar o arquivo: " + ex.Message);
            }
        }
        else
        {
            Console.WriteLine($"O arquivo {caminhoDeEntrada} não foi encontrado.");
        }
    }
}