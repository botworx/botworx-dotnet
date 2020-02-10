using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;

using System.Xml;
using System.Xml.Serialization;

namespace Botworx.Mia.Runtime
{
    public static class Archiver
    {
        static public void SerializeToXML(Brain brain)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            //settings.ConformanceLevel = ConformanceLevel.Auto;
            settings.Indent = true;
            settings.IndentChars = "\t";
            XmlWriter writer = XmlWriter.Create("Brain.xml", settings);
            brain.Save(writer);
            writer.Close();
        }
    }
    static class XmlArchiver
    {
        public static void Save(this Brain brain, XmlWriter writer)
        {
            writer.WriteStartDocument(true);
            //
            //String strPI = "type='text/xsl' href='Tree.xsl'";
            //writer.WriteProcessingInstruction("xml-stylesheet", strPI);
            //
            writer.WriteStartElement("brain");
            brain.Processes.Save(writer);
            writer.WriteEndElement();
            //
            writer.WriteEndDocument();
        }
        public static void Save(this List<Process> contexts, XmlWriter writer)
        {
            if (contexts.Count == 0)
                return;
            writer.WriteStartElement("contexts");
            //
            foreach (var context in contexts)
            {
                context.Save(writer);
            }
            writer.WriteEndElement();
        }
        public static void Save(this Process context, XmlWriter writer)
        {
            writer.WriteStartElement("context");
            //
            context.Proposal.Save(writer);
            //
            context.SaveClauses(writer);
            //
            context.Processes.Save(writer);
            //
            writer.WriteEndElement();
        }
        public static void SaveClauses(this Process process, XmlWriter writer)
        {
            writer.WriteStartElement("clauses");
            foreach (var clause in process.Context.Clauses)
            {
                clause.Save(writer);
            }
            writer.WriteEndElement();
        }
        public static void Save(this Proposal proposal, XmlWriter writer)
        {
            writer.WriteStartElement("proposal");
            proposal.Message.Save(writer);
            writer.WriteEndElement();
        }
        public static void Save(this Message message, XmlWriter writer)
        {
            writer.WriteStartElement("message");
            if(message.Clause != null)
                message.Clause.Save(writer);
            writer.WriteEndElement();
        }
        public static void Save(this Atom clause, XmlWriter writer)
        {
            writer.WriteStartElement("clause");
            //
            /*string content = string.Format("{0} {1} ", 
                clause.Subject.ToString(),
                clause.Predicate.ToString()
                );
            if (clause.Object == null)
                content += "NIL";
            else
                content += clause.Object.ToString();
            //
            writer.WriteString(content);*/
            writer.WriteString(clause.ToString());
            //
            writer.WriteEndElement();
        }
    }
}
