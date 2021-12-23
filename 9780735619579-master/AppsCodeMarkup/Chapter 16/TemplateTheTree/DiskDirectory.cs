//----------------------------------------------
// DiskDirectory.cs (c) 2006 by Charles Petzold
//----------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;

namespace Petzold.TemplateTheTree
{
    public class DiskDirectory
    {
        DirectoryInfo dirinfo;

        // Constructor requires DirectoryInfo object.
        public DiskDirectory(DirectoryInfo dirinfo)
        {
            this.dirinfo = dirinfo;
        }

        // Name property returns directory name.
        public string Name
        {
            get { return dirinfo.Name; }
        }

        // Subdirectories property returns collection of DiskDirectory objects.
        public List<DiskDirectory> Subdirectories
        {
            get
            {
                List<DiskDirectory> dirs = new List<DiskDirectory>();
                DirectoryInfo[] subdirs;

                try
                {
                    subdirs = dirinfo.GetDirectories();
                }
                catch
                {
                    return dirs;
                }

                foreach (DirectoryInfo subdir in subdirs)
                    dirs.Add(new DiskDirectory(subdir));

                return dirs;
            }
        }
    }
}
