using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Botworx.Wpf.GraphViewInternals
{
    public class LayoutEdge : LayoutAtom
    {
        public LayoutNode Source;
        public LayoutNode Target;
        //
        public double xa;
        public double ya;
        public double xb;
        public double yb;
        public double xc;
        public double yc;
        public double xd;
        public double yd;
        //
        public double X1
        {
            get
            {
                return xa;
            } 
        }
        public double Y1 { 
            get {
                return ya;
            } 
        }
        public double X2 { 
            get {
                return xd;
            } 
        }
        public double Y2 { 
            get {
                return yd;
            } 
        }
        //
        public LayoutEdge(TreeLayout tree, LayoutNode source, LayoutNode target, object element)
        {
            Tree = tree;
            Source = source;
            Target = target;
            Element = element;
        }
        public override void Toggle()
        {
            base.Toggle();
            Target.Toggle();
        }
        //
        public void Update(TreeLayout tree)
        {
            switch (tree.Config.iRootOrientation)
            {
                case TreeLayout.RO_TOP:
                    xa = Source.X + (Source.W / 2);
                    ya = Source.Y + Source.H;
                    break;

                case TreeLayout.RO_BOTTOM:
                    xa = Source.X + (Source.W / 2);
                    ya = Source.Y;
                    break;

                case TreeLayout.RO_RIGHT:
                    xa = Source.X;
                    ya = Source.Y + (Source.H / 2);
                    break;

                case TreeLayout.RO_LEFT:
                    xa = Source.X + Source.W;
                    ya = Source.Y + (Source.H / 2);
                    break;
            }

                switch (tree.Config.iRootOrientation)
                {
                    case TreeLayout.RO_TOP:
                        xd = xc = Target.X + (Target.W / 2);
                        yd = Target.Y;
                        xb = xa;
                        switch (tree.Config.iNodeJustification)
                        {
                            case TreeLayout.NJ_TOP:
                                yb = yc = yd - tree.Config.iLevelSeparation / 2;
                                break;
                            case TreeLayout.NJ_BOTTOM:
                                yb = yc = ya + tree.Config.iLevelSeparation / 2;
                                break;
                            case TreeLayout.NJ_CENTER:
                                yb = yc = ya + (yd - ya) / 2;
                                break;
                        }
                        break;

                    case TreeLayout.RO_BOTTOM:
                        xd = xc = Target.X + (Target.W / 2);
                        yd = Target.Y + Target.H;
                        xb = xa;
                        switch (tree.Config.iNodeJustification)
                        {
                            case TreeLayout.NJ_TOP:
                                yb = yc = yd + tree.Config.iLevelSeparation / 2;
                                break;
                            case TreeLayout.NJ_BOTTOM:
                                yb = yc = ya - tree.Config.iLevelSeparation / 2;
                                break;
                            case TreeLayout.NJ_CENTER:
                                yb = yc = yd + (ya - yd) / 2;
                                break;
                        }
                        break;

                    case TreeLayout.RO_RIGHT:
                        xd = Target.X + Target.W;
                        yd = yc = Target.Y + (Target.H / 2);
                        yb = ya;
                        switch (tree.Config.iNodeJustification)
                        {
                            case TreeLayout.NJ_TOP:
                                xb = xc = xd + tree.Config.iLevelSeparation / 2;
                                break;
                            case TreeLayout.NJ_BOTTOM:
                                xb = xc = xa - tree.Config.iLevelSeparation / 2;
                                break;
                            case TreeLayout.NJ_CENTER:
                                xb = xc = xd + (xa - xd) / 2;
                                break;
                        }
                        break;

                    case TreeLayout.RO_LEFT:
                        xd = Target.X;
                        yd = yc = Target.Y + (Target.H / 2);
                        yb = ya;
                        switch (tree.Config.iNodeJustification)
                        {
                            case TreeLayout.NJ_TOP:
                                xb = xc = xd - tree.Config.iLevelSeparation / 2;
                                break;
                            case TreeLayout.NJ_BOTTOM:
                                xb = xc = xa + tree.Config.iLevelSeparation / 2;
                                break;
                            case TreeLayout.NJ_CENTER:
                                xb = xc = xa + (xd - xa) / 2;
                                break;
                        }
                        break;
                }


                /*tree.ctx.save();
                tree.ctx.strokeStyle = tree.config.linkColor;
                tree.ctx.beginPath();			
                switch (tree.config.linkType)
                {
                    case "M":						
                        tree.ctx.moveTo(xa,ya);
                        tree.ctx.lineTo(xb,yb);
                        tree.ctx.lineTo(xc,yc);
                        tree.ctx.lineTo(xd,yd);						
                        break;
						
                    case "B":
                        tree.ctx.moveTo(xa,ya);
                        tree.ctx.bezierCurveTo(xb,yb,xc,yc,xd,yd);	
                        break;					
                }
                tree.ctx.stroke();
                tree.ctx.restore();*/
        }
    }
}
