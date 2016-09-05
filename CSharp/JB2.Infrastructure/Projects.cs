using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;

namespace JB2.Infrastructure
{
    public static class Projects
    {
        //private const string PRODJECTCODE = "JB2:projectCode";

        private static JB2.Common.Data.AzureTableRepository repo = JB2.Infrastructure.Storage.GeneralAccount.GetTable("projects");

        public static JB2.Common.IProject<string>  Project(string projectCode)
        {
            try
            {
                JB2.Infrastructure.Data.JB2Project project = repo.GetEntity<JB2.Infrastructure.Data.JB2Project>("project", "code:" + projectCode);
                project._rng = getProjectRNG(projectCode);

                return project;
            }
            catch(Exception ex)
            {
                return new JB2.Infrastructure.Data.JB2Project();
            }
        }

        public static IEnumerable<JB2.Common.IVersionNumber> Versions(string projectCode)
        {
            try
            {
                return repo.GetByRowKeyStartWith<JB2.Infrastructure.Data.ReleaseVersion>("release:" + projectCode, "version:", 1000, false);
            }
            catch(Exception ex)
            {
                return new List<JB2.Common.IVersionNumber>(0);
            }
        }

        public static IEnumerable<JB2.Common.IReleaseNote> ReleaseNotes(string projectCode,JB2.Common.IVersionNumber version)
        {
            try
            {
                return repo.GetByRowKeyStartWith<JB2.Infrastructure.Data.ReleaseNote>("notes:" + projectCode, "version:" + version.ToInt().ToString(), 1000, false);

            }
            catch(Exception ex)
            {
                return new List<JB2.Common.IReleaseNote>(0);
            }
        }

        private static ushort getProjectRNG(string projectCode)
        {
            try
            {
                ushort rng = 0;

                var p = repo.GetEntity<Microsoft.WindowsAzure.Storage.Table.DynamicTableEntity>("project", "code:" + projectCode);

                rng = (ushort)p.Properties["LastUsedRNG"].Int32Value.GetValueOrDefault();

                return rng;
            }

            catch (Exception ex)
            {
                return 0;
            }
        }

        public static bool UpdateProjectRNG(string projectCode, ushort newRNG)
        {
            try
            {
                Microsoft.WindowsAzure.Storage.Table.DynamicTableEntity e = new Microsoft.WindowsAzure.Storage.Table.DynamicTableEntity();
                e.Properties.Add("LastUsedRNG", new Microsoft.WindowsAzure.Storage.Table.EntityProperty(newRNG));

                e.PartitionKey = "project";
                e.RowKey = "code:" + projectCode;

                repo.Insert<Microsoft.WindowsAzure.Storage.Table.DynamicTableEntity>(e, Common.Data.TableInsertMode.Merge, false);

                //test to make sure it past
                ushort test = getProjectRNG(projectCode);

                return test == newRNG;

            }

            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
