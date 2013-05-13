using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AppNetDotNet;
using AppNetDotNet.Model;
using AppNetDotNet.ApiCalls;

/*
 * This file is part of Crypt.NET.

    Crypt.NET is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    Crypt.NET. is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with Crypt.NET..  If not, see <http://www.gnu.org/licenses/>.
 * 
 */

namespace ProjectClient
{    
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new Mainform());

            //System.Windows.Forms.Application.Run(new Keys());
        }
    }
}
