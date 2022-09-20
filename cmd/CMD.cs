﻿public class CMD
{
	public static string CurrentPath { get; set; } = @"D:\";
	public static string Clear { get; set; } = "cls";
	public static string Cd { get; set; } = "cd ";
	public static string Dir { get; set; } = "dir";
	public static string Delete { get; set; } = "del ";
	public static string MoveBack { get; set; } = "cd..";
	public static string Mkdir { get; set; } = "mkdir ";
	public static string Copy { get; set; } = "copy ";
	public static string Xcopy { get; set; } = "xcopy ";
	public static string Tree { get; set; } = "tree";
	public static string Move { get; set; } = "move ";
	public static string Xmove { get; set; } = "xmove ";

	public static void ClearConsole() => Console.Clear();

	public static void ChangeToOldPath()
	{
		var directory = new DirectoryInfo(CurrentPath);

		if (directory.Parent == null)
			CurrentPath = @"D:\";
		else
			CurrentPath = directory.Parent.FullName;
	}

	public static void Color(string text)
	{
		var data = text.Split();

		if (data.Length == 2)
		{
			int no = int.Parse(data[1]);

			switch ((Colors)no)
			{
				case Colors.Blue:
					Console.ForegroundColor = ConsoleColor.Blue;
					break;
				case Colors.Yellow:
					Console.ForegroundColor = ConsoleColor.Yellow;
					break;
				case Colors.DarkRed:
					Console.ForegroundColor = ConsoleColor.DarkRed;
					break;
			}
		}
	}

	public static void Show()
	{
		var dirInfo = new DirectoryInfo(CurrentPath);
		var dirs = dirInfo.GetFileSystemInfos();

		if (dirs != null)
		{
			foreach (var dir in dirs)
			{
				Console.Write(dir.CreationTime);
				Console.Write(" => " + dir.Name);
				Console.WriteLine();
			}
		}
		else
			Console.WriteLine("There is not FILE or DIRECTORY !");
	}

	public static void ShowAll(string path)
	{
		var dirInfo = new DirectoryInfo(path);
		var dirs = dirInfo.GetDirectories();

		foreach (var dirr in dirs)
		{
			if (dirr.Name == "$RECYCLE.BIN")
				continue;
			if (dirr.Name == "System Volume Information")
				continue;

			var files = dirr.GetFiles();
			foreach (var file in files)
			{
				Console.Write("-----");
				Console.WriteLine(file);
			}

			ShowAll(dirr.FullName);
		}
	}
	public static string OpenFolder(string path, string data)
	{
		var dirinfo = new DirectoryInfo(path);
		var dirs = dirinfo.GetFileSystemInfos();
		var data2 = data.Split(' ');

		if (dirs != null)
		{
			foreach (var item in dirs)
				if (data2[1] == item.Name)
					return item.FullName;
		}
		else
			Console.WriteLine("There is not FILE or DIRECTORY !");

		return null;
	}

	public static void CreatFolder(string data)
	{
		var dirInfo = new DirectoryInfo(CurrentPath);
		var dirs = dirInfo.GetFileSystemInfos();
		bool check = false;

		foreach (var item in dirs)
		{
			if (item.Name == data)
			{
				check = true;
				break;
			}
		}

		if (check)
			Console.WriteLine("Already there is a FOLDER !");
		else
			dirInfo.CreateSubdirectory(data);
	}

	public static void CreatTxt(string data)
	{
		var dirInfo = new DirectoryInfo(CurrentPath);
		var dirs = dirInfo.GetFileSystemInfos();
		bool check = false;

		foreach (var item in dirs)
		{
			if (item.Name == data)
			{
				check = true;
				break;
			}
		}

		if (check)
			Console.WriteLine("Already there is a FILE !");
		else
		{
			string fileName = CurrentPath + @"\" + data;
			using (StreamWriter sw = File.CreateText(fileName))
			{
				sw.WriteLine("");
			}
		}
	}

	public static void CopyFile(string data, string path)
	{
		var dirInfo = new DirectoryInfo(CurrentPath);
		var dirs = dirInfo.GetFileSystemInfos();

		foreach (var item in dirs)
			if (data == item.FullName)
				File.Copy(data, path + @"\" + Path.GetFileName(data));
	}

	public static void MoveFile(string data, string path)
	{
		var dirInfo = new DirectoryInfo(CurrentPath);
		var dirs = dirInfo.GetFileSystemInfos();

		foreach (var item in dirs)
			if (data == item.FullName)
				File.Move(data, path + @"\" + Path.GetFileName(data));
	}

	public static void CopyDirectory(string sourceDirectory, string targetDirectory)
	{
		var diSource = new DirectoryInfo(sourceDirectory);
		var diTarget = new DirectoryInfo(targetDirectory);

		CopyAll(diSource, diTarget);
	}

	public static void CopyAll(DirectoryInfo source, DirectoryInfo target)
	{
		Directory.CreateDirectory(target.FullName);

		// Copy each file into the new directory
		foreach (FileInfo fi in source.GetFiles())
			fi.CopyTo(Path.Combine(target.FullName, fi.Name), true);

		// Copy each subdirectory using recursion
		foreach (DirectoryInfo diSourceSubDir in source.GetFileSystemInfos())
		{
			DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);

			CopyAll(diSourceSubDir, nextTargetSubDir);
		}
	}

	public static void MoveDirectory(string sourceDirectory, string targetDirectory)
	{
		var diSource = new DirectoryInfo(sourceDirectory);
		var diTarget = new DirectoryInfo(targetDirectory);

		MoveAll(diSource, diTarget);
	}

	public static void MoveAll(DirectoryInfo source, DirectoryInfo target)
	{
		Directory.CreateDirectory(target.FullName);

		// Copy each file into the new directory
		foreach (FileInfo fi in source.GetFiles())
		{
			fi.MoveTo(Path.Combine(target.FullName, fi.Name));
		}

		// Copy each subdirectory using recursion
		foreach (DirectoryInfo diSourceSubDir in source.GetFileSystemInfos())
		{
			DirectoryInfo nextTargetSubDir = target.CreateSubdirectory(diSourceSubDir.Name);

			MoveAll(diSourceSubDir, nextTargetSubDir);
		}
	}

	public static void DeleteFile(string path)
	{
		var dirInfo = new DirectoryInfo(CurrentPath);
		var dirs = dirInfo.GetFileSystemInfos();

		foreach (var item in dirs)
			if (path == item.FullName)
				File.Delete(path);
	}
}