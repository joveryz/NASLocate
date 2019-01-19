using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NASLocate.Information
{
    public class SSHInfo : ObservableObject
    {
        private string _UserName;
        public string UserName
        {
            get { return _UserName; }
            set
            {
                _UserName = value;
                RaisePropertyChanged(() => UserName);
            }
        }

        private string _IP;
        public string IP
        {
            get { return _IP; }
            set
            {
                _IP = value;
                RaisePropertyChanged(() => IP);
            }
        }

        private string _Port;
        public string Port
        {
            get { return _Port; }
            set
            {
                _Port = value;
                RaisePropertyChanged(() => Port);
            }
        }

        private string _System;
        public string System
        {
            get { return _System; }
            set
            {
                _System = value;
                RaisePropertyChanged(() => System);
            }
        }

        private string _Type;
        public string Type
        {
            get { return _Type; }
            set
            {
                _Type = value;
                RaisePropertyChanged(() => Type);
            }
        }

        private string _Password;
        public string Password
        {
            get { return _Password; }
            set
            {
                _Password = value;
                RaisePropertyChanged(() => Password);
            }
        }

        public SSHInfo(string UserName, string IP, string Port, string System, string Type, string Password)
        {
            this.UserName = UserName;
            this.IP = IP;
            this.Port = Port;
            this.System = System;
            this.Type = Type;
            this.Password = Password;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (this.GetType() != obj.GetType())
                return false;
            var info = obj as SSHInfo;
            if (UserName == info.UserName && IP == info.IP && Port == info.Port && System == info.System && Type == info.Type && Password == info.Password)
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            return this.UserName.GetHashCode() ^ this.IP.GetHashCode() ^ this.Port.GetHashCode() ^ this.System.GetHashCode() ^ this.Type.GetHashCode() ^ this.Password.GetHashCode();
        }

        public override string ToString()
        {
            return UserName + " " + IP + " " + Port + " " + System + " " + Type + " " + Password + ",";
        }
    }
}
