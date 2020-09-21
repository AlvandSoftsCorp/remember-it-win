using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RememberIt
{
    public class HtmlDocBuilder
    {
        String Doc = "";

        public void OpenTag(int indent, String tagName, String[] attributes)
        {
            String sp = "";
            String att = "";

            for (int i = 0; i < indent * 4; i++)
            {
                sp = sp + " ";
            }

            if (attributes != null)
            {
                for (int i = 0; i < attributes.Length; i++)
                {
                    att = att + attributes[i] + " ";
                }
                att = " " + att;
            }
            String tag = String.Format("{0}<{1}{2}>\r\n", sp, tagName, att);
            this.Doc = this.Doc + tag;
        }

        public void OpenTag(int indent, String tagName)
        {
            this.OpenTag(indent, tagName, null);
        }


        public void CloseTag(int indent, String tagName)
        {
            String sp = "";

            for (int i = 0; i < indent * 4; i++)
            {
                sp = sp + " ";
            }
            String tag = String.Format("{0}</{1}>\r\n", sp, tagName);
            this.Doc = this.Doc + tag;
        }


        public void PutText(int indent, String text)
        {
            String sp = "";

            for (int i = 0; i < indent * 4; i++)
            {
                sp = sp + " ";
            }

            String txt = String.Format("{0}{1}\r\n", sp, text);
            this.Doc = this.Doc + txt;
        }

        public void PutEmptyLine(int indent)
        {
            String sp = "";

            for (int i = 0; i < indent * 4; i++)
            {
                sp = sp + " ";
            }

            String br = String.Format("{0}<br />\r\n", sp);
            this.Doc = this.Doc + br;
        }


        public String GetDoc()
        {
            return this.Doc;
        }


        public static string GetParagraphTag(string Text)
        {
            byte[] file_bytes = global::RememberIt.Properties.Resources.Paragraph;
            if (file_bytes.Length <= 3) return "";
            string tag = ASCIIEncoding.ASCII.GetString(file_bytes, 3, file_bytes.Length - 3);
            
            tag = tag.Replace("$paragraph", Text);
            tag += "\r\n";
            return tag;
        }


        public static string GetAudioTag(string Source, string SourceType)
        {
            byte[] file_bytes = global::RememberIt.Properties.Resources.Audio;
            if (file_bytes.Length <= 3) return "";
            string tag = ASCIIEncoding.ASCII.GetString(file_bytes, 3, file_bytes.Length - 3);

            tag = tag.Replace("$source", Source);
            tag = tag.Replace("$type", SourceType);
            tag += "\r\n";
            return tag;
        }
        
        
        public static string GetImageTag(string Source, string Alt, int Width)
        {
            byte[] file_bytes = global::RememberIt.Properties.Resources.Image;
            if (file_bytes.Length <= 3) return "";
            string tag = ASCIIEncoding.ASCII.GetString(file_bytes, 3, file_bytes.Length - 3);
            
            tag = tag.Replace("$source", Source);
            tag = tag.Replace("$alt", Alt);
            tag = tag.Replace("$width", Width.ToString());
            tag += "\r\n";
            return tag;
        }
    }
}
