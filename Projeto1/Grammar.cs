using System;
using System.Collections.Generic;
using System.Linq;

namespace Projeto1
{
    class Grammar
    {
        public string[] Variaveis { get; set; }
        
        public string[] Alfabeto { get; set; }

        public string Inicial { get; set; }

        public List<string> P0  { get; set; }

        public List<string> P1 { get; set; }

        public List<string> Aux { get; set; }

        public Grammar()
        {
            Variaveis = new string[] { "" };
            Inicial = "";
            P0 = new List<string>();
            P1 = new List<string>();
            Aux = new List<string>();
        } 
        
        public void GetVariaveis()
        {
            bool repeat = true;

            while (repeat)
            {
                Console.WriteLine("Insira os dados da Linguagem: \n");
                Console.WriteLine("Digite as variáveis separadas por vírgula. Ex: X, Y, Z");
                Variaveis = Console.ReadLine().Replace(" ", string.Empty).Split(',');

                repeat = CheckVariaveis();
            }
        }

        public bool CheckVariaveis()
        {
            if (Variaveis.Length <= 0 || Variaveis[0] == "")
            {
                Console.WriteLine("\n\tERRO: Por favor, digite ao menos uma variável!\n");
                return true;
            }
            else
            {
                return false;
            }
        }

        public void GetAlfabeto()
        {
            bool repeat = true;

            while (repeat)
            {
                Console.WriteLine("\nDigite o Alfabeto separado por vírgula. Ex: a, b, c");
                Alfabeto = Console.ReadLine().Replace(" ", string.Empty).Split(',');
                repeat = CheckAlfabeto();
            }
        }

        public bool CheckAlfabeto()
        {
            if (Alfabeto.Length <= 0 || Alfabeto[0] == "")
            {
                Console.WriteLine("\n\tERRO: Por favor, digite ao menos um item para o Alfabeto!");
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public void GetInicial()
        {
            bool repeat = true;

            while (repeat)
            {
                Console.WriteLine("\nDigite a variável Inicial");
                Inicial = Console.ReadLine().Replace(" ", string.Empty);
                repeat = CheckInicial();
            }
        }

        public bool CheckInicial()
        {
            if (string.IsNullOrEmpty(Inicial))
            {
                Console.WriteLine("\n\tERRO: Por favor, insira a variável Inicial!");
                return true;
            }
            else if (!Variaveis.Contains(Inicial))
            {
                Console.WriteLine("\n\tERRO: Por favor, a variável Inicial deve ser uma das variáveis!");
                Console.WriteLine("\nRegras: " + string.Join(" ", Variaveis));
                return true;
            }
            else
            {
                return false;
            }
        }

        public void GetRegrasProducao()
        {
            bool repeat = true;

            while (repeat)
            {
                Console.WriteLine("\nDigite as regras de produção seguindo o exemplo: S>AB, A>aA, Bb>bB");
                Aux = Console.ReadLine().Replace(" ", string.Empty).Split(',').ToList();

                foreach (string s in Aux)
                {
                    if (s.Length > 2)
                    {
                        P0.Add(s.Split('>')[0]);
                        P1.Add(s.Split('>')[1]);
                    }
                }

                repeat = CheckRegrasProducao();
            }
        }

        public bool CheckRegrasProducao()
        {
            if (P0.Count == 0)
            {
                Console.WriteLine("\n\tERRO: Por favor, insira pelo menos uma regra de produção!");
                P0 = new List<string>();
                P1 = new List<string>();
                return true;
            }
            else if (!P0.Contains(Inicial))
            {
                Console.WriteLine("\n\tERRO: Por favor, ao menos uma regra deve conter a variável Inicial!");
                P0 = new List<string>();
                P1 = new List<string>();
                return true;
            }
            else
            {
                return false;
            }            
        }
    }
}
