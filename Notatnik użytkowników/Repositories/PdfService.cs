using Notatnik_użytkowników.Models;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Notatnik_użytkowników.Repositories
{
    public class PdfService : IPdfService
    {
        static IContainer Block(IContainer container)
        {
            return container
                .Border(1)                
                .Background(Colors.Grey.Lighten3)
                .ShowOnce()
                .MinWidth(60)
                .MinHeight(50)
                .AlignCenter()
                .AlignMiddle();
        }

        public void GeneratePdf(Stream stream, IEnumerable<UserModel> userModelList)
        {
            var userModel = userModelList.FirstOrDefault();

            var document = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(15));

                    page.Header()
                        .Height(40)
                        .AlignCenter()
                        .Background(Colors.Grey.Lighten1)
                        .AlignMiddle()
                        .Text($"Raport użytkownika")
                        .SemiBold().FontSize(20)
                        .FontColor(Colors.Black);

                    page.Content().Table(table =>
                    {
                        table.ColumnsDefinition(columns =>
                        {
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                            columns.RelativeColumn();
                        });

                        table.Header(header =>
                       {
                           header.Cell().Element(Block).Background(Colors.White).Text("Tytuł");
                           header.Cell().Element(Block).Background(Colors.White).Text("Imie");
                           header.Cell().Element(Block).Background(Colors.White).Text("Nazwisko");
                           header.Cell().Element(Block).Background(Colors.White).Text("Data urodzenia");
                           header.Cell().Element(Block).Background(Colors.White).Text("Płeć");
                           header.Cell().Element(Block).Background(Colors.White).Text("Wiek");
                       });

                        foreach (UserModel user in userModelList)
                        {
                            if (user.Gender == Gender.mężczyzna)
                            {
                                table.Cell().Element(Block).Text("Pan");
                            }
                            else
                            {
                                table.Cell().Element(Block).Text("Pani");
                            }
                            
                            table.Cell().Element(Block).Text(user.Name);
                            table.Cell().Element(Block).Text(user.Surname);
                            table.Cell().Element(Block).Text(user.DateOfBirth.ToString("MM/dd/yyyy"));
                            table.Cell().Element(Block).Text(user.Gender);
                            table.Cell().Element(Block).Text(user.SetAge());
                        }

                        

                    });

                    page.Footer()
                      .AlignCenter()
                      .Text(x =>
                      {
                          x.Span("Page ");
                          x.CurrentPageNumber();
                      });
                });
            });

            document.GeneratePdf(stream);
        }
    }    
}