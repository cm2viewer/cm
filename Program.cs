using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace cm
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SaveGame.path = Application.StartupPath + "\\";
            if (System.Diagnostics.Debugger.IsAttached)
                SaveGame.path = "C:\\Download\\cm9798\\";

            if (!System.IO.File.Exists(SaveGame.path + "GAMESS16.IDX"))
            {
                MessageBox.Show(SaveGame.path + "GAMESS16.IDX not found");
            }
            else
            {
                var list = SaveGame.ReadSaveGameList();
                int numAvailable = list.Count(s => s.Available.Equals(true));
                if (numAvailable == 1)
                {
                    int index = list.First(s => s.Available == true).index;
                    SaveGame.SelectSaveGame(index);
                    Application.Run(new FormMain());
                }
                else
                {
                    Application.Run(new FormSelect());
                    if (SaveGame.SelectedSaveGame > 0)
                        Application.Run(new FormMain());
                }
            }
        }
    }
}
