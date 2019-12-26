using System;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

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

        public PrincipalSearchResult<Principal> GetUsersByDisplayName(string sUserName)
        {
            PrincipalContext oPrincipalContext = GetPrincipalContext();

            //Create a "user object" in the context
            UserPrincipal user = new UserPrincipal(oPrincipalContext);

            //Specify the search parameters
            user.DisplayName = sUserName + "*";

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

        /// <summary>
        /// Получить кабинет пользователя
        /// </summary>
        /// <param name="sLogin">Имя пользователя для которого нужно получить номер кабинета</param>
        /// <returns>Возвращает объект String</returns>
        public string getOffice(string sLogin)
        {
            string filter = string.Format("(&(ObjectClass={0})(sAMAccountName={1}))", "person", sLogin);
            string domain = "BTRC";
            string[] properties = new string[] { "physicalDeliveryOfficeName" };

            DirectoryEntry adRoot = new DirectoryEntry("LDAP://" + domain, null, null, AuthenticationTypes.Secure);
            DirectorySearcher searcher = new DirectorySearcher(adRoot);
            searcher.SearchScope = SearchScope.Subtree;
            searcher.ReferralChasing = ReferralChasingOption.All;
            searcher.PropertiesToLoad.AddRange(properties);
            searcher.Filter = filter;

            SearchResult result = searcher.FindOne();
            DirectoryEntry directoryEntry = result.GetDirectoryEntry();

            if (directoryEntry.Properties["physicalDeliveryOfficeName"].Count > 0)
            {
                return directoryEntry.Properties["physicalDeliveryOfficeName"][0].ToString();
            }
            else
            {
                return String.Empty;
            }
        }

        /// <summary>
        /// Получить мобильный номер пользователя
        /// </summary>
        /// <param name="sLogin">Имя пользователя для которого нужно получить мобильный номер</param>
        /// <returns>Возвращает объект String</returns>
        public string getMobileNumber(string sLogin)
        {
            string filter = string.Format("(&(ObjectClass={0})(sAMAccountName={1}))", "person", sLogin);
            string domain = "BTRC";
            string[] properties = new string[] { "mobile" };

            DirectoryEntry adRoot = new DirectoryEntry("LDAP://" + domain, null, null, AuthenticationTypes.Secure);
            DirectorySearcher searcher = new DirectorySearcher(adRoot);
            searcher.SearchScope = SearchScope.Subtree;
            searcher.ReferralChasing = ReferralChasingOption.All;
            searcher.PropertiesToLoad.AddRange(properties);
            searcher.Filter = filter;

            SearchResult result = searcher.FindOne();
            DirectoryEntry directoryEntry = result.GetDirectoryEntry();

            if (directoryEntry.Properties["mobile"].Count > 0)
            {
                return directoryEntry.Properties["mobile"][0].ToString();
            }
            else
            {
                return String.Empty;
            }
        }

        #endregion
    }
}
