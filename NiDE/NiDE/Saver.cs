using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace NiDE.NiDE
{
    public class Saver
    {
        private string text;
        private string file = "save.bin";

        public Saver(string _text)
        {
            text = _text;
        }

        public void Save()
        {
            string[] lines = text.Split(new string[] { "\n" }, StringSplitOptions.None);
            string savesFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "saves");

            if (!Directory.Exists(savesFolderPath))
            {
                Directory.CreateDirectory(savesFolderPath);
            }

            string filePath = Path.Combine(savesFolderPath, file);

            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(filePath, FileMode.Create)))
                {
                    foreach (string line in lines)
                    {
                        writer.Write(line);
                    }
                }
            }
            catch (IOException e)
            {
                MessageBox.Show("Error saving file: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
