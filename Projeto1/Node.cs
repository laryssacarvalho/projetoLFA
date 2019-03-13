using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto1
{
    class Node
    {
        public Node parent { get; set; }
        public string palavra;
        public int regra { get; set; }
        public Node(string palavra, Node parent, int regra_anterior)
        {
            this.palavra = palavra;
            this.parent = parent;
            this.regra = regra_anterior;       
        }

        public Node(string palavra)
        {
            this.palavra = palavra;
        }

        public Node()
        {

        }
    }
}
