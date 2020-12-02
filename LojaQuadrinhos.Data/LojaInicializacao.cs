
using LojaQuadrinhos.Model.Entidades;
using LojaQuadrinhos.WebApp.Data;
using System.Collections.Generic;
using System.Linq;

namespace LojaQuadrinhos.Data
{
    public class LojaInicializacao
    {

        public static void Initialize(LojaQuadrinhosContext context)
        {
            context.Database.EnsureCreated();
            if (context.Categorias.Any())
            {
                return;
            }
            var categorias = new List<Categoria>
            {
                new Categoria{Descricao ="Infantis" },
                new Categoria{Descricao ="Mangas" },
                new Categoria{Descricao ="Marvel" },
                new Categoria{Descricao ="Infantis" },
                new Categoria{Descricao ="Comics" },
                new Categoria{Descricao ="Faroeste" },
                new Categoria{Descricao ="Disney" },
                new Categoria{Descricao ="Terros" },
                new Categoria{Descricao ="Diversos" },
                new Categoria{Descricao ="Importados" },
                new Categoria{Descricao ="Albuns/Figurinhas" },
                new Categoria{Descricao ="DC Comics" },
                new Categoria{Descricao ="Infanto Juveno" },
                new Categoria{Descricao ="Livros/Manuais" },
                new Categoria{Descricao ="Harvey" },

            };

            foreach (Categoria item in categorias)
            {
                context.Categorias.Add(item);
            }
            context.SaveChanges();

            List<Categoria> categoria = context.Categorias.AsEnumerable().ToList();

            var produtos = new List<Produto>
            {
                new Produto{Ano = "1991", Editora = "Globo",Idioma = "Português", Paginas = 68,Titulo = "Akira Ed.Globo", Serie = "6",Autor=" ", Preco = 15.00m,Quantidade = 100,Categoria = categoria.FirstOrDefault(x => x.Descricao == "Mangas")},
                new Produto{Ano = "2020", Editora = "Planet Manga",Idioma = "Português", Paginas = 208,Titulo = "One-Punch Man - 01", Serie = "2",Autor="One, Yusuke Murata", Preco = 24.90m,Quantidade = 100, Categoria = categoria.FirstOrDefault(x => x.Descricao == "Mangas")},
                new Produto{Ano = "2020", Editora = "Planet Manga",Idioma = "Português", Paginas = 208,Titulo = "Berserk - 38", Serie = "3",Autor="Kentaro Miura", Preco = 24.90m,Quantidade = 100, Categoria = categoria.FirstOrDefault(x => x.Descricao == "Mangas")},
                new Produto{Ano = "2020", Editora = "Marvel",Idioma = "Português", Paginas = 336,Titulo = "Soldado Invernal: O Inverno Mais Longo", Serie = "10",Autor="Butch Guice, Ed Brubaker, Michael Lark", Preco = 122.90m,Quantidade = 100, Categoria = categoria.FirstOrDefault(x => x.Descricao == "Comics")},
                new Produto{Ano = "2020", Editora = "Marvel",Idioma = "Português", Paginas = 170,Titulo = "Doutor Estranho", Serie = "4",Autor=" Andy MacDonald, Javier Piña, Jesús Saíz, Mark Waid , Pornsak Pichetshote, Tini Howard", Preco = 18.90m,Quantidade = 100, Categoria = categoria.FirstOrDefault(x => x.Descricao == "Comics")},
                new Produto{Ano = "2020", Editora = "DC",Idioma = "Português", Paginas = 168,Titulo = "Quarto Mundo por Jack Kirby", Serie = "6",Autor="Jack Kirby", Preco = 26.90m,Quantidade = 100, Categoria = categoria.FirstOrDefault(x => x.Descricao == "Comics")},
                new Produto{Ano = "2020", Editora = "DC",Idioma = "Português", Paginas = 96,Titulo = "Mulher Maravilha", Serie = "43",Autor="Bilquis Evely, Gail Simone, Greg Rucka, José Luis García-López, Liam Sharp, Steve Orlando", Preco = 20.90m,Quantidade = 100, Categoria = categoria.FirstOrDefault(x => x.Descricao == "Comics")},
                new Produto{Ano = "2020", Editora = "Netflix",Idioma = "Português", Paginas = 68,Titulo = "Stranger Things", Serie = "2",Autor="Edgar Salazar, Jody Houser", Preco = 41.00m,Quantidade = 100, Categoria = categoria.FirstOrDefault(x => x.Descricao == "Comics")},

                new Produto{Ano = "2020", Editora = "Mauricio de Sousa, Turma da Mônica",Idioma = "Português", Paginas = 80,Titulo = "Cebolinha -O Julgamento do Louco", Serie = "65",Autor="Carlos Kina, Daniel Mallzhen, Juliana M. Assis, Sidnei L. Salustre", Preco = 7.90m,Quantidade = 100, Categoria = categoria.FirstOrDefault(x => x.Descricao == "Infantis")},
                new Produto{Ano = "2020", Editora = "Mauricio de Sousa, Turma da Mônica",Idioma = "Português", Paginas = 80,Titulo = "Monica -OBrincando de Casinha", Serie = "65",Autor="Carlos Kina, Daniel Mallzhen, Juliana M. Assis, Sidnei L. Salustre", Preco = 7.90m,Quantidade = 100, Categoria = categoria.FirstOrDefault(x => x.Descricao == "Infantis")},
                new Produto{Ano = "2020", Editora = "Mauricio de Sousa, Turma da Mônica",Idioma = "Português", Paginas = 80,Titulo = "Magali -O Julgamento do Louco", Serie = "64",Autor="Carlos Kina, Daniel Mallzhen, Juliana M. Assis, Sidnei L. Salustre", Preco = 7.90m, Quantidade = 100,Categoria = categoria.FirstOrDefault(x => x.Descricao == "Infantis")},
                new Produto{Ano = "2020", Editora = "Mauricio de Sousa, Turma da Mônica",Idioma = "Português", Paginas = 80,Titulo = "Almanaque do cebolinha", Serie = "64",Autor="Carlos Kina, Daniel Mallzhen, Juliana M. Assis, Sidnei L. Salustre", Preco = 7.90m,Quantidade = 100, Categoria = categoria.FirstOrDefault(x => x.Descricao == "Infantis")},
                new Produto{Ano = "2020", Editora = "Mauricio de Sousa, Turma da Mônica",Idioma = "Português", Paginas = 80,Titulo = "Almanaque da Monica", Serie = "64",Autor="Carlos Kina, Daniel Mallzhen, Juliana M. Assis, Sidnei L. Salustre", Preco = 7.90m,Quantidade = 100, Categoria = categoria.FirstOrDefault(x => x.Descricao == "Infantis")},
                new Produto{Ano = "2020", Editora = "Mauricio de Sousa, Turma da Mônica",Idioma = "Português", Paginas = 80,Titulo = "Almanaque da Magali", Serie = "64",Autor="Carlos Kina, Daniel Mallzhen, Juliana M. Assis, Sidnei L. Salustre", Preco = 7.90m,Quantidade = 100, Categoria = categoria.FirstOrDefault(x => x.Descricao == "Infantis")},
                new Produto{Ano = "2020", Editora = "Mauricio de Sousa, Turma da Mônica",Idioma = "Português", Paginas = 80,Titulo = "Almanaque do Cascão", Serie = "64",Autor="Carlos Kina, Daniel Mallzhen, Juliana M. Assis, Sidnei L. Salustre", Preco = 7.90m,Quantidade = 100, Categoria = categoria.FirstOrDefault(x => x.Descricao == "Infantis")},
                new Produto{Ano = "2020", Editora = "Disney Comics",Idioma = "Português", Paginas = 224,Titulo = "A Saga de Mac Donald", Serie = "6",Autor=" ", Preco = 69.00m,Quantidade = 100, Categoria = categoria.FirstOrDefault(x => x.Descricao == "Disney")},
               
            };
            foreach (Produto item in produtos)
            {
                context.Produtos.Add(item);
            }
            context.SaveChanges();

        }

    }
}