using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NASLocate.Information
{
    public class SearchInfo : ObservableObject
    {
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set
            {
                _Name = value;
                RaisePropertyChanged(() => Name);
            }
        }

        private string _Path;
        public string Path
        {
            get { return _Path; }
            set
            {
                _Path = value;
                RaisePropertyChanged(() => Path);
            }
        }

        private string _Size;
        public string Size
        {
            get { return _Size; }
            set
            {
                _Size = value;
                RaisePropertyChanged(() => Size);
            }
        }

        private string _DateModified;
        public string DateModified
        {
            get { return _DateModified; }
            set
            {
                _DateModified = value;
                RaisePropertyChanged(() => DateModified);
            }
        }

        public SearchInfo(string Name, string Path, String Size, String DateModified)
        {
            this.Name = Name;
            this.Path = Path;
            this.Size = Size;
            this.DateModified = DateModified;
        }
    }
}
