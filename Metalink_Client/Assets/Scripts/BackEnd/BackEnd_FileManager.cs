using System.Collections.Generic;
using System.IO;
using System;

using UnityEngine;

using Renci.SshNet;

namespace Metalink.BackEnd
{
    public class FileManager : MonoBehaviour
    {
        private static readonly string m_Host = "nugunga.com";
        private static readonly string m_Username = "tester";
        private static readonly string m_Password = "tester";
        private static readonly int m_Port = 21123;

        /// <summary>
        /// SFTP ������ ���� ���ε�
        /// </summary>
        /// <param name="p_UploadDirectory">���ε��� ��� (����)</param>
        /// <param name="p_UploadFilePath">���ε��� ���� �̸� (����)</param>
        /// <param name="p_UploadFile">���ε��� ���� ���</param>
        /// <exception cref="Exception"></exception>
        /// <exception cref="NullReferenceException"></exception>
        public static void FileUploadSFTP(string p_Root, string p_UploadDirectory, string p_UploadFilePath, string p_UploadFile)
        {
            using var l_client = new SftpClient(m_Host, m_Port, m_Username, m_Password);
            l_client.Connect();

            if (!l_client.IsConnected)
                throw new Exception("������ ������ �� ����");

            if (string.IsNullOrEmpty(p_UploadDirectory))
                throw new Exception("���丮 �̸��� �����ϴ�.");

            l_client.ChangeDirectory("./root/" + p_Root);
            CreateDirectory(l_client, p_UploadDirectory.Split("/"));
            UploadFile(l_client, p_UploadDirectory + "/" + p_UploadFilePath, p_UploadFile);

            l_client.Disconnect();
        }

        /// <summary>
        /// ���� ���翩�� Ȯ���� ������ ����, ������ ���;����� �̵�
        /// </summary>
        /// <param name="p_Client">����� Ŭ���̾�Ʈ</param>
        /// <param name="p_Directory">������ ���</param>
        private static void CreateDirectory(SftpClient p_Client, string[] p_Directories, string p_CurrentDirectory = "", int index = 0)
        {
            if (index == p_Directories.Length)
                return;

            string l_directory = p_CurrentDirectory + p_Directories[index];
            if (!p_Client.Exists(l_directory))
                p_Client.CreateDirectory(l_directory);
            l_directory += "/";

            CreateDirectory(p_Client, p_Directories, l_directory, index + 1);
        }

        /// <summary>
        /// ���� ���ε�
        /// </summary>
        /// <param name="p_Client">����� Ŭ���̾�Ʈ</param>
        /// <param name="p_UploadPath">���ε� ���</param>
        /// <param name="p_UploadFile">���ε��� ����</param>
        private static void UploadFile(SftpClient p_Client, string p_UploadPath, string p_UploadFile)
        {
            using var fileStream = File.Open(p_UploadFile, FileMode.Open);
            p_Client.BufferSize = 4 * 1024; // bypass Payload error large files
            p_Client.UploadFile(fileStream, p_UploadPath);
        }

        /// <summary>
        /// SFTP �������� ���� �ٿ�ε�
        /// </summary>
        /// <param name="p_DownloadDirectory">�ٿ�ε��� ���� ���</param>
        /// <param name="p_DownloadFilePath">�ٿ�ε��� ���� �̸�</param>
        /// <param name="p_DownloadFile">������ ���</param>
        /// <exception cref="Exception"></exception>
        public static void FileDownloadSFTP(string p_Root, string p_DownloadDirectory, string p_DownloadFilePath, string p_DownloadFile)
        {
            using var l_client = new SftpClient(m_Host, m_Port, m_Username, m_Password);
            l_client.Connect();

            if (!l_client.IsConnected)
                throw new Exception("������ ������ �� ����");

            l_client.ChangeDirectory("./root/" + p_Root);

            string l_serverPath = (p_DownloadDirectory + "/" + p_DownloadFilePath);
            DownloadFIle(l_client, l_serverPath, p_DownloadFile);

            l_client.Disconnect();
        }

        public static void DeleteDirectory(string p_Root, string p_DeleteDirectory)
        {
            using var l_client = new SftpClient(m_Host, m_Port, m_Username, m_Password);
            l_client.Connect();

            if (!l_client.IsConnected)
                throw new Exception("������ ������ �� ����");

            l_client.ChangeDirectory("./root/" + p_Root);

            try {
                var l_dirList = l_client.ListDirectory(p_DeleteDirectory);

                foreach (var l_sftpFile in l_dirList)
                    if (!l_sftpFile.IsDirectory)
                        l_client.DeleteFile(l_sftpFile.FullName);
                l_client.DeleteDirectory(p_DeleteDirectory);
            }
            catch {
                return;
            }
        }

        public static void DeleteFile(string p_Root, string p_DeleteFile)
        {
            using var l_client = new SftpClient(m_Host, m_Port, m_Username, m_Password);
            l_client.Connect();

            l_client.ChangeDirectory("./root/" + p_Root);
            l_client.DeleteFile(p_DeleteFile);
        }

        /// <summary>
        /// ���� �ٿ�ε�
        /// </summary>
        /// <param name="p_Client">����� Ŭ���̾�Ʈ</param>
        /// <param name="p_DownloadPath">�ٿ�ε��� ���� ���</param>
        /// <param name="p_DownloadFile">������ ���</param>
        private static void DownloadFIle(SftpClient p_Client, string p_DownloadPath, string p_DownloadFile)
        {
            using var outfile = File.Create(p_DownloadFile);
            p_Client.DownloadFile(p_DownloadPath, outfile);
        }
    }

}