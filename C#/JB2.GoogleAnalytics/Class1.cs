using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JB2
{
    public static class GoogleAnalytics
    {
        static string _trackingID = string.Empty;
        static readonly string _javascriptTracking = "@<script>(function(i, s, o, g, r, a, m){i['GoogleAnalyticsObject'] = r; i[r]=i[r]||function(){(i[r].q = i[r].q ||[]).push(arguments)},i[r].l=1*new Date(); a=s.createElement(o), m=s.getElementsByTagName(o)[0];a.async=1;a.src=g;m.parentNode.insertBefore(a,m)})(window,document,'script','//www.google-analytics.com/analytics.js','ga');ga('create', '{0}', 'auto');ga('send', 'pageview');</script>";


        static string TrackingID
        {
            get
            {
                return _trackingID;

            }
            set
            {
                _trackingID = value;
            }
        }

        static string TrackingJavascript
        {
            get
            {
                return string.Format(_javascriptTracking, _trackingID);
            }
        }

        static string GetTrackingJavascript(string trackingID)
        {
            return string.Format(_javascriptTracking, trackingID);
        }





    }
}
