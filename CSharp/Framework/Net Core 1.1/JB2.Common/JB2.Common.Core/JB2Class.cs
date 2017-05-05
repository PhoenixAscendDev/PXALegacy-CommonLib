using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JB2.Common
{
    public abstract class JB2ClassWithLog<TLogger> : JB2ClassWithLog<TLogger, Enum.LogServerityType, string, ILogEntry>, IClass
        where TLogger : ILoggerAsync
    {
        public JB2ClassWithLog(TLogger logger) : base(logger)
        {

        }
    }

    public abstract class JB2ClassWithLog<TLogger,TLogServerity, TLogKey,TLogEntry> : JB2Class, IClass
        where TLogger : ILogger<TLogServerity, TLogKey, TLogEntry>
        where TLogServerity : IComparable
        where TLogEntry :  ILogEntry<TLogKey, TLogServerity>
    {
        #region Fields
        protected TLogger _logger;
        protected TLogServerity _serverityType;
        #endregion Fields

        #region Constructors

        public JB2ClassWithLog(TLogger logger) : base()
        {
            _logger = logger;
        }


        #endregion Constructors


        public virtual void SetProperty<T>(string index, T newValue, bool changeLastUpdate, bool logit)
        {
            TLogEntry logEntry;
            if (_props[index] != null)
            {
                T oldValue = (T)_props[index].GetValue().ObjectValue;
                logEntry = changeLogEntry<T>(index, oldValue, newValue, DateTime.Now);
            }
            else
            {
                logEntry = newLogEntry<T>(index, newValue, DateTime.Now);
            }
                

            if (logit)
            {
                _logger.Log(logEntry);
            }

            base.SetProperty<T>(index, newValue, changeLastUpdate);
        }

        protected abstract TLogEntry changeLogEntry<T>(string index,T oldvalue, T newvalue, DateTime changeDate);
        protected abstract TLogEntry newLogEntry<T>(string index, T newvalue, DateTime changeDate);

    }



    public abstract class JB2Class : IClass
        
    {
        #region Fields 
        protected MetaDataCollection _props;
        protected DateTime _lastupdate;
        protected bool _defaultchangeLastUpdate;
        #endregion Fields

        public JB2Class()
        {
            clearProps();
        }

        public virtual T GetProperity<T>(string index)
        {
            return _props.GetProperty<T>(index);
        }

        public virtual T GetProperity<T>(string index, T defaultValue)
        {
            return _props.GetProperty<T>(index, defaultValue);
        }

        public virtual void SetProperty<T>(string index, T newValue)
        {
            SetProperty<T>(index, newValue, _defaultchangeLastUpdate);
        }

        public virtual void SetProperty<T>(string index, T newValue, bool changeLastUpdate)
        {
            
            MetaData<T> newMeta = new MetaData<T>(index, newValue);

            if (_props == null)
                clearProps();
            //if property already exists and different then  dump and add
            if (_props[index] != null)
            {
                _props[index].UpdateValue(newValue);
            }
            else
            {
                _props.Add(newMeta);
            }

            //update Last Update if needed
            if (changeLastUpdate)
                _lastupdate = System.DateTime.Now;
        }

        public virtual DateTime GetLastUpdate()
        {
            return _lastupdate;
        }

        private void clearProps()
        {
            _props = new MetaDataCollection();
        }
    }
}
