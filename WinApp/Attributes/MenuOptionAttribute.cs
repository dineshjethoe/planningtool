using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinApp.Attributes
{
    public class MenuOptionAttribute : Attribute
    {
        public string Name { get; private set; }

        public MenuOptionAttribute(string name)
        {
            this.Name = name;
        }
    }
}
