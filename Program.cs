using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace opentk
{
    public class Program
    {

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool AllocConsole();


    [STAThread]
        public static void Main(string[] args)
        {        

            string fileName = "";
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    fileName = openFileDialog.FileName;
                }
            }
            //Console.WriteLine(fileName);

            if (string.IsNullOrEmpty(fileName))
            {
                Console.WriteLine("no se cargo..." + Environment.NewLine + "cerrar este programa...");
                Console.Read();
                return;
            }
            LoadOpenTK(fileName);

        }
        public static void LoadOpenTK(string fileName)
        {
            var nativeWindowSettings = new NativeWindowSettings()
            {
                Size = new Vector2i(800, 600),
                Title = "Programacion Grafica",                
                Flags = ContextFlags.ForwardCompatible,
            };

            using (var window = new game(fileName, GameWindowSettings.Default, nativeWindowSettings))
            {
                window.Run();
            }
        }
    }
}
