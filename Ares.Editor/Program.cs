﻿/*
 Copyright (c) 2010 [Joerg Ruedenauer]
 
 This file is part of Ares.

 Ares is free software; you can redistribute it and/or modify
 it under the terms of the GNU General Public License as published by
 the Free Software Foundation; either version 2 of the License, or
 (at your option) any later version.

 Ares is distributed in the hope that it will be useful,
 but WITHOUT ANY WARRANTY; without even the implied warranty of
 MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 GNU General Public License for more details.

 You should have received a copy of the GNU General Public License
 along with Ares; if not, write to the Free Software
 Foundation, Inc., 51 Franklin St, Fifth Floor, Boston, MA  02110-1301  USA
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Ares.Editor
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                BassRegistration.Registration.RegisterBass();

                if (!Un4seen.Bass.Bass.BASS_Init(-1, 44100, Un4seen.Bass.BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero))
                {
                    MessageBox.Show(StringResources.BassInitFail, StringResources.Ares, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                if (!Un4seen.Bass.AddOn.Fx.BassFx.LoadMe())
                {
                    MessageBox.Show(StringResources.BassInitFail, StringResources.Ares, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
            }
            catch (Exception)
            {
                MessageBox.Show(StringResources.BassInitFail, StringResources.Ares, MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            try
            {
                Application.Run(new MainForm());
            }
            catch (Ares.Ipc.ApplicationAlreadyStartedException)
            {
            }
            Un4seen.Bass.Bass.BASS_Free();
        }
    }
}
