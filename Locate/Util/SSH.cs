using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NASLocate.Information;
using Renci.SshNet;

namespace NASLocate.Util
{
    public class SSH
    {

        public static string SSHExcute(SshClient ssh, string SSHCommand)
        {
            var SSHResult = ssh.RunCommand(SSHCommand);
            return SSHResult.Result;
        }
    }
}
