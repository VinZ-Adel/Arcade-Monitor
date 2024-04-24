using System.Diagnostics;
using System.Text.RegularExpressions;

namespace Arcade_Monitor
{
    public partial class Form1 : Form
    {
        const string MainName = "adelfors-arcade";
        string directory;
        string mainExe;
		string configPath;
		string[] config = [];

		List<string> gameNames = new();
        bool skip = false;
        List<string> folders = new();

		//readonly Regex regex = new(@"\w*\.exe*$", RegexOptions.IgnoreCase);

		public Form1()
		{
			InitializeComponent();
			directory = System.AppDomain.CurrentDomain.BaseDirectory;
			label1.Text = directory;
			mainExe = directory + @"/adelfors-arcade.exe";
			configPath = directory + @"/arcade_config.txt";

			try
			{
				config = File.ReadAllLines(configPath);
			}
			catch { }

			bool read = false;
			for (int i = 0; i < config.Count(); i++)
			{
				if (config[i] == "[Folders]")
				{
					read = true;
					continue;
				}
				if (read)
				{
					char[] chars = config[i].ToCharArray();
					if (chars[0] == '.')
					{
						folders.Add(directory + chars[1..]);
					}
					else if (chars[0] == '[')
					{
						read = false;
						continue;
					}
					else { folders.Add(config[i]); }
				}
			}

			if (folders.Count == 0)
			{
				folders.Add(directory + @"/games");
			}


			List<string> gameExes = new();
			foreach (string fl in folders)
			{
				try
				{
					gameExes.AddRange(Directory.GetFiles(fl, "*.EXE", SearchOption.AllDirectories));
				}
				catch { label1.Text = "ERROR: Problem with one or more game directories. Please check config file syntax."; skip = true; gameNames = ["ERROR"]; }
			}
			foreach (string gameEx in gameExes)
			{
				//Match match = regex.Match(gameEx);
				//gameNames.Add(match.Value.Split(".")[0]);
				string[] temp = gameEx.Split(@"\");
				gameNames.Add(temp[^1].Split(".")[0]);
			}

			string tempFound = "";
			string tempDirs = "";

			foreach (string game in gameNames)
				tempFound += "\n" + game;
			richTextBoxFound.Text = tempFound;
			foreach (string dir in folders)
				tempDirs += "\n" + dir;
			richTextBoxDirs.Text = tempDirs;
		}




        /// <summary>
        /// Forces Window with given handle to foreground (forces focus to it)
        /// </summary>
        /// <param name="hWnd"></param>
        public static void ForceWindowIntoForeground(IntPtr hWnd) // copypasta
        {
            uint currentThread = Win32.GetCurrentThreadId();

            IntPtr activeWindow = Win32.GetForegroundWindow();
            uint activeProcess;
            uint activeThread = Win32.GetWindowThreadProcessId(activeWindow, out activeProcess);

            uint windowProcess;
            uint windowThread = Win32.GetWindowThreadProcessId(hWnd, out windowProcess);

            if (currentThread != activeThread)
                Win32.AttachThreadInput(currentThread, activeThread, true);
            if (windowThread != currentThread)
                Win32.AttachThreadInput(windowThread, currentThread, true);

            uint oldTimeout = 0, newTimeout = 0;
            Win32.SystemParametersInfo(Win32.SPI_GETFOREGROUNDLOCKTIMEOUT, 0, ref oldTimeout, 0);
            Win32.SystemParametersInfo(Win32.SPI_SETFOREGROUNDLOCKTIMEOUT, 0, ref newTimeout, 0);
            Win32.LockSetForegroundWindow(Win32.LSFW_UNLOCK);
            Win32.AllowSetForegroundWindow(Win32.ASFW_ANY);

            Win32.SetForegroundWindow(hWnd);
            Win32.ShowWindow(hWnd, Win32.SW_RESTORE);

            Win32.SystemParametersInfo(Win32.SPI_SETFOREGROUNDLOCKTIMEOUT, 0, ref oldTimeout, 0);

            if (currentThread != activeThread)
                Win32.AttachThreadInput(currentThread, activeThread, false);
            if (windowThread != currentThread)
                Win32.AttachThreadInput(windowThread, currentThread, false);
        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                timer1.Interval = (int)numericUpDown1.Value;
            }
            catch
            { checkBox1.Checked = false; }
            List<Process> processes = [.. Process.GetProcesses()];
            List<Process> runningGames = new();
            if (!skip)
            {
                runningGames = processes.Where(c => gameNames.Contains(c.ProcessName) && c.ProcessName != "UnityCrashHandler64").ToList();
            }
            List<Process> runningMains = processes.Where(c => MainName == c.ProcessName).ToList();
            //Process thisProcess = Process.GetCurrentProcess();

            string tempRunning = "";
			string tempFound = "";
			string tempDirs = "";

			foreach (Process p in runningGames)
                tempRunning += '\n' + p.ProcessName;
            richTextBoxRunning.Text = tempRunning;
			foreach (string game in gameNames)
				tempFound += "\n" + game;
			richTextBoxFound.Text = tempFound;
			foreach (string dir in folders)
				tempDirs += "\n" + dir;
			richTextBoxDirs.Text = tempDirs;

            nint focusHWND = Win32.GetForegroundWindow();

            //If no games have focus								and there are games running		and this window doesn't have focus
            if (!runningGames.Any(c => c.MainWindowHandle == focusHWND) && runningGames.Count() >= 1 && this.Handle != focusHWND) //thisProcess.MainWindowHandle
            {
                ForceWindowIntoForeground(runningGames[^1].MainWindowHandle); //force focus to game
            }       //if no games are running	and menu IS running			and menu doesn't have focus
            else if (runningGames.Count() == 0 && runningMains.Count >= 1 && !runningMains.Any(c => c.MainWindowHandle == focusHWND))
            {
                ForceWindowIntoForeground(runningMains[^1].MainWindowHandle); //force focus to menu
            }
            else
            {
                if (runningMains.Count() == 0) //if no menu is running
                    Process.Start(mainExe); //start
            }



            //List<nint> runningGameHandles = new();

            //foreach (Process process in runningGames)
            //	runningGameHandles.Add(process.MainWindowHandle);

            //if (runningMains.Count() > 1)
            //{
            //	for (int i = 0; i < runningMains.Count() - 1; i++)
            //	{
            //		runningMains[i].Kill();
            //	}
            //}

            //
            //if (runningGames.Count() != 0)
            //{
            //	if (!runningGameHandles.Contains(FocusHWND))
            //	{
            //		ForceWindowIntoForeground(runningGameHandles[^1]);
            //	}
            //}
            //else if (runningMains.Count() != 0)
            //{

            //}


        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox? cb = sender as CheckBox;
            if (cb != null)
                timer1.Enabled = cb.Checked;
        }
    }
}
