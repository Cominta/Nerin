using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NiDE.NiDE
{
    public class Loader
    {
        private string file = "save.bin";

        public List<string> Load()
        {
            string savesFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "saves");
            string filePath = Path.Combine(savesFolderPath, file);

            List<string> loadedData = new List<string>();

            try
            {
                if (File.Exists(filePath))
                {
                    using (BinaryReader reader = new BinaryReader(File.Open(filePath, FileMode.Open)))
                    {
                        while (reader.BaseStream.Position < reader.BaseStream.Length)
                        {
                            string line = reader.ReadString();
                            loadedData.Add(line);
                        }
                    }
                }
            }
            catch (IOException e)
            {
                MessageBox.Show("Error saving file: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return loadedData;
        }
    }
}
