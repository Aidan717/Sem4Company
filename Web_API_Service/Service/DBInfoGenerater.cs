using Microsoft.AspNetCore.Mvc;
using NuGet.Frameworks;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Web_API_Service.Models;

namespace Web_API_Service.Service {
	public class DBInfoGenerater : IDBInfoGenerater {
		Random numGenenerater = new Random();


		public DBSchema._Source GetNewData() {
			

			int i = 0;
			//while (i < amount) { }
			DBSchema._Source data = new DBSchema._Source();
			
			//should it generate errors of not
			if(numGenenerater.Next(0,100)< 40) {
				
				SetNewError(data);
				data.errorForTrainer = "1";
			}


			data.name = GetName();
			data.timestamp = GetRandomDate();


			
			return data;
		}


		public void SetNewError(DBSchema._Source data) {
			int numberOFerrors = numGenenerater.Next(1, 4);
			int ii = 0;
			while (ii < numberOFerrors) {
				int i = numGenenerater.Next(1, 4);
				switch (i) {
                    case 1:
                        data.exceptionstackTraceString = GetRandomStackTrace();
                        break;
                    case 2:
                        data.exceptioninnerExceptionmessage = GetRandomInnerException();
                        break;
                    case 3:
                        data.exceptionfailedRecipient = GetRandomFailedRecipient();
                        break;
                        //case 4:
                        //	data.exceptionerrorsserver = GetRandomServerError();
                        //	break;
                        //case 5:
                        //	data.activitiestype = GetRandomStackTrace();
                        //	break;
                }
				ii++;
			}
		}
		public string GetName() {
			return new Utility.UserNameList().Name;			
		}

		public string GetRandomDate() {
			DateTime date = new DateTime(2010, 1, 1);
			//DateTime spike1 = new DateTime(2015, 12, 12);
			//DateTime spike2 = new Datetime(2015, 12, 13);
			int range = (DateTime.Now - date).Days;
			//int range = (spike2 - spike1).Days;
			return date.AddDays(numGenenerater.Next(range)).AddHours(numGenenerater.Next(0, 24)).AddMinutes(numGenenerater.Next(0, 60)).AddSeconds(numGenenerater.Next(0, 60)).ToString("yyyy'/'MM'/'dd' 'HH':'mm':'ss");

		}

		public string GetRandomStackTrace() {
			string result = "";
			int indexMessage = numGenenerater.Next(1,5);

			switch (indexMessage) {
				case 1:
					result = "System.Net.Http.ConnectHelper.ConnectAsync(String host, Int32 port, CancellationToken cancellationToken)\r\n   at System.Net.Http.HttpConnectionPool.ConnectAsync(HttpRequestMessage request, Boolean allowHttp2, CancellationToken cancellationToken)\r\n   at System.Net.Http.HttpConnectionPool.CreateHttp11ConnectionAsync(HttpRequestMessage request, CancellationToken cancellationToken)\r\n   at System.Net.Http.HttpConnectionPool.GetHttpConnectionAsync(HttpRequestMessage request, CancellationToken cancellationToken)\r\n   at System.Net.Http.HttpConnectionPool.SendWithRetryAsync(HttpRequestMessage request, Boolean doRequestAuth, CancellationToken cancellationToken)\r\n   at System.Net.Http.RedirectHandler.SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)\r\n   at System.Net.Http.DiagnosticsHandler.SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)\r\n   at System.Net.Http.HttpClient.FinishSendAsyncBuffered(Task`1 sendTask, HttpRequestMessage request, CancellationTokenSource cts, Boolean disposeCts)\r\n   at Web_API_Service.Controllers.ElkCheckController.AbuseThisGenerater(Int32 amount) in C:\\Users\\Jens Jesen\\Source\\Repos\\Sem4Company\\Web_API_Service\\Controllers\\ElkCheckController.cs:line 539";
					break;
				case 2:
					result = "javax.servlet.ServletException: Something bad happened at com.example.myproject.OpenSessionInViewFilter.doFilter(OpenSessionInViewFilter.java:60) at org.mortbay.jetty.servlet.ServletHandler$CachedChain.doFilter(ServletHandler.java:1157) at com.example.myproject.ExceptionHandlerFilter.doFilter(ExceptionHandlerFilter.java:28) at org.mortbay.jetty.servlet.ServletHandler$CachedChain.doFilter(ServletHandler.java:1157) at com.example.myproject.OutputBufferFilter.doFilter(OutputBufferFilter.java:33) at org.mortbay.jetty.servlet.ServletHandler$CachedChain.doFilter(ServletHandler.java:1157) at org.mortbay.jetty.servlet.ServletHandler.handle(ServletHandler.java:388) at org.mortbay.jetty.security.SecurityHandler.handle(SecurityHandler.java:216) at org.mortbay.jetty.servlet.SessionHandler.handle(SessionHandler.java:182) at org.mortbay.jetty.handler.ContextHandler.handle(ContextHandler.java:765) at org.mortbay.jetty.webapp.WebAppContext.handle(WebAppContext.java:418) at org.mortbay.jetty.handler.HandlerWrapper.handle(HandlerWrapper.java:152) at org.mortbay.jetty.Server.handle(Server.java:326) at org.mortbay.jetty.HttpConnection.handleRequest(HttpConnection.java:542) at org.mortbay.jetty.HttpConnection$RequestHandler.content(HttpConnection.java:943) at org.mortbay.jetty.HttpParser.parseNext(HttpParser.java:756) at org.mortbay.jetty.HttpParser.parseAvailable(HttpParser.java:218) at org.mortbay.jetty.HttpConnection.handle(HttpConnection.java:404) at org.mortbay.jetty.bio.SocketConnector$Connection.run(SocketConnector.java:228) at org.mortbay.thread.QueuedThreadPool$PoolThread.run(QueuedThreadPool.java:582) Caused by: com.example.myproject.MyProjectServletException at com.example.myproject.MyServlet.doPost(MyServlet.java:169) at javax.servlet.http.HttpServlet.service(HttpServlet.java:727) at javax.servlet.http.HttpServlet.service(HttpServlet.java:820) at org.mortbay.jetty.servlet.ServletHolder.handle(ServletHolder.java:511) at org.mortbay.jetty.servlet.ServletHandler$CachedChain.doFilter(ServletHandler.java:1166) at com.example.myproject.OpenSessionInViewFilter.doFilter(OpenSessionInViewFilter.java:30) ... 27 more Caused by: org.hibernate.exception.ConstraintViolationException: could not insert: [com.example.myproject.MyEntity] at org.hibernate.exception.SQLStateConverter.convert(SQLStateConverter.java:96) at org.hibernate.exception.JDBCExceptionHelper.convert(JDBCExceptionHelper.java:66) at org.hibernate.id.insert.AbstractSelectingDelegate.performInsert(AbstractSelectingDelegate.java:64) at org.hibernate.persister.entity.AbstractEntityPersister.insert(AbstractEntityPersister.java:2329) at org.hibernate.persister.entity.AbstractEntityPersister.insert(AbstractEntityPersister.java:2822) at org.hibernate.action.EntityIdentityInsertAction.execute(EntityIdentityInsertAction.java:71) at org.hibernate.engine.ActionQueue.execute(ActionQueue.java:268) at org.hibernate.event.def.AbstractSaveEventListener.performSaveOrReplicate(AbstractSaveEventListener.java:321) at org.hibernate.event.def.AbstractSaveEventListener.performSave(AbstractSaveEventListener.java:204) at org.hibernate.event.def.AbstractSaveEventListener.saveWithGeneratedId(AbstractSaveEventListener.java:130) at org.hibernate.event.def.DefaultSaveOrUpdateEventListener.saveWithGeneratedOrRequestedId(DefaultSaveOrUpdateEventListener.java:210) at org.hibernate.event.def.DefaultSaveEventListener.saveWithGeneratedOrRequestedId(DefaultSaveEventListener.java:56) at org.hibernate.event.def.DefaultSaveOrUpdateEventListener.entityIsTransient(DefaultSaveOrUpdateEventListener.java:195) at org.hibernate.event.def.DefaultSaveEventListener.performSaveOrUpdate(DefaultSaveEventListener.java:50) at org.hibernate.event.def.DefaultSaveOrUpdateEventListener.onSaveOrUpdate(DefaultSaveOrUpdateEventListener.java:93) at org.hibernate.impl.SessionImpl.fireSave(SessionImpl.java:705) at org.hibernate.impl.SessionImpl.save(SessionImpl.java:693) at org.hibernate.impl.SessionImpl.save(SessionImpl.java:689) at sun.reflect.GeneratedMethodAccessor5.invoke(Unknown Source) at sun.reflect.DelegatingMethodAccessorImpl.invoke(DelegatingMethodAccessorImpl.java:25) at java.lang.reflect.Method.invoke(Method.java:597) at org.hibernate.context.ThreadLocalSessionContext$TransactionProtectionWrapper.invoke(ThreadLocalSessionContext.java:344) at $Proxy19.save(Unknown Source) at com.example.myproject.MyEntityService.save(MyEntityService.java:59) <-- relevant call (see notes below) at com.example.myproject.MyServlet.doPost(MyServlet.java:164) ... 32 more Caused by: java.sql.SQLException: Violation of unique constraint MY_ENTITY_UK_1: duplicate value(s) for column(s) MY_COLUMN in statement [...] at org.hsqldb.jdbc.Util.throwError(Unknown Source) at org.hsqldb.jdbc.jdbcPreparedStatement.executeUpdate(Unknown Source) at com.mchange.v2.c3p0.impl.NewProxyPreparedStatement.executeUpdate(NewProxyPreparedStatement.java:105) at org.hibernate.id.insert.AbstractSelectingDelegate.performInsert(AbstractSelectingDelegate.java:57)";
					break;
				case 3:
					result = "Exception thrown: 'System.IndexOutOfRangeException' in ML_forcasting_test.dll 'ML_forcasting_test.exe'(CoreCLR: clrhost): Loaded 'C:\\Program Files\\dotnet\\shared\\Microsoft.NETCore.App\\3.1.9\\System.Diagnostics.StackTrace.dll'. 'ML_forcasting_test.exe'(CoreCLR: clrhost): Loaded 'C:\\Program Files\\dotnet\\shared\\Microsoft.NETCore.App\\3.1.9\\System.Reflection.Metadata.dll'.at ML_forcasting_test.Program.<> c__DisplayClass2_0.< Main > b__7(DBschemeModel rental, Int32 index) in C: \\Users\\Bjerne\\source\\repos\\ML_forcasting_test\\Program.cs:line 80 at System.Linq.Enumerable.SelectIterator[TSource, TResult](IEnumerable`1 source, Func`3 selector) + MoveNext() at ML_forcasting_test.Program.< Main > g__Forecast | 2_1(IDataView testData, Int32 horizon, TimeSeriesPredictionEngine`2 forecaster, MLContext mlContext) in C: \\Users\\Bjerne\\source\\repos\\ML_forcasting_test\\Program.cs:line 90 at ML_forcasting_test.Program.Main(String[] args) in C: \\Users\\Bjerne\\source\\repos\\ML_forcasting_test\\Program.cs:line 51";
					break;
				case 4:
					result = "Exception thrown: 'System.DivideByZeroException' in ML_forcasting_test.dll 'ML_forcasting_test.exe'(CoreCLR: clrhost): Loaded 'C:\\Program Files\\dotnet\\shared\\Microsoft.NETCore.App\\3.1.9\\System.Diagnostics.StackTrace.dll'. 'ML_forcasting_test.exe'(CoreCLR: clrhost): Loaded 'C:\\Program Files\\dotnet\\shared\\Microsoft.NETCore.App\\3.1.9\\System.Reflection.Metadata.dll'.at ML_forcasting_test.Program.Main(String[] args) in C: \\Users\\Bjerne\\source\\repos\\ML_forcasting_test\\Program.cs:line 53";
					break;
				case 5:
					result = "System.Net.Http.ConnectHelper.ConnectAsync(String host, Int32 port, CancellationToken cancellationToken)\r\n at System.Net.Http.HttpConnectionPool.ConnectAsync(HttpRequestMessage request, Boolean allowHttp2, CancellationToken cancellationToken)\r\n at System.Net.Http.HttpConnectionPool.CreateHttp11ConnectionAsync(HttpRequestMessage request, CancellationToken cancellationToken)\r\n at System.Net.Http.HttpConnectionPool.GetHttpConnectionAsync(HttpRequestMessage request, CancellationToken cancellationToken)\r\n at System.Net.Http.HttpConnectionPool.SendWithRetryAsync(HttpRequestMessage request, Boolean doRequestAuth, CancellationToken cancellationToken)\r\n at System.Net.Http.RedirectHandler.SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)\r\n at System.Net.Http.DiagnosticsHandler.SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)\r\n at System.Net.Http.HttpClient.FinishSendAsyncBuffered(Task`1 sendTask, HttpRequestMessage request, CancellationT kenSource cts, Boolean disposeCts)\r\n at Web_API_Service.Controllers.ElkCheckController.PostCheckIfErrorSingleObject(_Source result) in C:\\Users\\Bjerne\\source\\repos\\Aidan717\\Sem4Company\\Web_API_Service\\Controllers\\ElkCheckController.cs:line 399";
					break;
				//case 6:
				//	result = "Nullpointer exception";
				//	break;
				//case 7:
				//	result = "Token required";
				//	break;
				//case 8:
				//	result = "Service unavailable";
				//	break;
				//case 9:
				//	result = "Not responding";
				//	break;
				//case 10:
				//	result = "Launcher failed";
				//	break;
			}

			return result;
		}

		public string GetRandomInnerException() {
			string result = "";
			int indexMessage = numGenenerater.Next(1, 4);

			switch (indexMessage) {
				case 1:
					result = "In Main catch block. Caught: Error caused by trying ThrowInner. Inner Exception is MyAppException: ExceptExample inner exception at ExceptExample.ThrowInner() at ExceptExample.CatchInner()";
					break;
				case 2:
					result = "AppException: Error in CatchInner caused by calling the ThrowInner method. ---> AppException: Exception in ThrowInner method. at Example.ThrowInner() in d:\\Windows\\Temp\\tc3jjgjd.0.cs:line 34 at Example.CatchInner() in d:\\Windows\\Temp\\tc3jjgjd.0.cs:line 41 --- End of inner exception stack trace --- at Example.CatchInner() in d:\\Windows\\Temp\\tc3jjgjd.0.cs:line 45 at Example.Main() in d:\\Windows\\Temp\\tc3jjgjd.0.cs:line 19";
					break;
				case 3:
					result = "System.Net.Sockets.SocketException (11001): Værten kendes ikke.\r\n   at System.Net.Http.ConnectHelper.ConnectAsync(String host, Int32 port, CancellationToken cancellationToken)";
					break;
				case 4:
					result = "Attempted to divide by zero. at Web_API_Service.Controllers.ElkCheckController.AbuseThisGenerater(Int32 amount) in C:\\Users\\Jens Jensen\\Source\\Repos\\Sem4Company\\Web_API_Service\\Controllers\\ElkCheckController.cs:line 536 at lambda_method(Closure , Object ) at Microsoft.Extensions.Internal.ObjectMethodExecutorAwaitable.Awaiter.GetResult() at Microsoft.AspNetCore.Mvc.Infrastructure.ActionMethodExecutor.AwaitableObjectResultExecutor.Execute(IActionResultTypeMapper mapper, ObjectMethodExecutor executor, Object controller, Object[] arguments) at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeActionMethodAsync>g__Awaited|12_0(ControllerActionInvoker invoker, ValueTask`1 actionResultValueTask) at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeNextActionFilterAsync>g__Awaited|10_0(ControllerActionInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted) at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Rethrow(ActionExecutedContextSealed context) at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted) at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.InvokeInnerFilterAsync() --- End of stack trace from previous location where exception was thrown --- at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeFilterPipelineAsync>g__Awaited|19_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted) at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope) at Microsoft.AspNetCore.Routing.EndpointMiddleware.<Invoke>g__AwaitRequestTask|6_0(Endpoint endpoint, Task requestTask, ILogger logger) at Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context) at Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddleware.Invoke(HttpContext context)";
					break;
				case 5:
					result = "";
					break;
					//case 6:
					//	result = "Nullpointer exception";
					//	break;
					//case 7:
					//	result = "Token required";
					//	break;
					//case 8:
					//	result = "Service unavailable";
					//	break;
					//case 9:
					//	result = "Not responding";
					//	break;
					//case 10:
					//	result = "Launcher failed";
					//	break;
			}

			return result;
		}

		public string GetRandomFailedRecipient() {
			string result = "";
			int indexMessage = numGenenerater.Next(1, 6);

			switch (indexMessage) {
				case 1:
					result = "<unknown@example.com>: host example.com [123.123.123.123] said: 550 5.1.1 <unknown@example.com>: Recipient address rejected: User unknown in relay recipient table";
					break;
				case 2:
					result = "<user@example.com>: host mail9.example.com said: 552 Requested mail action aborted: exceeded storage allocation";
					break;
				case 3:
					result = "<registrations@conference2019.net>: host xxx.server.lt[194.135.87.23] said: 550-SPF check failed - 88.99.71.145 is not allowed to send mail from 550 conference2019.net";
					break;
				case 4:
					result = "<test@hotmail.com>: host mx2.hotmail.com[104.44.194.236] said: 550 SC-001 (SNT004-MC10F3) Unfortunately, messages from 148.251.246.136 weren't sent. Please contact your Internet service provider since part of their network is on our block list. You can also refer your provider to http://mail.live.com/mail/troubleshooting.aspx#errors";
					break;
				case 5:
					result = "MailKit.ServiceNotConnectedException: The SmtpClient must be connected before you can authenticate. at MailKit.Net.Smtp.SmtpClient.AuthenticateAsync(Encoding encoding, ICredentials credentials, Boolean doAsync, CancellationToken cancellationToken) at MailKit.Net.Smtp.SmtpClient.Authenticate(Encoding encoding, ICredentials credentials, CancellationToken cancellationToken) at MailKit.MailService.Authenticate(Encoding encoding, String userName, String password, CancellationToken cancellationToken) at MailKit.MailService.Authenticate(String userName, String password, CancellationToken cancellationToken) at Web_API_Service.Service.MailService.SendWarningEmailAsync(String methodName, String Query, String Destination, String Error) in C:\\Users\\Jens Jensen\\Source\\Repos\\Sem4Company\\Web_API_Service\\Service\\MailService.cs:line 107 at Web_API_Service.Controllers.ElkCheckController.AbuseThisGenerater(Int32 amount) in C:\\Users\\Jens Jensen\\Source\\Repos\\Sem4Company\\Web_API_Service\\Controllers\\ElkCheckController.cs:line 572 at lambda_method(Closure , Object ) at Microsoft.Extensions.Internal.ObjectMethodExecutorAwaitable.Awaiter.GetResult() at Microsoft.AspNetCore.Mvc.Infrastructure.ActionMethodExecutor.AwaitableObjectResultExecutor.Execute(IActionResultTypeMapper mapper, ObjectMethodExecutor executor, Object controller, Object[] arguments) at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeActionMethodAsync>g__Awaited|12_0(ControllerActionInvoker invoker, ValueTask`1 actionResultValueTask) at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeNextActionFilterAsync>g__Awaited|10_0(ControllerActionInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted) at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Rethrow(ActionExecutedContextSealed context) at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.Next(State& next, Scope& scope, Object& state, Boolean& isCompleted) at Microsoft.AspNetCore.Mvc.Infrastructure.ControllerActionInvoker.<InvokeInnerFilterAsync>g__Awaited|13_0(ControllerActionInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted) at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeFilterPipelineAsync>g__Awaited|19_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted) at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope) at Microsoft.AspNetCore.Routing.EndpointMiddleware.<Invoke>g__AwaitRequestTask|6_0(Endpoint endpoint, Task requestTask, ILogger logger) at Microsoft.AspNetCore.Authorization.AuthorizationMiddleware.Invoke(HttpContext context)at Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddleware.Invoke(HttpContext context)";
					break;
                case 6:
                    result = "Failure sending mail. ---> System.Net.Internals.SocketExceptionFactory+ExtendedSocketException at System.Net.Sockets.Socket.DoConnect(EndPoint endPointSnapshot, SocketAddress socketAddress) at System.Net.Sockets.Socket.Connect(EndPoint remoteEP) at System.Net.Sockets.TcpClient.Connect(IPEndPoint remoteEP) at System.Net.Sockets.TcpClient.Connect(String hostname, Int32 port) --- End of stack trace from previous location where exception was thrown --- at System.Runtime.ExceptionServices.ExceptionDispatchInfo.Throw() at System.Net.Sockets.TcpClient.Connect(String hostname, Int32 port) at System.Net.Mail.SmtpConnection.GetConnection(String host, Int32 port) at System.Net.Mail.SmtpTransport.GetConnection(String host, Int32 port) at System.Net.Mail.SmtpClient.GetConnection() at System.Net.Mail.SmtpClient.Send(MailMessage message) --- End of inner exception stack trace --- at System.Net.Mail.SmtpClient.Send(MailMessage message) at ConsoleApp2.Program.Main(String[] args)";
                    break;
                    //case 7:
                    //	result = "Token required";
                    //	break;
                    //case 8:
                    //	result = "Service unavailable";
                    //	break;
                    //case 9:
                    //	result = "Not responding";
                    //	break;
                    //case 10:
                    //	result = "Launcher failed";
                    //	break;
            }

			return result;
		}

		public string GetRandomServerError() {
			string result = "";
			int indexMessage = numGenenerater.Next(1, 5);

			switch (indexMessage) {
				case 1:
					result = "";
					break;
				case 2:
					result = "";
					break;
				case 3:
					result = "";
					break;
				case 4:
					result = "";
					break;
				case 5:
					result = "";
					break;
					//case 6:
					//	result = "Nullpointer exception";
					//	break;
					//case 7:
					//	result = "Token required";
					//	break;
					//case 8:
					//	result = "Service unavailable";
					//	break;
					//case 9:
					//	result = "Not responding";
					//	break;
					//case 10:
					//	result = "Launcher failed";
					//	break;
			}

			return result;
		}
	}
}

