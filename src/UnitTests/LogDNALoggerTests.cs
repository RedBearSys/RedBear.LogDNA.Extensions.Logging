using Microsoft.Extensions.Logging;
using Moq;
using RedBear.LogDNA;
using RedBear.LogDNA.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging.Internal;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xunit;

namespace UnitTests
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class LogDNALoggerTests
    {

        #region "Lorem Ipsum"

        private const string LoremIpsum =
            @"Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla eget eleifend libero. Phasellus non felis ac nunc placerat ultricies in sed sem. In maximus lacus vel diam gravida pulvinar. Fusce odio elit, convallis id accumsan id, volutpat eget augue. Nam sit amet erat eu massa aliquam dapibus ac eget dui. Vivamus eu ligula ligula. Sed tincidunt fermentum leo. Curabitur nec neque id lacus elementum sagittis.

Sed quam ante, dictum sed quam eu, ultrices accumsan tortor. In non nunc fermentum nisl lacinia cursus et sit amet felis. Nullam aliquet cursus tortor vitae molestie. Suspendisse interdum feugiat mattis. Duis ut metus nisl. Curabitur consectetur augue vel pellentesque tincidunt. Integer consequat nulla vel nibh euismod, at ullamcorper velit commodo. Praesent gravida laoreet diam sed lobortis. Nulla ut lectus sit amet purus mattis tempus. Morbi vitae orci at nisl pharetra tristique. Nunc facilisis erat felis, ultricies posuere enim consequat at. Nam sed turpis posuere, tempus odio a, ultricies dolor. Etiam euismod tristique pretium. Duis congue tellus et purus tincidunt, quis posuere dolor aliquet.

Etiam sed efficitur ligula. In ac est vulputate, scelerisque tortor a, tincidunt diam. Praesent bibendum hendrerit ex, vel viverra purus mollis vel. Suspendisse mauris sapien, semper et pretium id, imperdiet sit amet enim. Fusce sit amet tristique magna. Cras varius, lectus et lacinia eleifend, neque orci mollis mauris, iaculis luctus libero mi in lorem. Nulla ultrices sem turpis, eget dapibus diam molestie eu. Suspendisse nunc velit, laoreet vel congue et, mattis id est. Praesent pellentesque elit ac ex commodo convallis. Aliquam euismod lobortis commodo. Donec aliquam ligula vel dui vehicula, sit amet auctor tortor commodo. Sed porta eros eget dolor tincidunt, nec vestibulum sapien blandit. Pellentesque id tempus felis. Donec nibh lorem, cursus ut diam in, porta consectetur est. Pellentesque sed velit sem.

Maecenas quis blandit quam. Aenean elementum, ante ac feugiat sollicitudin, erat lectus condimentum risus, sed imperdiet sem ex non dui. In pellentesque purus ac ligula mattis, et faucibus nulla gravida. Praesent molestie sem quis consectetur tristique. Aliquam erat volutpat. Phasellus placerat accumsan turpis id dapibus. Vivamus in diam cursus, rutrum lorem id, venenatis sapien. Maecenas sagittis dignissim est, a iaculis erat gravida id. Praesent semper eget augue eget varius. Vivamus luctus bibendum iaculis. Suspendisse at turpis at ante hendrerit facilisis quis sit amet risus. Praesent nec mi urna.

Maecenas dapibus et elit sed cursus. Phasellus id gravida leo. Curabitur pulvinar ultrices dolor a porta. Phasellus non gravida nunc. Nunc imperdiet volutpat ex sit amet feugiat. Nulla ultrices dui ligula, eget ullamcorper ante consectetur vel. Nullam a nulla lacus. Fusce euismod posuere semper. Praesent condimentum vel nulla a interdum. Quisque sed urna dui. Interdum et malesuada fames ac ante ipsum primis in faucibus. Nam eleifend vulputate sapien ac semper. In at ipsum lacus.

Curabitur efficitur lacus eget justo consectetur, quis congue eros pulvinar. Maecenas tempus ornare malesuada. Maecenas ornare augue odio, eu congue ante porttitor id. In enim purus, sodales eu ornare et, placerat vel elit. Fusce feugiat maximus mauris non facilisis. Donec aliquet ipsum ac ligula auctor, sit amet hendrerit eros rhoncus. Curabitur viverra sem vel aliquet consequat. Suspendisse cursus nibh nec risus sollicitudin, sit amet lobortis ante porttitor. Praesent suscipit laoreet sem, in tristique ante faucibus sed. Phasellus risus nisi, lacinia ut felis fermentum, feugiat aliquam orci. Phasellus sit amet tempor tortor, in rhoncus leo. Cras luctus venenatis nunc a tempus. Morbi gravida arcu sed tortor tincidunt varius. Nulla a metus at risus lobortis rutrum.

Aenean eu elit diam. Cras sed gravida ex. Aenean consequat sodales ultrices. Morbi pellentesque turpis ante, elementum bibendum nunc aliquet quis. Integer consectetur ante sapien. Etiam ut erat vel lorem lobortis laoreet. Nulla facilisi. Morbi nec dui in ante mattis pretium. Duis rhoncus sodales pretium. Nam sapien diam, ultricies nec mauris vel, elementum vehicula dui. In volutpat vitae leo sit amet maximus. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Quisque ut leo nulla. Etiam blandit neque quis imperdiet semper.

Pellentesque elit nisl, aliquam a lobortis sed, pharetra eget felis. Pellentesque semper maximus augue consectetur accumsan. Sed nec lacus commodo ex sodales aliquet. Vestibulum ut volutpat lectus. Cras sit amet neque dui. Suspendisse malesuada dictum felis sit amet accumsan. Quisque nec dapibus neque. Nam pharetra dolor ut dolor ultricies, et finibus orci dapibus. Aenean dignissim lobortis aliquam. Quisque tempus non ante vitae aliquet. Donec commodo ante gravida orci tincidunt blandit. Donec vel placerat urna. Vestibulum eu erat massa.

Donec viverra, ligula id ultrices eleifend, arcu ligula consequat odio, vel elementum massa quam condimentum quam. Aenean euismod pulvinar mollis. Aenean libero ligula, tristique imperdiet orci ut, condimentum euismod urna. Vestibulum diam turpis, scelerisque a dictum vitae, commodo ut justo. Maecenas ac lectus augue. Aenean sit amet nibh vitae dui venenatis scelerisque sit amet cursus quam. Quisque sit amet porttitor odio. Ut mollis mauris id purus ultricies pretium. Sed quis vestibulum nunc.

Mauris luctus semper condimentum. Nam consectetur facilisis quam at vestibulum. Donec bibendum mollis leo, non ultrices turpis ultricies ac. Fusce in ultricies diam. Praesent sagittis placerat enim sed mattis. Aenean ex tortor, viverra at orci dapibus, pellentesque lobortis nisl. Cras egestas elit et lectus euismod, in rhoncus neque suscipit.

Nunc vel scelerisque libero. Pellentesque habitant morbi tristique senectus et netus et malesuada fames ac turpis egestas. Pellentesque feugiat tincidunt leo eu tempor. Maecenas erat mauris, aliquam eu turpis ac, suscipit finibus massa. Cras tempus blandit tellus vitae elementum. Sed ac ultricies metus. Nulla bibendum cursus magna, ut pretium est hendrerit eget. Sed ultricies quam magna. Cras viverra luctus ex eget condimentum. Sed finibus turpis at gravida bibendum. Vestibulum sollicitudin aliquet porta. Nulla ut ligula lacus. Nunc et semper nisi, ac accumsan metus. Vivamus efficitur dui ut arcu facilisis faucibus. Nulla facilisi.

In lacinia ligula ex, vel venenatis nibh vehicula id. Aenean non lorem sit amet nisl cursus tristique sed in neque. Vivamus ac efficitur mi. Nunc mauris augue, malesuada a fringilla eu, pulvinar in turpis. Aliquam consectetur, felis et ultrices facilisis, quam sapien molestie nisi, vel auctor dolor ante a enim. Aliquam ultricies lobortis urna a bibendum. In eleifend ultricies nisi nec facilisis. Integer rutrum vulputate varius. Nulla ut velit vel enim tempus posuere.

In aliquet tellus vitae velit dignissim, at congue est accumsan. Vestibulum eu ante mattis, lobortis mi eget, sodales odio. Etiam dictum sem nec arcu tincidunt malesuada. In fermentum pellentesque libero, tristique placerat nibh interdum ut. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae; Morbi eleifend hendrerit massa quis finibus. Aliquam erat volutpat. Fusce cursus nisl vel nisi sagittis blandit. Curabitur mattis sem et ligula venenatis rutrum. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.

Vestibulum mi sapien, gravida laoreet hendrerit id, condimentum at est. Aliquam facilisis magna eget lectus placerat volutpat. Praesent ultricies nibh sed urna tincidunt, eget suscipit dui mollis. Suspendisse venenatis pellentesque est nec accumsan. Sed at aliquam enim. Proin et nisi id justo accumsan ultrices. Etiam vitae quam tellus. Etiam vulputate elit vel odio molestie ornare. Nulla id dolor et ipsum porta viverra ac a augue. Nunc porttitor lectus ac arcu placerat sagittis.

Vivamus aliquam sit amet metus non interdum. Mauris facilisis nibh lorem, vitae hendrerit urna ornare ac. Duis consectetur sit amet diam non accumsan. In fringilla metus lacus, eu vulputate neque hendrerit vitae. Pellentesque volutpat laoreet mauris, vel fringilla nibh pellentesque sollicitudin. Praesent sed congue odio. Suspendisse eget dui nec sapien sagittis lobortis molestie vitae velit. Aliquam eget urna in lacus gravida ullamcorper. Donec consectetur bibendum justo sit amet viverra. Quisque ac mi arcu. Nunc nec sem pulvinar, placerat elit et, pulvinar tortor. Vivamus sed tempor elit. Phasellus eu nibh ultricies, tempus lorem nec, dictum tellus. Proin risus sapien, fringilla non feugiat sed, consequat quis arcu. Aliquam pretium ipsum in hendrerit faucibus. Cras a nulla finibus, tincidunt nunc accumsan, condimentum libero.

Fusce at mattis lectus. Cras sed neque ornare, rutrum odio at, pellentesque ipsum. Nam rutrum varius dui, sed semper nibh. Phasellus malesuada egestas dui ut efficitur. Cras convallis magna non tellus hendrerit, non sagittis lacus fermentum. In ullamcorper enim ut sem tristique malesuada. Quisque cursus quam ipsum, cursus pretium ligula gravida non. Sed et diam sit amet velit pulvinar sagittis sed vel erat. Praesent quis ante euismod, elementum enim ut, malesuada quam. Curabitur dapibus lobortis ex vel suscipit. Duis vitae metus malesuada, gravida dolor eget, fringilla diam. Donec vulputate lectus eget nisi pharetra, id lobortis ex commodo.

Quisque a urna sed eros mattis sodales. Phasellus venenatis facilisis sapien, non interdum nibh condimentum mollis. Duis quis felis odio. Vivamus ornare arcu ut risus rhoncus sodales. Proin molestie volutpat justo, sed ultricies orci commodo et. Quisque nec placerat nisl. Nam congue tincidunt elementum. Ut et turpis nulla. Donec placerat diam ipsum, vel vulputate velit accumsan sed. Etiam porttitor erat eget facilisis varius. Phasellus auctor feugiat semper. Morbi feugiat libero eget ipsum sodales, at dapibus nunc pulvinar. Nunc in urna nisi. Curabitur ac lorem nec elit egestas pellentesque eu pharetra ante. Etiam in leo nec ante efficitur molestie vitae vel massa.

Proin mattis, lacus eu fringilla efficitur, odio eros congue purus, scelerisque luctus ipsum ipsum vel tellus. Praesent sed lectus tristique, scelerisque ante pellentesque, pulvinar nisl. Pellentesque vehicula pretium ligula, et ultricies elit suscipit quis. Nullam porttitor sapien eget finibus fermentum. Sed et velit feugiat urna aliquet tempus mollis eget orci. Duis luctus facilisis consectetur. Vivamus id sapien turpis. Integer lobortis justo vel felis ornare, sed bibendum erat semper. Vestibulum ac mattis libero, at ultricies tellus. Nunc pharetra pellentesque gravida. Nulla accumsan augue id lobortis tempus. Maecenas tempus augue orci, vel imperdiet nulla aliquet ac.

Vestibulum sit amet neque mauris. Aenean tristique at lorem vel fringilla. Curabitur vel lectus nec ipsum ullamcorper euismod. Nam elementum volutpat ligula ac lobortis. Curabitur erat magna, fringilla nec dui a, eleifend imperdiet lectus. Mauris cursus placerat ex, nec tristique sem efficitur fermentum. Curabitur id nisi diam. Donec et cursus turpis. Nullam dolor dolor, pharetra ac mollis at, aliquam tempus eros. In nec congue dolor, ut facilisis diam. Curabitur sed blandit urna. Pellentesque auctor hendrerit eleifend. Quisque fermentum risus velit, vel luctus turpis tincidunt et. Nam at viverra nisi. Nunc in neque porta, consequat augue volutpat, pharetra nulla.

Vivamus accumsan mauris sit amet blandit suscipit. Mauris sagittis orci quam, quis ornare elit convallis ut. Praesent maximus ex justo, non malesuada quam maximus eu. Pellentesque at ipsum quis velit facilisis molestie. Aliquam sollicitudin orci ac turpis ultricies, eget ultrices leo tincidunt. Aliquam tincidunt leo non urna egestas, id interdum dolor pharetra. Interdum et malesuada fames ac ante ipsum primis in faucibus. Maecenas sit amet turpis efficitur, vehicula nisi et, vehicula nibh. Suspendisse potenti. Suspendisse potenti. Morbi id facilisis arcu. Morbi lorem nunc, viverra id vehicula vitae, vulputate vitae odio.

Sed maximus facilisis ullamcorper. Integer condimentum bibendum dui quis hendrerit. Donec a nisi lectus. Suspendisse bibendum magna ligula, eu mattis diam viverra tincidunt. Cras vulputate mi eget malesuada ullamcorper. Nunc malesuada eget ex a dictum. Cras facilisis faucibus varius. Integer eget sapien erat.

Nunc ante massa, faucibus quis nibh vitae, suscipit blandit mauris. Pellentesque eros tortor, aliquam a mi eu, tristique aliquet leo. Nullam maximus justo sapien, eu egestas eros ultrices sed. Ut ac hendrerit nulla. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Duis vel leo sit amet ligula molestie hendrerit quis a ipsum. Cras mattis sit amet quam et auctor. Aenean in convallis arcu. Sed accumsan magna dapibus orci aliquam fringilla. Nunc est quam, viverra ut sapien non, bibendum maximus justo. Vestibulum euismod non sem eget rhoncus. Fusce pellentesque arcu non nisi laoreet scelerisque. Morbi in neque mi. Aenean mollis ut lectus semper rutrum. Nam velit tortor, consectetur a feugiat facilisis, accumsan sit amet nisl. Morbi vulputate cursus purus, nec pharetra augue vehicula ac.

Phasellus arcu tellus, dapibus sed imperdiet at, malesuada et ligula. Nam a convallis odio. Ut accumsan eros vel neque sollicitudin, et dictum enim tempus. Maecenas dapibus at dui vel vestibulum. Phasellus sapien justo, condimentum quis lacinia nec, molestie vitae metus. Praesent dolor ligula, bibendum at sem id, scelerisque efficitur nunc. Cras felis diam, molestie ut lectus vel, fringilla malesuada dolor. Duis vel vehicula libero. Vestibulum viverra faucibus lacinia. Etiam sed molestie ex. Cras diam eros, laoreet eget urna eu, pulvinar tincidunt augue. Pellentesque finibus, magna eget suscipit suscipit, orci mi dictum nunc, sit amet vulputate felis metus eget elit. Quisque cursus lobortis tincidunt. Nam dignissim mauris eu leo viverra varius.

Proin congue justo lobortis nisi commodo, in faucibus libero interdum. Proin mattis cursus sodales. Donec congue, arcu et commodo suscipit, sapien nunc vehicula ante, gravida posuere sem mauris vel felis. Proin vulputate nunc id cursus vulputate. Maecenas ac egestas libero. Nunc tincidunt, eros vel pellentesque placerat, mi ex pulvinar enim, quis porttitor nulla quam ut metus. Praesent tempor tempor sapien sed accumsan. Mauris at interdum erat. Maecenas vestibulum felis ut tortor sollicitudin ultrices. Donec at dui ex. Praesent a feugiat leo. Donec ante nulla, ullamcorper quis lectus nec, ornare dictum velit. Curabitur facilisis purus lorem, ac efficitur purus blandit non. Sed augue libero, malesuada nec dapibus non, tempor sit amet elit.

Vestibulum pharetra, ex vel convallis accumsan, leo dolor eleifend erat, eget laoreet erat ipsum eget nisi. Proin venenatis tempor eleifend. Phasellus id augue blandit, posuere elit ut, maximus dui. Vivamus fermentum et turpis sed mollis. Vivamus pretium pulvinar urna, eget tempus est aliquam a. Sed eget mi cursus, convallis augue a, suscipit magna. Fusce condimentum, odio in pulvinar commodo, magna mauris eleifend augue, non porttitor odio quam id erat. Nullam pretium aliquet tempor. Aliquam vestibulum lacinia massa id semper. Pellentesque dapibus ligula augue, nec ornare ipsum tristique a. Ut quis velit massa. Nunc sit amet dui quis ex luctus tristique. Duis laoreet elit est, id elementum ex rhoncus vel. In quis porta sem. Proin commodo nisl eleifend pretium semper. Nam condimentum volutpat mauris non vestibulum.

Sed vel scelerisque elit. Fusce dapibus dui rutrum enim dignissim elementum id eget turpis. Suspendisse quis ullamcorper lectus. Sed bibendum, diam eget dapibus dictum, ante augue fringilla ante, quis tristique tortor tortor eget est. Ut maximus pretium risus, eu fermentum odio pharetra vitae. Duis volutpat posuere nibh in blandit. In mollis varius ultrices. Morbi lorem leo, mattis ac tincidunt id, sollicitudin id quam.

Duis libero tortor, imperdiet eget eleifend quis, lobortis in orci. Aenean condimentum vestibulum sem eu commodo. Praesent tristique gravida nulla pellentesque viverra. Mauris justo purus, faucibus eu risus nec, porta accumsan dolor. Aliquam erat volutpat. Nulla diam arcu, auctor ut dolor a, sagittis viverra libero. Aliquam vel dolor eget massa dapibus laoreet eget nullam.";
        #endregion

        [Fact]
        public void ConstructorNullClientThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                var unused = new LogDNALogger("name", null, new LogDNAOptions("key"));
            });
        }

        [Fact]
        public void ConstructorNullOptionsThrowsException()
        {
            var mockClient = new Mock<IApiClient>();
            var client = mockClient.Object;

            Assert.Throws<ArgumentNullException>(() =>
            {
                var unused = new LogDNALogger("name", client, null);
            });
        }

        [Fact]
        public void LogSimpleType()
        {
            var options = new LogDNAOptions("key", LogLevel.Critical);
            var mockClient = new Mock<IApiClient>();
            mockClient.Setup(x => x.AddLine(It.IsAny<LogLine>())).Callback<LogLine>(line =>
            {
                var detail = GetDetail(line.Content);

                Assert.Equal("name", line.Filename);
                Assert.Equal("message", detail.Message);
                Assert.Equal("CRITICAL", detail.Level);
                Assert.Null(detail.Value);
            });

            var client = mockClient.Object;

            var logger = new LogDNALogger("name", client, options);

            logger.Log(
                LogLevel.Critical,
                new EventId(), 
                new FormattedLogValues("message", null),
                null,
                null
                );
        }

        [Fact]
        public void LogFormattedSimpleType()
        {
            var options = new LogDNAOptions("key", LogLevel.Critical);
            var mockClient = new Mock<IApiClient>();
            mockClient.Setup(x => x.AddLine(It.IsAny<LogLine>())).Callback<LogLine>(line =>
            {
                var detail = GetDetail(line.Content);

                Assert.Equal("name", line.Filename);
                Assert.Equal("message 1", detail.Message);
                Assert.Equal("CRITICAL", detail.Level);
                Assert.Null(detail.Value);
            });

            var client = mockClient.Object;

            var logger = new LogDNALogger("name", client, options);

            logger.Log(
                LogLevel.Critical,
                new EventId(),
                new FormattedLogValues("message {0}", 1),
                null,
                null
            );
        }

        [Fact]
        public void LogComplexType()
        {
            var options = new LogDNAOptions("key", LogLevel.Critical);
            var mockClient = new Mock<IApiClient>();
            mockClient.Setup(x => x.AddLine(It.IsAny<LogLine>())).Callback<LogLine>(line =>
            {
                var detail = GetDetail(line.Content);

                Assert.Equal("name", line.Filename);
                Assert.Equal("message", detail.Message);
                Assert.Equal("CRITICAL", detail.Level);
                Assert.NotNull(detail.Value);

                var value = ((JObject) detail.Value).ToObject<KeyValuePair<string, string>>();
                Assert.Equal("key", value.Key);
                Assert.Equal("value", value.Value);
            });

            var client = mockClient.Object;

            var logger = new LogDNALogger("name", client, options);

            logger.Log(
                LogLevel.Critical,
                new EventId(),
                new FormattedLogValues("message", new KeyValuePair<string, string>("key", "value")),
                null,
                null
            );
        }

        [Fact]
        public void LogWrapper()
        {
            var options = new LogDNAOptions("key", LogLevel.Critical);
            var mockClient = new Mock<IApiClient>();
            mockClient.Setup(x => x.AddLine(It.IsAny<LogLine>())).Callback<LogLine>(line =>
            {
                var detail = GetDetail(line.Content);

                Assert.Equal("name", line.Filename);
                Assert.Equal("message", detail.Message);
                Assert.Equal("CRITICAL", detail.Level);
                Assert.NotNull(detail.Value);

                var value = ((JObject)detail.Value).ToObject<KeyValuePair<string, string>>();
                Assert.Equal("key", value.Key);
                Assert.Equal("value", value.Value);
            });

            var client = mockClient.Object;

            var logger = new LogDNALogger("name", client, options);

            logger.Log(
                LogLevel.Critical,
                new EventId(),
                new FormattedLogValues("message", new Wrapper(new KeyValuePair<string, string>("key", "value"))),
                null,
                null
            );
        }

        [Fact]
        public void CheckEnabled()
        {
            // Arrange
            var options = new LogDNAOptions("key", LogLevel.Debug)
                .AddNamespace("Microsoft.Something", LogLevel.Warning)
                .AddNamespace("Newtonsoft.Json", LogLevel.Critical);

            var mockClient = new Mock<IApiClient>();
            var client = mockClient.Object;

            var logger = new LogDNALogger("Foo", client, options);
            // Should log debug
            Assert.True(logger.IsEnabled(LogLevel.Debug));
            // Should log critical
            Assert.True(logger.IsEnabled(LogLevel.Critical));
            // Shouldn't log trace
            Assert.False(logger.IsEnabled(LogLevel.Trace));
            // Shouldn't log none
            Assert.False(logger.IsEnabled(LogLevel.None));

            logger = new LogDNALogger("Microsoft.Something.Blah", client, options);
            // Shouldn't log debug
            Assert.False(logger.IsEnabled(LogLevel.Debug));
            // Should log critical
            Assert.True(logger.IsEnabled(LogLevel.Critical));
            // Shouldn't log trace
            Assert.False(logger.IsEnabled(LogLevel.Trace));
            // Shouldn't log none
            Assert.False(logger.IsEnabled(LogLevel.None));

            logger = new LogDNALogger("Newtonsoft.Json", client, options);
            // Shouldn't log debug
            Assert.False(logger.IsEnabled(LogLevel.Debug));
            // Should log critical
            Assert.True(logger.IsEnabled(LogLevel.Critical));
            // Shouldn't log trace
            Assert.False(logger.IsEnabled(LogLevel.Trace));
            // Shouldn't log none
            Assert.False(logger.IsEnabled(LogLevel.None));
        }

        [Fact]
        public void LogException()
        {
            var options = new LogDNAOptions("key", LogLevel.Trace)
            {
                MaxInnerExceptionDepth = 3
            };

            var mockClient = new Mock<IApiClient>();
            mockClient.Setup(x => x.AddLine(It.IsAny<LogLine>())).Callback<LogLine>(line =>
            {
                Assert.Contains("Level 17", line.Content);
                Assert.DoesNotContain("Level 16", line.Content);
            });

            var client = mockClient.Object;

            var logger = new LogDNALogger("name", client, options);

            var ex = CreateTwentyLevelException();

            logger.LogError(ex, ex.Message);
        }

        Exception CreateTwentyLevelException(Exception inner = null, int exceptionCount = 0)
        {
            var result = new Exception($"Level {exceptionCount}", inner);

            exceptionCount++;

            if (exceptionCount < 20)
                result = CreateTwentyLevelException(result, exceptionCount);

            try
            {
                throw result;
            }
            catch (Exception ex)
            {
                result = ex;
            }

            return result;
        }

        [Fact]
        public void LogLongValue()
        {
            var options = new LogDNAOptions("key", LogLevel.Critical);
            var mockClient = new Mock<IApiClient>();
            mockClient.Setup(x => x.AddLine(It.IsAny<LogLine>())).Callback<LogLine>(line =>
            {
                var detail = GetDetail(line.Content);

                Assert.Equal("name", line.Filename);
                Assert.Equal("message", detail.Message);
                Assert.NotEqual(LoremIpsum, (string)detail.Value);
                Assert.Equal("CRITICAL", detail.Level);
            });

            var client = mockClient.Object;

            var logger = new LogDNALogger("name", client, options);

            logger.Log(
                LogLevel.Critical,
                new EventId(),
                new FormattedLogValues("message", new KeyValuePair<string, string>("Lorem", LoremIpsum)),
                null,
                null
            );
        }

        private MessageDetail GetDetail(string content)
        {
            return JsonConvert.DeserializeObject<MessageDetail>(content.Substring(content.IndexOf("{", StringComparison.Ordinal)));
        }
    }
}
