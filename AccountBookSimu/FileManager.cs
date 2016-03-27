using System.Diagnostics;
using System.IO;
using System.Text;

namespace AccountBookSimu
{
    public class FileManager
    {
        public void SaveSettingFile(string filePath, string fileText)
        {
            File.WriteAllText(filePath, fileText, Encoding.UTF8);
        }

        public string[] ReadSettingFile(string filePath)
        {
            return File.ReadAllLines(filePath, Encoding.UTF8);
        }

        public void SaveSimulatedFile(string filePath, string fileText)
        {
            Trace.TraceInformation("begin FileManager.SaveSimulatedFile");
            File.WriteAllText(filePath, fileText, Encoding.GetEncoding("shift_jis"));
            Trace.TraceInformation("end FileManager.SaveSimulatedFile");
        }
    }
}
