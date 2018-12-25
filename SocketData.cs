using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doan
{
    //Class dùng để truyền dữ liệu
    [Serializable]
    public class SocketData
    {
        private int command;
        public int Command { get => command; set => command = value; }
        

        private Point point;
        public Point Point { get => point; set => point = value; }

        private string message;
        public string Message { get => message; set => message = value; }
        
        //Hàm dựng
        public SocketData(int command, string message, Point point)
        {
            this.Command = command;
            this.Point = point;
            this.Message = message;
        }       
    }
    
    //Hàm liệt kê các command mà socket sẽ truyền dữ liệu
    public enum SocketCommand
    {
        SEND_POINT,
        NOTIFY,
        NEW_GAME,
        END_GAME,
        TIME_OUT,
        QUIT
    }
}
