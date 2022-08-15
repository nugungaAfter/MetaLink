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
        /// SFTP 서버에 파일 업로드
        /// </summary>
        /// <param name="p_UploadDirectory">업로드할 경로 (서버)</param>
        /// <param name="p_UploadFilePath">업로드할 파일 이름 (서버)</param>
        /// <param name="p_UploadFile">업로드할 파일 경로</param>
        /// <exception cref="Exception"></exception>
        /// <exception cref="NullReferenceException"></exception>
        public static void FileUploadSFTP(string p_Root, string p_UploadDirectory, string p_UploadFilePath, string p_UploadFile)
        {
            using var l_client = new SftpClient(m_Host, m_Port, m_Username, m_Password);
            l_client.Connect();

            if (!l_client.IsConnected)
                throw new Exception("서버에 연결할 수 없음");

            if (string.IsNullOrEmpty(p_UploadDirectory))
                throw new Exception("디렉토리 이름이 없습니다.");

            l_client.ChangeDirectory("./root/" + p_Root);
            CreateDirectory(l_client, p_UploadDirectory.Split("/"));
            UploadFile(l_client, p_UploadDirectory + "/" + p_UploadFilePath, p_UploadFile);

            l_client.Disconnect();
        }

        /// <summary>
        /// 폴터 존재여부 확인후 없으면 생성, 다음에 폴터안으로 이동
        /// </summary>
        /// <param name="p_Client">연결된 클라이언트</param>
        /// <param name="p_Directory">생성할 경로</param>
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
        /// 파일 업로드
        /// </summary>
        /// <param name="p_Client">연결된 클라이언트</param>
        /// <param name="p_UploadPath">업로드 경로</param>
        /// <param name="p_UploadFile">업로드할 파일</param>
        private static void UploadFile(SftpClient p_Client, string p_UploadPath, string p_UploadFile)
        {
            using var fileStream = File.Open(p_UploadFile, FileMode.Open);
            p_Client.BufferSize = 4 * 1024; // bypass Payload error large files
            p_Client.UploadFile(fileStream, p_UploadPath);
        }

        /// <summary>
        /// SFTP 서버에서 파일 다운로드
        /// </summary>
        /// <param name="p_DownloadDirectory">다운로드할 파일 경로</param>
        /// <param name="p_DownloadFilePath">다운로드할 파일 이름</param>
        /// <param name="p_DownloadFile">저장할 경로</param>
        /// <exception cref="Exception"></exception>
        public static void FileDownloadSFTP(string p_Root, string p_DownloadDirectory, string p_DownloadFilePath, string p_DownloadFile)
        {
            using var l_client = new SftpClient(m_Host, m_Port, m_Username, m_Password);
            l_client.Connect();

            if (!l_client.IsConnected)
                throw new Exception("서버에 연결할 수 없음");

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
                throw new Exception("서버에 연결할 수 없음");

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
        /// 파일 다운로드
        /// </summary>
        /// <param name="p_Client">연결된 클라이언트</param>
        /// <param name="p_DownloadPath">다운로드할 파일 경로</param>
        /// <param name="p_DownloadFile">저장할 경로</param>
        private static void DownloadFIle(SftpClient p_Client, string p_DownloadPath, string p_DownloadFile)
        {
            using var outfile = File.Create(p_DownloadFile);
            p_Client.DownloadFile(p_DownloadPath, outfile);
        }
    }

}