﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AllModelCreatorInWPF.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "16.3.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("G:\\My Drive\\02. Bay View\\03. BIM\\Trade Partners Workspace\\02 Weekly Updates ALL\\C" +
            "urrent\\")]
        public string InputRootFolder {
            get {
                return ((string)(this["InputRootFolder"]));
            }
            set {
                this["InputRootFolder"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("_")]
        public string NWDFolderQualifier {
            get {
                return ((string)(this["NWDFolderQualifier"]));
            }
            set {
                this["NWDFolderQualifier"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("G:\\My Drive\\02. Bay View\\03. BIM\\Trade Partners Workspace\\02 Weekly Updates ALL\\C" +
            "urrent\\ZZ Published Container NWD\\CurrentContainer\\")]
        public string OutputFolder {
            get {
                return ((string)(this["OutputFolder"]));
            }
            set {
                this["OutputFolder"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("license.whiting-turner.com")]
        public string LicenseServer {
            get {
                return ((string)(this["LicenseServer"]));
            }
            set {
                this["LicenseServer"] = value;
            }
        }
    }
}
