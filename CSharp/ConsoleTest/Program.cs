using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Configuration;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.Azure.KeyVault;
using System.Threading;
using System.IO;

using JB2.Common;

using JB2.Common.Log;
using JB2.Common.Scheduler;

namespace ConsoleTest
{

    public class TestJob : JB2.Common.Scheduler.OnetimeJob
    {
        public override ServiceResult DoWork()
        {
            Console.WriteLine("Don't bother me I am working");
            return true;
        }
    }

    public class TestRepeatJob : JB2.Common.Scheduler.RepeatableJob
    {
        public override ServiceResult DoWork()
        {
            Console.WriteLine( string.Format("I told you {0} times...Don't bother me I am working",this.CurrentCounter));
            return true;
        }

        public override int GetCoolDownSeconds()
        {
            return 1;
        }

        public override int GetMaxCounter()
        {
            return 10;
        }
    }

    public class TestTimeRange : JB2.Common.Scheduler.DayOfWeekTimeJob
    {
        public override ServiceResult DoWork()
        {
            Console.WriteLine(string.Format("I told you that I am working during ", this.GetDayofWeekTimeRange().ToString()));
            return true;
        }

        public override DayofWeekTimeRange GetDayofWeekTimeRange()
        {
            return new DayofWeekTimeRange(DayOfWeek.Tuesday, new TimeSpan(12, 0, 0), new TimeSpan(13, 0, 0));
        }
    }

    public class TestScheduler : JB2.Common.Scheduler.ThreadScheduler<string, object>
    {

        #region Fields
        private ILogger _logger;

        #endregion Fields

        public TestScheduler(ILogger logger)
        {
            _logger = logger;
        }
        public override void Add(IJob<string, object> job)
        {
            throw new NotImplementedException();
        }

        public override IEnumerable<IJob<string, object>> GetJobs()
        {
            return new IJob<string, object>[3] { new TestJob(), new TestRepeatJob(), new TestTimeRange() };
        }

        public override ILogger GetLogger()
        {
            return _logger;
        }

        public override bool LoggingEnabled()
        {
            return _logger != null;
        }

        public override void Remove(IJob<string, object> job)
        {
            throw new NotImplementedException();
        }
    }


    class Program
    {


        private async static Task<string> GetToken(string authority, string resource, string scope)
        {
            var authContext = new AuthenticationContext(authority);
            ClientCredential clientCred = new ClientCredential(
                ConfigurationManager.AppSettings["JB2:AD-clientID"],
                ConfigurationManager.AppSettings["JB2:AD-clientSecret"]);
            AuthenticationResult result = await authContext.AcquireTokenAsync(resource, clientCred);

            if (result == null)
                throw new InvalidOperationException("Failed to obtain the JWT token");

            return result.AccessToken;
        }

        //KeyVaultKeyResolver cloudResolver = new KeyVaultKeyResolver(GetToken);

        static void Main(string[] args)
        {
            //var table = JB2.Infrastructure.Storage.LogAccount.GetTable("apptest");
            //var test = new JB2.Common.Log.AzureRepo(table, "logentry");
            //var entry = JB2.Common.Log.LogEntry.NewLogEntry(JB2.Common.Log.Enum.LogServerityType.Informational, "This is a message");
            //test.StoreLogEntry(entry);

            var scheduler = new TestScheduler(null);

            //scheduler.StartJobs();




            //var logentries = test.GetLogEntry(JB2.Common.Log.LogSearch.SearchByID("7yL5PlNxvEykzfTKZsdk9g"));
            //var logentries = test.GetMostRecentLogEntries(2);
            //Microsoft.Azure.KeyVault.RsaKey key = new RsaKey("private:key1");
            //System.Drawing.Font f = JB2.Common.FontHelper.GetFont("ffft1");

            //var test = JB2.Infrastructure.KeyVaultUtility.JB2KeyVaultClient;

            //var keyid = JB2.Infrastructure.KeyVaultUtility.CreateRSAKey("JBsquaredRSAKey1");

            var key1 = JB2.Infrastructure.KeyVaultUtility.RSAKey1;
            var key2 = JB2.Infrastructure.Vault.RSAKey1;

            Console.WriteLine(JB2.Common.NewID.ShortGuid("start_{0}_end"));
            Console.WriteLine(JB2.Common.NewID.Guid());
            Console.WriteLine(JB2.Common.NewID.Base62());
            Console.WriteLine(JB2.Common.NewID.TickHash());
            Console.WriteLine(JB2.Common.NewID.TimeHash());
            Console.WriteLine(JB2.Common.NewID.UriHash(new Uri("http://wwww.jbsquared.com")));
            //JB2.Common.RGB rgb = new RGB("fff1f1");

            //Console.WriteLine(rgb.ToString());

            //var countries = JB2.Common.MapHelper.AllCountries;

            //var c = countries.FirstOrDefault(x => x.Abbreviation == "USA");

            //JB2.Common.JB2Color color = JB2Color.FromHex("6B4106");
            Console.ReadLine();
            //JB2.Common.Data.AzureHelper.AccountName = "jb2bowtie";
            //JB2.Common.Data.AzureHelper.AccountKey = "frIlemrNzlvAbKNhiyYCeW+otbFXBoJb0TodzbgwzF8IBEZMtifrHfx0Y+o1+jwvUL4FcAGYepHlgqrG0iCc1Q==";

            //JB2.Common.Data.AzureBlobRepository cardRepo = new JB2.Common.Data.AzureBlobRepository("gameobjects");
            //string link = cardRepo.GetUrl("EmptyBingoCard_5x5.png");

            //var webClient = new System.Net.WebClient();
            //byte[] imageBytes = webClient.DownloadData("https://jb2bowtie.blob.core.windows.net/gameobjects/EmptyBingoCard_5x5.png");

            //System.IO.MemoryStream cardStream = cardRepo.GetStream("EmptyBingoCard_5x5.png");

            ////byte[] cardByte = cardRepo.GetByteArray("EmptyBingoCard_5x5.png");
            //JB2Image cardimage = JB2Image.FromUrl("https://jb2bowtie.blob.core.windows.net/gameobjects/EmptyBingoCard_5x5.png");
            //JB2Image cardimage1 = JB2Image.FromByteArray(imageBytes);
        }
    }
}
