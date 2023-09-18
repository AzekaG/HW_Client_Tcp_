using HW_Client_Tcp_.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HW_Client_Tcp_.Controller
{
    internal class Controller
    {
        Form1 form;
        mySocket mySock;
        public Controller(Form1 form1)
        {
            this.form = form1;
            mySock = new mySocket(this);
        }
        public void setMessageToFormstring (string text)
        {
            form.IniTextBox(text);
        }
        public string GetMessageFromForm()
        {
            return form.GetMassageFromTextBox();
        }

        public bool SocketIsNull() => mySock.SocketIsNull();
        public void CloseSocket()
        { 
            if (!SocketIsNull()) mySock.CloseSocket(); 
        }
       
       
        public void Exchange()=>mySock.Exchange();
        public void Connect() =>mySock.Connect();
    }
}
