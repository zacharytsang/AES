using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


namespace AES
{
    class Program
    {
        static void Main(string[] args)
        {
            Random ra=new Random((int)DateTime.Today.Ticks);
            List<string> ALL_Files = new List<string>();
            //GetAllFileByDir("e:\\", "*.rar", ALL_Files);
            ALL_Files = GetAllFileByDir2("e:\\");
            FileInfo info = new FileInfo("E:\\BaiduYunDownload\\1.rar");
            long filelength = 0;
            if (info.Length > 100)
            {
                filelength = 100;
            }
            else
            {
                filelength = info.Length;
            }
            //string length = (info.Length).ToString();
            //dir /a /-d /s /OE
            FileStream fs = File.Open("E:\\BaiduYunDownload\\1.rar",FileMode.Open,FileAccess.ReadWrite,FileShare.None);
            BinaryReader br = new BinaryReader(fs);
            //byte[] readbuffer = new byte[filelength];
            //readbuffer = br.ReadBytes((int)filelength);
            char[] readbuffer = br.ReadChars((int)filelength);
            //UTF8Encoding temp = new UTF8Encoding(true);
            //int len = fs.Read(readbuffer, 0, readbuffer.Length);
           // Console.Write(temp.GetString(readbuffer));
           // Console.Write("-----------------------" + len .ToString()+ "------------------------------\n");
            //string orgstring = System.Text.Encoding.ASCII.GetString(readbuffer);
            string orgstring = String.Join("", readbuffer);
            string AESresult = AesEncryptDecryptHelper.AesEncrypt(orgstring);
            string dummy = System.Text.Encoding.ASCII.GetString(new byte[filelength]);
            try
            {
                //FileStream tempfs = File.Open("E:\\BaiduYunDownload\\1.rar" + ".1"  /*ra.Next().ToString()*/, FileMode.Create, FileAccess.Write, FileShare.None);
                System.IO.File.WriteAllText("E:\\BaiduYunDownload\\1.rar" + ".1",AESresult);
                //byte[] tempfilearr = Encoding.UTF8.GetBytes(AESresult); ;
                //tempfs.Write(tempfilearr, 0, tempfilearr.Length);
                //tempfs.Flush();
                //tempfs.Close();
                fs.Seek(0, 0);
                StreamWriter sw = new StreamWriter(fs);
                sw.Write(dummy);
                sw.Close();
                fs.Close();
            }
            catch (Exception ex) { 
            
            }
            string contents = System.IO.File.ReadAllText("E:\\BaiduYunDownload\\1.rar" + ".1");
            FileStream fs2 = File.Open("E:\\BaiduYunDownload\\1.rar",FileMode.Open,FileAccess.ReadWrite,FileShare.None);
           // Console.Write(AESresult);
           // Console.Write("--------------------------"+AESresult.Length.ToString()+"---------------------------\n");
            string AESorg = AesEncryptDecryptHelper.AesDecrypt(contents);
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(AESorg);
            //byte[] bytes = AESorg.Split(new[] { "-" }, StringSplitOptions.None);
            //StreamWriter sw2 = new StreamWriter(fs2);
           // char[] tempchars = new Char[AESorg.Length];
            //sw2.Write(AESorg);
            //BinaryWriter bw =new BinaryWriter(fs2);
            //bw.Write(AESorg);
            fs2.Write(bytes,0,bytes.Length);
            //sw2.Close();
            //bw.Close();
            fs2.Flush();
            fs2.Close(); 
           // Console.Write(AESorg);
           // Console.Write("-----------------------------------------------------\n");
        }

        static void GetAllFileByDir(string DirPath, string searchPattern, List<string> LI_Files)
        {
            //列举出所有文件,添加到AL
            foreach (string file in Directory.GetFiles(DirPath, searchPattern))
                try
                {
                    LI_Files.Add(file);
                }
                catch (Exception ex)
                {

                }
                
            //列举出所有子文件夹,并对之调用GetAllFileByDir自己;
            foreach (string dir in Directory.GetDirectories(DirPath))
                try
                {
                        GetAllFileByDir(dir, searchPattern, LI_Files);
                }
                catch(Exception ex) 
                {
                
                }

        }

        static  List<string> GetAllFileByDir2(string sSourcePath)
        {
            List<String> list = new List<string>();
            //遍历文件夹
            DirectoryInfo theFolder = new DirectoryInfo(sSourcePath);
            FileInfo[] thefileInfo = theFolder.GetFiles("*.*", SearchOption.TopDirectoryOnly);
            foreach (FileInfo NextFile in thefileInfo)  //遍历文件
                list.Add(NextFile.FullName);
            //遍历子文件夹
            DirectoryInfo[] dirInfo = theFolder.GetDirectories();
            foreach (DirectoryInfo NextFolder in dirInfo)
            {
                try
                {
                    FileInfo[] fileInfo = NextFolder.GetFiles("*.*", SearchOption.AllDirectories);
                    foreach (FileInfo NextFile in fileInfo)  //遍历文件
                        list.Add(NextFile.FullName);
                }
                catch (Exception ex)
                {

                }
            }
            return list;
        }

    }
}
