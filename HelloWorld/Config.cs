using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web.Configuration;

namespace Config
{
    /// <summary>
    /// 配置文件类
    /// </summary>
    public abstract class AbstConfig
    {
        protected Configuration conf;
        public static AbstConfig GetInstance()
        {
            return null;
        }
        public static AbstConfig GetInstance(string fileName)
        {
            return null;
        }

        public abstract string ReadValue(string ID, string groupName);
        public abstract bool WriteValue(string ID, string value, string groupName);
    }

    /// <summary>
    /// 配置文件数据类
    /// </summary>
    public sealed class ConfigData : ConfigurationSection
    {
        [ConfigurationProperty("ID")]
        public string ID
        {
            get { return (string)this["ID"]; }
            set { this["ID"] = value; }
        }
        [ConfigurationProperty("Value")]
        public string Value
        {
            get { return (string)this["Value"]; }
            set { this["Value"] = value; }
        }
    }

    /// <summary>
    /// 读写web.cofig配置文件的类
    /// </summary>
    public sealed class BSDbConfig : AbstConfig
    {
        public BSDbConfig(string fileName)
        {
            fileName = fileName == "~" ? fileName : "~\\" + fileName;
            conf = WebConfigurationManager.OpenWebConfiguration(fileName);
        }
        private BSDbConfig():this("~")
        {            
        }
        public override string ReadValue(string ID, string groupName)
        {
            string res = null;
            if (conf.SectionGroups[groupName] != null)
            {
                try
                {
                   res = (conf.SectionGroups[groupName].Sections[ID] as ConfigData)?.Value;
                }
                catch (Exception)
                {
                }
            }
            return res;
        }
        public override bool WriteValue(string id, string value, string groupName)
        {
            bool res = true;
            try
            {
                if (conf.SectionGroups[groupName] == null)
                {
                    conf.SectionGroups.Add(groupName, new ConfigurationSectionGroup());
                }
                var data = conf.SectionGroups[groupName].Sections[id] as ConfigurationSection;
                var cfd = new ConfigData() { ID = id,Value = value};
                if (data == null)
                {
                    conf.SectionGroups[groupName].Sections.Add(id, cfd);
                }
                else
                {
                    conf.SectionGroups[groupName].Sections.Remove(id);
                    conf.SectionGroups[groupName].Sections.Add(id, cfd);
                }
                conf.Save();                
            }
            catch (Exception)
            {
                res = false;
            }
            return res;
        }
    }

    /// <summary>
    /// 读写app.config配置文件的类
    /// </summary>
    public sealed class CSDbConfig : AbstConfig
    {
        private CSDbConfig(string configFilePath, string fileName)
        {
            conf = ConfigurationManager.OpenExeConfiguration(System.IO.Path.Combine(configFilePath, fileName));
        }
        private CSDbConfig(string fileName):this(Environment.CurrentDirectory,fileName)
        {            
        }
        public override string ReadValue(string ID, string groupName)
        {
            string res = null;
            if (conf.SectionGroups[groupName] != null)
            {
                res = (conf.SectionGroups[groupName].Sections[ID] as ConfigData)?.Value;
            }
            return res;
        }
        public override bool WriteValue(string id, string value, string groupName)
        {
            bool res = true;
            try
            {
                if (conf.SectionGroups[groupName] == null)
                {
                    conf.SectionGroups.Add(groupName, new ConfigurationSectionGroup());
                }
                var data = conf.SectionGroups[groupName].Sections[id] as ConfigurationSection;
                var cfd = new ConfigData() { ID = id, Value = value };
                if (data == null)
                {
                    conf.SectionGroups[groupName].Sections.Add(id, cfd);
                }
                else
                {
                    conf.SectionGroups[groupName].Sections.Remove(id);
                    conf.SectionGroups[groupName].Sections.Add(id, cfd);
                }
                conf.Save();
            }
            catch
            {
               res = false;
            }
            return res;
        }
    }
}
