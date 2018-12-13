using System;

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
