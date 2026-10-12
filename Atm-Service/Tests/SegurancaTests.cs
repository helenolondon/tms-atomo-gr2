using AtmServiceDominio.Erros;
using AtmServiceDominio.ObjetosDeDominio;
using AtmServiceDominio.ObjetosDeDominio.Seguranca.Erros;
using AtmServiceInfra.ODP;
using AtmServiceInfra.Seguranca;
using Moq;
using System;
using Xunit;

namespace Tests
{
    [Trait("Domínio/Seguraça", "Login")]
    public class SegurancaTests
    {
        [Fact]
        public void LoginRetornaObjetoDeLoginTest()
        {
            var segurancaRepMock = new Mock<ISegurancaRepositorio>();
            var loginInfoRepMock = new Mock<ILoginInfoRepositorio>();

            var loginInfo = new LoginInfoODP()
            {
                CodLoginInfo = 1,
                DataAlt = new DateTime(),
                DataLogin = new DateTime(),
                DataCad = new DateTime(),
                Login = "TEST.LOGIN",
                Token = "XXX",
                UsuarioCad = "SISTEMA"
            };

            loginInfoRepMock.Setup(m => m.ObterLoginInfoPorLogin("TEST.LOGIN")).Returns(loginInfo);
            loginInfoRepMock.Setup(m => m.Salvar(It.IsAny<LoginInfoODP>()));

            var dom = new SegurancaOD(segurancaRepMock.Object, loginInfoRepMock.Object);

            var testOdp = new LoginODP() 
            {
                Login = "TEST.LOGIN",
                CodLogin = 1,
                DataCad = new DateTime(),
                Senha = dom.Criptografar("123"),
                UsuarioCad = "SISTEMA"
            };

            segurancaRepMock.Setup(m => m.ObterLoginPorLogin("TEST.LOGIN")).Returns(testOdp);
            
            string senha;

            var loginObjODN = dom.Login("Test.login", "123", out senha);

            Assert.NotNull(loginObjODN);
        }

        [Fact]
        public void LoginFalhaRetornaNullLoginTest()
        {
            var segurancaRepMock = new Mock<ISegurancaRepositorio>();
            var loginInfoRepMock = new Mock<ILoginInfoRepositorio>();

            var loginInfo = new LoginInfoODP()
            {
                CodLoginInfo = 1,
                DataAlt = new DateTime(),
                DataLogin = new DateTime(),
                DataCad = new DateTime(),
                Login = "TEST.LOGIN",
                Token = "XXX",
                UsuarioCad = "SISTEMA"
            };

            loginInfoRepMock.Setup(m => m.ObterLoginInfoPorLogin("TEST.LOGIN")).Returns(loginInfo);
            loginInfoRepMock.Setup(m => m.Salvar(It.IsAny<LoginInfoODP>()));

            var dom = new SegurancaOD(segurancaRepMock.Object, loginInfoRepMock.Object);

            var testOdp = new LoginODP()
            {
                Login = "TEST.LOGIN",
                CodLogin = 1,
                DataCad = new DateTime(),
                Senha = dom.Criptografar("123"),
                UsuarioCad = "SISTEMA"
            };

            segurancaRepMock.Setup(m => m.ObterLoginPorLogin("TEST.LOGIN")).Returns(testOdp);

            string senha;

            var exception = Assert.Throws<DominioExcecao>(() => { dom.Login("Test.login.errado", "123", out senha); });

            Assert.Equal("Login inválido", exception.Message);        
        }

        [Fact]
        public void LoginFalhaQuandoSemLoginLoginTest()
        {
            var segurancaRepMock = new Mock<ISegurancaRepositorio>();
            var loginInfoRepMock = new Mock<ILoginInfoRepositorio>();

            var loginInfo = new LoginInfoODP()
            {
                CodLoginInfo = 1,
                DataAlt = new DateTime(),
                DataLogin = new DateTime(),
                DataCad = new DateTime(),
                Login = "TEST.LOGIN",
                Token = "XXX",
                UsuarioCad = "SISTEMA"
            };

            loginInfoRepMock.Setup(m => m.ObterLoginInfoPorLogin("TEST.LOGIN")).Returns(loginInfo);
            loginInfoRepMock.Setup(m => m.Salvar(It.IsAny<LoginInfoODP>()));

            var dom = new SegurancaOD(segurancaRepMock.Object, loginInfoRepMock.Object);

            var testOdp = new LoginODP()
            {
                Login = "TEST.LOGIN",
                CodLogin = 1,
                DataCad = new DateTime(),
                Senha = dom.Criptografar("123"),
                UsuarioCad = "SISTEMA"
            };

            segurancaRepMock.Setup(m => m.ObterLoginPorLogin("TEST.LOGIN")).Returns(testOdp);

            string senha;

            var exception = Assert.Throws<LoginInvalidoDE>(() => { dom.Login("", "123", out senha); });

            Assert.Equal("Login inválido", exception.Message);
        }

        [Fact]
        public void LoginFalhaQuandoSemSenhaLoginTest()
        {
            var segurancaRepMock = new Mock<ISegurancaRepositorio>();
            var loginInfoRepMock = new Mock<ILoginInfoRepositorio>();

            var loginInfo = new LoginInfoODP()
            {
                CodLoginInfo = 1,
                DataAlt = new DateTime(),
                DataLogin = new DateTime(),
                DataCad = new DateTime(),
                Login = "TEST.LOGIN",
                Token = "XXX",
                UsuarioCad = "SISTEMA"
            };

            loginInfoRepMock.Setup(m => m.ObterLoginInfoPorLogin("TEST.LOGIN")).Returns(loginInfo);
            loginInfoRepMock.Setup(m => m.Salvar(It.IsAny<LoginInfoODP>()));

            var dom = new SegurancaOD(segurancaRepMock.Object, loginInfoRepMock.Object);

            var testOdp = new LoginODP()
            {
                Login = "TEST.LOGIN",
                CodLogin = 1,
                DataCad = new DateTime(),
                Senha = dom.Criptografar("123"),
                UsuarioCad = "SISTEMA"
            };

            segurancaRepMock.Setup(m => m.ObterLoginPorLogin("TEST.LOGIN")).Returns(testOdp);

            string senha;

            var exception = Assert.Throws<LoginInvalidoDE>(() => { dom.Login("Test.login", "", out senha); });

            Assert.Equal("Login inválido", exception.Message);
        }

        [Fact]
        public void LoginFalhaQuandoLoginNullLoginTest()
        {
            var segurancaRepMock = new Mock<ISegurancaRepositorio>();
            var loginInfoRepMock = new Mock<ILoginInfoRepositorio>();

            var loginInfo = new LoginInfoODP()
            {
                CodLoginInfo = 1,
                DataAlt = new DateTime(),
                DataLogin = new DateTime(),
                DataCad = new DateTime(),
                Login = "TEST.LOGIN",
                Token = "XXX",
                UsuarioCad = "SISTEMA"
            };

            loginInfoRepMock.Setup(m => m.ObterLoginInfoPorLogin("TEST.LOGIN")).Returns(loginInfo);
            loginInfoRepMock.Setup(m => m.Salvar(It.IsAny<LoginInfoODP>()));

            var dom = new SegurancaOD(segurancaRepMock.Object, loginInfoRepMock.Object);

            var testOdp = new LoginODP()
            {
                Login = "TEST.LOGIN",
                CodLogin = 1,
                DataCad = new DateTime(),
                Senha = dom.Criptografar("123"),
                UsuarioCad = "SISTEMA"
            };

            segurancaRepMock.Setup(m => m.ObterLoginPorLogin("TEST.LOGIN")).Returns(testOdp);

            string token;

            var exception = Assert.Throws<LoginInvalidoDE>(() => { dom.Login(null, "123", out token); });

            Assert.Equal("Login inválido", exception.Message);
        }

        [Fact]
        public void LoginFalhaQuandoSenhaNullLoginTest()
        {
            var segurancaRepMock = new Mock<ISegurancaRepositorio>();
            var loginInfoRepMock = new Mock<ILoginInfoRepositorio>();

            var loginInfo = new LoginInfoODP()
            {
                CodLoginInfo = 1,
                DataAlt = new DateTime(),
                DataLogin = new DateTime(),
                DataCad = new DateTime(),
                Login = "TEST.LOGIN",
                Token = "XXX",
                UsuarioCad = "SISTEMA"
            };

            loginInfoRepMock.Setup(m => m.ObterLoginInfoPorLogin("TEST.LOGIN")).Returns(loginInfo);
            loginInfoRepMock.Setup(m => m.Salvar(It.IsAny<LoginInfoODP>()));

            var dom = new SegurancaOD(segurancaRepMock.Object, loginInfoRepMock.Object);

            var testOdp = new LoginODP()
            {
                Login = "TEST.LOGIN",
                CodLogin = 1,
                DataCad = new DateTime(),
                Senha = dom.Criptografar("123"),
                UsuarioCad = "SISTEMA"
            };

            segurancaRepMock.Setup(m => m.ObterLoginPorLogin("TEST.LOGIN")).Returns(testOdp);

            string token;

            var exception = Assert.Throws<LoginInvalidoDE>(() => { dom.Login("Test.login", null, out token); });

            Assert.Equal("Login inválido", exception.Message);
        }

        [Fact]
        public void LoginRetornaRenovaTokenLoginTest()
        {
            var segurancaRepMock = new Mock<ISegurancaRepositorio>();
            var loginInfoRepMock = new Mock<ILoginInfoRepositorio>();

            var loginInfo = new LoginInfoODP()
            {
                CodLoginInfo = 1,
                DataAlt = new DateTime(),
                DataLogin = new DateTime(),
                DataCad = new DateTime(),
                Login = "TEST.LOGIN",
                Token = "XXX",
                UsuarioCad = "SISTEMA"
            };

            loginInfoRepMock.Setup(m => m.ObterLoginInfoPorLogin("TEST.LOGIN")).Returns(loginInfo);
            loginInfoRepMock.Setup(m => m.Salvar(It.IsAny<LoginInfoODP>()));
            loginInfoRepMock.SetupSequence(m => m.ObterProximoValorDeSequencia("TD_LOGININFO_SEQ")).Returns(1);
            
            var dom = new SegurancaOD(segurancaRepMock.Object, loginInfoRepMock.Object);

            var testOdp = new LoginODP()
            {
                Login = "TEST.LOGIN",
                CodLogin = 1,
                DataCad = new DateTime(),
                Senha = dom.Criptografar("123"),
                UsuarioCad = "SISTEMA"
            };

            segurancaRepMock.Setup(m => m.ObterLoginPorLogin("TEST.LOGIN")).Returns(testOdp);

            string token = null;

            dom.Login("Test.login", "123", out token);

            Assert.True(!string.IsNullOrEmpty(token));
        }

        [Fact]
        public void LoginRetornaTokenAtivoLoginTest()
        {
            var segurancaRepMock = new Mock<ISegurancaRepositorio>();
            var loginInfoRepMock = new Mock<ILoginInfoRepositorio>();

            var loginInfo = new LoginInfoODP()
            {
                CodLoginInfo = 1,
                DataAlt = null,
                DataLogin = DateTime.Now,
                DataCad = DateTime.Now,
                Login = "TEST.LOGIN",
                Token = "XXX",
                UsuarioCad = "SISTEMA"
            };

            loginInfoRepMock.Setup(m => m.ObterLoginInfoPorLogin("TEST.LOGIN")).Returns(loginInfo);
            loginInfoRepMock.Setup(m => m.Salvar(It.IsAny<LoginInfoODP>()));

            var dom = new SegurancaOD(segurancaRepMock.Object, loginInfoRepMock.Object);

            var testOdp = new LoginODP()
            {
                Login = "TEST.LOGIN",
                CodLogin = 1,
                DataCad = DateTime.Now,
                Senha = dom.Criptografar("123"),
                UsuarioCad = "SISTEMA"
            };

            segurancaRepMock.Setup(m => m.ObterLoginPorLogin("TEST.LOGIN")).Returns(testOdp);

            string token = null;

            dom.Login("Test.login", "123", out token);

            Assert.True(!string.IsNullOrEmpty(token));
        }
    }
}
