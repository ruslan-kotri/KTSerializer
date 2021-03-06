﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KT.Common.Properties {
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
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("KT.Common.Classes.Application.Properties.Resources", typeof(Resources).Assembly);
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
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to GUID &apos;{0}&apos; of a serialization attribute is not unique..
        /// </summary>
        internal static string Error_AttributeGuidIsNotUnique {
            get {
                return ResourceManager.GetString("Error_AttributeGuidIsNotUnique", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Attribute &apos;{0}&apos; cannot be applied more than once in a class..
        /// </summary>
        internal static string Error_AttributeIsMoreThanOnce {
            get {
                return ResourceManager.GetString("Error_AttributeIsMoreThanOnce", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to When serializer is set to not to use processed types, a common header stream must not be provided..
        /// </summary>
        internal static string Error_CommonHeaderStreamWithoutUseProcessedTypes {
            get {
                return ResourceManager.GetString("Error_CommonHeaderStreamWithoutUseProcessedTypes", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to deserialize GUID from string &apos;{0}&apos;..
        /// </summary>
        internal static string Error_FailedToDeserializeGuid {
            get {
                return ResourceManager.GetString("Error_FailedToDeserializeGuid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to When serializer is set to use processed types, a common header stream must be provided..
        /// </summary>
        internal static string Error_NoCommonHeaderStreamWithUseProcessedTypes {
            get {
                return ResourceManager.GetString("Error_NoCommonHeaderStreamWithUseProcessedTypes", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Failed to find a stored type for GUID &apos;{0}&apos;..
        /// </summary>
        internal static string Error_NoStoredTypeForGuid {
            get {
                return ResourceManager.GetString("Error_NoStoredTypeForGuid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to GUID &apos;{0}&apos; of a type serialization attribute is not unique..
        /// </summary>
        internal static string Error_TypeAttributeGuidIsNotUnique {
            get {
                return ResourceManager.GetString("Error_TypeAttributeGuidIsNotUnique", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Type &apos;{0}&apos; has no defined constructor without arguments..
        /// </summary>
        internal static string Error_TypeHasNoConstructor {
            get {
                return ResourceManager.GetString("Error_TypeHasNoConstructor", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Type &apos;{0}&apos; is not marked as serializable..
        /// </summary>
        internal static string Error_TypeIsNotSerializable {
            get {
                return ResourceManager.GetString("Error_TypeIsNotSerializable", resourceCulture);
            }
        }
    }
}
