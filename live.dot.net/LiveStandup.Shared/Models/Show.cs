﻿using Humanizer;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

// ClintonRocksmith cheered 1500 on June 25th 2019
// h0usebesuch gifted 2 subs on June 25th 2019
// LotanB gifted 1 sub on June 25th 2019

namespace LiveStandup.Shared.Models
{
    public class Show
    {
        // Pa6qtu1wIs8
        public string Id { get; set; }

        //Format: ".NET Community Standup - Monday, Day Year - Topic
        public string Title { get; set; }

        public string Topic { get; set; }

        public bool HasDisplayTitle { get; set; }

        public string DisplayTitle { get; set; }

        public string CommunityLinksUrl { get; set; }

        public string Description { get; set; }

        public DateTime ScheduledStartTime { get; set; }

        public DateTime? ActualStartTime { get; set; }

        public DateTime? ActualEndTime { get; set; }

        public bool HasLinks { get; set; }

        // https://www.youtube.com/watch?v=Pa6qtu1wIs8&list=PL1rZQsJPBU2StolNg0aqvQswETPcYnNKL&index=0
        public string Url { get; set; }

        //https://i.ytimg.com/vi/Pa6qtu1wIs8/hqdefault_live.jpg
        public string ThumbnailUrl { get; set; }

        // ASP.NET, Xamarin, Desktop, Visual Studio
        public string Category { get; set; }

        [JsonIgnore]
        public string ScheduledStartTimeHumanized
        {
            get
            {
                if ((DateTime.UtcNow - ScheduledStartTime).TotalDays <= 7)
                    return ScheduledStartTime.Humanize();

                var culture = CultureInfo.CurrentCulture;
                var regex = new Regex("dddd[,]{0,1}");
                var shortDatePattern = regex.Replace(culture.DateTimeFormat.LongDatePattern.Replace("MMMM", "MMM"), string.Empty).Trim();
                return ScheduledStartTime.ToString($"{shortDatePattern}", culture);               
            }
        }

        [JsonIgnore]
        public bool IsNew => !IsInFuture &&
                     !IsOnAir &&
                     (DateTime.UtcNow - ScheduledStartTime).TotalDays <= 14;

        [JsonIgnore]
        public bool IsInFuture => ScheduledStartTime > DateTime.UtcNow;

        [JsonIgnore]
        public bool IsOnAir =>
            ActualStartTime.HasValue &&
            !ActualEndTime.HasValue;
    }
}
