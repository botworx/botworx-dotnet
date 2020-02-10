using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Xml;
using System.Windows.Markup;

using GraphLayout;

namespace Botworx.AgentLib.ClientLib.Workshop.Gui
{
    public class ContextGraphCanvas : TreeCanvas, INameScope
    {
        public AgentModel Model;
        //
        private Dictionary<string, object> Scope = new Dictionary<string, object>();
        //
        public ContextGraphCanvas(){
            NameScope.SetNameScope(this, this);
        }
        #region INameScope Members

        object INameScope.FindName(string name)
        {
            return Scope[name];
            //throw new NotImplementedException();
        }

        void INameScope.RegisterName(string name, object scopedElement)
        {
            Scope[name] = scopedElement;
            //throw new NotImplementedException();
        }

        void INameScope.UnregisterName(string name)
        {
            Scope.Remove(name);
            //throw new NotImplementedException();
        }

        #endregion
    }
}
