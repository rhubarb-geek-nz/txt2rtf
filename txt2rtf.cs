/**************************************************************************
 *
 *  Copyright 2022, Roger Brown
 *
 *  This file is part of Roger Brown's Toolkit.
 *
 *  This program is free software: you can redistribute it and/or modify it
 *  under the terms of the GNU General Public License as published by the
 *  Free Software Foundation, either version 3 of the License, or (at your
 *  option) any later version.
 * 
 *  This program is distributed in the hope that it will be useful, but WITHOUT
 *  ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or
 *  FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for
 *  more details.
 *
 *  You should have received a copy of the GNU Lesser General Public License
 *  along with this program.  If not, see <http://www.gnu.org/licenses/>
 *
 */

 /* 
  * $Id: txt2rtf.cs 272 2023-12-08 01:38:43Z rhubarb-geek-nz $
  */
 
using System;
using System.Text;

namespace txt2rtf
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.ASCII;

            Console.WriteLine("{\\rtf1\\ansi\\deff0\\deftab160 {\\fonttbl\\f0 Courier;}");

            foreach (string arg in args)
            {
                // eg \\fs16
                Console.WriteLine(arg);
            }

            int last = -1;

            while (true)
            {
                int c = Console.Read();

                if (c < 0) break;

                if ((c>=0x20)&&(c<0x7F)&&(c!='\\'))
                {
                    Console.Write((char)c);
                    last = c;
                }
                else
                {
                    if ((c=='\n')||(c=='\r'))
                    {
                        if ((last=='\r')&&(c=='\n'))
                        {

                        }
                        else
                        {
                            Console.WriteLine("\\line");
                        }
                        last = c;
                    }
                    else
                    {
                        if (c < 0x100)
                        {
                            Console.Write(String.Format("\\\'{0:x2}",c));
                        }
                        else
                        {
                            Console.Write(String.Format("\\u{0:x4}", c));
                        }

                        last = c;
                    }
                }
            }
            Console.WriteLine("}");
        }
    }
}
