using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace kakao
{
    // cool and useful functions
    class Toolkit
    {
        // check if window = form is already open
        public bool IsFormOpen(string name)
        {
            // go through all of the forms
            foreach (Form window in Application.OpenForms)
            {
                // check name of each form and return true
                // if it matches our search
                if (window.Name == name) return true;
            }

            // above didn't hit true so we just return false
            return false;
        }

        // check if form is visible
        public bool IsFormVisible(string name)
        {
            // go through all of the forms
            foreach (Form window in Application.OpenForms)
            {
                // check name of each form and return true
                // if it matches our search + form is visible
                if (window.Name == name && window.Visible)
                    return true;
            }

            // above didn't hit true so we just return false
            return false;
        }

        // change memory protection
        public bool DisableMemoryProtection(uint address, int size)
        { return VirtualProtectEx(Main.mainGameProcess.Handle, (IntPtr)address, size, 0x40, out IntPtr zero); }

        // write memory easily, a cool wrapper
        public bool WriteMemory(uint address, byte[] array)
        {
            // longer function in wrapper
            return WriteProcessMemory(Main.mainGameProcess.Handle, (IntPtr)address,
                array, array.Length, out IntPtr nullification);
        }

        public uint ReadMemoryInt32(uint address)
        {
            // 4 because int is 4
            byte[] rawBytes = new byte[4];

            // read raw bytes
            ReadProcessMemory(Main.mainGameProcess.Handle, (IntPtr)address,
            rawBytes, rawBytes.Length, out IntPtr nullification);

            // return parsed bytes to int
            return BitConverter.ToUInt32(rawBytes, 0);
        }

        // reads float value from memory
        public float ReadMemoryFloat(uint address)
        {
            // 4 because float is 4
            byte[] rawBytes = new byte[4];

            // read raw bytes
            ReadProcessMemory(Main.mainGameProcess.Handle, (IntPtr)address,
            rawBytes, rawBytes.Length, out IntPtr nullification);

            // return parsed bytes to float
            return BitConverter.ToSingle(rawBytes, 0);
        }

        // checks if key is pressed
        public bool IsKeyPressed(int keyCode)
        {
            // read key status from table and compare
            short keyStatus = GetAsyncKeyState(keyCode);

            // return if pressed
            if ((keyStatus & 0x8000) > 0) return true;
            else return false;
        }

        // merging byte arrays with concat
        public byte[] MergeByteArrays(params byte[][] arrays)
        {
            // create empty array
            byte[] byteArray = new byte[0];

            // go through each arrays and connect with concat
            foreach (byte[] array in arrays)
                byteArray = byteArray.Concat(array).ToArray();

            // returned merged arrays
            return byteArray;
        }

        // imported protection change
        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool VirtualProtectEx(IntPtr hProcess, IntPtr lpAddress,
        int dwSize, uint flNewProtect, out IntPtr lpflOldProtect);

        // imported write memory 
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteProcessMemory(
        IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, out IntPtr lpNumberOfBytesWritten);

        // exported read memory
        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool ReadProcessMemory(
        IntPtr hProcess, IntPtr lpBaseAddress, byte[] lpBuffer, int nSize, out IntPtr lpNumberOfBytesRead);

        // imported key state
        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vKey);
    }
}
