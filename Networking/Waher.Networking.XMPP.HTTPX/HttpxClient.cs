﻿#define	LOG_SOCKS5_EVENTS

using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Waher.Content;
using Waher.Content.Xml;
using Waher.Networking.HTTP;

namespace Waher.Networking.XMPP.HTTPX
{
	/// <summary>
	/// HTTPX client.
	/// </summary>
	public class HttpxClient : XmppExtension
	{
		/// <summary>
		/// urn:xmpp:http
		/// </summary>
		public const string Namespace = "urn:xmpp:http";

		/// <summary>
		/// http://jabber.org/protocol/shim
		/// </summary>
		public const string NamespaceHeaders = "http://jabber.org/protocol/shim";

		private InBandBytestreams.IbbClient ibbClient = null;
		private P2P.SOCKS5.Socks5Proxy socks5Proxy = null;
		private IEndToEndEncryption e2e;
		private readonly int maxChunkSize;

		/// <summary>
		/// HTTPX client.
		/// </summary>
		/// <param name="Client">XMPP Client.</param>
		/// <param name="MaxChunkSize">Max Chunk Size to use.</param>
		public HttpxClient(XmppClient Client, int MaxChunkSize)
			: base(Client)
		{
			this.e2e = null;
			this.maxChunkSize = MaxChunkSize;

			HttpxChunks.RegisterChunkReceiver(this.client);
		}

		/// <summary>
		/// HTTPX client.
		/// </summary>
		/// <param name="Client">XMPP Client.</param>
		/// <param name="E2e">End-to-end encryption interface.</param>
		/// <param name="MaxChunkSize">Max Chunk Size to use.</param>
		public HttpxClient(XmppClient Client, IEndToEndEncryption E2e, int MaxChunkSize)
			: base(Client)
		{
			this.e2e = E2e;
			this.maxChunkSize = MaxChunkSize;

			HttpxChunks.RegisterChunkReceiver(this.client);
		}

		/// <summary>
		/// Implemented extensions.
		/// </summary>
		public override string[] Extensions => new string[] { "XEP-0332" };

		/// <summary>
		/// Optional end-to-end encryption interface to use in requests.
		/// </summary>
		public IEndToEndEncryption E2e
		{
			get { return this.e2e; }
			set { this.e2e = value; }
		}

		/// <summary>
		/// In-band bytestream client, if supported.
		/// </summary>
		public InBandBytestreams.IbbClient IbbClient
		{
			get { return this.ibbClient; }
			set
			{
				if (this.ibbClient != null)
					this.ibbClient.OnOpen -= this.IbbClient_OnOpen;

				this.ibbClient = value;
				this.ibbClient.OnOpen += this.IbbClient_OnOpen;
			}
		}

		/// <summary>
		/// SOCKS5 proxy, if supported.
		/// </summary>
		public P2P.SOCKS5.Socks5Proxy Socks5Proxy
		{
			get { return this.socks5Proxy; }
			set
			{
				if (this.socks5Proxy != null)
					this.socks5Proxy.OnOpen -= this.Socks5Proxy_OnOpen;

				this.socks5Proxy = value;
				this.socks5Proxy.OnOpen += this.Socks5Proxy_OnOpen;
			}
		}

		/// <summary>
		/// <see cref="IDisposable.Dispose"/>
		/// </summary>
		public override void Dispose()
		{
			base.Dispose();

			HttpxChunks.UnregisterChunkReceiver(this.client);
		}

		/// <summary>
		/// Performs an HTTP GET request.
		/// </summary>
		/// <param name="To">Full JID of entity to query.</param>
		/// <param name="Resource">Local HTTP resource to query.</param>
		/// <param name="Callback">Callback method to call when response is returned.</param>
		/// <param name="DataCallback">Callback method to call when data is returned.</param>
		/// <param name="State">State object to pass on to the callback method.</param>
		/// <param name="Headers">HTTP headers of the request.</param>
		public void GET(string To, string Resource, HttpxResponseEventHandler Callback,
			HttpxResponseDataEventHandler DataCallback, object State, params HttpField[] Headers)
		{
			this.Request(To, "GET", Resource, Callback, DataCallback, State, Headers);
		}

		// TODO: Add more HTTP methods.

		/// <summary>
		/// Performs an HTTP request.
		/// </summary>
		/// <param name="To">Full JID of entity to query.</param>
		/// <param name="Method">HTTP Method.</param>
		/// <param name="LocalResource">Local HTTP resource to query.</param>
		/// <param name="Callback">Callback method to call when response is returned.</param>
		/// <param name="DataCallback">Callback method to call when data is returned.</param>
		/// <param name="State">State object to pass on to the callback method.</param>
		/// <param name="Headers">HTTP headers of the request.</param>
		public void Request(string To, string Method, string LocalResource, HttpxResponseEventHandler Callback,
			HttpxResponseDataEventHandler DataCallback, object State, params HttpField[] Headers)
		{
			this.Request(To, Method, LocalResource, 1.1, Headers, null, Callback, DataCallback, State);
		}

		/// <summary>
		/// Performs an HTTP request.
		/// </summary>
		/// <param name="To">Full JID of entity to query.</param>
		/// <param name="Method">HTTP Method.</param>
		/// <param name="LocalResource">Local resource.</param>
		/// <param name="HttpVersion">HTTP Version.</param>
		/// <param name="Headers">HTTP headers.</param>
		/// <param name="DataStream">Data Stream, if any, or null, if no data is sent.</param>
		/// <param name="Callback">Callback method to call when response is returned.</param>
		/// <param name="DataCallback">Local resource.</param>
		/// <param name="State">State object to pass on to the callback method.</param>
		public void Request(string To, string Method, string LocalResource, double HttpVersion, IEnumerable<HttpField> Headers,
			Stream DataStream, HttpxResponseEventHandler Callback, HttpxResponseDataEventHandler DataCallback, object State)
		{
			// TODO: Local IP & port for quick P2P response (TLS, or POST back, web hook).

			StringBuilder Xml = new StringBuilder();

			Xml.Append("<req xmlns='");
			Xml.Append(Namespace);
			Xml.Append("' method='");
			Xml.Append(Method);
			Xml.Append("' resource='");
			Xml.Append(XML.Encode(LocalResource));
			Xml.Append("' version='");
			Xml.Append(HttpVersion.ToString("F1").Replace(System.Globalization.NumberFormatInfo.CurrentInfo.NumberDecimalSeparator, "."));
			Xml.Append("' maxChunkSize='");
			Xml.Append(this.maxChunkSize.ToString());
			Xml.Append("' sipub='false' ibb='");
			Xml.Append(CommonTypes.Encode(this.ibbClient != null));
			Xml.Append("' s5='");
			Xml.Append(CommonTypes.Encode(this.socks5Proxy != null));
			Xml.Append("' jingle='false'>");

			Xml.Append("<headers xmlns='");
			Xml.Append(HttpxClient.NamespaceHeaders);
			Xml.Append("'>");

			foreach (HttpField HeaderField in Headers)
			{
				Xml.Append("<header name='");
				Xml.Append(XML.Encode(HeaderField.Key));
				Xml.Append("'>");
				Xml.Append(XML.Encode(HeaderField.Value));
				Xml.Append("</header>");
			}
			Xml.Append("</headers>");

			string StreamId = null;

			if (DataStream != null)
			{
				if (DataStream.Length < this.maxChunkSize)
				{
					int c = (int)DataStream.Length;
					byte[] Data = new byte[c];

					DataStream.Position = 0;
					DataStream.Read(Data, 0, c);

					Xml.Append("<data><base64>");
					Xml.Append(Convert.ToBase64String(Data));
					Xml.Append("</base64></data>");
				}
				else
				{
					StreamId = Guid.NewGuid().ToString().Replace("-", string.Empty);

					Xml.Append("<data><chunkedBase64 streamId='");
					Xml.Append(StreamId);
					Xml.Append("'/></data>");
				}
			}

			Xml.Append("</req>");

			if (this.e2e != null)
				this.e2e.SendIqSet(this.client, E2ETransmission.NormalIfNotE2E, To, Xml.ToString(), this.ResponseHandler, new object[] { Callback, DataCallback, State }, 60000, 0);
			else
				this.client.SendIqSet(To, Xml.ToString(), this.ResponseHandler, new object[] { Callback, DataCallback, State }, 60000, 0);

			if (!string.IsNullOrEmpty(StreamId))
			{
				byte[] Data = new byte[this.maxChunkSize];
				long Pos = 0;
				long Len = DataStream.Length;
				int Nr = 0;
				int i;

				DataStream.Position = 0;

				while (Pos < Len)
				{
					if (Pos + this.maxChunkSize <= Len)
						i = this.maxChunkSize;
					else
						i = (int)(Len - Pos);

					DataStream.Read(Data, 0, i);

					Xml.Clear();
					Xml.Append("<chunk xmlns='");
					Xml.Append(Namespace);
					Xml.Append("' streamId='");
					Xml.Append(StreamId);
					Xml.Append("' nr='");
					Xml.Append(Nr.ToString());
					Xml.Append("'>");
					Xml.Append(Convert.ToBase64String(Data, 0, i));
					Xml.Append("</chunk>");
					Nr++;

					if (this.e2e != null)
					{
						this.e2e.SendMessage(this.client, E2ETransmission.NormalIfNotE2E, QoSLevel.Unacknowledged,
							MessageType.Normal, string.Empty, To, Xml.ToString(), string.Empty, string.Empty,
							string.Empty, string.Empty, string.Empty, null, null);
					}
					else
					{
						this.client.SendMessage(MessageType.Normal, To, Xml.ToString(), string.Empty, string.Empty, string.Empty,
							string.Empty, string.Empty);
					}
				}
			}
		}

		private async Task ResponseHandler(object Sender, IqResultEventArgs e)
		{
			XmlElement E = e.FirstElement;
			HttpResponse Response;
			string StatusMessage;
			double Version;
			int StatusCode;
			object[] P = (object[])e.State;
			HttpxResponseEventHandler Callback = (HttpxResponseEventHandler)P[0];
			HttpxResponseDataEventHandler DataCallback = (HttpxResponseDataEventHandler)P[1];
			object State = P[2];
			byte[] Data = null;
			bool HasData = false;
			bool DisposeResponse = true;

			if (e.Ok && E != null && E.LocalName == "resp" && E.NamespaceURI == Namespace)
			{
				Version = XML.Attribute(E, "version", 0.0);
				StatusCode = XML.Attribute(E, "statusCode", 0);
				StatusMessage = XML.Attribute(E, "statusMessage");
				Response = new HttpResponse();

				foreach (XmlNode N in E.ChildNodes)
				{
					switch (N.LocalName)
					{
						case "headers":
							foreach (XmlNode N2 in N.ChildNodes)
							{
								switch (N2.LocalName)
								{
									case "header":
										string Key = XML.Attribute((XmlElement)N2, "name");
										string Value = N2.InnerText;

										Response.SetHeader(Key, Value);
										break;
								}
							}
							break;

						case "data":
							foreach (XmlNode N2 in N.ChildNodes)
							{
								switch (N2.LocalName)
								{
									case "text":
										MemoryStream ms = new MemoryStream();
										Response.SetResponseStream(ms);
										Data = Response.Encoding.GetBytes(N2.InnerText);
										ms.Write(Data, 0, Data.Length);
										ms.Position = 0;
										HasData = true;
										break;

									case "xml":
										ms = new MemoryStream();
										Response.SetResponseStream(ms);
										Data = Response.Encoding.GetBytes(N2.InnerText);
										ms.Write(Data, 0, Data.Length);
										ms.Position = 0;
										HasData = true;
										break;

									case "base64":
										ms = new MemoryStream();
										Response.SetResponseStream(ms);
										Data = Convert.FromBase64String(N2.InnerText);
										ms.Write(Data, 0, Data.Length);
										ms.Position = 0;
										HasData = true;
										break;

									case "chunkedBase64":
										string StreamId = XML.Attribute((XmlElement)N2, "streamId");

										HttpxChunks.chunkedStreams.Add(e.From + " " + StreamId, new ClientChunkRecord(this,
											new HttpxResponseEventArgs(e, Response, State, Version, StatusCode, StatusMessage, true, null),
											Response, DataCallback, State, StreamId, e.From, e.To, false, null, null));

										DisposeResponse = false;
										HasData = true;
										break;

									case "ibb":
										StreamId = XML.Attribute((XmlElement)N2, "sid");

										HttpxChunks.chunkedStreams.Add(e.From + " " + StreamId, new ClientChunkRecord(this,
											new HttpxResponseEventArgs(e, Response, State, Version, StatusCode, StatusMessage, true, null),
											Response, DataCallback, State, StreamId, e.From, e.To, false, null, null));

										DisposeResponse = false;
										HasData = true;
										break;

									case "s5":
										StreamId = XML.Attribute((XmlElement)N2, "sid");
										bool E2e = XML.Attribute((XmlElement)N2, "e2e", false);

										HttpxChunks.chunkedStreams.Add(e.From + " " + StreamId, new ClientChunkRecord(this,
											new HttpxResponseEventArgs(e, Response, State, Version, StatusCode, StatusMessage, true, null),
											Response, DataCallback, State, StreamId, e.From, e.To, E2e, e.E2eReference, e.E2eSymmetricCipher));

										DisposeResponse = false;
										HasData = true;
										break;

									case "sipub":
										// TODO: Implement File Transfer support.
										break;

									case "jingle":
										// TODO: Implement Jingle support.
										break;
								}
							}
							break;
					}
				}
			}
			else
			{
				Version = 0.0;
				StatusCode = 503;
				StatusMessage = "Service Unavailable";
				Response = new HttpResponse();
			}

			HttpxResponseEventArgs e2 = new HttpxResponseEventArgs(e, Response, State, Version, StatusCode, StatusMessage, HasData, Data);

			try
			{
				await Callback(this, e2);
			}
			catch (Exception)
			{
				// Ignore.
			}
			finally
			{
				if (DisposeResponse)
					Response.Dispose();
			}
		}

		/// <summary>
		/// Requests the transfer of a stream to be cancelled.
		/// </summary>
		/// <param name="To">The sender of the stream.</param>
		/// <param name="StreamId">Stream ID.</param>
		public void CancelTransfer(string To, string StreamId)
		{
			HttpxChunks.chunkedStreams.Remove(To + " " + StreamId);

			StringBuilder Xml = new StringBuilder();

			Xml.Append("<cancel xmlns='");
			Xml.Append(Namespace);
			Xml.Append("' streamId='");
			Xml.Append(StreamId);
			Xml.Append("'/>");

			if (this.e2e != null)
			{
				this.e2e.SendMessage(this.client, E2ETransmission.NormalIfNotE2E, QoSLevel.Unacknowledged,
					MessageType.Normal, string.Empty, To, Xml.ToString(), string.Empty, string.Empty, string.Empty,
					string.Empty, string.Empty, null, null);
			}
			else
				this.client.SendMessage(MessageType.Normal, To, Xml.ToString(), string.Empty, string.Empty, string.Empty, string.Empty, string.Empty);
		}

		private Task IbbClient_OnOpen(object Sender, InBandBytestreams.ValidateStreamEventArgs e)
		{
			string Key = e.From + " " + e.StreamId;

			if (HttpxChunks.chunkedStreams.ContainsKey(Key))
				e.AcceptStream(this.IbbDataReceived, this.IbbStreamClosed, new object[] { Key, -1, null });

			return Task.CompletedTask;
		}

		private async Task IbbDataReceived(object Sender, InBandBytestreams.DataReceivedEventArgs e)
		{
			object[] P = (object[])e.State;
			string Key = (string)P[0];
			int Nr = (int)P[1];
			byte[] PrevData = (byte[])P[2];

			if (HttpxChunks.chunkedStreams.TryGetValue(Key, out ChunkRecord Rec))
			{
				if (PrevData != null)
					await Rec.ChunkReceived(Nr, false, PrevData);

				Nr++;
				P[1] = Nr;
				P[2] = e.Data;
			}
		}

		private async Task IbbStreamClosed(object Sender, InBandBytestreams.StreamClosedEventArgs e)
		{
			object[] P = (object[])e.State;
			string Key = (string)P[0];
			int Nr = (int)P[1];
			byte[] PrevData = (byte[])P[2];

			if (HttpxChunks.chunkedStreams.TryGetValue(Key, out ChunkRecord Rec))
			{
				if (e.Reason == InBandBytestreams.CloseReason.Done)
				{
					if (PrevData != null)
						await Rec.ChunkReceived(Nr, true, PrevData);
					else
						await Rec.ChunkReceived(Nr, true, new byte[0]);

					P[2] = null;
				}
				else
					HttpxChunks.chunkedStreams.Remove(Key);
			}
		}

		private Task Socks5Proxy_OnOpen(object Sender, P2P.SOCKS5.ValidateStreamEventArgs e)
		{
			string Key = e.From + " " + e.StreamId;
			ClientChunkRecord ClientRec;

			if (HttpxChunks.chunkedStreams.TryGetValue(Key, out ChunkRecord Rec))
			{
				ClientRec = Rec as ClientChunkRecord;

				if (ClientRec != null)
				{
#if LOG_SOCKS5_EVENTS
					this.client.Information("Accepting SOCKS5 stream from " + e.From);
#endif
					e.AcceptStream(this.Socks5DataReceived, this.Socks5StreamClosed, new Socks5Receiver(Key, e.StreamId, 
                        ClientRec.from, ClientRec.to, ClientRec.e2e, ClientRec.endpointReference, ClientRec.symmetricCipher));
				}
			}

			return Task.CompletedTask;
		}

		private class Socks5Receiver
		{
			public string Key;
			public string StreamId;
			public string From;
			public string To;
			public string EndpointReference;
            public IE2eSymmetricCipher SymmetricCipher;
            public int State = 0;
			public int BlockSize;
			public int BlockPos;
			public int Nr = 0;
			public byte[] Block;
			public bool E2e;

			public Socks5Receiver(string Key, string StreamId, string From, string To, bool E2e, string EndpointReference, 
                IE2eSymmetricCipher SymmetricCipher)
			{
				this.Key = Key;
				this.StreamId = StreamId;
				this.From = From;
				this.To = To;
				this.E2e = E2e;
                this.EndpointReference = EndpointReference;
                this.SymmetricCipher = SymmetricCipher;
            }
		}

		private async Task Socks5DataReceived(object Sender, P2P.SOCKS5.DataReceivedEventArgs e)
		{
			Socks5Receiver Rx = (Socks5Receiver)e.State;

			if (HttpxChunks.chunkedStreams.TryGetValue(Rx.Key, out ChunkRecord Rec))
			{
#if LOG_SOCKS5_EVENTS
				this.client.Information(e.Count.ToString() + " bytes received over SOCKS5 stream " + Rx.Key + ".");
#endif
				byte[] Buffer = e.Buffer;
				int Offset = e.Offset;
				int Count = e.Count;
				int d;

				while (Count > 0)
				{
					switch (Rx.State)
					{
						case 0:
							Rx.BlockSize = Buffer[Offset++];
							Count--;
							Rx.State++;
							break;

						case 1:
							Rx.BlockSize <<= 8;
							Rx.BlockSize |= Buffer[Offset++];
							Count--;

							if (Rx.BlockSize == 0)
							{
								HttpxChunks.chunkedStreams.Remove(Rx.Key);
								await Rec.ChunkReceived(Rx.Nr++, true, new byte[0]);
								e.Stream.Dispose();
								return;
							}

							Rx.BlockPos = 0;

							if (Rx.Block is null || Rx.Block.Length != Rx.BlockSize)
								Rx.Block = new byte[Rx.BlockSize];

							Rx.State++;
							break;

						case 2:
							d = Math.Min(Count, Rx.BlockSize - Rx.BlockPos);

							Array.Copy(Buffer, Offset, Rx.Block, Rx.BlockPos, d);
							Offset += d;
							Rx.BlockPos += d;
							Count -= d;

							if (Rx.BlockPos >= Rx.BlockSize)
							{
								if (Rx.E2e)
								{
									string Id = Rec.NextId().ToString();
									Rx.Block = this.e2e.Decrypt(Rx.EndpointReference, Id, Rx.StreamId, Rx.From, Rx.To, Rx.Block, Rx.SymmetricCipher);
									if (Rx.Block is null)
									{
										string Message = "Decryption of chunk " + Rx.Nr.ToString() + " failed.";
#if LOG_SOCKS5_EVENTS
										this.client.Error(Message);
#endif
										await Rec.Fail(Message);
										e.Stream.Dispose();
										return;
									}
								}

#if LOG_SOCKS5_EVENTS
								this.client.Information("Chunk " + Rx.Nr.ToString() + " received and forwarded.");
#endif
								await Rec.ChunkReceived(Rx.Nr++, false, Rx.Block);
								Rx.State = 0;
							}
							break;
					}
				}
			}
			else
			{
#if LOG_SOCKS5_EVENTS
				this.client.Warning(e.Count.ToString() + " bytes received over SOCKS5 stream " + Rx.Key + " and discarded.");
#endif
				e.Stream.Dispose();
			}
		}

		private async Task Socks5StreamClosed(object Sender, P2P.SOCKS5.StreamEventArgs e)
		{
#if LOG_SOCKS5_EVENTS
			this.client.Information("SOCKS5 stream closed.");
#endif
			Socks5Receiver Rx = (Socks5Receiver)e.State;

			if (HttpxChunks.chunkedStreams.TryGetValue(Rx.Key, out ChunkRecord Rec))
			{
				HttpxChunks.chunkedStreams.Remove(Rx.Key);
				await Rec.ChunkReceived(Rx.Nr++, true, new byte[0]);
			}
		}

	}
}
