using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_courseProject
{
    class Container
    {
        List<Block> blocks;
        List<Pellet> pells;
        private static Container container;

        public List<Block> Blocks
        {
            get { return blocks; }
            set { blocks = value; }
        }

        public List<Pellet> Pells
        {
            get { return pells; }
            set { pells = value; }
        }

        private Container() { }

        public static Container onCreate()
        {
            if (container == null)
                container = new Container();
            return container;
        }
    }
}
