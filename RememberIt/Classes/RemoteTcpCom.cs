using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;

namespace RememberIt
{
    public class RemoteTcpCom
    {
        public Control Owner = null;

        byte[] BufferCommand = new byte[1024 * 100];
        byte[] BufferEvent = new byte[1024 * 100];
        byte[] BufferDataIncoming = new byte[1024 * 100];

        bool IncomingFileAvailable = false;
        string IncomingFileName = "";
        BinaryWriter IncomingStream = null;
        long IncomingFileLength = 0;


        public string RemoteIpAddress = "127.0.0.1";
        
        public int PortNoCommand = 50400;
        public int PortNoEvent = 50401;
        public int PortNoDataOutgoing = 50402;
        public int PortNoDataIncomming = 50403;

        private TcpClient ClientCommand = new TcpClient();
        private TcpClient ClientEvent = new TcpClient();
        private TcpClient ClientDataOutgoing = new TcpClient();
        private TcpClient ClientDataIncomming = new TcpClient();

        private BackgroundWorker bgwEventReceiver = new BackgroundWorker();
        private BackgroundWorker bgwIncomingData = new BackgroundWorker();
        
        private bool bgwEventReceiverStopFlag = false;
        private bool bgwIncomingDataStopFlag = false;

        public delegate void DelegateLog(string Log);
        public event DelegateLog OnLog = null;

        public delegate void DelegateFileSentCompeleted(string FileName, long FileLength);
        public event DelegateFileSentCompeleted OnFileSentCompeleted = null;

        public delegate void DelegateFileReceivedCompleted(string FileName, long FileLength);
        public event DelegateFileReceivedCompleted OnFileReceivedCompeleted = null;

        public bool Initialized = false;
        public void Init()
        {
            if (this.Initialized) return;
            bgwEventReceiver.DoWork += new DoWorkEventHandler(bkwEventReceiver_DoWork);
            bgwIncomingData.DoWork += new DoWorkEventHandler(bgwIncomingData_DoWork);

            this.Initialized=true;
        }

        void bgwIncomingData_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if(bgwIncomingDataStopFlag) break;

                if (this.IncomingFileAvailable == false)
                {
                    System.Threading.Thread.Sleep(100);
                    continue;
                }



                long total_bytes_received = 0;
                while (true)
                {
                    if (bgwIncomingDataStopFlag) break;
                    
                    if (ClientDataIncomming.Client.Available == 0)
                    {
                        System.Threading.Thread.Sleep(100);
                        continue;
                    }
                    int cnt = ClientDataIncomming.Client.Receive(BufferDataIncoming);
                    IncomingStream.Write(BufferDataIncoming, 0, cnt);
                    total_bytes_received += cnt;
                    if (total_bytes_received >= IncomingFileLength) break;
                }
                IncomingStream.Flush();
                IncomingStream.Close();
                // Do Log
                this.IncomingFileName = "";
                this.IncomingFileAvailable = false;
            }
        }

        void bkwEventReceiver_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                //if (bgwEventReceiverStopFlag == true) return;
                //if (ClientEvent.Client.Available == 0)
                //{
                //    System.Threading.Thread.Sleep(100);
                //    continue;
                //}

                if (bgwEventReceiverStopFlag) break;

                int cnt = ClientEvent.Client.Receive(BufferEvent);
                if (cnt == 0)
                {
                    System.Threading.Thread.Sleep(100);
                    continue;
                }
                string event_str = ASCIIEncoding.ASCII.GetString(BufferEvent, 0, cnt);
                HandleDeviceEvent(event_str);
            }
        }

        public void Connect()
        {
            try
            {
                ClientCommand.Connect(RemoteIpAddress, PortNoCommand);
                ClientEvent.Connect(RemoteIpAddress, PortNoEvent);
                ClientDataOutgoing.Connect(RemoteIpAddress, PortNoDataOutgoing);
                ClientDataIncomming.Connect(RemoteIpAddress, PortNoDataIncomming);

                bgwEventReceiver.RunWorkerAsync();
                bgwIncomingData.RunWorkerAsync();
            }
            catch(Exception ex)
            {
                throw new Exception("Unable to connect to remote host.\n" + ex.Message);
            }
        }

        public void Disconnect()
        {
            try
            {
                bgwEventReceiverStopFlag = true;
                bgwIncomingDataStopFlag = true;

                ClientCommand.Client.Disconnect(true);
                ClientEvent.Client.Disconnect(true);
                ClientDataOutgoing.Client.Disconnect(true);
                ClientDataIncomming.Client.Disconnect(true);

            }
            catch (Exception ex)
            {
                // throw new Exception("Unable to disconnect from remote host.\n" + ex.Message);
            }
        }


        public string RequestCommand(string Command, int TimeouMs)
        {
            try
            {
                ClientCommand.Client.ReceiveTimeout = TimeouMs;
                ClientCommand.Client.Send(ASCIIEncoding.ASCII.GetBytes(Command+"\r\n"));
                //CommandWriter.WriteLine(Command);
                int cnt = ClientCommand.Client.Receive(BufferCommand);
                string resp = ASCIIEncoding.ASCII.GetString(BufferCommand, 0, cnt);
                return resp;
            }
            catch (Exception ex)
            {
                // throw new Exception(ex.Message);
                return null;
            }
        }

        public string EchoBack(string Text)
        {
            string cmd = string.Format("req:echo_back; text:{0};", Text);
            string answer = RequestCommand(cmd,2000);
            KeyValPair kvp = new KeyValPair(';',':');
            kvp.Fill(answer);
            string resp = kvp.GetVal("resp");
            string desc = kvp.GetVal("desc");
            return desc;
        }

        public void FileCopyTo(string LocalSourceFileName, string RemoteDestinationFileName)
        {

            string cmd = "";
            string answer = "";
            string resp = "";
            string desc = "";
            KeyValPair kvp = new KeyValPair(';', ':');


            FileInfo fi = new FileInfo(LocalSourceFileName);
            if (fi.Exists == false) throw new Exception("Local source file not found!");

            //byte checksum = 0;
            

            string destination_dir_path = Globals.GetDirectoryOfFile(RemoteDestinationFileName);

            cmd = string.Format("req:dir_exists; path:{0};", destination_dir_path);
            answer = RequestCommand(cmd,2000);
            //MessageBox.Show(answer);
            HandleDeviceEvent(answer);
            kvp.Clear();
            kvp.Fill(answer);
            resp = kvp.GetVal("resp");
            desc = kvp.GetVal("desc");
            // add a log here with 'desc' field
            if (resp == "0")
            {
                cmd = string.Format("req:dir_create; path:{0};", destination_dir_path);
                resp = RequestCommand(cmd,2000);
                if (resp == "0") throw new Exception("Unable to locate destination folder");
            }

            cmd = string.Format("req:file_create; file_name:{0}; file_length:{1}", RemoteDestinationFileName, fi.Length);
            answer = RequestCommand(cmd,2000);
            //MessageBox.Show(answer);
            HandleDeviceEvent(answer);
            kvp.Clear();
            kvp.Fill(answer);
            resp = kvp.GetVal("resp");
            desc = kvp.GetVal("desc");
            // add a log here with 'desc' field
            if (resp == "1")    // The other side is ready to receive the content of the file.
            {
                long total_bytes_sent = SendOutData(LocalSourceFileName);
            }
        }

        private void HandleDeviceEvent(string EventString)
        {
            if (this.OnLog == null) return;
            if (this.Owner == null) return;
            if (EventString == null || EventString == "") return;
            if (this.Owner.InvokeRequired)
            {
                try
                {
                    this.Owner.Invoke(new DelegateLog(HandleDeviceEvent), new object[] { EventString });
                }
                catch(Exception ex)
                { 
                    //
                }
            }
            else
            {
                KeyValPair kvp = new KeyValPair(';', ':');
                kvp.Fill(EventString);
                string event_name = kvp.GetVal("event");

                if (event_name == "file_received")
                {
                    string file_name = kvp.GetVal("file_name");
                    long file_length = 0;
                    long.TryParse(kvp.GetVal("file_length"), out file_length);
                    if (this.OnFileSentCompeleted != null)
                    {
                        OnFileSentCompeleted(file_name, file_length);
                        this.OnLog(string.Format("File '{0}' Copied.", file_name));
                    }
                }
                else if (event_name == "file_transmition_finished")
                {
                    string file_name = kvp.GetVal("file_name");
                    long file_length = 0;
                    long.TryParse(kvp.GetVal("file_length"), out file_length);
                    
                    if (this.OnFileReceivedCompeleted != null)
                    {
                        OnFileReceivedCompeleted(file_name, file_length);
                        this.OnLog(string.Format("File '{0}' Fetched.", file_name));
                    }
                }
            }
        }



        private long SendOutData(string LocalSourceFileName)
        {
            byte[] buff = new byte[1024];
            try
            {
                long total_bytes_sent = 0;
                FileStream fs = new FileStream(LocalSourceFileName, FileMode.Open, FileAccess.Read);
                while (true)
                {
                    int cnt = fs.Read(buff, 0, buff.Length);
                    total_bytes_sent += cnt;
                    if (cnt == 0) break;
                    ClientDataOutgoing.Client.Send(buff, cnt, SocketFlags.None);
                }
                fs.Close();
                return total_bytes_sent;
            }
            catch
            {
                return -1;
            }
        }
        
        
        public void FileCopyFrom(string RemoteSourceFileName, string LocalDestinationFileName)
        {

        }

        public bool DirExists(string RemoteDirPath)
        {
            return false;
        }

        public bool DirCreate(string RemoteDirPathRel)
        {
            string cmd = string.Format("req:dir_create; path:{0};", RemoteDirPathRel);
            string answer = RequestCommand(cmd, 10000);
            KeyValPair kvp = new KeyValPair(';', ':');
            kvp.Fill(answer);
            if (kvp.GetVal("resp") != "1") return false;
            else return true;
        }

        public bool DirDelete(string RemoteDirPath)
        {

            return false;
        }

        public string[] DirSubdirList(string RemoteDirPath)
        {

            return null;
        }

        public string[] DirFileList(string RemoteDirPath)
        {
            return null;
        }

        public bool FileExists(string RemoteFileName)
        {
            return false;
        }

        public void FileDelete(string RemoteFileName)
        {

        }

        public void FileCreateOpen(string RemoteFileName)
        {

        }

        public void FileClose(string RemoteFileName)
        {

        }

        public long GetFileLength(string RemoteFileName)
        {

            return 0;
        }

        internal string[] GetListOfAllFilesAndDirs(string RemotePath)
        {
            List<string> res = new List<string>();
            string cmd = string.Format("req:dir_get_all_files_and_dirs; path:{0};", RemotePath);
            string answer = RequestCommand(cmd, 10000);

            KeyValPair kvp = new KeyValPair(';', ':');
            kvp.Fill(answer);
            if (kvp.GetVal("resp") != "1") return res.ToArray();

            try
            {
                int dir_count = int.Parse(kvp.GetVal("dir_count"));
                for (int i = 0; i < dir_count; i++)
                {
                    res.Add(kvp.GetVal(string.Format("d_{0}", i + 1)));
                }

                int file_count = int.Parse(kvp.GetVal("file_count"));
                for (int i = 0; i < file_count; i++)
                {
                    res.Add(kvp.GetVal(string.Format("f_{0}", i + 1)));
                }
            }
            catch (Exception ex)
            {
                return res.ToArray();
            }

            return res.ToArray();
        }
        
        
        internal string[] GetListOfAllFiles(string RemotePath)
        {
            List<string> res = new List<string>();
            string cmd = string.Format("req:dir_get_all_files; path:{0};", RemotePath);
            string answer = RequestCommand(cmd, 10000);

            KeyValPair kvp = new KeyValPair(';', ':');
            kvp.Fill(answer);
            if (kvp.GetVal("resp") != "1") return res.ToArray();

            try
            {
                int file_count = int.Parse(kvp.GetVal("file_count"));
                for (int i = 0; i < file_count; i++)
                {
                    res.Add(kvp.GetVal(string.Format("f_{0}", i + 1)));
                }
            }
            catch (Exception ex)
            {
                return res.ToArray();
            }
            return res.ToArray();
        }

        internal string[] GetListOfAllDirs(string RemotePath)
        {
            List<string> res = new List<string>();
            string cmd = string.Format("req:dir_get_all_dirs; path:{0};", RemotePath);
            string answer = RequestCommand(cmd, 10000);

            KeyValPair kvp = new KeyValPair(';', ':');
            kvp.Fill(answer);
            if (kvp.GetVal("resp") != "1") return res.ToArray();

            try
            {
                int dir_count = int.Parse(kvp.GetVal("dir_count"));
                for (int i = 0; i < dir_count; i++)
                {
                    res.Add(kvp.GetVal(string.Format("d_{0}", i + 1)));
                }
            }
            catch (Exception ex)
            {
                return res.ToArray();
            }

            return res.ToArray();
        }
        internal string[] GetListOfDirs(string RemotePath)
        {
            List<string> res = new List<string>();
            string cmd = string.Format("req:dir_get_dirs; path:{0};", RemotePath);
            string answer = RequestCommand(cmd, 10000);

            KeyValPair kvp = new KeyValPair(';', ':');
            kvp.Fill(answer);
            if (kvp.GetVal("resp") != "1") return res.ToArray();

            try
            {
                int dir_count = int.Parse(kvp.GetVal("dir_count"));
                for (int i = 0; i < dir_count; i++)
                {
                    res.Add(kvp.GetVal(string.Format("d_{0}", i + 1)));
                }
            }
            catch (Exception ex)
            {
                return res.ToArray();
            }

            return res.ToArray();
        }

        public string FetchFile(string RemoteFullFileName, string LocalDirPath)
        {
            string cmd = string.Format("req:file_get_preper_to_send_file; full_file_name:{0};", RemoteFullFileName);
            string answer = RequestCommand(cmd, 10000);
            KeyValPair kvp = new KeyValPair(';', ':');
            kvp.Fill(answer);

            string resp = kvp.GetVal("resp");
            if (resp == "1")
            {
                long file_length = 0;
                long.TryParse(kvp.GetVal("file_length"), out file_length);

                cmd = string.Format("req:file_sned_file; full_file_name:{0};", RemoteFullFileName);
                answer = RequestCommand(cmd, 10000);
                kvp.Clear();
                kvp.Fill(answer);
                if (kvp.GetVal("resp") == "1")
                {
                    string remote_root = kvp.GetVal("root");
                    string remote_full_file_name = kvp.GetVal("full_file_name");
                    string remote_rel_file_name = remote_full_file_name.Replace(remote_root, "");


                    string local_full_file_name = LocalDirPath + remote_rel_file_name;

                    local_full_file_name = local_full_file_name.Replace("/", "\\");
                    this.IncomingFileLength = file_length;
                    this.IncomingFileName = local_full_file_name;
                    string local_path = Globals.GetDirOfFile(local_full_file_name);
                    try
                    {
                        if (Directory.Exists(local_path) == false) Directory.CreateDirectory(local_path);
                        this.IncomingStream = new BinaryWriter(new FileStream(IncomingFileName, FileMode.Create));
                        this.IncomingFileAvailable = true;
                    }
                    catch
                    {
                        //                     
                    }
                }
            }
            return answer;
        }
        public string FetchFile(string RemoteFullFileName)
        {
            string local_dir_path = Globals.GetExecutablePath();
            return FetchFile(RemoteFullFileName, local_dir_path);
        }

        public string[] GetListOfCardGroups()
        {
            List<string> res = new List<string>();
            string cmd = string.Format("req:card_get_groups;");
            string answer = RequestCommand(cmd, 10000);

            KeyValPair kvp = new KeyValPair(';', ':');
            kvp.Fill(answer);
            if (kvp.GetVal("resp") != "1") return res.ToArray();

            try
            {
                int group_count = int.Parse(kvp.GetVal("group_count"));
                for (int i = 0; i < group_count; i++)
                {
                    res.Add(kvp.GetVal(string.Format("g_{0}", i + 1)));
                }
            }
            catch (Exception ex)
            {
                return res.ToArray();
            }
            return res.ToArray();
        }
        
        public string[] GetListOfCards(string Group)
        {
            List<string> res = new List<string>();
            string cmd = string.Format("req:card_get_cards; group:{0}",Group);
            string answer = RequestCommand(cmd, 10000);

            KeyValPair kvp = new KeyValPair(';', ':');
            kvp.Fill(answer);
            if (kvp.GetVal("resp") != "1") return res.ToArray();

            try
            {
                int group_count = int.Parse(kvp.GetVal("card_count"));
                for (int i = 0; i < group_count; i++)
                {
                    res.Add(kvp.GetVal(string.Format("c_{0}", i + 1)));
                }
            }
            catch (Exception ex)
            {
                return res.ToArray();
            }
            return res.ToArray();
        }

        internal bool RemoveCard(string CardPath)
        {
            List<string> res = new List<string>();
            string cmd = string.Format("req:card_remove; card_path:{0}", CardPath);
            string answer = RequestCommand(cmd, 10000);

            KeyValPair kvp = new KeyValPair(';', ':');
            kvp.Fill(answer);
            if (kvp.GetVal("resp") != "1") return false;
            return true;
        }

        internal string GetManifestData(string CardPath)
        {
            string cmd = string.Format("req:card_get_manifest; card_path:{0}", CardPath);
            string answer = RequestCommand(cmd, 10000);
            if (answer == null) return "";
            return answer.Trim();
        }
    }
}
