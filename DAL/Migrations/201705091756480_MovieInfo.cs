namespace DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MovieInfo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actors",
                c => new
                    {
                        ActorId = c.Guid(nullable: false),
                        Name = c.String(),
                        Dob = c.DateTime(),
                        Bio = c.String(),
                    })
                .PrimaryKey(t => t.ActorId);
            
            CreateTable(
                "dbo.Movies",
                c => new
                    {
                        MovieId = c.Guid(nullable: false),
                        Name = c.String(),
                        YearOfRelease = c.String(),
                        Plot = c.String(),
                        Poster = c.Binary(),
                        ProducerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.MovieId)
                .ForeignKey("dbo.Producers", t => t.ProducerId, cascadeDelete: true)
                .Index(t => t.ProducerId);
            
            CreateTable(
                "dbo.Producers",
                c => new
                    {
                        ProducerId = c.Guid(nullable: false),
                        Name = c.String(),
                        Dob = c.DateTime(),
                        Bio = c.String(),
                    })
                .PrimaryKey(t => t.ProducerId);
            
            CreateTable(
                "dbo.MovieInfoes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        MovieId = c.Guid(nullable: false),
                        ActorId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Actors", t => t.ActorId, cascadeDelete: true)
                .ForeignKey("dbo.Movies", t => t.MovieId, cascadeDelete: true)
                .Index(t => t.MovieId)
                .Index(t => t.ActorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MovieInfoes", "MovieId", "dbo.Movies");
            DropForeignKey("dbo.MovieInfoes", "ActorId", "dbo.Actors");
            DropForeignKey("dbo.Movies", "ProducerId", "dbo.Producers");
            DropIndex("dbo.MovieInfoes", new[] { "ActorId" });
            DropIndex("dbo.MovieInfoes", new[] { "MovieId" });
            DropIndex("dbo.Movies", new[] { "ProducerId" });
            DropTable("dbo.MovieInfoes");
            DropTable("dbo.Producers");
            DropTable("dbo.Movies");
            DropTable("dbo.Actors");
        }
    }
}
