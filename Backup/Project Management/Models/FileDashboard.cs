using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_Management.Models
{
    public class FileDashboard
    {
        public List<projects> pProject { get; set; }
        public List<modules> pModules { get; set; }
        public List<moms> pMoms { get; set; }
        
    }

    public class projects
    {
        public string pFileName { get; set; }
        public string pFilePath { get; set; }
    }

    public class modules
    {
        public int mId { get; set; }
        public string mName { get; set; }
        public List<moduleData> mData { get; set; }
        
    }
    public class moduleData
    {
        public string mFileName { get; set; }
        public string mPathName { get; set; }
    }

    public class moms
    {
        public int momId { get; set; }
        public List<momData> momsData { get; set; }

    }
    public class momData
    {
        public string momFileName { get; set; }
        public string momPathName { get; set; }
    }
}