using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doan
{
    //Class người chơi
    public class Player
    {
        //Tên người chơi
        private string name;

        public string Name { get => name; set => name = value; }

        //Kí hiệu
        private Image mark;
        public Image Mark { get => mark; set => mark = value; }
        //Hàm dựng
        public Player(string name, Image mark)
        {
            this.Name = name;
            this.Mark = mark;
        }
    }
}
