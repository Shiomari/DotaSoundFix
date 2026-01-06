using System.Diagnostics;
using System.Runtime.InteropServices;
using WindowsInput;
using WindowsInput.Native;
using Interop.UIAutomationClient;

public class DragDropSimulator
{
    // Импорт необходимых WinAPI функций
    [DllImport("user32.dll")]
    private static extern bool SetForegroundWindow(IntPtr hWnd);

    [DllImport("user32.dll")]
    private static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(out POINT lpPoint);

    [DllImport("user32.dll")]
    private static extern bool SetCursorPos(int x, int y);

    [DllImport("user32.dll")]
    private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);


    // Структуры
    [StructLayout(LayoutKind.Sequential)]
    private struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct POINT
    {
        public int X;
        public int Y;
    }

    public static void SimulateDragDropToApp(string targetProcessName, string[] files,
        int waitForStartup = 8000)
    {
        if (files == null || files.Length == 0)
            throw new ArgumentException("Не указаны файлы для перетаскивания");

        foreach (var file in files)
        {
            if (!File.Exists(file) && !Directory.Exists(file))
                throw new FileNotFoundException($"Файл или папка не найдены: {file}");
        }

        Process targetProcess = StartProcess(targetProcessName, waitForStartup);

        PerformRealDragDropSimulation(targetProcess, files);
    }

    private static Process StartProcess(string targetProcessName, int waitForStartup)
    {
        Process targetProcess = null;

        string processName = Path.GetFileNameWithoutExtension(targetProcessName);

        Process[] processes;
        if (File.Exists(targetProcessName))
        {
            var startInfo = new ProcessStartInfo
            {
                FileName = targetProcessName,
                UseShellExecute = true
            };

            targetProcess = Process.Start(startInfo);
            Thread.Sleep(waitForStartup); // Даем время на запуск
            Console.WriteLine($"Запущен новый процесс: {targetProcessName}");

            processes = Process.GetProcessesByName(processName);
        }
        else
        {
            throw new FileNotFoundException($"Приложение не найдено: {targetProcessName}");
        }

        foreach (var process in processes)
        {
            if (process.MainWindowHandle != IntPtr.Zero)
                targetProcess = process;
            else
                process.CloseMainWindow();
        }

        return targetProcess;
    }

    private static void PerformRealDragDropSimulation(Process targetProcess, string[] files)
    {
        var inputSimulator = new InputSimulator();

        GetCursorPos(out POINT originalCursorPos);

        try
        {
            Process explorerProcess = OpenExplorerWithFile(files[0]);
            Thread.Sleep(1000);

            explorerProcess = Process.GetProcessesByName("explorer").Last();

            MoveWindow(targetProcess.MainWindowHandle, 100, 100, 200, 200, true);
            MoveWindow(explorerProcess.MainWindowHandle, 500, 500, 1000, 500, true);

            SetForegroundWindow(explorerProcess.MainWindowHandle);
            Thread.Sleep(300);

            inputSimulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.CONTROL, VirtualKeyCode.VK_A);
            Thread.Sleep(200);

            RECT explorerRect;
            GetWindowRect(explorerProcess.MainWindowHandle, out explorerRect);

            TryGetFirstItemPosition(explorerProcess.MainWindowHandle, out int x, out int y);

            int fileX = x;
            int fileY = y;

            SetCursorPos(fileX, fileY);
            Thread.Sleep(200);

            inputSimulator.Mouse.LeftButtonDown();
            Thread.Sleep(300);

            RECT targetRect;
            GetWindowRect(targetProcess.MainWindowHandle, out targetRect);

            int targetX = targetRect.Left + (targetRect.Right - targetRect.Left) / 2;
            int targetY = targetRect.Top + (targetRect.Bottom - targetRect.Top) / 2;

            AnimateDragMovement(fileX, fileY, targetX, targetY, inputSimulator);

            Thread.Sleep(500);
            inputSimulator.Mouse.LeftButtonUp();
            Thread.Sleep(15000);

            explorerProcess.CloseMainWindow(); 
            targetProcess.CloseMainWindow();
        }
        finally
        {
            SetCursorPos(originalCursorPos.X, originalCursorPos.Y);
        }
    }

    public static bool TryGetFirstItemPosition(IntPtr explorerHandle, out int x, out int y)
    {
        x = 0;
        y = 0;
        CUIAutomation automation = new CUIAutomation();

        IUIAutomationElement windowElement = automation.ElementFromHandle(explorerHandle);

        IUIAutomationCondition listCondition = automation.CreateOrCondition(
            automation.CreatePropertyCondition(UIA_PropertyIds.UIA_ControlTypePropertyId, UIA_ControlTypeIds.UIA_ListControlTypeId),
            automation.CreatePropertyCondition(UIA_PropertyIds.UIA_ControlTypePropertyId, UIA_ControlTypeIds.UIA_DataGridControlTypeId)
        );

        IUIAutomationElement listElement = windowElement.FindFirst(TreeScope.TreeScope_Descendants, listCondition);

        return TryGetFirstItemFromList(automation, listElement, out x, out y);
    }

    private static bool TryGetFirstItemFromList(CUIAutomation automation, IUIAutomationElement listElement, out int x, out int y)
    {
        x = 0;
        y = 0;

        try
        {
            IUIAutomationCondition itemCondition = automation.CreatePropertyCondition(
                UIA_PropertyIds.UIA_ControlTypePropertyId,
                UIA_ControlTypeIds.UIA_ListItemControlTypeId
            );

            IUIAutomationElement firstItem = listElement.FindFirst(TreeScope.TreeScope_Children, itemCondition);

            if (firstItem != null)
            {
                tagRECT boundingRect = firstItem.CurrentBoundingRectangle;

                if (boundingRect.left != 0 || boundingRect.top != 0 ||
                    boundingRect.right != 0 || boundingRect.bottom != 0)
                {
                    x = (boundingRect.left + boundingRect.right) / 2;
                    y = boundingRect.bottom - 5; 

                    Console.WriteLine($"Первый элемент найден! Координаты: X={x}, Y={y}");
                    Console.WriteLine($"Bounding Rect: L={boundingRect.left}, T={boundingRect.top}, R={boundingRect.right}, B={boundingRect.bottom}");

                    return true;
                }
            }

            Console.WriteLine("Первый элемент списка не найден или имеет нулевые координаты.");
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка получения элемента списка: {ex.Message}");
            return false;
        }
    }

    private static void AnimateDragMovement(int startX, int startY, int endX, int endY, InputSimulator inputSimulator)
    {
        int steps = 20;
        int delay = 20;

        for (int i = 1; i <= steps; i++)
        {
            double t = (double)i / steps;
            double easedT = t * t * (3 - 2 * t);

            int currentX = (int)(startX + (endX - startX) * easedT);
            int currentY = (int)(startY + (endY - startY) * easedT);

            SetCursorPos(currentX, currentY);
            Thread.Sleep(delay);
        }
    }

    public static Process OpenExplorerWithFile(string filePath)
    {
        string directory = Path.GetDirectoryName(filePath);
        var startInfo = new ProcessStartInfo
        {
            FileName = "explorer.exe",
            Arguments = $"/select,\"{filePath}\"",
            UseShellExecute = true
        };

        return Process.Start(startInfo);
    }
}