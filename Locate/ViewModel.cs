using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using NASLocate.Util;
using NASLocate.Information;
using System.Collections;
using Microsoft.Win32;
using Renci.SshNet;
using System.Collections.Generic;
using Renci.SshNet.Common;
using System.Net.Sockets;
using System.IO;
using Locate;
using System.Collections.ObjectModel;

namespace NASLocate
{
    public class ViewModel : ObservableObject
    {

        private ObservableCollection<SshClient> _SSHNASClientList;
        public ObservableCollection<SshClient> SSHNASClientList
        {
            get { return _SSHNASClientList; }
            set
            {
                _SSHNASClientList = value;
                RaisePropertyChanged(() => SSHNASClientList);
            }
        }
        private ObservableCollection<SshClient> _SSHWSLClientList;
        public ObservableCollection<SshClient> SSHWSLClientList
        {
            get { return _SSHWSLClientList; }
            set
            {
                _SSHWSLClientList = value;
                RaisePropertyChanged(() => SSHWSLClientList);
            }
        }
        
        public RelayCommand ConfigCommand { get; private set; }

        #region LocateConfig
        private bool _IgnoreCaseDist;
        public bool IgnoreCaseDist
        {
            get { return _IgnoreCaseDist; }
            set
            {
                _IgnoreCaseDist = value;
                RaisePropertyChanged(() => IgnoreCaseDist);
            }
        }

        private bool _LimitOutputNum;
        public bool LimitOutputNum
        {
            get { return _LimitOutputNum; }
            set
            {
                _LimitOutputNum = value;
                RaisePropertyChanged(() => LimitOutputNum);
            }
        }

        private string _LimitNum;
        public string LimitNum
        {
            get { return _LimitNum; }
            set
            {
                _LimitNum = value;
                RaisePropertyChanged(() => LimitNum);
            }
        }
        #endregion

        #region SearchConfig
        private bool _SearchInNAS;
        public bool SearchInNAS
        {
            get { return _SearchInNAS; }
            set
            {
                _SearchInNAS = value;
                RaisePropertyChanged(() => SearchInNAS);
            }
        }

        private bool _SearchInWSL;
        public bool SearchInWSL
        {
            get { return _SearchInWSL; }
            set
            {
                _SearchInWSL = value;
                RaisePropertyChanged(() => SearchInWSL);
            }
        }
        #endregion

        #region SSHConfig
        private string _SSHUserName;
        public string SSHUserName
        {
            get { return _SSHUserName; }
            set
            {
                _SSHUserName = value;
                RaisePropertyChanged(() => SSHUserName);
            }
        }

        private string _SSHPort;
        public string SSHPort
        {
            get { return _SSHPort; }
            set
            {
                _SSHPort = value;
                RaisePropertyChanged(() => SSHPort);
            }
        }

        private string _SSHIP;
        public string SSHIP
        {
            get { return _SSHIP; }
            set
            {
                _SSHIP = value;
                RaisePropertyChanged(() => SSHIP);
            }
        }

        private string _SSHPassword;
        public string SSHPassword
        {
            get { return _SSHPassword; }
            set
            {
                _SSHPassword = value;
                RaisePropertyChanged(() => SSHPassword);
            }
        }

        private string _SSHSystem;
        public string SSHSystem
        {
            get { return _SSHSystem; }
            set
            {
                _SSHSystem = value;
                RaisePropertyChanged(() => SSHSystem);
            }
        }

        private string _SSHType;
        public string SSHType
        {
            get { return _SSHType; }
            set
            {
                _SSHType = value;
                RaisePropertyChanged(() => SSHType);
            }
        }

        private bool _SSHReset;
        public bool SSHReset
        {
            get { return _SSHReset; }
            set
            {
                _SSHReset = value;
                RaisePropertyChanged(() => SSHReset);
            }
        }

        public RelayCommand SSHResetCommand { get; set; }
        public RelayCommand SSHTestCommand { get; set; }
        public RelayCommand SSHSaveCommand { get; set; }
        #endregion

        #region SSHList
        public ObservableHashSet<string> SSHSystemList { get; set; }
        public ObservableHashSet<string> SSHTypeList { get; set; }

        private ObservableHashSet<SSHInfo> _SSHInfoHashSet;
        public ObservableHashSet<SSHInfo> SSHInfoHashSet
        {
            get { return _SSHInfoHashSet; }
            set
            {
                _SSHInfoHashSet = value;
                RaisePropertyChanged(() => SSHInfoHashSet);
            }
        }

        public RelayCommand<SSHInfo> SSHInfoDeleteCommand { get; set; }
        #endregion

        #region PathConfig
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
        public RelayCommand PathResetCommand { get; set; }
        public RelayCommand PathSaveCommand { get; set; }
        #endregion

        #region PathList
        private ObservableHashSet<PathInfo> _PathInfoHashSet;
        public ObservableHashSet<PathInfo> PathInfoHashSet
        {
            get { return _PathInfoHashSet; }
            set
            {
                _PathInfoHashSet = value;
                RaisePropertyChanged(() => PathInfoHashSet);
            }
        }

        public RelayCommand<PathInfo> PathInfoDeleteCommand { get; set; }
        #endregion

        #region ConfigFile
        private string _ConfigFilePath;
        public string ConfigFilePath
        {
            get { return _ConfigFilePath; }
            set
            {
                _ConfigFilePath = value;
                RaisePropertyChanged(() => ConfigFilePath);
            }
        }

        public RelayCommand SSHConnectCommand { get; set; }
        public RelayCommand ConfigFileImportCommand { get; set; }
        public RelayCommand ConfigFileSaveCommand { get; set; }
        public RelayCommand ConfigFileClearCommand { get; set; }
        #endregion

        #region SearchField
        private string _SearchText;
        public string SearchText
        {
            get { return _SearchText; }
            set
            {
                _SearchText = value;
                RaisePropertyChanged(() => SearchText);
            }
        }

        public RelayCommand<SearchInfo> OpenFolderPathCommand { get; set; }
        public ObservableHashSet<SearchInfo> SearchInfoHashSet { get; set; }

        #endregion

        #region InitMethods
        private void ResetLocateVars()
        {
            IgnoreCaseDist = true;
            LimitOutputNum = false;
            LimitNum = "0";
        }

        private void ResetSearchVars()
        {
            SearchInNAS = true;
            SearchInWSL = true;
        }

        private void ResetSSHVars()
        {
            SSHUserName = "root";
            SSHIP = "192.168.";
            SSHPort = "22";
            SSHReset = true;
            SSHSystem = "Linux";
            SSHType = "NAS";
        }

        private void ResetPathVars()
        {
            WindowsPath = "WindowsPath";
            LinuxPath = "LinuxPath";
            
        }

        private void ResetConfigVars()
        {
            ConfigFilePath = System.Environment.CurrentDirectory+"\\naslocate.config";
        }

        private void ResetSearchFieldVars()
        {
            SearchText = "";
        }

        private void SetCommandAndHashSet()
        {

            SSHSystemList = new ObservableHashSet<string>() { "Linux", "Unix" };
            SSHTypeList = new ObservableHashSet<string>() { "NAS", "WSL" };
            
            SSHResetCommand = new RelayCommand(ResetSSHVars);
            SSHTestCommand = new RelayCommand(SSHTest);
            SSHSaveCommand = new RelayCommand(SSHSave);
            SSHInfoDeleteCommand = new RelayCommand<SSHInfo>(SSHInfoDelete);

            PathInfoHashSet = new ObservableHashSet<PathInfo>();
            SSHInfoHashSet = new ObservableHashSet<SSHInfo>();
            
            PathResetCommand = new RelayCommand(ResetPathVars);
            PathSaveCommand = new RelayCommand(PathSave);
            PathInfoDeleteCommand = new RelayCommand<PathInfo>(PathInfoDelete);

            SSHConnectCommand = new RelayCommand(SSHConnect);
            ConfigFileImportCommand = new RelayCommand(ConfigFileImport);
            ConfigFileSaveCommand = new RelayCommand(ConfigFileSave);
            ConfigFileClearCommand = new RelayCommand(ConfigFileClear);

            OpenFolderPathCommand = new RelayCommand<SearchInfo>(OpenFolderPath);
            SearchInfoHashSet = new ObservableHashSet<SearchInfo>();

            ConfigCommand = new RelayCommand(OnConfig);
        }

        public void SSHConnect()
        {
            SSHNASClientList = new ObservableCollection<SshClient>();
            SSHWSLClientList = new ObservableCollection<SshClient>();
            foreach (SSHInfo i in SSHInfoHashSet)
            {
                SshClient ssh = new SshClient(i.IP, int.Parse(i.Port), i.UserName, i.Password);
                try
                {
                    ssh.Connect();
                    if (i.Type == "NAS")
                    {
                        SSHNASClientList.Add(ssh);
                        if (MessageBox.Show("Updatedb in " + i.IP + "?", "Confirm", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                        {
                            string res = SSH.SSHExcute(ssh, "./uplocate.sh");
                            MessageBox.Show("NAS connection: updadb fininshed!", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                    else
                    {
                        SSHWSLClientList.Add(ssh);
                        if (MessageBox.Show("Updatedb in " + i.IP + "?", "Confirm", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                        {
                            SSH.SSHExcute(ssh, "updatedb");
                            MessageBox.Show("WSL connection: updadb fininshed!", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("SSH connection failed!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            MessageBox.Show("NAS connection: " + SSHNASClientList.Count + "\nWSL connection: " + SSHWSLClientList.Count, "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SetAll()
        {
            ResetLocateVars();
            ResetSearchVars();
            ResetSSHVars();
            ResetPathVars();
            ResetConfigVars();
            ResetSearchFieldVars();
            SetCommandAndHashSet();
        }

        public ViewModel()
        {
            SetAll();
            ReadConfigFromFile(ConfigFilePath);
            SSHReset = false;

            
            SSHConnect();
        }

        private void OnConfig()
        {
            ConfigWindow configWindow = new ConfigWindow(this);
            configWindow.Show();
        }
        #endregion

        private void SSHTest()
        {
            SshClient ssh = new SshClient(SSHIP, int.Parse(SSHPort), SSHUserName, SSHPassword);
            try
            {
                ssh.Connect();
            }
            catch (Exception)
            {
                MessageBox.Show("SSH connection failed!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MessageBox.Show("SSH connected!", "SUCCESS", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void SSHSave()
        {
            if (SSHUserName != null && SSHIP != null && SSHPort != null && SSHSystem != null && SSHType != null && SSHPassword != null)
            {
                var newSSHInfo = new SSHInfo(SSHUserName, SSHIP, SSHPort, SSHSystem, SSHType, SSHPassword);
                SSHInfoHashSet.Add(newSSHInfo);
                SshClient ssh = new SshClient(newSSHInfo.IP, int.Parse(newSSHInfo.Port), newSSHInfo.UserName, newSSHInfo.Password);
                if (newSSHInfo.Type == "NAS")
                {
                    SSHNASClientList.Add(ssh);
                }
                else
                {
                    SSHWSLClientList.Add(ssh);
                }
                ssh.Connect();
                ResetSSHVars();
            }
            ConfigFileSave();
        }

        private void SSHInfoDelete(SSHInfo info)
        {
            SSHInfoHashSet.Remove(info);
            SshClient ssh = new SshClient(info.IP, int.Parse(info.Port), info.UserName, info.Password);
            if (info.Type == "NAS")
            {
                foreach (var sshClient in SSHNASClientList)
                {
                    if (sshClient.ToString() == ssh.ToString())
                    {
                        SSHNASClientList.Remove(sshClient);
                        break;
                    }
                }
            }
            else
            {
                foreach (var sshClient in SSHWSLClientList)
                {
                    if (sshClient.ToString() == ssh.ToString())
                    {
                        SSHWSLClientList.Remove(sshClient);
                        break;
                    }
                }
            }
            ConfigFileSave();
        }

        private void PathSave()
        {
            if (WindowsPath != "" && LinuxPath != "")
            {
                PathInfoHashSet.Add(new PathInfo(LinuxPath, WindowsPath));
                ResetPathVars();
            }
            ConfigFileSave();
        }

        private void PathInfoDelete(PathInfo info)
        {
            PathInfoHashSet.Remove(info);
            ConfigFileSave();
        }

        private bool IsStringTrue(string str)
        {
            if (str == "True")
                return true;
            else
                return false;
        }

        private void ReadConfigFromFile(string ConfigFilePath)
        {
            if (!ConfigIO.ExistConfigFile(ConfigFilePath))
            {
                ConfigFileSave();
            }
            else
            {
                IgnoreCaseDist = IsStringTrue(ConfigIO.ConfigReadValue(ConfigFilePath, "Locate Config", "IgnoreCaseDist"));
                LimitOutputNum = IsStringTrue(ConfigIO.ConfigReadValue(ConfigFilePath, "Locate Config", "LimitOutputNum"));
                LimitNum = ConfigIO.ConfigReadValue(ConfigFilePath, "Locate Config", "LimitNum");

                SearchInNAS = IsStringTrue(ConfigIO.ConfigReadValue(ConfigFilePath, "Search Config", "SearchInNAS"));
                SearchInWSL = IsStringTrue(ConfigIO.ConfigReadValue(ConfigFilePath, "Search Config", "SearchInWSL"));

                string AllSSHInfo = ConfigIO.ConfigReadValue(ConfigFilePath, "SSH Config", "SSH", true);
                if (AllSSHInfo != "")
                {
                    ArrayList ArraySSHInfo = new ArrayList(AllSSHInfo.Split(','));
                    foreach (string i in ArraySSHInfo)
                    {
                        ArrayList TempArray = new ArrayList(i.Split(' '));
                        if (i.ToString() == "")
                            continue;
                        SSHInfoHashSet.Add(new SSHInfo(TempArray[0].ToString(), TempArray[1].ToString(), TempArray[2].ToString(), TempArray[3].ToString(), TempArray[4].ToString(), TempArray[5].ToString()));
                    }
                }

                string AllPathInfo = ConfigIO.ConfigReadValue(ConfigFilePath, "Path Config", "Path", true);
                if (AllPathInfo != "")
                {
                    ArrayList ArrayPathInfo = new ArrayList(AllPathInfo.Split(','));
                    foreach (string i in ArrayPathInfo)
                    {
                        ArrayList TempArray = new ArrayList(i.Split(' '));
                        if (i.ToString() == "")
                            continue;
                        PathInfoHashSet.Add(new PathInfo(TempArray[0].ToString(), TempArray[1].ToString()));
                    }
                }
                }
        }

        private void ConfigFileSave()
        {
            ConfigIO.DeleteConfigFile(ConfigFilePath);

            ConfigIO.ConfigWriteValue(ConfigFilePath, "Locate Config", "IgnoreCaseDist", IgnoreCaseDist.ToString());
            ConfigIO.ConfigWriteValue(ConfigFilePath, "Locate Config", "LimitOutputNum", LimitOutputNum.ToString());
            ConfigIO.ConfigWriteValue(ConfigFilePath, "Locate Config", "LimitNum", LimitNum.ToString());

            ConfigIO.ConfigWriteValue(ConfigFilePath, "Search Config", "SearchInNAS", SearchInNAS.ToString());
            ConfigIO.ConfigWriteValue(ConfigFilePath, "Search Config", "SearchInWSL", SearchInWSL.ToString());

            string AllSSHInfo = "";
            foreach(SSHInfo i in SSHInfoHashSet)
                AllSSHInfo += i.ToString();
            ConfigIO.ConfigWriteValue(ConfigFilePath, "SSH Config", "SSH", AllSSHInfo, true);

            string AllPathInfo = "";
            foreach (PathInfo i in PathInfoHashSet)
                AllPathInfo += i.ToString();
            ConfigIO.ConfigWriteValue(ConfigFilePath, "Path Config", "Path", AllPathInfo, true);

        }

        private void ConfigFileImport()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;//该值确定是否可以选择多个文件
            dialog.Title = "Choose Config File";
            dialog.Filter = "Config File(*.config)|*.config|All Files(*.*)|*.*";
            if (dialog.ShowDialog() == true)
            {
                string file = dialog.FileName;
                ReadConfigFromFile(file);
                ConfigFileSave();
            }
        }

        private void ConfigFileClear()
        {
            SetAll();
            SSHInfoHashSet.Clear();
            PathInfoHashSet.Clear();
            ConfigFileSave();
        }

        private string GenerateLocateCommand()
        {
            string Command = "locate ";
            if (IgnoreCaseDist)
                Command += "-i ";
            if (LimitOutputNum)
                Command = Command + "-l " + LimitNum + " ";
            if (SearchText.Contains(":"))
            {
                Command = Command + "\'" + SearchText.Replace(' ', '*') + "*\'";
                foreach (PathInfo i in PathInfoHashSet)
                {
                    Command = Command.Replace(i.WindowsPath, i.LinuxPath);
                }
            }
            else
                Command = Command + "\'/mnt*" + SearchText.Replace(' ', '*') + "*\'";
            Console.WriteLine(Command);
            return Command;
        }

        private string ReplaceLocation(string str)
        {
            foreach(PathInfo i in PathInfoHashSet)
            {
                str=str.Replace(i.LinuxPath,i.WindowsPath);
            }
            return str.Replace('/', '\\');
        }

        private string SSHSearchResult(SshClient sshclient)
        {
            return SSH.SSHExcute(sshclient, GenerateLocateCommand());
        }

        public void SSHSearch()
        {
            SearchInfoHashSet.Clear();
            String ResultInfo = "";
            if(SearchInNAS)
            {
                foreach (SshClient i in SSHNASClientList)
                    ResultInfo += SSHSearchResult(i);
            }
            if(SearchInWSL)
            {
                foreach (SshClient i in SSHWSLClientList)
                    ResultInfo += SSHSearchResult(i);
            }
            ResultInfo = ReplaceLocation(ResultInfo);
            ArrayList ArrayResultInfo = new ArrayList(ResultInfo.Split('\n'));
            foreach (string i in ArrayResultInfo)
            {
                if (i != "" && !i.Contains("jails"))
                    SearchInfoHashSet.Add(new SearchInfo("", i, "", ""));
            }
        }

        private void OpenFolderPath(SearchInfo info)
        {
            if (new DirectoryInfo(info.Path).Exists)
                System.Diagnostics.Process.Start("explorer.exe", info.Path);
            else
                System.Diagnostics.Process.Start("explorer.exe", info.Path.Substring(0, info.Path.LastIndexOf('\\')));
            
        }
    }
}
