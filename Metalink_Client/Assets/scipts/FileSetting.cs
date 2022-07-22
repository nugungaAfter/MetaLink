using Renci.SshNet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class FileSetting : MonoBehaviour
{
    string host;
    int port;
    string username;
    string password;
    public Text textMassage;
    public Text directoryName;

    private void Awake()
    {
        host = "nugunga.com";
        port = 21123;
        username = "narahs";
        password = "0120@";
    }

    public void FileUploadSFTP()
    {
        // 파일 업로드 
        var uploadFile = Application.dataPath + "/file.txt";
        var DirectoryPath = "./web/";
        using (var client = new SftpClient(host, port, username, password))
        {
            client.Connect();
            if (client.IsConnected)
            {
                if (directoryName.text != "")
                {
                    DirectoryPath += directoryName.text;
                    try
                    {
                        client.CreateDirectory(DirectoryPath);
                    }
                    catch (System.Exception)
                    {
                        Debug.Log("tjdrnt");
                    }
                }
                else
                {
                    textMassage.text = "디렉토리 이름이 없습니다.";
                }

                //  Debug.WriteLine("I'm connected to the client");
                //  
                using (var fileStream = File.Open(uploadFile, FileMode.Open))
                {
                    client.BufferSize = 4 * 1024; // bypass Payload error large files
                    var uploadPath = DirectoryPath + "/file.txt";
                    client.UploadFile(fileStream, uploadPath);
                }
            }
            else
            {
                Debug.Log("I couldn't connect");
            }
        }
    }

    public void FileDownloadSFTP()
    {
        var DownloadFile = "./web/" + directoryName + "file.txt";

        using (var client = new SftpClient(host, port, username, password))
        {
            client.Connect();
            if (client.IsConnected)
            {
                using (var outfile = File.Create("C:/Users/rahs0/Desktop/file.txt"))
                {
                    try
                    {
                        client.DownloadFile(DownloadFile, outfile);
                    }
                    catch (Exception ex)
                    {
                        textMassage.text = "파일을 찾을 수 없습니다.";
                    }
                }
            }
            else
            {
                Debug.Log("I couldn't connect");
            }
        }
    }
}
