using System.Data;
using System.Diagnostics;

Process wingetver = Process.Start(
    new ProcessStartInfo()
    {
        FileName = "winget",
        Arguments = "-v",
        UseShellExecute = false,
        RedirectStandardOutput = true
    }
);

string output = wingetver.StandardOutput.ReadToEnd();
wingetver.WaitForExit();

Console.Title = "WinGet Interactive";

if (wingetver is not null)
{
    Console.WriteLine(
        "Windows Package Manager" +
        (output.Contains("preview") ? " (Preview) " : " ") +
        output.Replace("\n", "")
    );
}

Console.Write("Copyright (c) Microsoft Corporation. All rights reserved.");
Console.WriteLine("\n");

while (true)
{
    // Print the prompt
    const string prompt = "winget > ";
    Console.Write(prompt);

    // Get input from the user
    string? input = Console.ReadLine();

    if (input is not null)
    {
        // Uncomment for debugging
        // Console.WriteLine("DEBUG: " + input, Console.ForegroundColor = ConsoleColor.Magenta);

        if (input is "cls" or "clear")
        {
            Console.Clear();
        }
        else if (input is "quit" or "exit")
        {
            Environment.Exit(0);
        }
        else
        {
            Process winget = Process.Start("winget", input);
            winget.WaitForExit();
        }
    }
}