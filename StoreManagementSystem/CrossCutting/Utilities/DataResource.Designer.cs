﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CrossCutting.Utilities {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class DataResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal DataResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("CrossCutting.Utilities.DataResource", typeof(DataResource).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0}.Up_Save_{1}.
        /// </summary>
        public static string Operation_AddCommand {
            get {
                return ResourceManager.GetString("Operation_AddCommand", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0}.Up_Save_{1}s.
        /// </summary>
        public static string Operation_AddCommands {
            get {
                return ResourceManager.GetString("Operation_AddCommands", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0}.Up_Delete_{1}.
        /// </summary>
        public static string Operation_DeleteCommand {
            get {
                return ResourceManager.GetString("Operation_DeleteCommand", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0}.Up_Delete_{1}s.
        /// </summary>
        public static string Operation_DeleteCommands {
            get {
                return ResourceManager.GetString("Operation_DeleteCommands", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0}.Up_Save_{1}.
        /// </summary>
        public static string Operation_UpdateCommand {
            get {
                return ResourceManager.GetString("Operation_UpdateCommand", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0}.Up_Save_{1}s.
        /// </summary>
        public static string Operation_UpdateCommands {
            get {
                return ResourceManager.GetString("Operation_UpdateCommands", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to {0}.Up_Get_{1}s.
        /// </summary>
        public static string Query_SelectCommand {
            get {
                return ResourceManager.GetString("Query_SelectCommand", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to @{0}s.
        /// </summary>
        public static string TVP_ParameterName {
            get {
                return ResourceManager.GetString("TVP_ParameterName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to TVP_{0}s.
        /// </summary>
        public static string TVP_ParameterType {
            get {
                return ResourceManager.GetString("TVP_ParameterType", resourceCulture);
            }
        }
    }
}
