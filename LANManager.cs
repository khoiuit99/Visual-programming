using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Doan
{
    //Hàm quản lí mạng LAN
    public class LANManager
    {
        #region Client
        Socket Client;
        //Hàm kết nối với server
        public bool ConnectServer()
        {
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse(IP), Port);
            Client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //Cố gắng để kết nối
            try
            {
                Client.Connect(iep);
                return true;
            }
            catch
            {
                return false;
            }          
        }
        #endregion
        #region Server
        Socket server;
        //Hàm tạo server
        public void CreatServer()
        {
            IPEndPoint iep = new IPEndPoint(IPAddress.Parse(IP), Port);
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            server.Bind(iep);
            server.Listen(10);

            //Tạo một luồng riêng để khởi tạo server
            Thread acceptClient = new Thread(() =>
            {
                Client = server.Accept();
            });

            //Để tránh chương trình bị cắt ngang mà kết nối không tắt, gây tốn tài nguyên
            acceptClient.IsBackground = true;
            acceptClient.Start();
        }
        #endregion
        #region Both
        public string IP = "127.0.0.1"; //IP
        public int Port = 9999; //Port
        public bool isServer = true; //kết nối server
        public const int BUFFER = 1024; //data
        //Hàm gửi
        public bool Send(object data)
        {
            byte[] sendData = SerializeData(data);
            return SendData(Client, sendData);
        }
        //Hàm nhận
        public object Reveive()
        {
            byte[] receiveData = new byte[BUFFER];

            //Xem xem nhận dữ liệu có thành công hay không?
            bool isOK = ReceiveData(Client, receiveData);
            return DeserializeData(receiveData);
        }
        //Hàm gửi dữ liệu
        private bool SendData(Socket target, byte[] data)
        {
            return target.Send(data) == 1 ? true : false;
        }
        //Hàm nhận dữ liệu
        private bool ReceiveData(Socket target, byte[] data)
        {
            return target.Receive(data) == 1 ? true : false;
        }
        //Hàm phân mảnh dữ liệu
        public byte[] SerializeData(Object o)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf1 = new BinaryFormatter();
            bf1.Serialize(ms, o);
            return ms.ToArray();
        }
        //Hàm gộp dữ liệu bị phân mảnh
        public object DeserializeData(byte[] theByteArray)
        {
            MemoryStream ms = new MemoryStream(theByteArray);
            BinaryFormatter bf1 = new BinaryFormatter();
            ms.Position = 0;
            return bf1.Deserialize(ms);
        }
        //Lấy ra địa chỉ IPv4 của card mạng đang dùng
        public string GetLocalIPv4(NetworkInterfaceType _type)
        {
            string output = "";
            foreach(NetworkInterface item in NetworkInterface.GetAllNetworkInterfaces())
            {
                if(item.NetworkInterfaceType == _type && item.OperationalStatus == OperationalStatus.Up)
                {
                    foreach(UnicastIPAddressInformation ip in item.GetIPProperties().UnicastAddresses)
                    {
                        if(ip.Address.AddressFamily == AddressFamily.InterNetwork)
                        {
                            output = ip.Address.ToString();
                        }
                    }
                }
            }
            return output;
        }
        #endregion
    }
}
