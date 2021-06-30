using Microsoft.EntityFrameworkCore.Migrations;

namespace webshop_gyakorlas.Data.Migrations
{
    public partial class PopulateBrands : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Brands (Name) VALUES ('Rolex'),('Patek Philippe'),('Audemars Piguet'),('A.Lange & Söhne'),('Omega'),('Blancpain'),('IWC Schaffhausen'),('Jaeger - LeCoultre'),('Chopard'),('Piaget'),('Cartier'),('Vacheron Constantin'),('Montblanc'),('Ulysse Nardin'),('Panerai'),('Girard - Perregaux'),('Hublot'),('Bulgari'),('Nordgreen'),('NOMOS Glashütte'),('Vincero'),('Breitling'),('Bremont'),('TAG Heuer'),('Baume & Mercier'),('Rado'),('Maurice Lacroix'),('Roger Dubuis'),('F.P.Journe'),('Tiffany & Co.'),('Louis Vuitton'),('Bamford'),('Zenith'),('Bell & Ross'),('Arnold & Son'),('Tudor'),('Alpina'),('Seiko'),('Jaquet Droz'),('Laurent Ferrier'),('Hamilton'),('Longines'),('Maurice de Mauriac'),('Parmigiani Fleurier'),('Gucci'),('Weiss'),('Armani'),('Tissot'),('Van Cleef & Arpels'),('Junghans'),('Bulova'),('Bovet Fleurier'),('Oris'),('Armin Strom'),('Ressence'),('Sinn')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
