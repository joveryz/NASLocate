using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NASLocate.Information
{
    public class PathInfo : ObservableObject
    {
        private string _WindowsPath;
        public string WindowsPath
        {
            get { return _WindowsPath; }
            set
            {
                _WindowsPath = value;
                RaisePropertyChanged(() => WindowsPath);
            }
        }

        private string _LinuxPath;
        public string LinuxPath
        {
            get { return _LinuxPath; }
            set
            {
                _LinuxPath = value;
                RaisePropertyChanged(() => LinuxPath);
            }
        }

        public PathInfo(string LinuxPath , string WindowsPath)
        {
            this.WindowsPath = WindowsPath;
            this.LinuxPath = LinuxPath;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            if (this.GetType() != obj.GetType())
                return false;
            var info = obj as PathInfo;
            if (WindowsPath == info.WindowsPath && LinuxPath == info.LinuxPath)
                return true;
            return false;
        }

        public override int GetHashCode()
        {
            return this.WindowsPath.GetHashCode() ^ this.LinuxPath.GetHashCode();
        }

        public override string ToString()
        {
            return LinuxPath + " " + WindowsPath + ",";
        }
    }
}
