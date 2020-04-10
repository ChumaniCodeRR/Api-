using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocuSign.eSign.Api;
using DocuSign.eSign.Model;
using DocuSign.eSign.Client;

namespace ApiTest_Docusign
{
    class Program
    {
        static void Main(string[] args)
        {
            string username = "cmqulo@broll.com";
            string password = "RPu%T?8yG6HfzSK";
            string integratorKey = "3b9fa7cc-dd91-468a-925d-056ee392643a";

            ApiClient apiClient = new ApiClient("https://demo.docusign.net/restapi");
            Configuration.Default.ApiClient = apiClient;

            string authHeader = "{\"Username\":\"" + username + "\", \"Password\":\"" + password + "\",\"IntegratorKey\":\"" + integratorKey + "\"}";

            Configuration.Default.AddDefaultHeader("X-DocuSign-Authentication", authHeader);

            string accountid = null;

            AuthenticationApi authApi = new AuthenticationApi();
            LoginInformation loginInfo = authApi.Login();

            accountid = loginInfo.LoginAccounts[0].AccountId;

            EnvelopeDefinition envDef = new EnvelopeDefinition();
            envDef.EmailSubject = "[DocuSign C# SDK] - Sample Signature Request";

            envDef.TemplateId = "";

            TemplateRole tRole = new TemplateRole();
            tRole.Email = "cmqulo@broll.com";
            tRole.Name = "Chumani";
            tRole.RoleName = "Junior Developer";

            List<TemplateRole> rolesList = new List<TemplateRole>() { tRole };
            envDef.TemplateRoles = rolesList;

            envDef.Status = "sent";

            EnvelopesApi envelopesApi = new EnvelopesApi();
            EnvelopeSummary envelopeSummary = envelopesApi.CreateEnvelope(accountid, envDef);
        }
    }
}
