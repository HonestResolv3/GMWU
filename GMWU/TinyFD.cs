using System;
using System.Runtime.InteropServices;

namespace GMWU
{
    // A class to hold all the dialogs that this programs will use
    // It is a C library that natively calls dialogs based on the users OS
    public static class TinyFD
    {
        const string fileDialogDll = "tinyfiledialogs32.dll";

        // Cross-platform file dialogs
        [DllImport(fileDialogDll, CallingConvention = CallingConvention.Cdecl)] public static extern void tinyfd_beep();

        [DllImport(fileDialogDll, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern int tinyfd_messageBox(string aTitle, string aMessage, string aDialogTyle, string aIconType, int aDefaultButton);

        [DllImport(fileDialogDll, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr tinyfd_openFileDialog(string aTitle, string aDefaultPathAndFile, int aNumOfFilterPatterns, string[] aFilterPatterns, string aSingleFilterDescription, int aAllowMultipleSelects);

        [DllImport(fileDialogDll, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr tinyfd_selectFolderDialog(string aTitle, string aDefaultPathAndFile);

        [DllImport(fileDialogDll, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr tinyfd_inputBox(string aTitle, string aMessage, string aDefaultInput);
    }
}
