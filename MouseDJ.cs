using System;
using System.Runtime.InteropServices;

namespace AutoMouse
{
    class MouseDJ
    {
        [DllImport("user32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint SendInput(uint numberOfInputs, INPUT[] inputs, int sizeOfInputStructure);
        [DllImport("user32.dll")]
        static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, int dwExtraInfo);
        const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        const uint MOUSEEVENTF_LEFTUP = 0x0004;
        const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
        const uint MOUSEEVENTF_RIGHTUP = 0x0010;
        const uint MOUSEEVENTF_WHEEL = 0x0800;

        [StructLayout(LayoutKind.Sequential)]

        struct INPUT
        {
            public uint type;
            public MOUSEKEYBDHARDWAREINPUT data;
        }

        [StructLayout(LayoutKind.Explicit)]
        struct MOUSEKEYBDHARDWAREINPUT
        {
            [FieldOffset(0)]
            public MOUSEINPUT mouseInput;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public int mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }   

        public static void MouseClick(int clickCount, int interval)
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
            //mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
            /*  for (int i = 0; i < clickCount; i++){
              INPUT input = new INPUT();
                  input.type = INPUT_MOUSE;
                  input.data.mouseInput.dx = 0;
                  input.data.mouseInput.dy = 0;
                  input.data.mouseInput.mouseData = 0;
                  input.data.mouseInput.dwFlags = MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP;
                  input.data.mouseInput.time = 0;
                  input.data.mouseInput.dwExtraInfo = IntPtr.Zero;

                  SendInput(1, new INPUT[] { input }, Marshal.SizeOf(typeof(INPUT)));
                 // System.Threading.Thread.Sleep(interval);
              }*/
        }
    }
}
