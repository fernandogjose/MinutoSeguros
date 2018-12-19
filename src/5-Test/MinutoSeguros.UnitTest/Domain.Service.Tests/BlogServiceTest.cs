using System;
using System.Xml.Linq;
using Bogus;
using Microsoft.Extensions.DependencyInjection;
using MinutoSeguros.Application.AppServices;
using MinutoSeguros.Application.Interfaces.AppServices;
using MinutoSeguros.Application.Mappers;
using MinutoSeguros.Domain.Interfaces.DomainServices;
using MinutoSeguros.Domain.Interfaces.Https;
using MinutoSeguros.Domain.Services;
using MinutoSeguros.Domain.Validations;
using Moq;
using Xunit;

namespace MinutoSeguros.UnitTest.Domain.Service.Tests {
    public class BlogServiceTest {
        private readonly IServiceCollection _serviceCollection;

        private readonly ServiceProvider _services;

        private IBlogAppService _blogAppService;

        private readonly Mock<IBlogHttp> _blogHttpMock;

        private readonly Faker _faker;

        private string urlXml;

        public BlogServiceTest () {
            // faker
            _faker = new Faker (); // deixei o fake aqui para caso precise gerar alguma informação, ele é bem útil para cadastros

            // moq
            _blogHttpMock = new Mock<IBlogHttp> ();

            // di
            _serviceCollection = new ServiceCollection ();
            _serviceCollection.AddSingleton<IBlogHttp> (_blogHttpMock.Object);
            _serviceCollection.AddSingleton<BlogValidation> ();
            _serviceCollection.AddSingleton<BlogMapper> ();
            _serviceCollection.AddSingleton<IBlogService, BlogService> ();
            _serviceCollection.AddSingleton<IBlogAppService, BlogAppService> ();
            _services = _serviceCollection.BuildServiceProvider ();
        }

        [Theory]
        [InlineData ("teste")]
        public void DeveRetornarUmaExceptionQuandoOXmlResponseForInválido (string urlXml) {
            _blogAppService = _services.GetService<IBlogAppService> ();
            const string messageExpected = "Xml de retorno inválido!";

            _blogHttpMock.Setup (r => r.LoadXml (urlXml));
            var ex = Assert.Throws<Exception> (() => _blogAppService.ListTop10 (urlXml));

            Assert.Equal (ex.Message, messageExpected);
        }

        [Theory]
        [InlineData ("")]
        public void DeveRetornarUmaExceptionQuandoAUrlXmlNaoForPassada (string urlXml) {
            _blogAppService = _services.GetService<IBlogAppService> ();
            const string messageExpected = "Url do xml é obrigatória!";
            
            var ex = Assert.Throws<Exception> (() => _blogAppService.ListTop10 (urlXml));

            Assert.Equal (ex.Message, messageExpected);
        }

        [Theory]
        [InlineData ("https://www.minutoseguros.com.br/blog/feed/")]
        public void DeveRetornarUmBlogViewModelValido (string urlXml) {
            _blogAppService = _services.GetService<IBlogAppService> ();
            
            _blogHttpMock.Setup (r => r.LoadXml (urlXml)).Returns(new XElement("")); // criar um xelement valido
            var blogResponse = _blogAppService.ListTop10 (urlXml);

            Assert.True (!string.IsNullOrEmpty(blogResponse.Title));
            Assert.True (!string.IsNullOrEmpty(blogResponse.Description));
        }

        // TODO:: Incluir outros testes
    }
}