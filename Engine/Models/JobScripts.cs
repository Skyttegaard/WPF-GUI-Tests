using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Models
{
    public class JobScripts : BaseNotificationClass
    {
        public string Description { get; }
        public string ScriptFail { get; }
        public string Solution { get; }
        public string Hints { get; }
        public string ScriptFix { get; }
        public string Title {get;}

        public JobScripts(string title, string description, string scriptFail, string solution, string hints, string scriptFix)
        {
            Title = title;
            Description = description;
            ScriptFail = scriptFail;
            Solution = solution;
            Hints = hints;
            ScriptFix = scriptFix;
        }
    }
}
