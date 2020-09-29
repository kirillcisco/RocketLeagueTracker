using System;
using System.Linq;

namespace Common.Models
{
    public static class ViewModelMapper
    {
        public static void TrackedUser(TrackedUser user, ref TrackedUserViewModel vm)
        {
            var defaultUri = @"https://trackercdn.com/cdn/tracker.gg/rocket-league/ranks/s4-0.png";

            try
            {
                //This could probably be done with automapper, but the config would be just as ugly as this
                vm.Name = user.Data.PlatformInfo.PlatformUserHandle;
                vm.UserId = user.UserId;
                vm.Platform = user.PlatForm;
                vm.Avatar = user.Data.PlatformInfo.AvatarUrl;
                vm.PlayerUri = new Uri($"https://rocketleague.tracker.network/rocket-league/profile/{Common.Search.RlTracker.GetPlatformString(user.PlatForm)}/{user.UserId}");

                var causal = user.Data.Segments.FirstOrDefault(x => x.Attributes.PlaylistId == 0);
                if (causal != null)
                {
                    vm.CasualMmr = causal.Stats.Rating.Value.ToString();
                    vm.CasualMatchesPlayed = causal.Stats.MatchesPlayed.Value;
                }

                var threes = user.Data.Segments.FirstOrDefault(x => x.Attributes.PlaylistId == 13);
                if (threes != null)
                {
                    vm.ThreesUri = threes.Stats.Tier.Metadata.IconUrl;
                    vm.ThreesPic = ImageManager.Instance().GetImageFromUri(vm.ThreesUri.ToString());
                    vm.ThreesTitle = threes.Stats.Tier.Metadata.Name;
                    vm.ThreesMmr = threes.Stats.Rating.Value.ToString();
                    vm.ThreesMatchesPlayed = threes.Stats.MatchesPlayed.Value;
                }
                else
                {
                    vm.ThreesUri = new Uri(defaultUri);
                    vm.ThreesPic = ImageManager.Instance().GetImageFromUri(vm.ThreesUri.ToString());
                    vm.ThreesTitle = "N/A";
                    vm.ThreesMmr = "N/A";
                    vm.ThreesMatchesPlayed = 0;
                }

                var twos = user.Data.Segments.FirstOrDefault(x => x.Attributes.PlaylistId == 11);
                if (threes != null)
                {
                    vm.TwosUri = twos.Stats.Tier.Metadata.IconUrl;
                    vm.TwosTitle = twos.Stats.Tier.Metadata.Name;
                    vm.TwosMmr = twos.Stats.Rating.Value.ToString();
                    vm.TwosMatchesPlayed = twos.Stats.MatchesPlayed.Value;
                    vm.TwosPic = ImageManager.Instance().GetImageFromUri(vm.TwosUri.ToString());
                }
                else
                {
                    vm.TwosUri = new Uri(defaultUri);
                    vm.TwosPic = ImageManager.Instance().GetImageFromUri(vm.ThreesUri.ToString());
                    vm.TwosTitle = "N/A";
                    vm.TwosMmr = "N/A";
                    vm.TwosMatchesPlayed = 0;
                }

                var ones = user.Data.Segments.FirstOrDefault(x => x.Attributes.PlaylistId == 10);
                if (ones != null)
                {
                    vm.OnesUri = ones.Stats.Tier.Metadata.IconUrl;
                    vm.OnesTitle = ones.Stats.Tier.Metadata.Name;
                    vm.OnesMmr = ones.Stats.Rating.Value.ToString();
                    vm.OnesMatchesPlayed = ones.Stats.MatchesPlayed.Value;
                    vm.OnesPic = ImageManager.Instance().GetImageFromUri(vm.OnesUri.ToString());
                }
                else
                {
                    vm.OnesUri = new Uri(defaultUri);
                    vm.OnesPic = ImageManager.Instance().GetImageFromUri(vm.ThreesUri.ToString());
                    vm.OnesTitle = "N/A";
                    vm.OnesMmr = "N/A";
                    vm.OnesMatchesPlayed = 0;
                }

                var tournament = user.Data.Segments.FirstOrDefault(x => x.Attributes.PlaylistId == 34);
                if (tournament != null)
                {
                    vm.TournamentsUri = tournament.Stats.Tier.Metadata.IconUrl;
                    vm.TournamentsTitle = tournament.Stats.Tier.Metadata.Name;
                    vm.TournamentsMmr = tournament.Stats.Rating.Value;
                    vm.TournamentsPic = ImageManager.Instance().GetImageFromUri(vm.TournamentsUri.ToString());
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
