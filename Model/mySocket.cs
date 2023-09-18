using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HW_Client_Tcp_.Model
{
    internal class mySocket
    {
        Controller.Controller controller;
        Socket sock;
        public mySocket(Controller.Controller controller) => this.controller = controller;


        public void Connect()
        {

            try
            {
                IPAddress iPAddr = IPAddress.Loopback;
                controller.setMessageToFormstring(iPAddr.ToString());
                IPEndPoint iPEndPoint = new IPEndPoint(iPAddr, 49152);
                sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                sock.Connect(iPEndPoint);
                byte[] msg = Encoding.Default.GetBytes(Dns.GetHostName());
                int bytesSent = sock.Send(msg);
                controller.setMessageToFormstring("Клиент: " + Dns.GetHostName() + " установил соежинение с " + sock.RemoteEndPoint.ToString());

            }
            catch (Exception ex)
            {
                controller.setMessageToFormstring(ex.Message);
            }
        }

        public void Exchange()
        {
            try
            {
                string theMessage = controller.GetMessageFromForm();
                byte[] msg = Encoding.UTF8.GetBytes(theMessage);
                int bytesSent = sock.Send(msg);
                Thread thread = new Thread(() => WaitAnswerFromServer());
                thread.Start();

                if (theMessage.IndexOf("<end>") > -1)
                {
                    byte[] bytes = new byte[1024];
                    int bytesRec = sock.Receive(bytes);
                    controller.setMessageToFormstring("Сервер (" + sock.RemoteEndPoint.ToString() + ") ответил : " + Encoding.Default.GetString(bytes, 0, bytesRec));
                    sock.Shutdown(SocketShutdown.Both);
                    sock.Close();
                }
                
                
            }
            catch (Exception ex)
            {
                controller.setMessageToFormstring("Клиент: " + ex.Message);
            }
        }

        public void WaitAnswerFromServer()
        {
            
                byte[] msgAnsw = new byte[1024];
                int size;
                string data = "";
                while (true)
                {
                    size = sock.Receive(msgAnsw);
                    if (size == 0)
                    {
                        sock.Shutdown(SocketShutdown.Both);
                        sock.Close();
                        return;
                    }
                    data = Encoding.UTF8.GetString(msgAnsw, 0, size);
                    controller.setMessageToFormstring(data);

                }


           
           
        }

        public bool SocketIsNull() => sock == null ? true : false;

        public void CloseSocket()
        {
            if (sock != null)
            {
                sock.Shutdown(SocketShutdown.Both);
                sock.Close();
            }

        }



     

  

    }
}
