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
            bool repeat = true, repeatProg = true;

            while (repeatProg)
            {
                string[] variaveis = { "" };
                string[] alfabeto;
                string inicial = "", palavra = "", repetir = "";
                List<string> p0 = new List<string>();
                List<string> p1 = new List<string>();
                List<string> aux = new List<string>();
                List<int> entrada = new List<int>();
                int escolha = 0;

                repeat = true;
                Console.Clear();
                while (repeat)
                {
                    Console.WriteLine("Insira os dados da Linguagem: \n");
                    Console.WriteLine("Digite as variáveis separadas por vírgula. Ex: X, Y, Z");
                    variaveis = Console.ReadLine().Replace(" ", string.Empty).Split(',');

                    if (variaveis.Length <= 0 || variaveis[0] == "")
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

                    foreach (string s in aux)
                    {
                        if (s.Length > 2)
                        {
                            p0.Add(s.Split('>')[0]);
                            p1.Add(s.Split('>')[1]);
                        }
                    }
                    if (p0.Count == 0)
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

                repeat = true;

                if (escolha == 1)
                {
                    while (repeat)
                    {
                        Console.WriteLine("\nDigite os valores de entrada separados por vírgula. Ex: 1, 12, 7");
                        aux = Console.ReadLine().Replace(" ", string.Empty).Split(',').ToList();

                        foreach (string s in aux)
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
                        else if (entrada.Max() > p0.Count)
                        {
                            Console.WriteLine("\n\tERRO: Por favor, digite apenas entradas válidas!");
                            entrada = new List<int>();
                        }
                        else if (p0[entrada[0] - 1] != inicial)
                        {
                            Console.WriteLine("\n\tERRO: Por favor, a primeira entrada deve possuir a variável inicial!");
                            entrada = new List<int>();
                        }
                        else
                        {
                            repeat = false;
                        }
                    }
                    string saida = decifrarPalavra(p0, p1, entrada);

                    if (variaveis.Any(c => saida.Contains(c)))
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

                    List<int> regras = new List<int>();

                    Node saida = derivarPalavra(p0, p1, inicial, palavra);

                    if (saida != null)
                    {
                        Node n = saida;
                        while (n.palavra != inicial)
                        {
                            regras.Add(n.regra + 1);
                            n = n.parent;
                        }
                        regras.Reverse();
                        Console.WriteLine($"\nPalavra decifrada: {saida.palavra}");
                        Console.WriteLine("\nRegras: " + string.Join(" ", regras));                        
                    }
                }

                repeat = true;

                while (repeat)
                {
                    Console.WriteLine($"\nDeseja fazer uma nova operação? (S/N)");
                    repetir = Console.ReadLine();

                    if (repetir == "S" || repetir == "s")
                    {
                        repeatProg = true;
                        repeat = false;
                    }
                    else if (repetir == "N" || repetir == "n")
                    {
                        repeatProg = false;
                        repeat = false;
                    }
                    else
                    {
                        repeat = true;
                    }
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
    }
}
