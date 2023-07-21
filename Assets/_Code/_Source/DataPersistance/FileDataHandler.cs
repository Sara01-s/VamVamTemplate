using System.Security.Cryptography;
using System.Collections.Generic;
using VamVam.Source.Utils;
using UnityEngine;
using System.Text;
using System.Linq;
using System.IO;
using System;

namespace VamVam.Source.Data {

    public sealed class FileDataHandler<TData> where TData : IData { 

        private string _dataDirectoryPath = string.Empty;
        private string _dataFileName      = string.Empty;
        private bool   _useEncryption     = false;
        private static byte[] Key = Encoding.UTF8.GetBytes("VamVamosQSePuede");

        public FileDataHandler(string dataDirectoryPath, string dataFileName, bool useEncryption) {
            _dataDirectoryPath = dataDirectoryPath;
            _dataFileName      = dataFileName;
            _useEncryption     = useEncryption;

            if (_useEncryption) {
                // TODO - Make Key persistent, generate it with GenerateRandomKey(), and use it along AES Encryption
            }
        }

        
        public TData LoadFromFile(string profileID) {
            // using Path.Combine for different OS's path separators
            var fullPath = Path.Combine(_dataDirectoryPath, profileID, _dataFileName);
            var loadedData = default(TData);

            return ReadData(loadedData, fullPath);
        }

        public TData LoadFromFile() {
            // using Path.Combine for different OS's path separators
            var fullPath = Path.Combine(_dataDirectoryPath, _dataFileName);
            var loadedData = default(TData);

            return ReadData(loadedData, fullPath);
        }



        private TData ReadData(TData loadedData, string fullPath) {
            
            if (File.Exists(fullPath)) {
                try {
                    var dataToLoad = "";

                    if (_useEncryption) {
                        byte[] bytesToLoad;

                        using (var stream = new FileStream(fullPath, FileMode.Open))
                        using (var binaryReader = new BinaryReader(stream)) {
                            bytesToLoad = binaryReader.ReadBytes((int) stream.Length);
                        }

                        bytesToLoad = Decrypt(bytesToLoad);
                        // Deserialize data from Encrypted JSON to T object
                        try {
                            loadedData = JsonUtility.FromJson<TData>(Encoding.UTF8.GetString(bytesToLoad));
                        }
                        catch (Exception e) {
                            Debug.LogError(LogUtils.ErrorMessage("Error loading encrypted data from JSON", e));
                        }

                        return loadedData;
                    }

                    using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                    using (StreamReader streamReader = new StreamReader(stream)) {
                        dataToLoad = streamReader.ReadToEnd();
                    }

                    // Deserialize data from JSON to T object
                    try {
                        loadedData = JsonUtility.FromJson<TData>(dataToLoad);
                    }
                    catch (Exception e) {
                        Debug.LogError(LogUtils.ErrorMessage("Error loading data from JSON", e));
                    }
                }
                catch (Exception e) {
                    Debug.LogError(LogUtils.ErrorMessage("Error ocurred when trying to load data from file ", e));
                }
            }

            return loadedData;
        }

        public void SaveToFile(TData data, string profileID) {
            // using Path.Combine for different OS's path separators
            var fullPath = Path.Combine(_dataDirectoryPath, profileID, _dataFileName);
            WriteData(data, fullPath);
        }

        public void SaveToFile(TData data) {
            // using Path.Combine for different OS's path separators
            var fullPath = Path.Combine(_dataDirectoryPath, _dataFileName);
            WriteData(data, fullPath);
        }

        private void WriteData(TData data, string fullPath) {
            try {
                // create the directory if it doesn't exist
                Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            
                if (_useEncryption) {
                    var bytesToStore = Encrypt(Encoding.UTF8.GetBytes(JsonUtility.ToJson(data))); // Encrypt returns byte[]

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    using (var streamWriter = new StreamWriter(stream)) {
                        streamWriter.Write(bytesToStore);
                    }
                }

                // Serialize C# data to JSON
                var dataToStore = JsonUtility.ToJson(data, true);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                using (var streamWriter = new StreamWriter(stream)) {
                    streamWriter.Write(dataToStore);
                }
            }
            catch (Exception e) {
                Debug.LogError(LogUtils.ErrorMessage("Error occurred when trying to save data to file ", e));
            }
        }

        public void DeleteData(string profileID) {
            if (profileID is null) return;

            var fullPath = Path.Combine(_dataDirectoryPath, profileID, _dataFileName);

            try {
                if (File.Exists(fullPath))
                    Directory.Delete(Path.GetDirectoryName(fullPath), true);
                else 
                    Debug.LogWarning("Data System: Tried to delete data, but data was not found at path: " + fullPath);
            }
            catch (Exception e) {
                Debug.LogError(LogUtils.ErrorMessage("Failed to delete profile data for profile ID: " + profileID, e));
            }
        }

        public Dictionary<string, TData> LoadAllProfiles() {
            var profileDictionary = new Dictionary<string, TData>();
            var directoryInfos    = new DirectoryInfo(_dataDirectoryPath).EnumerateDirectories().ToList();

            foreach(var info in directoryInfos) {
                var profileID = info.Name;
                var fullPath  = Path.Combine(_dataDirectoryPath, profileID, _dataFileName);

                if (!File.Exists(fullPath)) {
                    Debug.LogWarning("Data System : Skipping directories on profiles load: " + profileID);
                    continue;
                }

                var profileData = LoadFromFile(profileID);

                if (profileData != null) profileDictionary.Add(profileID, profileData);
                else Debug.LogError($"Data System : Tried to load profile, but something went wrong, {LogUtils.Colorize("maybe the file is empty?", LogColor.Orange)}. Profile ID: {profileID}");
            }

            return profileDictionary;
        }


        // -- Encryption -- //
        private byte[] GenerateRandomKey(int length) {
            var key = new byte[length];
            using (var rand = new RNGCryptoServiceProvider()) {
                rand.GetBytes(key);
            }
            return key;
        }

        private byte[] Encrypt(byte[] plainText) {
            using (var aes = Aes.Create()) {
                aes.Key = Key;
                aes.GenerateIV();

                using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV)) {
                using (var memoryStream = new MemoryStream()) {
                    memoryStream.Write(aes.IV, 0, aes.IV.Length);

                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write)) {
                        cryptoStream.Write(plainText, 0, plainText.Length);
                        cryptoStream.FlushFinalBlock();
                    }

                    return memoryStream.ToArray();
                    }
                }
            }
        }

        private byte[] Decrypt(byte[] encryptedText) {
            using (var aes = Aes.Create()) {
                aes.Key = Key;

                using (var memoryStream = new MemoryStream(encryptedText)) {
                    var iv = new byte[aes.IV.Length];

                    memoryStream.Read(iv, 0, iv.Length);
                    aes.IV = iv;

                    using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV)) {
                    using (var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read)) {
                    using (var memoryStreamOut = new MemoryStream()) {

                        var buffer = new byte[1024];
                        int read;

                        while ((read = cryptoStream.Read(buffer, 0, buffer.Length)) > 0)
                            memoryStreamOut.Write(buffer, 0, read);

                        return memoryStreamOut.ToArray();
                            }
                        }
                    }
                }
            }
        }

    }
}