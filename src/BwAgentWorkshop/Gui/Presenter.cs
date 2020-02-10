using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.AgentLib.ClientLib.Workshop.Gui
{
    public class Presenter
    {
        public Model Model;
        public Presenter(Model model)
        {
            Model = model;
        }
        public virtual void Refresh(bool totalRefresh)
        {
        }
    }
}
