using Newtonsoft.Json;
using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace Common.Models.Search
{
    public partial class SearchData : INotifyPropertyChanged
    {
        [JsonProperty("platformId")]
        public long? PlatformId { get; set; }

        [JsonProperty("platformSlug")]
        public string PlatformSlug { get; set; }

        [JsonProperty("platformUserIdentifier")]
        public string PlatformUserIdentifier { get; set; }

        [JsonProperty("platformUserId")]
        public object PlatformUserId { get; set; }

        [JsonProperty("platformUserHandle")]
        public string PlatformUserHandle { get; set; }

        [JsonProperty("avatarUrl")]
        public Uri AvatarUrl { get; set; }

        [JsonProperty("additionalParameters")]
        public object AdditionalParameters { get; set; }

        private bool isTracked = false;
        private QuickDetails quickdetails = null;
        private UserData data = null;

        [JsonIgnore]
        public bool IsTracked
        {
            get => this.isTracked;
            set
            {
                if(isTracked != value)
                {
                    isTracked = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [JsonIgnore]
        public QuickDetails QuickDetails
        {
            get => this.quickdetails;
            set
            {
                if(quickdetails != value)
                {
                    this.quickdetails = value;
                    NotifyPropertyChanged();
                }
            }
        }

        [JsonIgnore]
        public UserData Data
        {
            get => this.data;
            set
            {
                if(data != value)
                {
                    this.data = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        // This method is called by the Set accessor of each property.  
        // The CallerMemberName attribute that is applied to the optional propertyName  
        // parameter causes the property name of the caller to be substituted as an argument.  
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
