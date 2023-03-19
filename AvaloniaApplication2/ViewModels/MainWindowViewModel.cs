using System;
using System.Threading;

namespace AvaloniaApplication2.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public string Greeting => "Welcome to Avalonia!";

    public static string TimeString => GetTime();

    static string GetTime()
    {
        while (true)
        {
            DateTime currentTime;
            currentTime = DateTime.Now;
            Thread.Sleep(1000);
            return currentTime.ToString("hh:mm:ss");
        }
    }
}
