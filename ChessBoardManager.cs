using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Doan
{
    public class ChessBoardManager
    {
        //Các tính chất
        #region Properties
        private Panel Play;
        public Panel Play1 { get => Play; set => Play = value; }

        private List<Player> player;
        public List<Player> Player { get => player; set => player = value; }

        private int currentPlayer;
        private int CurrentPlayer { get => currentPlayer; set => currentPlayer = value; }
   
        private TextBox PlayerName;
        public TextBox PlayerName1 { get => PlayerName; set => PlayerName = value; }
        
        private PictureBox PlayerMark;
        public PictureBox PlayerMark1 { get => PlayerMark; set => PlayerMark = value; }

        private List<List<Button>> matrix;
        public List<List<Button>> Matrix { get => matrix; set => matrix = value; }
        
        //Sự kiện người chơi đánh vào ô cờ
        private event EventHandler<buttonclickevent> playermarked;
        public event EventHandler<buttonclickevent> PlayerMarked
        {
            add
            {
                playermarked += value;
            }
            remove
            {
                playermarked -= value;
            }
        }
        //Sự kiện kết thúc game
        private event EventHandler endedgame;
        public event EventHandler endgame
        {
            add
            {
                endedgame += value;
            }
            remove
            {
                endedgame -= value;
            }
        }
        #endregion
        //Hàm khởi tạo
        #region Initialize
        //Hàm tạo người chơi, tên người chơi, kí hiệu cho người chơi
        public ChessBoardManager(Panel play, TextBox playerName, PictureBox Mark)
        {
            this.Play = play;
            this.PlayerName1 = playerName;
            this.PlayerMark1 = Mark;
            this.Player = new List<Player>()//Khởi tạo 2 người chơi = list player
            {
                new Player("Player1", Image.FromFile(@".\X.png")),
                new Player("Player2", Image.FromFile(@".\O.png"))
            };
            
        }
        #endregion
        //Các hàm phương thức
        #region Methods
        //Hàm vẽ bàn cờ
        public void DrawChessBoard()
        {
            //Cho người chơi hiện tại = 0
            CurrentPlayer = 0;
            //Hàm hiện thông tin hiện tại của người đang đánh
            ChangePlayer();
            //Cho phép người chơi đánh
            Play.Enabled = true;
            //Tạo một list button
            matrix = new List<List<Button>>();
            //Button cũ (button trước đó)
            Button oldButton = new Button() { Width = 0, Location = new Point(0, 0) };
            //Vẽ mảng button (các ô cờ)
            for (int i = 0; i < ChessBoard.Chess_board_height; i++)
            {
                //Tạo một list các button
                Matrix.Add(new List<Button>());
                for (int j = 0; j < ChessBoard.Chess_board_width; j++)
                {
                    //Khởi tạo button
                    Button btn = new Button()
                    {
                        //Các thông số cho các button
                        Width = ChessBoard.Chess_Width,
                        Height = ChessBoard.Chess_Height,
                        Location = new Point(oldButton.Location.X + oldButton.Width, oldButton.Location.Y),
                        BackgroundImageLayout = ImageLayout.Stretch,//giúp kí hiệu nằm trong không gian của ô cờ
                        Tag = i.ToString()//Lưu vị trí của button
                    };
                    btn.Click += btn_Click;//Sự kiện click cho button
                    Play.Controls.Add(btn);//ADD các button vào panel
                    matrix[i].Add(btn);//ADD các button vào matrix
                    oldButton = btn;//Gán oldbutton = button
                }
                oldButton.Location = new Point(0, oldButton.Location.Y + ChessBoard.Chess_Height);//Set lại vị trí mới của oldbutton khi hết hàng(xuống dòng)
                //Set lại kích thước của oldbutton
                oldButton.Width = 0;
                oldButton.Height = 0;
            }

        }
        //Hàm xóa panel cũ sau khi ấn Play again
        public void DeletePanel()
        {
            Play.Controls.Clear();
        }
        //SỰ kiện click vào các ô button
        private void btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            //Nếu button đã được người chơi đánh thì sẽ không đánh chèn vô được nữa
            if (btn.BackgroundImage != null)
                return;
            //Hàm hiện kí hiệu hiện tại của người đánh
            Mark(btn);
 
            currentPlayer = CurrentPlayer == 1 ? 0 : 1; //Nếu người chơi = 1 thì chuyển sang 0, nếu 0 thì chuyển sang 1

            ChangePlayer();//Hàm hiện thông tin hiện tại của người đang đánh

            if (playermarked != null)
                playermarked(this, new buttonclickevent(GetChessPoint(btn)));

            //Nếu kết thúc game
            if(isEndGame(btn))
            {
                EndGame();
            }
        }
        //Hàm hiện kí hiệu hiện tại của người đánh
        private void Mark(Button btn)
        {
            btn.BackgroundImage = Player[CurrentPlayer].Mark;
        }
        //Hàm hiện thông tin hiện tại của người đang đánh
        private void ChangePlayer()
        {
            PlayerName1.Text = Player[CurrentPlayer].Name;//Hàm hiện tên hiện tại của người chơi

            PlayerMark1.Image = Player[CurrentPlayer].Mark;//Hàm hiện tên kí hiệu của người chơi
        }
        //Hàm đối với người chơi thứ 2 khi người chơi thứ nhất đánh
        public void OtherPlayer(Point point)
        {
            //Xác định vị trí của button của người chơi thứ 2 mới đánh
            Button btn = Matrix[point.Y][point.X];
            //Nếu button đó đã bị đánh thì quay lại
            if (btn.BackgroundImage != null)
                return;

            Mark(btn);

            currentPlayer = CurrentPlayer == 1 ? 0 : 1;

            ChangePlayer();

            if (isEndGame(btn))
            {
                EndGame();
            }
        }
        //Hàm ENDGAME (xử lý thắng thua)
        public void EndGame()
        {
            if (endedgame != null)
                endedgame(this, new EventArgs());
        }
        //Lấy tọa độ của button
        private Point GetChessPoint(Button btn)
        {       
            int vertical = Convert.ToInt32(btn.Tag);
            int horizontal = matrix[vertical].IndexOf(btn);
            Point point = new Point(horizontal, vertical);
            return point;
        }
        //Hàm kết thúc game
        private bool isEndGame(Button btn)
        {
            //Khi một trong 4 trường hợp xảy ra: thắng ngang, thắng thẳng, thắng đường chéo chính, thắng đường chéo phụ
            return isEndHorizontal(btn) || isEndVertical(btn) || isEndDiagonal(btn) || isEndSub(btn);
        }
        //Hàm thắng ngang
        private bool isEndHorizontal(Button btn)
        {
            //Xét vị trí của button
            Point point = GetChessPoint(btn);
            int countLeft = 0;
            for (int i = point.X; i >= 0 ; i--)
            {
                if (matrix[point.Y][i].BackgroundImage == btn.BackgroundImage)
                {
                    countLeft++;
                }
                else
                    break;
            }
            int countRight = 0;
            for (int i = point.X + 1; i < ChessBoard.Chess_board_width ; i++)
            {
                if (matrix[point.Y][i].BackgroundImage == btn.BackgroundImage)
                {
                    countRight++;
                }
                else
                    break;
            }
            return countLeft + countRight == 5;
        }
        //Hàm thắng dọc
        private bool isEndVertical(Button btn)
        {
            Point point = GetChessPoint(btn);
            int countTop = 0;
            for (int i = point.Y; i >= 0; i--)
            {
                if (matrix[i][point.X].BackgroundImage == btn.BackgroundImage)
                {
                    countTop++;
                }
                else
                    break;
            }
            int countbottom = 0;
            for (int i = point.Y + 1; i < ChessBoard.Chess_board_height; i++)
            {
                if (matrix[i][point.X].BackgroundImage == btn.BackgroundImage)
                {
                    countbottom++;
                }
                else
                    break;
            }
            return countTop + countbottom == 5;
        }
        //Hàm thắng đường chéo chính
        private bool isEndDiagonal(Button btn)
        {
            Point point = GetChessPoint(btn);
            int countTop = 0;
            for (int i = 0; i <= point.X; i++)
            {
                if (point.Y - i < 0 || point.X - i < 0)
                    break;
                if (matrix[point.Y - i][point.X - i].BackgroundImage == btn.BackgroundImage)
                {
                    countTop++;
                }
                else
                    break;
            }
            int countbottom = 0;
            for (int i = 1; i <= ChessBoard.Chess_board_width - point.X; i++)
            {
                if (point.X + i >= ChessBoard.Chess_board_width || point.Y + i >= ChessBoard.Chess_board_height)
                    break;
                if (matrix[point.Y + i][point.X + i].BackgroundImage == btn.BackgroundImage)
                {
                    countbottom++;
                }
                else
                    break;
            }
            return countTop + countbottom == 5;
        }
        //Hàm thắng đường chéo phụ
        private bool isEndSub(Button btn)
        {
            Point point = GetChessPoint(btn);
            int countTop = 0;
            for (int i = 0; i <= point.X; i++)
            {
                if (point.Y - i < 0 || point.X + i >= ChessBoard.Chess_board_width)
                    break;
                if (matrix[point.Y - i][point.X + i].BackgroundImage == btn.BackgroundImage)
                {
                    countTop++;
                }
                else
                    break;
            }
            int countbottom = 0;
            for (int i = 1; i <= ChessBoard.Chess_board_width - point.X; i++)
            {
                if (point.X - i < 0 || point.Y + i >= ChessBoard.Chess_board_height)
                    break;
                if (matrix[point.Y + i][point.X - i].BackgroundImage == btn.BackgroundImage)
                {
                    countbottom++;
                }
                else
                    break;
            }
            return countTop + countbottom == 5;
        }
        #endregion

    }
    //Sự kiện click
    public class buttonclickevent : EventArgs
    {
        private Point clickedPoint;

        public Point ClickedPoint { get => clickedPoint; set => clickedPoint = value; }
        public buttonclickevent(Point point)
        {
            this.ClickedPoint = point;
        }
    }
}
