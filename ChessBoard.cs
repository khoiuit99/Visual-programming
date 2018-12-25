using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doan
{
    public class ChessBoard
    {
        //Cài đặt các thông số cho bàn cờ, thanh progress bar, timer.
        public static int Chess_Width = 30; //Chiều dài của ô button
        public static int Chess_Height = 30; //Chiều cao của ô button
        public static int Chess_board_width = 17;//Chiều dài của bàn cờ
        public static int Chess_board_height = 16;//Chiều cao của bàn cờ
        public static int COOL_DOWN_INTERVAL = 100;//Thời gian của timer
        public static int COOL_DOWN_VALUE = 10000;//Giá trị của progress bar
        public static int COOL_DOWN_STEP = 100;//Giá trị nhảy bậc của progress bar
    }
}
