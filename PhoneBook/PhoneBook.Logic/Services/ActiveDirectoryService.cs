using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Text;

namespace PhoneBook.Logic.Services
{
    public class ActiveDirectoryService
    {
        #region Переменные

        readonly string _domain = "**";
        readonly string _defaultOU = "**";
        readonly string _defaultRootOU = "**";
        readonly string _serviceUser = "**";
        readonly string _servicePassword = "**";

        #endregion

        #region Методы проверки

        /// <summary>
        /// Проверка существования пользователя в AD
        /// </summary>
        /// <param name="sUserName">Имя пользователя</param>
        /// <returns>Возвращает true, если пользователь существует</returns>
        public bool IsUserExisiting(string sUserName)
        {
            return GetUser(sUserName) != null;
        }

        #endregion

        #region Методы поиска

        /// <summary>
        /// Получить указанного пользователя Active Directory
        /// </summary>
        /// <param name="sUserName">Имя пользователя для извлечения</param>
        /// <returns>Объект UserPrincipal</returns>
        public UserPrincipal GetUser(string sUserName)
        {
            PrincipalContext oPrincipalContext = GetPrincipalContext();
            return UserPrincipal.FindByIdentity(oPrincipalContext, IdentityType.SamAccountName, sUserName);
        }

        public PrincipalSearchResult<Principal> GetUsersByName(string sUserName)
        {
            PrincipalContext oPrincipalContext = GetPrincipalContext();

            //Create a "user object" in the context
            UserPrincipal user = new UserPrincipal(oPrincipalContext);

            //Specify the search parameters
            user.Name = sUserName;

            //Create the searcher
            //pass (our) user object
            PrincipalSearcher pS = new PrincipalSearcher();
            pS.QueryFilter = user;

            //Perform the search
            PrincipalSearchResult<Principal> results = pS.FindAll();

            return results;
        }

        public PrincipalSearchResult<Principal> GetUsersByDisplayName(string sUserName)
        {
            PrincipalContext oPrincipalContext = GetPrincipalContext();

            //Create a "user object" in the context
            UserPrincipal user = new UserPrincipal(oPrincipalContext);

            //Specify the search parameters
            user.DisplayName = sUserName;

            //Create the searcher
            //pass (our) user object
            PrincipalSearcher pS = new PrincipalSearcher();
            pS.QueryFilter = user;

            //Perform the search
            PrincipalSearchResult<Principal> results = pS.FindAll();

            return results;
        }

        public PrincipalSearchResult<Principal> GetUsersByLogin(string sLogin)
        {
            PrincipalContext oPrincipalContext = GetPrincipalContext();

            //Create a "user object" in the context
            UserPrincipal user = new UserPrincipal(oPrincipalContext);

            //Specify the search parameters
            user.SamAccountName = sLogin;

            //Create the searcher
            //pass (our) user object
            PrincipalSearcher pS = new PrincipalSearcher();
            pS.QueryFilter = user;

            //Perform the search
            PrincipalSearchResult<Principal> results = pS.FindAll();

            return results;
        }

        public PrincipalSearchResult<Principal> GetUsersByTelephone(string sTelephone)
        {
            PrincipalContext oPrincipalContext = GetPrincipalContext();

            //Create a "user object" in the context
            UserPrincipal user = new UserPrincipal(oPrincipalContext);

            //Specify the search parameters
            user.VoiceTelephoneNumber = sTelephone + "*";

            //Create the searcher
            //pass (our) user object
            PrincipalSearcher pS = new PrincipalSearcher();
            pS.QueryFilter = user;

            //Perform the search
            PrincipalSearchResult<Principal> results = pS.FindAll();

            return results;
        }

        #endregion

        #region Методы управления учетными записями
   
        /// <summary>
        /// Редактирование пользователя Active Directory
        /// </summary>
        /// <param name="sUserName">Имя пользователя</param>
        /// <param name="sGivenName">Имя</param>
        /// <param name="sSurName">Фамилия</param>
        /// <returns>Возвращает объект UserPrincipal</returns>
        public UserPrincipal EditUser(string sUserName, string sGivenName, string sSurName, string sDescription, string sOffice, string sTelephone, string sEmail,
            DateTime? sAccountExpiries, bool sExpirePassword, bool sPasswordNeverExpires, bool sUserCannotChangePassword, bool sEnableAccount)
        {
            if (sOffice == null) sOffice = " ";


            if (!IsUserExisiting(sUserName))
            {
                //тут вернет ошибку!!
                return GetUser(sUserName);
            }
            else
            {
                PrincipalContext oPrincipalContext = GetPrincipalContext();

                UserPrincipal oUserPrincipal = GetUser(sUserName);
                oUserPrincipal.GivenName = sGivenName;
                oUserPrincipal.Surname = sSurName;
                oUserPrincipal.DisplayName = sSurName + " " + sGivenName;
                oUserPrincipal.Description = sDescription;
                oUserPrincipal.VoiceTelephoneNumber = sTelephone;
                oUserPrincipal.EmailAddress = sEmail;
                oUserPrincipal.AccountExpirationDate = sAccountExpiries;

                if (sExpirePassword)
                    oUserPrincipal.ExpirePasswordNow();
                oUserPrincipal.PasswordNeverExpires = sPasswordNeverExpires;
                oUserPrincipal.UserCannotChangePassword = sUserCannotChangePassword;
                oUserPrincipal.Enabled = sEnableAccount;

                oUserPrincipal.Save();

                using (var entry = (DirectoryEntry)oUserPrincipal.GetUnderlyingObject())
                {
                    try
                    {
                        if (entry.Properties["physicalDeliveryOfficeName"].Count == 0) entry.Properties["physicalDeliveryOfficeName"].Add(sOffice);
                        else entry.Properties["physicalDeliveryOfficeName"][0] = sOffice;
                        entry.CommitChanges();
                    }
                    catch { }
                }

                return oUserPrincipal;
            }
        }

        #endregion

        #region Вспомогательные методы

        /// <summary>
        /// Получить базовый основной контекст
        /// </summary>
        /// <returns>Возвращает объект PrincipalContext</returns>
        public PrincipalContext GetPrincipalContext()
        {
            return new PrincipalContext(ContextType.Domain, _domain, _defaultRootOU, _serviceUser, _servicePassword);
        }

        /// <summary>
        /// Получить основной контекст указанного OU
        /// </summary>
        /// <param name="sOU">OU для которого нужно получить основной контекст</param>
        /// <returns>Возвращает объект PrincipalContext</returns>
        public PrincipalContext GetPrincipalContext(string sOU)
        {
            return new PrincipalContext(ContextType.Domain, _domain, string.IsNullOrEmpty(sOU) ? _defaultOU : sOU, _serviceUser, _servicePassword);
        }

        #endregion
    }
}
