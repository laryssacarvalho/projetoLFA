using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Projeto1
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue<string> fila = new Queue<string>();
            string[] variaveis = { "" };
            string[] alfabeto;
            string inicial = "";
            string saida = "";
            string palavra = "";
            List<string> p0 = new List<string>();
            List<string> p1 = new List<string>();
            List<string> aux = new List<string>();
            List<int> entrada = new List<int>();

            bool repeat = true;

            while (repeat)
            {
                Console.WriteLine("Digite as variáveis separadas por vírgula. Ex: X, Y, Z");
                variaveis = Console.ReadLine().Replace(" ", string.Empty).Split(',');
                
                if(variaveis.Length <= 0 || variaveis[0] == "")
                {
                    Console.WriteLine("\n\tERRO: Por favor, digite ao menos uma variável!\n");
                }
                else
                {
                    repeat = false;
                }
            }

            repeat = true;

            while (repeat)
            {
                Console.WriteLine("\nDigite o alfabeto separado por vírgula. Ex: a, b, c");
                alfabeto = Console.ReadLine().Replace(" ", string.Empty).Split(',');

                if (alfabeto.Length <= 0 || alfabeto[0] == "")
                {
                    Console.WriteLine("\n\tERRO: Por favor, digite ao menos um item para o alfabeto!");
                }
                else
                {
                    repeat = false;
                }
            }

            repeat = true;

            while (repeat)
            {
                Console.WriteLine("\nDigite a variável inicial");
                inicial = Console.ReadLine().Replace(" ", string.Empty);

                if (String.IsNullOrEmpty(inicial))
                {
                    Console.WriteLine("\n\tERRO: Por favor, insira a variável inicial!");
                }
                else if (!variaveis.Contains(inicial))
                {
                    Console.WriteLine("\n\tERRO: Por favor, a variável inicial deve ser uma das variáveis!");
                    variaveis.ToList().ForEach(Console.Write);
                }
                else
                {
                    repeat = false;
                }
            }

            repeat = true;

            while (repeat)
            {
                Console.WriteLine("\nDigite as regras de produção seguindo o exemplo: S>AB, A>aA, Bb>bB");
                aux = Console.ReadLine().Replace(" ", string.Empty).Split(',').ToList();

                foreach(string s in aux)
                {
                    if(s.Length > 2)
                    {
                        p0.Add(s.Split('>')[0]);
                        p1.Add(s.Split('>')[1]);
                    }
                }
                if(p0.Count == 0)
                {
                    Console.WriteLine("\n\tERRO: Por favor, insira pelo menos uma regra de produção!");
                    p0 = new List<string>();
                    p1 = new List<string>();
                }
                else if (!p0.Contains(inicial))
                {
                    Console.WriteLine("\n\tERRO: Por favor, ao menos uma regra deve conter a variável inicial!");
                    p0 = new List<string>();
                    p1 = new List<string>();
                }
                else
                {
                    repeat = false;
                }
            }

            repeat = true;

            //while (repeat)
            //{
            //    Console.WriteLine("\nDigite os valores de entrada separados por vírgula. Ex: 1, 12, 7");
            //    aux = Console.ReadLine().Replace(" ", string.Empty).Split(',').ToList();

            //    foreach (string s in aux)
            //    {
            //        if(s != "")
            //        {
            //            entrada.Add(Convert.ToInt32(s));
            //        }
            //    }

            //    if (entrada.Count <= 0)
            //    {
            //        Console.WriteLine("\n\tERRO: Por favor, digite ao menos um valor de entrada!");
            //        entrada = new List<int>();
            //    }
            //    else if(entrada.Max() > p0.Count)
            //    {
            //        Console.WriteLine("\n\tERRO: Por favor, digite apenas entradas válidas!");
            //        entrada = new List<int>();
            //    }
            //    else if (p0[entrada[0]-1] != inicial)
            //    {
            //        Console.WriteLine("\n\tERRO: Por favor, a primeira entrada deve possuir a variável inicial!");
            //        entrada = new List<int>();
            //    }
            //    else
            //    {
            //        repeat = false;
            //    }
            //}

            while (repeat)
            {
                Console.WriteLine("\nDigite a palavra a ser derivada.");
                palavra = Console.ReadLine();                

                if (palavra == "")
                {
                    Console.WriteLine("\n\tERRO: Por favor, digite uma palavra!");                    
                }
                else
                {
                    repeat = false;
                }
            }

            //saida = decifrarPalavra(p0, p1, entrada);

            saida = derivarPalavra(p0, p1, p1[0], palavra);
            Console.WriteLine($"\nPalavra decifrada: {saida}");

            //if (variaveis.Any(c => saida.Contains(c)))
            //{
            //    Console.WriteLine($"\nNão foi possível decifrar a palavra. Saída: {saida}");
            //}
            //else
            //{
            //    Console.WriteLine($"\nPalavra decifrada: {saida}");
            //}            
            Console.ReadLine();
        }

        public static string decifrarPalavra(List<string> p0, List<string> p1, List<int> entrada)
        {
            string palavra = "";
            foreach (int i in entrada)
            {
                if (palavra == "")
                {
                    palavra = p1[i - 1].ToString();
                }
                else
                {
                    if (palavra.Contains(p0[i - 1]))
                    {
                        int x = palavra.IndexOf(p0[i - 1]);
                        palavra = palavra.Remove(x, p0[i - 1].Length).Insert(x, p1[i - 1]);
                    }
                }
            }
            return palavra;
        }

        public static string derivarPalavra(List<string> p0, List<string> p1, string inicial, string palavra)
        {
            string saida = "";
            int i;
            Queue<string> fila = new Queue<string>();            
            fila.Enqueue(inicial);
            while(fila.Count > 0)
            {
                string atual = fila.Dequeue();
                if(atual == palavra)
                {
                    return palavra;
                }

                //monta os nós adjacentes ao nó atual, realizando a substituição da regra
                for(i = 0; i < p0.Count; i++)
                {
                    if (atual.Contains(p0[i]))
                    {
                        string adj = atual.Replace(p0[i], p1[i]);

                        if (!fila.Contains(adj))
                        {
                            fila.Enqueue(adj);
                        }
                    }                    
                }
            }

            return saida;
        }
    }
}
