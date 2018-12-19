using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using MinutoSeguros.Application.ViewModels;

namespace MinutoSeguros.Application.Mappers {

    public class BlogMapper {

        private readonly List<string> _wordsExceptions;

        public BlogMapper () {

            // aqui eu pensei em fazer com regex ou incluir em um banco de dados e carrego quando sobe a aplicação e deixo em memória
            _wordsExceptions = new List<string> ();
            _wordsExceptions.Add ("a");
            _wordsExceptions.Add ("e");
            _wordsExceptions.Add ("i");
            _wordsExceptions.Add ("o");
            _wordsExceptions.Add ("u");
            _wordsExceptions.Add ("ao");
            _wordsExceptions.Add ("mais");
            _wordsExceptions.Add ("que");
            _wordsExceptions.Add ("para");
            _wordsExceptions.Add ("com");
            _wordsExceptions.Add ("estão");
            _wordsExceptions.Add ("como");
            _wordsExceptions.Add ("fim");
            _wordsExceptions.Add ("dos");
            _wordsExceptions.Add ("do");
            _wordsExceptions.Add ("em");
            _wordsExceptions.Add ("as");
            _wordsExceptions.Add ("na");
            _wordsExceptions.Add ("de");
            _wordsExceptions.Add ("os");
            _wordsExceptions.Add ("já");
            _wordsExceptions.Add ("um");
            _wordsExceptions.Add ("da");
            _wordsExceptions.Add ("no");
        }

        private BlogViewModel GetBlog (XElement channel) {
            BlogViewModel blog = new BlogViewModel ();

            try {
                blog.Title = channel.Element ("title").Value;
                blog.Description = channel.Element ("description").Value;
                blog.LastBuildDate = Convert.ToDateTime (channel.Element ("lastBuildDate").Value);
                blog.BlogItens = new List<BlogItemViewModel> ();
                blog.BestWords = new List<BlogBestWordViewModel> ();
            } catch {
                // TODO:: Fazer algum tratamento de erro
                return new BlogViewModel ();
            }

            return blog;
        }

        private BlogItemViewModel GetBlogItem (XElement item) {
            BlogItemViewModel blogItem = new BlogItemViewModel ();

            try {

                blogItem.BestWords = new List<BlogBestWordViewModel> ();
                blogItem.Title = item.Element ("title").Value;
                blogItem.Link = item.Element ("link").Value;
                blogItem.PubDate = Convert.ToDateTime (item.Element ("pubDate").Value);
                blogItem.Description = item.Element ("description").Value;

                // TODO:: Pensar em uma solução melhor para recuperar a imagem
                try {
                    blogItem.Image = item.Value.Split (new string[] { "img" }, StringSplitOptions.None) [1].Split (new string[] { "src=\"" }, StringSplitOptions.None) [1].Split (new string[] { "\"" }, StringSplitOptions.None) [0];
                } catch {
                    blogItem.Image = "https://www.minutoseguros.com.br/blog/wp-content/uploads/2018/12/para-brisa-trincado-conclusao.jpg";
                }

                // Recupera as categorias
                blogItem.Categories = new List<string> ();
                foreach (var category in item.Elements ("category")) {
                    blogItem.Categories.Add (category.Value);
                }

                // adiciona as melhores palavras para o item pelo titulo e descricao
                blogItem.BestWords = ListBestWords (blogItem.BestWords, blogItem.Title);
                blogItem.BestWords = ListBestWords (blogItem.BestWords, blogItem.Title);

            } catch {
                // TODO:: Fazer algum tratamento de erro
            }

            return blogItem;
        }

        private List<BlogBestWordViewModel> ListBestWords (List<BlogBestWordViewModel> bestWords, string wordRequest) {
            var words = wordRequest.Split (' ');

            foreach (var word in words) {
                if (string.IsNullOrEmpty (word.Trim ()) || _wordsExceptions.Any (x => x == word.ToLower () || word.Length <= 3))
                    continue;

                var wordInBestWords = bestWords.FirstOrDefault (x => x.Word == word.ToLower ());
                if (wordInBestWords == null) {
                    bestWords.Add (new BlogBestWordViewModel { Word = word.ToLower (), Quantity = 1 });
                } else {
                    wordInBestWords.Quantity += 1;
                }
            }

            return bestWords;
        }

        public BlogViewModel XmlElementToViewModel (XElement xElementRequest) {

            // recuperando o channel do xml
            XElement channel = xElementRequest.Element ("channel");

            // recuperando o header do blog
            BlogViewModel blog = GetBlog (channel);

            // recuperando os itens do blog
            foreach (XElement item in channel.Elements ("item").OrderByDescending (x => Convert.ToDateTime (x.Element ("pubDate").Value)).Take (10)) {
                BlogItemViewModel blogItem = GetBlogItem (item);

                // calculando as melhores palavras no titulo e na descricao
                blog.BestWords = ListBestWords (blog.BestWords, blogItem.Title);
                blog.BestWords = ListBestWords (blog.BestWords, blogItem.Title);
                blog.BlogItens.Add (blogItem);
            }

            return blog;
        }
    }
}