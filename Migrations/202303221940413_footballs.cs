namespace FootballLeague.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class footballs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Footballs",
                c => new
                    {
                        clubID = c.Int(nullable: false, identity: true),
                        clubName = c.String(),
                        goalsScored = c.Int(nullable: false),
                        goalsConceeded = c.Int(nullable: false),
                        Nationality = c.String(),
                    })
                .PrimaryKey(t => t.clubID);
            
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        matchID = c.Int(nullable: false, identity: true),
                        homeClub = c.String(),
                        awayClub = c.String(),
                        score = c.String(),
                    })
                .PrimaryKey(t => t.matchID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Matches");
            DropTable("dbo.Footballs");
        }
    }
}
