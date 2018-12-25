using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace Doan
{
    public partial class Caroplayer : Form
    {
        #region Properties
        ChessBoardManager chessBoard;
        LANManager socket;
        #endregion
        public Caroplayer()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();

            //Set các thuộc tính cho progress bar
            prgcooldown.Step = ChessBoard.COOL_DOWN_STEP;
            prgcooldown.Value = 0;
            prgcooldown.Maximum = ChessBoard.COOL_DOWN_VALUE;

            //Set time interval cho timer
            timecooldown.Interval = ChessBoard.COOL_DOWN_INTERVAL;


            chessBoard = new ChessBoardManager(pnplay, textName, picMark);

            //Không cho người chơi đổi tên
            textName.Enabled = false;

            //Các sự kiện của chessBoard
            chessBoard.endgame += ChessBoard_endgame;//Sự kiện endgame
            chessBoard.PlayerMarked += ChessBoard_PlayerMarked;//Sự kiện người chơi đánh vào ô cờ

            chessBoard.DrawChessBoard();//Vẽ bàn cờ

            socket = new LANManager();//Tạo socket
        }
        #region Methods
        //sự kiện người chơi đánh vào bàn cờ
        private void ChessBoard_PlayerMarked(object sender, buttonclickevent e)
        {
            //Timer bắt đầu chạy
            timecooldown.Start();

            //Bàn cờ sẽ không được phép đánh cho đến khi người chơi bên kia đánh
            pnplay.Enabled = false;

            //Cho giá trị ban đầu của progress bar = 0
            prgcooldown.Value = 0;

            //socket gửi dữ liệu SEND_POINT cho máy bên kia
            socket.Send(new SocketData((int)SocketCommand.SEND_POINT, "", e.ClickedPoint));

            //Tiến hành lắng nghe
            Listen();
        }
        //Hàm endgame
        void EndGame()
        {
            //Timer dừng đếm
            timecooldown.Stop();

            //Bàn cờ không cho phép đánh 
            pnplay.Enabled = false;
        }
        //Sự kiện endgame
        private void ChessBoard_endgame(object sender, EventArgs e)
        {
            //HÀm endgame
            EndGame();

            //socket gửi dữ liệu END_GAME
            socket.Send(new SocketData((int)SocketCommand.END_GAME, "", new Point()));
        }
        //Hàm Timer_Tick
        private void timer1_Tick(object sender, EventArgs e)
        {
            //Nếu giá trị của progress bar lớn hơn giá trị max của progress bar (tức là hết thời gian)
            if (prgcooldown.Value >= prgcooldown.Maximum)
            {
                //EndGame
                EndGame();

                //Socket gửi dữ liệu TIME_OUT
                socket.Send(new SocketData((int)SocketCommand.TIME_OUT, "", new Point()));
            }

            prgcooldown.PerformStep();
        }
        //Hàm khởi tạo trò chơi mới
        void NewGame()
        {
            //Giá trị của progress bar về 0
            prgcooldown.Value = 0;

            //Timer dừng đếm
            timecooldown.Stop();

            //Xóa bàn cờ hiện tại
            chessBoard.DeletePanel();

            //Vẽ lại bàn cờ mới
            chessBoard.DrawChessBoard();
        }
        //Sự kiện click vào button "quit"
        private void btnquit_Click(object sender, EventArgs e)
        {
            //Hiện thông báo
            if (MessageBox.Show("Bạn có muốn thoát không?", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                //Exit
                Application.Exit();

                //Socket gửi dữ liệu QUIT
                socket.Send(new SocketData((int)SocketCommand.QUIT, "", new Point()));
            }
        }
        //Sự kiện click vào button "play again"
        private void btnagain_Click(object sender, EventArgs e)
        {
            //Hàm NewGame
            NewGame();

            //Socket gửi dữ liệu NEW_GAME
            socket.Send(new SocketData((int)SocketCommand.NEW_GAME, "", new Point()));
        }
        //Sự kiện click vào button "Connect LAN"
        private void btnLAN_Click(object sender, EventArgs e)
        {
            //Địa chỉ IP của socket sẽ được đưa vào textbox LAN
            socket.IP = txbLAN.Text;

            //Nếu socket không kết nối được với server
            if (!socket.ConnectServer())
            {
                //Đây là server
                socket.isServer = true;

                //Bàn cờ sẽ được cho server đánh
                pnplay.Enabled = true;

                //Socket khởi tạo server
                socket.CreatServer();
            }
            else
            {
                //Đây là client
                socket.isServer = false;

                //Bàn cờ sẽ không cho đánh, đợi khi nào server đánh thì client mới được đánh
                pnplay.Enabled = false;

                //Lắng nghe
                Listen();
            }
        }

        //Sự kiện Form Shown
        private void Caroplayer_Shown(object sender, EventArgs e)
        {
            //Textbox LAN sẽ đưa ra ID nếu đây là kết nối wifi
            txbLAN.Text = socket.GetLocalIPv4(NetworkInterfaceType.Wireless80211);

            //Nếu không có wifi(textbox LAN == null)
            if (string.IsNullOrEmpty(txbLAN.Text))
            {
                //textbox LAN sẽ lấy ID của mạng dây (Ethernet)
                txbLAN.Text = socket.GetLocalIPv4(NetworkInterfaceType.Ethernet);
            }
        }
        //Hàm lắng nghe
        void Listen()
        {
            //Cho việc listen nằm ra một luồng riêng
            Thread listenThread = new Thread(() =>
            {
                //Cố gắng nhận dữ liệu từ server 
                try
                {
                    SocketData data = (SocketData)socket.Reveive();
                    ProcessData(data);
                }
                catch
                {

                }
            });
            listenThread.IsBackground = true;
            listenThread.Start();
        }
        //Hàm xử lý dữ liệu 
        private void ProcessData(SocketData data)
        {
            //Các trường hợp được liệt kê
            switch (data.Command)
            {
                //Các thông báo
                case (int)SocketCommand.NOTIFY:
                    MessageBox.Show(data.Message);
                    break;
                //Thông báo chơi lại
                case (int)SocketCommand.NEW_GAME:
                    //Dùng invoke để cho việc thay đổi giao diện được thực hiện
                    this.Invoke((MethodInvoker)(() =>
                    {
                        NewGame();
                    }));
                    break;
                //truyền ô cờ được đi từ người chơi
                case (int)SocketCommand.SEND_POINT:
                    this.Invoke((MethodInvoker)(() =>
                    {
                        prgcooldown.Value = 0;
                        pnplay.Enabled = true;
                        timecooldown.Start();
                        chessBoard.OtherPlayer(data.Point);
                    }));
                    break;
                //End game khi có 5 con
                case (int)SocketCommand.END_GAME:
                    MessageBox.Show("Đã kết thúc game!!!");
                    break;
                //Endgame khi hết thời gian
                case (int)SocketCommand.TIME_OUT:
                    MessageBox.Show("Hết giờ!!!");
                    break;
                //Thoát
                case (int)SocketCommand.QUIT:
                    this.Invoke((MethodInvoker)(() =>
                    {
                        timecooldown.Stop();
                        MessageBox.Show("Người chơi đã thoát!!!");
                    }));
                    break;
                default:
                    break;
            }
            Listen();
        }
    }
}
    #endregion
