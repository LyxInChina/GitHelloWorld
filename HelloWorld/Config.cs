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
        protected Configuration _conf;
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
            _conf = WebConfigurationManager.OpenWebConfiguration(fileName);
        }
        private BSDbConfig():this("~")
        {            
        }
        public override string ReadValue(string ID, string groupName)
        {
            string res = null;
            if (_conf.SectionGroups[groupName] != null)
            {
                try
                {
                   res = (_conf.SectionGroups[groupName].Sections[ID] as ConfigData)?.Value;
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
                if (_conf.SectionGroups[groupName] == null)
                {
                    _conf.SectionGroups.Add(groupName, new ConfigurationSectionGroup());
                }
                var data = _conf.SectionGroups[groupName].Sections[id] as ConfigurationSection;
                var cfd = new ConfigData() { ID = id,Value = value};
                if (data == null)
                {
                    _conf.SectionGroups[groupName].Sections.Add(id, cfd);
                }
                else
                {
                    _conf.SectionGroups[groupName].Sections.Remove(id);
                    _conf.SectionGroups[groupName].Sections.Add(id, cfd);
                }
                _conf.Save();                
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
            _conf = ConfigurationManager.OpenExeConfiguration(System.IO.Path.Combine(configFilePath, fileName));
        }
        private CSDbConfig(string fileName):this(Environment.CurrentDirectory,fileName)
        {            
        }
        public override string ReadValue(string ID, string groupName)
        {
            string res = null;
            if (_conf.SectionGroups[groupName] != null)
            {
                res = (_conf.SectionGroups[groupName].Sections[ID] as ConfigData)?.Value;
            }
            return res;
        }
        public override bool WriteValue(string id, string value, string groupName)
        {
            bool res = true;
            try
            {
                if (_conf.SectionGroups[groupName] == null)
                {
                    _conf.SectionGroups.Add(groupName, new ConfigurationSectionGroup());
                }
                var data = _conf.SectionGroups[groupName].Sections[id] as ConfigurationSection;
                var cfd = new ConfigData() { ID = id, Value = value };
                if (data == null)
                {
                    _conf.SectionGroups[groupName].Sections.Add(id, cfd);
                }
                else
                {
                    _conf.SectionGroups[groupName].Sections.Remove(id);
                    _conf.SectionGroups[groupName].Sections.Add(id, cfd);
                }
                _conf.Save();
            }
            catch
            {
               res = false;
            }
            return res;
        }
    }
}
