﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using vibrance.GUI.AMD.vendor.adl32;

namespace vibrance.GUI
{
    public enum GraphicsAdapter
    {
        UNKNOWN = 0,
        NVIDIA = 1,
        AMD = 2,
    }

    public class GraphicsAdapterHelper
    {

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr LoadLibrary(string dllToLoad);

        private const string nvidiaDllName = "nvapi.dll";
        private static string amdDllName = Environment.Is64BitOperatingSystem 
            ? ADLImport.Atiadl_FileName 
            : AMD.vendor.adl64.ADLImport.Atiadl_FileName;


        public static GraphicsAdapter getAdapter()
        {
            if (isAdapterAvailable(nvidiaDllName))
                return GraphicsAdapter.NVIDIA;
            if (isAdapterAvailable(amdDllName))
                return GraphicsAdapter.AMD;
            return GraphicsAdapter.UNKNOWN;
        }

        private static bool isAdapterAvailable(string dllName)
        {
            try
            {
                IntPtr pDll = LoadLibrary(dllName);
                if(pDll != IntPtr.Zero)
                    return true;
                return false;
            }
            catch (Exception)
            {
                return false;
            }    
        }
    }
}
