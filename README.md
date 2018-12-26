# sskisme
    
      Hàm xử lý thắng thua (thắng đường dọc, thắng đường ngang, thắng đường chéo chính, thắng đường chéo phụ)
* Vì để chiến thắng trong game caro thì cần 5 điểm nằm trên 1 đường thẳng, dựa trên điều đó, ta có thể làm ra thuật toán xử lý thắng thua
* Hàm thắng dọc
 //Hàm thắng dọc
        private bool isEndVertical(Button btn)
        {
            Point point = GetChessPoint(btn);
            int count top = 0;
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
        * Hàm thắng ngang
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
* Hàm thắng đường chéo chính
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
* Hàm thắng đường chéo phụ
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



 
