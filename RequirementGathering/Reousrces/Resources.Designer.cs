﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.35317
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RequirementGathering.Reousrces {
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
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("RequirementGathering.Reousrces.Resources", typeof(Resources).Assembly);
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
        ///   Looks up a localized string similar to Must be between 10 and 30..
        /// </summary>
        public static string AgeRange {
            get {
                return ResourceManager.GetString("AgeRange", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Age is required..
        /// </summary>
        public static string AgeRequired {
            get {
                return ResourceManager.GetString("AgeRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email is not valid..
        /// </summary>
        public static string EmailInvalid {
            get {
                return ResourceManager.GetString("EmailInvalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email is required..
        /// </summary>
        public static string EmailRequired {
            get {
                return ResourceManager.GetString("EmailRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to First name must be less than 50 characters..
        /// </summary>
        public static string FirstNameLong {
            get {
                return ResourceManager.GetString("FirstNameLong", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to First name is required..
        /// </summary>
        public static string FirstNameRequired {
            get {
                return ResourceManager.GetString("FirstNameRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Web tool for gathering customer requirements.
        /// </summary>
        public static string Headline {
            get {
                return ResourceManager.GetString("Headline", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You are invited for evaluation. Please login to {0}/{1}/Account/Login with your email address and password.&lt;br/&gt;&lt;br/&gt;{2}.
        /// </summary>
        public static string InvitationEmailBodyExisting {
            get {
                return ResourceManager.GetString("InvitationEmailBodyExisting", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You are invited for evaluation. Please login to {0}/{1}/Account/Login with your email address and password: &lt;b&gt;{2}&lt;/b&gt;.&lt;br/&gt;&lt;br/&gt;{3}.
        /// </summary>
        public static string InvitationEmailBodyNew {
            get {
                return ResourceManager.GetString("InvitationEmailBodyNew", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invitation for Evaluation: {0} {1}.
        /// </summary>
        public static string InvitationEmailSubject {
            get {
                return ResourceManager.GetString("InvitationEmailSubject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Last name must be less than 50 characters..
        /// </summary>
        public static string LastNameLong {
            get {
                return ResourceManager.GetString("LastNameLong", resourceCulture);
            }
        }
    }
}
