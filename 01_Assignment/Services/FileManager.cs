﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Assignment_.Services
{
    internal interface IFileManager // internal = acessbart inom det egna projektet, mellan klasser i projektet. och det interface = kontrakt
    {
        public void Save(string filePath, string content);
        public string Read(string filePath);
    }
    internal class FileManager : IFileManager // Method
    {
        public string Read(string filePath)
        {
            using var sr = new StreamReader(filePath);
            return sr.ReadToEnd();
        }

        public void Save(string filePath, string content)
        {
            using var sw = new StreamWriter(filePath);
            sw.WriteLine(content);
        }
    }
}
