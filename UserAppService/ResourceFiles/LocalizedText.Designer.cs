﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace UserAppService.ResourceFiles {
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
    internal class LocalizedText {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal LocalizedText() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("UserAppService.ResourceFiles.LocalizedText", typeof(LocalizedText).Assembly);
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
        ///   Looks up a localized string similar to Still awaiting email confirmation.
        /// </summary>
        internal static string AwaitingEmailConfirmation {
            get {
                return ResourceManager.GetString("AwaitingEmailConfirmation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Confirmation Email has been sent to your registered Email Address..
        /// </summary>
        internal static string ConfirmationEmailSent {
            get {
                return ResourceManager.GetString("ConfirmationEmailSent", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please confirm your account by clicking.
        /// </summary>
        internal static string ConfirmEmailBody {
            get {
                return ResourceManager.GetString("ConfirmEmailBody", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Confirm your account..
        /// </summary>
        internal static string ConfirmEmailSubject {
            get {
                return ResourceManager.GetString("ConfirmEmailSubject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email has been Confirmed..
        /// </summary>
        internal static string EmailConfirmed {
            get {
                return ResourceManager.GetString("EmailConfirmed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Login through Facebook Account has been successfull..
        /// </summary>
        internal static string FacebookLoginSuccessful {
            get {
                return ResourceManager.GetString("FacebookLoginSuccessful", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://graph.facebook.com/debug_token?input_token={0}&amp;access_token={1}.
        /// </summary>
        internal static string FacebookRequestUri {
            get {
                return ResourceManager.GetString("FacebookRequestUri", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Login through Gmail Account has been succesfull..
        /// </summary>
        internal static string GmailLoginSuccessful {
            get {
                return ResourceManager.GetString("GmailLoginSuccessful", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://www.googleapis.com/oauth2/v1/tokeninfo?id_token={0}.
        /// </summary>
        internal static string GoogleRequestUri {
            get {
                return ResourceManager.GetString("GoogleRequestUri", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Input is required..
        /// </summary>
        internal static string InputRequired {
            get {
                return ResourceManager.GetString("InputRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid API key.
        /// </summary>
        internal static string InvalidAPIKey {
            get {
                return ResourceManager.GetString("InvalidAPIKey", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to This user is currently locked out.
        /// </summary>
        internal static string LockedOut {
            get {
                return ResourceManager.GetString("LockedOut", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Not from an Authorized request source.
        /// </summary>
        internal static string NotAuthorizedSource {
            get {
                return ResourceManager.GetString("NotAuthorizedSource", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The password and confirmation password do not match..
        /// </summary>
        internal static string PasswordDoNotMatch {
            get {
                return ResourceManager.GetString("PasswordDoNotMatch", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Please use the following link to change your password: &lt; br /&gt; &lt;a href=\.
        /// </summary>
        internal static string PasswordResetEmailBody {
            get {
                return ResourceManager.GetString("PasswordResetEmailBody", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Reset Your Password..
        /// </summary>
        internal static string PasswordResetEmailSubject {
            get {
                return ResourceManager.GetString("PasswordResetEmailSubject", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You will receive an email with a link to reset your password. If you do not receive an email, please verify that the email address submitted is valid..
        /// </summary>
        internal static string PasswordResetRequest {
            get {
                return ResourceManager.GetString("PasswordResetRequest", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to AlternateId is Required..
        /// </summary>
        internal static string RequireAlternateId {
            get {
                return ResourceManager.GetString("RequireAlternateId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to API Key is required.
        /// </summary>
        internal static string RequireAPIKey {
            get {
                return ResourceManager.GetString("RequireAPIKey", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Code sent in confirmation Email is required..
        /// </summary>
        internal static string RequireConfirmEmailCode {
            get {
                return ResourceManager.GetString("RequireConfirmEmailCode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email is Required.
        /// </summary>
        internal static string RequireEmail {
            get {
                return ResourceManager.GetString("RequireEmail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Id is required.
        /// </summary>
        internal static string RequireId {
            get {
                return ResourceManager.GetString("RequireId", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Name is required.
        /// </summary>
        internal static string RequireName {
            get {
                return ResourceManager.GetString("RequireName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Password Required.
        /// </summary>
        internal static string RequirePassword {
            get {
                return ResourceManager.GetString("RequirePassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to PhoneNumber is Required..
        /// </summary>
        internal static string RequirePhoneNumber {
            get {
                return ResourceManager.GetString("RequirePhoneNumber", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UserName required.
        /// </summary>
        internal static string RequireUserName {
            get {
                return ResourceManager.GetString("RequireUserName", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Role {0} has been deleted successfully.
        /// </summary>
        internal static string RoleDeleted {
            get {
                return ResourceManager.GetString("RoleDeleted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Role {0} already exists.
        /// </summary>
        internal static string RoleExists {
            get {
                return ResourceManager.GetString("RoleExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Role with role id {0} doesn&apos;t exist.
        /// </summary>
        internal static string RoleForRoleIdNotExist {
            get {
                return ResourceManager.GetString("RoleForRoleIdNotExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Role does not exist.
        /// </summary>
        internal static string RoleNotExist {
            get {
                return ResourceManager.GetString("RoleNotExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Role {0} has been saved successfully.
        /// </summary>
        internal static string RoleSaved {
            get {
                return ResourceManager.GetString("RoleSaved", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You are not authorized for api access.
        /// </summary>
        internal static string UnauthorizedUser {
            get {
                return ResourceManager.GetString("UnauthorizedUser", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User does not have an account, prompt the user to create an account (call register api)..
        /// </summary>
        internal static string UserAccountNotExists {
            get {
                return ResourceManager.GetString("UserAccountNotExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User has been authenticated successfully.
        /// </summary>
        internal static string UserAuthenticated {
            get {
                return ResourceManager.GetString("UserAuthenticated", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User does not exist.
        /// </summary>
        internal static string UserNotExist {
            get {
                return ResourceManager.GetString("UserNotExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The user {0} does not exist..
        /// </summary>
        internal static string UserNotExists {
            get {
                return ResourceManager.GetString("UserNotExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The user {0} either does not exist or is not confirmed..
        /// </summary>
        internal static string UserNotExistsOrConfirmed {
            get {
                return ResourceManager.GetString("UserNotExistsOrConfirmed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Either user or role does not exist.
        /// </summary>
        internal static string UserOrRoleNotExist {
            get {
                return ResourceManager.GetString("UserOrRoleNotExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User has been registered..
        /// </summary>
        internal static string UserRegister {
            get {
                return ResourceManager.GetString("UserRegister", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User could not be registered..
        /// </summary>
        internal static string UserRegisterFail {
            get {
                return ResourceManager.GetString("UserRegisterFail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User role has been deleted successfully.
        /// </summary>
        internal static string UserRoleDeleted {
            get {
                return ResourceManager.GetString("UserRoleDeleted", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to User role already exists.
        /// </summary>
        internal static string UserRoleExists {
            get {
                return ResourceManager.GetString("UserRoleExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Role has been successfully granted to user.
        /// </summary>
        internal static string UserRoleGranted {
            get {
                return ResourceManager.GetString("UserRoleGranted", resourceCulture);
            }
        }
    }
}
