using System;
using System.Collections.Generic;
using System.Linq;

namespace Projeto1
{
    class Program
    {
        static void Main(string[] args)
        {
            bool repeatProg = true, newG = true;

            Grammar g = new Grammar();

            while (repeatProg)
            {                
                if (newG)
                {
                    Console.Clear();

                    g = new Grammar();

                    g.GetVariaveis();
                    g.GetAlfabeto();
                    g.GetInicial();
                    g.GetRegrasProducao();

                    newG = false;
                }

                string palavra = "";
                int escolha = 0;
                List<int> entrada = new List<int>();

                Console.Clear();

                escolha = SelecionaOpcoes();                

                if (escolha == 1)
                {
                    GetEntradas(g, entrada);

                    string saida = decifrarPalavra(g.P0, g.P1, entrada);

                    if (g.Variaveis.Any(c => saida.Contains(c)))
                    {
                        Console.WriteLine($"\nNão foi possível decifrar a palavra. Saída: {saida}");
                    }
                    else
                    {
                        Console.WriteLine($"\nPalavra decifrada: {saida}");
                    }
                }

                if (escolha == 2)
                {
                    palavra = GetPalavra(g);

                    List<int> regras = new List<int>();

                    Node saida = derivarPalavra(g.P0, g.P1, g.Inicial, palavra);

                    if (saida != null)
                    {
                        Node n = saida;
                        while (n.palavra != g.Inicial)
                        {
                            regras.Add(n.regra + 1);
                            n = n.parent;
                        }
                        regras.Reverse();
                        Console.WriteLine($"\nPalavra decifrada: {saida.palavra}");
                        Console.WriteLine("\nRegras: " + string.Join(" ", regras));                        
                    }
                }

                repeatProg = NovaOperacao();

                if (repeatProg)
                {
                    newG = NovaGramatica();
                }
            }
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

        public static Node derivarPalavra(List<string> p0, List<string> p1, string inicial, string palavra)
        {
            int i;
            Node initial_node = new Node(inicial);
            Queue<Node> fila = new Queue<Node>();
            fila.Enqueue(initial_node);

            while (fila.Count > 0)
            {
                Node atual = fila.Dequeue();
                if (atual.palavra.Equals(palavra))
                {
                    return atual;
                }

                //monta os nós adjacentes ao nó atual, realizando a substituição da regra
                for (i = 0; i < p0.Count; i++)
                {
                    if (atual.palavra.Contains(p0[i]))
                    {
                        string palavra_atual = atual.palavra.Replace(p0[i], p1[i]);
                        bool check_fila = fila.Any(f => f.palavra == palavra_atual && f.parent.palavra == atual.palavra);
                        if (check_fila == false)
                        {
                            Node adj = new Node(palavra_atual, atual, i);
                            fila.Enqueue(adj);
                        }
                    }
                }
            }

            return null;
        }

        public static int SelecionaOpcoes()
        {
            bool repeat = true;
            int escolha = 0;

            while (repeat)
            {
                Console.WriteLine("\nSelecione uma das opções abaixo:\n\n1 - Decifrar palavra\n2 - Derivar palavra");
                escolha = System.Convert.ToInt32(Console.ReadLine());

                if (escolha != 1 && escolha != 2)
                {
                    Console.WriteLine("\n\tERRO: Opção Inválida!");
                    repeat = true;
                }
                else
                {
                    repeat = false;
                }
            }

            return escolha;
        }

        public static bool NovaOperacao()
        {
            bool repeat = true;
            string repetir = "";

            while (repeat)
            {
                Console.WriteLine($"\nDeseja fazer uma nova operação? (S/N)");
                repetir = Console.ReadLine();

                if (repetir == "S" || repetir == "s")
                {
                    repeat = false;
                    return true;
                }
                else if (repetir == "N" || repetir == "n")
                {
                    repeat = false;
                    return false;
                }
                else
                {
                    repeat = true;
                }
            }
            return false;
        }

        public static bool NovaGramatica()
        {
            bool repeat = true;
            string repetir = "";

            while (repeat)
            {
                Console.WriteLine($"\nDeseja fazer usar uma nova Gramatica? (S/N)");
                repetir = Console.ReadLine();

                if (repetir == "S" || repetir == "s")
                {
                    repeat = false;
                    return true;
                }
                else if (repetir == "N" || repetir == "n")
                {
                    repeat = false;
                    return false;
                }
                else
                {
                    repeat = true;
                }
            }
            return false;
        }

        public static string GetPalavra(Grammar g)
        {
            string palavra = "";
            bool repeat = true;
            while (repeat)
            {
                Console.WriteLine("\nDigite a palavra a ser derivada.");
                palavra = Console.ReadLine();

                if (palavra == "")
                {
                    Console.WriteLine("\n\tERRO: Por favor, digite uma palavra!");
                }
                else if(!AlfabetoValido(g, palavra))
                {
                    Console.WriteLine("\n\tERRO: A palavra deve conter apenas letras presentes no alfabeto!");
                }
                else
                {
                    repeat = false;
                }
            }
            return palavra;
        }

        public static void GetEntradas(Grammar g, List<int> entrada)
        {
            bool repeat = true;

            while (repeat)
            {
                Console.WriteLine("\nDigite os valores de entrada separados por vírgula. Ex: 1, 12, 7");
                g.Aux = Console.ReadLine().Replace(" ", string.Empty).Split(',').ToList();

                foreach (string s in g.Aux)
                {
                    if (s != "")
                    {
                        entrada.Add(Convert.ToInt32(s));
                    }
                }

                if (entrada.Count <= 0)
                {
                    Console.WriteLine("\n\tERRO: Por favor, digite ao menos um valor de entrada!");
                    entrada = new List<int>();
                }
                else if (entrada.Max() > g.P0.Count)
                {
                    Console.WriteLine("\n\tERRO: Por favor, digite apenas entradas válidas!");
                    entrada = new List<int>();
                }
                else if (g.P0[entrada[0] - 1] != g.Inicial)
                {
                    Console.WriteLine("\n\tERRO: Por favor, a primeira entrada deve possuir a variável Inicial!");
                    entrada = new List<int>();
                }
                else
                {
                    repeat = false;
                }
            }
        }

        public static bool AlfabetoValido(Grammar g, string palavra)
        {
            palavra = palavra.Replace(" ", string.Empty);
            char[] characters = palavra.ToCharArray();
            foreach (char c in characters)
            {
                if (!g.Alfabeto.Contains(c.ToString()))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
