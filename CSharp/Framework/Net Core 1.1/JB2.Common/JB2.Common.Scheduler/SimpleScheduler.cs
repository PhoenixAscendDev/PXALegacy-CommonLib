using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JB2.Common.Log;

namespace JB2.Common.Scheduler
{

    public class SimpleScheduler : SimpleScheduler<string,object>
    {


        #region Constructors

        public SimpleScheduler() : base()
        {

        }

        public SimpleScheduler(IJobAsync job, JB2.Common.ILogger logger) : base(job,logger)
        {

        }

        public SimpleScheduler( IEnumerable<IJobAsync> jobs, JB2.Common.ILogger logger)
            : base(jobs,logger)
        {

        }

        #endregion Constructors

    }


    public class SimpleScheduler<TJobKey, TJobParameter> : ThreadScheduler<TJobKey, TJobParameter>
        where TJobKey: IComparable
    {

        #region Fields
        private List<IJobAsync<TJobKey, TJobParameter>> _jobs;
        private JB2.Common.ILogger _logger;
        #endregion Fields;

        #region Constructors

        public SimpleScheduler(IEnumerable<IJobAsync<TJobKey, TJobParameter>> jobs, JB2.Common.ILogger logger)
        {
            _jobs = jobs.ToList();
            _logger = logger;
        }
        public SimpleScheduler( IJobAsync<TJobKey,TJobParameter> job, JB2.Common.ILogger logger)
        {
            _jobs = new List<IJobAsync<TJobKey, TJobParameter>>();
            _jobs.Add(job);
            _logger = logger;
        }


        public SimpleScheduler()
        {
            _jobs = new List<IJobAsync<TJobKey, TJobParameter>>();
            _logger = null;
        }

        #endregion Constructors

        #region IScheduler
        public override void Add(IJobAsync<TJobKey, TJobParameter> job)
        {
            _jobs.Add(job);
        }

        public override IEnumerable<IJobAsync<TJobKey, TJobParameter>> GetJobs()
        {
            return _jobs;
        }

        public override ILogger GetLogger()
        {
            return _logger;
        }

        public override bool LoggingEnabled()
        {
            return _logger != null;
        }

        public override void Remove(IJobAsync<TJobKey, TJobParameter> job)
        {
            _jobs.Remove(job);
        }
        #endregion IScheduler
    }
}
