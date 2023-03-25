namespace FootballLeague.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fooballsmatches : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MatchesFootballs",
                c => new
                    {
                        Matches_matchID = c.Int(nullable: false),
                        Football_clubID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Matches_matchID, t.Football_clubID })
                .ForeignKey("dbo.Matches", t => t.Matches_matchID, cascadeDelete: true)
                .ForeignKey("dbo.Footballs", t => t.Football_clubID, cascadeDelete: true)
                .Index(t => t.Matches_matchID)
                .Index(t => t.Football_clubID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MatchesFootballs", "Football_clubID", "dbo.Footballs");
            DropForeignKey("dbo.MatchesFootballs", "Matches_matchID", "dbo.Matches");
            DropIndex("dbo.MatchesFootballs", new[] { "Football_clubID" });
            DropIndex("dbo.MatchesFootballs", new[] { "Matches_matchID" });
            DropTable("dbo.MatchesFootballs");
        }
    }
}
