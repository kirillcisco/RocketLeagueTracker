using System;
using System.Linq;

namespace Common.Models
{
    public static class ViewModelMapper
    {
        public static void TrackedUser(TrackedUser user, ref TrackedUserViewModel vm)
        {
            try
            {
                //This could probably be done with automapper, but the config would be just as ugly as this
                vm.Name = user.Data.PlatformInfo.PlatformUserHandle;
                vm.UserId = user.UserId;
                vm.Platform = user.PlatForm;
                vm.Avatar = user.Data.PlatformInfo.AvatarUrl;


                var threes = user.Data.Segments.FirstOrDefault(x => x.Attributes.PlaylistId == 13);
                if (threes != null)
                {
                    vm.ThreesUri = threes.Stats.Tier.Metadata.IconUrl;
                    vm.ThreesPic = ImageManager.Instance().GetImageFromUri(vm.ThreesUri.ToString());
                    vm.ThreesTitle = threes.Stats.Tier.Metadata.Name;
                    vm.ThreesMmr = threes.Stats.Rating.Value;
                    vm.ThreesMatchesPlayed = threes.Stats.MatchesPlayed.Value;
                }

                var twos = user.Data.Segments.FirstOrDefault(x => x.Attributes.PlaylistId == 11);
                if (threes != null)
                {
                    vm.TwosUri = twos.Stats.Tier.Metadata.IconUrl;
                    vm.TwosTitle = twos.Stats.Tier.Metadata.Name;
                    vm.TwosMmr = twos.Stats.Rating.Value;
                    vm.TwosMatchesPlayed = twos.Stats.MatchesPlayed.Value;
                    vm.TwosPic = ImageManager.Instance().GetImageFromUri(vm.TwosUri.ToString());
                }

                var ones = user.Data.Segments.FirstOrDefault(x => x.Attributes.PlaylistId == 10);
                if (ones != null)
                {
                    vm.OnesUri = ones.Stats.Tier.Metadata.IconUrl;
                    vm.OnesTitle = ones.Stats.Tier.Metadata.Name;
                    vm.OnesMmr = ones.Stats.Rating.Value;
                    vm.OnesMatchesPlayed = ones.Stats.MatchesPlayed.Value;
                    vm.OnesPic = ImageManager.Instance().GetImageFromUri(vm.OnesUri.ToString());
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
