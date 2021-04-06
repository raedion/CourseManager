using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;

using Livet;
using Newtonsoft.Json;

namespace CourseManager.Models
{
    public class DataManager : NotificationObject
    {
        public ObservableCollection<Data> ReadDataFromFile(string fileName)
        {
            string jsonStr = ReadAllLine(fileName, "utf-8");

            //JsonReader jsonTextReader = new JsonTextReader(new StringReader(jsonStr));
            ObservableCollection<Data> roles = new ObservableCollection<Data>();
            JsonReader jsonTextReader = new JsonTextReader(new StringReader(jsonStr));
            //JsonTextReader reader = new JsonTextReader(new StringReader(json));
            jsonTextReader.SupportMultipleContent = true;

            while (true) {
                if (!jsonTextReader.Read()) {
                    break;
                }

                JsonSerializer serializer = new JsonSerializer();
                Data role = serializer.Deserialize<Data>(jsonTextReader);

                roles.Add(role);
            }
            return roles;
            //while (jsonTextReader.Read()) {
            //    if (jsonTextReader.Value != null) {
            //        System.Diagnostics.Debug.WriteLine("Token: {0}, Value: {1}", jsonTextReader.TokenType, jsonTextReader.Value);
            //    }
            //    else {
            //        System.Diagnostics.Debug.WriteLine("Token: {0}", jsonTextReader.TokenType);
            //    }
            //}

            //Data jsonData = new Data();
            //JsonSerializer serializer = new JsonSerializer();
            //jsonData = serializer.Deserialize<Data>(jsonTextReader);
            //Console.WriteLine($"jsonData.id = {jsonData.Id}");
            //Console.WriteLine($"jsonData.name = {jsonData.SubjectName}");
            //Console.WriteLine($"jsonData.dept = {jsonData.IsExpert}");
        }
        public static string ReadAllLine(string filePath, string encodingName)
        {
            StreamReader sr = new StreamReader(filePath, Encoding.GetEncoding(encodingName));
            string allLine = sr.ReadToEnd();
            sr.Close();

            return allLine;
        }
    }
}
