using System;

enum Colors { Blue = 1, Yellow = 2, DarkRed = 3 };

class Program
{
	static void Start()
	{
		CMD.CurrentPath = @"C:\";

		while (true)
		{
			Console.Write(CMD.CurrentPath + '>');

			string? command = Console.ReadLine();

			if (command?.Trim() == CMD.MoveBack)
				CMD.ChangeToOldPath();
			else if (command.ToLower().StartsWith("color"))
				CMD.Color(command);
			else if (command.ToLower().StartsWith("cls"))
				CMD.ClearConsole();
			else if (command.ToLower().StartsWith("dir"))
				CMD.Show();
			else if (command.ToLower().StartsWith("cd "))
			{
				var result = CMD.OpenFolder(CMD.CurrentPath, command);

				if (result != null)
					CMD.CurrentPath = result;
			}
			else if (command.ToLower().StartsWith("mkdir "))
			{
				var result = command.Split(' ');

				CMD.CreatFolder(result[1]);
			}
			else if (command.ToLower().StartsWith("copy "))
			{
				var result = command.Split(' ');

				CMD.CopyFile(result[1], result[2]);
			}
			else if (command.ToLower().StartsWith("tree"))
				CMD.ShowAll(CMD.CurrentPath);
			else if (command.ToLower().StartsWith("move "))
			{
				var result = command.Split(' ');

				CMD.MoveFile(result[1], result[2]);
			}
			else if (command.ToLower().StartsWith("del "))
			{
				var result = command.Split(' ');

				CMD.DeleteFile(result[1]);
			}
			else if (command.ToLower().StartsWith("xcopy"))
			{
				var result = command.Split(' ');

				CMD.CopyDirectory(result[1], result[2]);
			}
			else if (command.ToLower().StartsWith("xmove"))
			{
				var result = command.Split(' ');

				CMD.MoveDirectory(result[1], result[2]);
			}
			else if (command.ToLower().StartsWith("exit"))
			{
				Environment.Exit(0);
				break;
			}
		}
	}

	static void Main()
	{
		while (true)
		{
			try
			{
				Start();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}
	}
}