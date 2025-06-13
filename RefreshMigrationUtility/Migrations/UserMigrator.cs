using Refresh.Database;
using Refresh.Database.Models.Users;
using Refresh.Schema.Realm.Impl;

namespace RefreshMigrationUtility.Migrations;

public class UserMigrator : Migrator<RealmGameUser, GameUser>
{
    public UserMigrator(RealmDatabaseContext realm, GameDatabaseContext ef) : base(realm, ef)
    {}

    public override GameUser Map(GameDatabaseContext ef, RealmGameUser old)
    {
        return new GameUser
        {
            _Role = old._Role,
            AllowIpAuthentication = old.AllowIpAuthentication,
            BanExpiryDate = old.BanExpiryDate,
            BanReason = old.BanReason,
            BetaIconHash = old.BetaIconHash,
            BetaPlanetsHash = old.BetaPlanetsHash,
            BooFaceHash = old.BooFaceHash,
            Description = old.Description,
            EmailAddress = old.EmailAddress,
            EmailAddressVerified = old.EmailAddressVerified,
            FilesizeQuotaUsage = old.FilesizeQuotaUsage,
            ForceMatch = old.ForceMatch,
            IconHash = old.IconHash,
            JoinDate = old.JoinDate,
            LastLoginDate = old.LastLoginDate,
            Lbp2PlanetsHash = old.Lbp2PlanetsHash,
            Lbp3PlanetsHash = old.Lbp3PlanetsHash,
            LevelVisibility = old.LevelVisibility,
            LocationX = old.LocationX,
            LocationY = old.LocationY,
            MehFaceHash = old.MehFaceHash,
            PasswordBcrypt = old.PasswordBcrypt,
            PresenceServerAuthToken = old.PresenceServerAuthToken,
            ProfileVisibility = old.ProfileVisibility,
            PsnAuthenticationAllowed = old.PsnAuthenticationAllowed,
            PspIconHash = old.PspIconHash,
            Role = old.Role,
            RootPlaylist = null, // TODO: set this during a post-migration task
            RpcnAuthenticationAllowed = old.RpcnAuthenticationAllowed,
            ShouldResetPassword = old.ShouldResetPassword,
            ShowModdedContent = old.ShowModdedContent,
            ShowReuploadedContent = old.ShowReuploadedContent,
            TimedLevelUploadExpiryDate = old.TimedLevelUploadExpiryDate,
            TimedLevelUploads = old.TimedLevelUploads,
            UnescapeXmlSequences = old.UnescapeXmlSequences,
            UserId = old.UserId,
            Username = old.Username,
            VitaIconHash = old.VitaIconHash,
            VitaPlanetsHash = old.VitaPlanetsHash,
            YayFaceHash = old.YayFaceHash
        };
    }
}